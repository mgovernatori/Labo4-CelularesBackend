namespace CelularesAPI.Services
{
    public interface IHashingServices
    {
        string Hash(string str);
        bool Verify(string str, string strHashed);
    }


    public class HashingServices : IHashingServices
    {
        public string Hash(string str)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt(10);
            return BCrypt.Net.BCrypt.HashPassword(str, salt);
        }

        public bool Verify(string str, string strHashed)
        {
            return BCrypt.Net.BCrypt.Verify(str, strHashed);    
        }
    }
}
