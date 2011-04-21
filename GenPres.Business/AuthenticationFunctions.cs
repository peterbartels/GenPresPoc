
using System.Collections.Generic;
using System.Linq;

namespace GenPres.Business
{
    public interface ISingleObject<T>
    {
        bool IsAvailable { get; }
    }

    public class SingleObject<T> : ISingleObject<T>
    {
        private readonly T _object;

        private SingleObject(T obj)
        {
            _object = obj;
        }

        public static ISingleObject<T> GetSingleObject(IEnumerable<T> objectList)
        {
            if (objectList.Count() == 0) return new SingleObjectUnavailable<T>();
            if (objectList.Count() > 1) return new SingleObjectOverflow<T>();
            return new SingleObject<T>(objectList.First());
        }

        internal T ObjectResult
        {
            get { return _object; }
        }

        public bool IsAvailable
        {
            get { return true; }
        }
    }

    public class SingleObjectUnavailable<T> : ISingleObject<T>
    {
        public bool IsAvailable
        {
            get { return false; }
        }
    }


    public class SingleObjectOverflow<T> : ISingleObject<T>
    {
        public bool IsAvailable
        {
            get { return false; }
        }
    }

    internal static class AuthenticationFunctions
    {
        public static string MD5(string password)
        {
            byte[] textBytes = System.Text.Encoding.Default.GetBytes(password);
            try
            {
                System.Security.Cryptography.MD5CryptoServiceProvider cryptHandler;
                cryptHandler = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] hash = cryptHandler.ComputeHash(textBytes);
                string ret = "";
                foreach (byte a in hash)
                {
                    if (a < 16)
                        ret += "0" + a.ToString("x");
                    else
                        ret += a.ToString("x");
                }
                return ret;
            }
            catch
            {
                throw;
            }
        }
    }
}
