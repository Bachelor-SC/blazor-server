using System.Security.Cryptography;
using System.Text;

namespace ScSoMeBlazorServer.Hasher
{
    public class PasswordHasher
    {
        public void HashPasswords(string password)
        {
            GetHashArray(password);
        }

        
        public string GetHashPasswords(string password)
        {
            return GetHashString(password);
        }
        

        private static byte[] GetHashArray(string inputString)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        
        private static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHashArray(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }
    }
}
