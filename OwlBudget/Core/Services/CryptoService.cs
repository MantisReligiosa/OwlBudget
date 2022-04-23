using System.Security.Cryptography;
using System.Text;
using Core.ServiceInterfaces;

namespace Core.Services;

public class CryptoService : ICryptoService
{
    public string Hash(string str)
    {
        var bytes = Encoding.Unicode.GetBytes(str);
        var hash = SHA256.Create().ComputeHash(bytes);
        var hashString = new StringBuilder();
        foreach (var x in hash) hashString.Append($"{x:x2}".ToUpper());
        return hashString.ToString();
    }
}