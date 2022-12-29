using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;
using System.Diagnostics;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;

namespace FFPRSaveEditor.Common
{
    public class SaveGame
    {
        static readonly string Password = "TKX73OHHK1qMonoICbpVT0hIDGe7SkW0";
        static readonly string Saltword = "71Ba2p0ULBGaE6oJ7TjCqwsls1jBKmRL";

        public static string Decrypt(string jsonData)
        {
            byte[] decodedJSONData = Convert.FromBase64String(jsonData);

            byte[] saltwordBytes = Encoding.UTF8.GetBytes(Saltword);

            using var generator = new Rfc2898DeriveBytes(Password, saltwordBytes, 10);

            var blockCipher = new CbcBlockCipher(new RijndaelEngine(256));
            var cipher = new PaddedBufferedBlockCipher(blockCipher, new ZeroBytePadding());
            var parameters = new ParametersWithIV(new KeyParameter(generator.GetBytes(32)), generator.GetBytes(32));

            byte[] decryptedJSONData = new byte[cipher.GetOutputSize(decodedJSONData.Length)];

            try
            {
                cipher.Init(false, parameters);
                int bytesProcessed = cipher.ProcessBytes(decodedJSONData, decryptedJSONData, 0);
                cipher.DoFinal(decryptedJSONData, bytesProcessed);
            }
            catch (Exception)
            {
                throw new Exception("Failed to decrypt stream!");
            }

            using var ds = new DeflateStream(new MemoryStream(decryptedJSONData), CompressionMode.Decompress);
            using var ms = new MemoryStream();

            try
            {
                ds.CopyTo(ms);
            }
            catch (Exception)
            {
                throw new Exception("Failed to decompress stream!");
            }

            byte[] decompressedJSONData = ms.ToArray();

            return DeStringify(Encoding.UTF8.GetString(decompressedJSONData));
        }

        private static string DeStringify(string jsonData) {
            // After decrypting the game file we have a JSON string where nearly all sub-objects are in "stringified" form
            // So here we will recursively expand any stringified sub-objects and array members to give a more "standard" JSON form
            var obj = JsonConvert.DeserializeObject<JObject>(jsonData);
            RecursivelyDeStringify(obj);

            // Serialize our newly-expanded JObject and store in the result variable
            string result = JsonConvert.SerializeObject(obj, Formatting.Indented);

            if (Debugger.IsAttached) {
                // I don't fully trust the de-stringification and re-stringification methods yet, so here we will re-stringify
                // the de-stringified json, which if all is working well will yield a string that matches the original jsonData
                // that was passed into this method.
                // If the string does not match, it may indicate an error in the de-stringification or re-stringification methods,
                // which could result in the game crashing when the modified game file is used, so we throw an excetion to abort.
                string reStringified = Stringify(result);
                if (reStringified != jsonData) {
                    throw new Exception("Re-stringified json does not match input jsonData");
                }

                // A second test of the de-stringification and re-stringification methods, which are working with game-specific classes.
                // Since we don't know what game the data is coming from, we parse with each in a try..catch to ignore exceptions related
                // to mismatches (ie loading a FF1 save into a FF6 class)
                if (jsonData.Contains("userData", StringComparison.OrdinalIgnoreCase)) {
                    var reStringifieds = new List<string>();
                    try {
                        reStringifieds.Add(Stringify(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<Models.FF1Models.SaveGame>(result))));
                    } catch {
                    }
                    try {
                        reStringifieds.Add(Stringify(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<Models.FF4Models.SaveGame>(result))));
                    } catch {
                    }
                    try {
                        reStringifieds.Add(Stringify(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<Models.FF6Models.SaveGame>(result))));
                    } catch {
                    }
                    if (!reStringifieds.Any(x => x == jsonData)) {
                        throw new Exception("Re-stringified json does not match input jsonData");
                    }
                }

