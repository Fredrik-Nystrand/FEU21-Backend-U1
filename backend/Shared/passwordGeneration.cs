using System.Security.Cryptography;
using System.Text;

namespace backend.Shared
{
    public class passwordGeneration
    {

        public byte[] PasswordHash;
        public byte[] PasswordSalt;


        public void GeneratePassword(string password)
        {
            using var hmac = new HMACSHA512();
            PasswordSalt = hmac.Key;
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        public bool ValidatePassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var _hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            for (int i = 0; i < passwordHash.Length; i++)
            {
                if (_hash[0] != passwordHash[0])
                    return false;
            }

            return true;
        }
    }
}
