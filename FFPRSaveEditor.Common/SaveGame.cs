using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;
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

            return Encoding.UTF8.GetString(decompressedJSONData);
        }

        public static string Encrypt(string jsonData)
        {
            using var msJSONData = new MemoryStream(Encoding.UTF8.GetBytes(jsonData));
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
    }
}
