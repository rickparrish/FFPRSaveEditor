using FFPRSaveEditor.Common.Models;
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

        static SaveGame() {
            // Set float parser to use Decimal, because default setting of Double doesn't handle all 'playTime' values correctly
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings {
                FloatParseHandling = FloatParseHandling.Decimal
            };
        }

        public static string Decrypt(string jsonData, bool verify)
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

            return DeStringify(Encoding.UTF8.GetString(decompressedJSONData), verify);
        }

        private static string DeStringify(string jsonData, bool verify) {
            // After decrypting the game file we have a JSON string where nearly all sub-objects are in "stringified" form
            // So here we will recursively expand any stringified sub-objects and array members to give a more "standard" JSON form
            var obj = JsonConvert.DeserializeObject<JObject>(jsonData);
            RecursivelyDeStringify(obj);

            // Serialize our newly-expanded JObject and store in the result variable
            string result = JsonConvert.SerializeObject(obj, Formatting.Indented);

            if (verify) {
                // If we're being asked to verify, we need to re-stringify the de-stringified json, which if all is working well
                // will yield a string that matches the original jsonData that was passed into this method.
                // If the string does not match, it may indicate an error in the de-stringification or re-stringification methods,
                // or in one of the model classes (as happened with the FF3 companionEntity property at one point), which could
                // result in the game crashing when the modified game file is used, so we throw an excetion to abort.
                string reStringified = Stringify(result);
                if (reStringified != jsonData) {
                    throw new InvalidDataException("Re-stringified json does not match input jsonData.");
                }

                // A second test of the de-stringification and re-stringification methods, which are working with game-specific classes.
                if (jsonData.Contains("userData", StringComparison.OrdinalIgnoreCase)) {
                    Type saveType = Type.GetType($"FF{DetectVersion(result)}SaveGame");
                    reStringified = Stringify(JsonConvert.SerializeObject(JsonConvert.DeserializeObject(result, saveType)));
                    if (reStringified != jsonData) {
                        throw new InvalidDataException("Re-stringified json does not match input jsonData");
                    }
                }

                // A third test to ensure encryption is working
                try {
                    Encrypt(result);
                } catch (Exception ex) {
                    throw new InvalidDataException($"Error re-encrypting the decrypted json: {ex.Message}");
                }
            }

            return result;
        }

        public static int DetectVersion(string jsonData) {
            var obj = JsonConvert.DeserializeObject<BaseSaveGame2>(jsonData);
            string ownedTransportationList = string.Join(",", obj.userData.ownedTransportationList.target.Select(x => x.id).OrderBy(x => x));

            if (jsonData.Contains("currentSelectedPartyId")) {
                if (ownedTransportationList == "1,2,3,4,5,6,7,8") {
                    return 6;
                } else {
                    throw new InvalidDataException("Version detection saw FF6 with an unknown ownedTransportationList");
                }
            } else if (jsonData.Contains("wonderWandIndex")) {
                if (ownedTransportationList == "1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,17,18,19,20") {
                    return 5;
                } else {
                    throw new InvalidDataException("Version detection saw FF5 with an unknown ownedTransportationList");
                }
            } else if (jsonData.Contains("totalGil")) {
                if (ownedTransportationList == "1,2,3,4,5,6,7,8,9,10,11") {
                    return 4;
                } else {
                    throw new InvalidDataException("Version detection saw FF4 with an unknown ownedTransportationList");
                }
            } else if (jsonData.Contains("viewType")) {
                if (ownedTransportationList == "1,2,3,5,6,8,9,10,11,12,13,14,15,16") {
                    return 3;
                } else {
                    throw new InvalidDataException("Version detection saw FF3 with an unknown ownedTransportationList");
                }
            } else if (ownedTransportationList == "1,2,3,4,5,6,7") {
                return 2;
            } else if (ownedTransportationList == "1,2,3,4") {
                return 1;
            }

            // If we get here version detection failed
            throw new InvalidDataException("Version detection failed");
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