                // A third test to ensure encryption is working
                try {
                    Encrypt(result);
                } catch (Exception ex) {
                    throw new Exception($"Error re-encrypting the decrypted json: {ex.Message}");
                }
            }

            return result;
        }

        public static string Encrypt(string jsonData)
        {
            using var msJSONData = new MemoryStream(Encoding.UTF8.GetBytes(Stringify(jsonData)));
            using var msCompressedJSONData = new MemoryStream();
            using var ds = new DeflateStream(msCompressedJSONData, CompressionMode.Compress);

            try
            {
                msJSONData.CopyTo(ds);
                ds.Close();
            }
            catch (Exception)
            {
                throw new Exception("Failed to compress stream!");
            }

            byte[] compressedJSONData = msCompressedJSONData.ToArray();

            byte[] saltwordBytes = Encoding.UTF8.GetBytes(Saltword);

            using var generator = new Rfc2898DeriveBytes(Password, saltwordBytes, 10);

            var blockCipher = new CbcBlockCipher(new RijndaelEngine(256));
            var cipher = new PaddedBufferedBlockCipher(blockCipher, new ZeroBytePadding());
            var parameters = new ParametersWithIV(new KeyParameter(generator.GetBytes(32)), generator.GetBytes(32));

            // GetOutputSize sometimes returns a size that is 32 bytes too small, so we add 32 to the size it returns.
            // Seems to happen on files that are exact multiples of the 32 byte block size, and this SO answer indicates
            // that BouncyCastle adds a block full of zeros in such cases.  So maybe GetOutputSize doesn't account for
            // that extra block of zeros?  See: https://stackoverflow.com/a/63737775/342378
            byte[] encryptedJSONData = new byte[cipher.GetOutputSize(compressedJSONData.Length) + 32];

            int bytesProcessed = 0;
            try
            {
                cipher.Init(true, parameters);
                bytesProcessed = cipher.ProcessBytes(compressedJSONData, encryptedJSONData, 0);
                bytesProcessed += cipher.DoFinal(encryptedJSONData, bytesProcessed);
            }
            catch (Exception)
            {
                throw new Exception("Failed to encrypt stream!");
            }

            return Convert.ToBase64String(encryptedJSONData, 0, bytesProcessed);
        }

        private static void RecursivelyDeStringify(JObject obj) {
            // Loop through each property in the given JObject, and expand any properties or array members
            // that appear to be stringified objects.
            var props = obj.Properties().ToList();
            foreach (var prop in props) {
                if (prop.Value.Type == JTokenType.String) {
                    string value = prop.Value.Value<string>();
                    if (value.StartsWith("{") && value.EndsWith("}")) {
                        // Value for current property appears to be a json string, so further destringification is needed
                        var newObj = JsonConvert.DeserializeObject<JObject>(value);
                        RecursivelyDeStringify(newObj);
                        prop.Value = newObj;
                    }
                } else if (prop.Value.Type == JTokenType.Array) {
                    var items = prop.Value.Value<JArray>();
                    for (int i = 0; i < items.Count; i++) {
                        if (items[i].Type == JTokenType.String) {
                            string value = items[i].Value<string>();
                            if (value.StartsWith("{") && value.EndsWith("}")) {
                                // Value for current item appears to be a json string, so further destringification is needed
                                var newObj = JsonConvert.DeserializeObject<JObject>(value);
                                RecursivelyDeStringify(newObj);
                                items[i].Replace(newObj);
                            }
                        }
                    }
                }
            }
        }

        private static void RecursivelyStringify(JObject obj) {
            // Loop through each property in the given JObject, and stringify any objects.
            var props = obj.Properties().ToList();
            foreach (var prop in props) {
                if (prop.Value.Type == JTokenType.Object) {
                    // Value for current property is an object, so further stringification is needed
                    if (prop.Name == "position") {
                        // "position" is not stringified in the decrypted JSON, so we don't re-stringify here
                    } else { 
                        var newObj = prop.Value.Value<JObject>();
                        RecursivelyStringify(newObj);
                        prop.Value = JsonConvert.SerializeObject(newObj);
                    }
                } else if (prop.Value.Type == JTokenType.Array) {
                    if (prop.Name == "otherPartyDataList") {
                        // "otherPartyDataList" is not stringified in the decrypted JSON, so we don't re-stringify here
                    } else {
                        var items = prop.Value.Value<JArray>();
                        for (int i = 0; i < items.Count; i++) {
                            if (items[i].Type == JTokenType.Object) {
                                // Value for current item is an object, so further stringification is needed
                                var newObj = items[i].Value<JObject>();
                                RecursivelyStringify(newObj);
                                items[i].Replace(JsonConvert.SerializeObject(newObj));
                            }
                        }
                    }
                }
            }
        }

        private static string Stringify(string jsonData) {
            // Nearly all sub-objects in the original game files are in "stringified" form, so here we will recursively
            // stringify any sub-objects to get back to that weird state
            var obj = JsonConvert.DeserializeObject<JObject>(jsonData);
            RecursivelyStringify(obj);
            return JsonConvert.SerializeObject(obj, Formatting.None);
        }
    }
}
