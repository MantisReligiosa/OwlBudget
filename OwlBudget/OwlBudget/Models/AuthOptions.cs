using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace OwlBudget.Models;

public static class AuthOptions
{
    public const string Issuer = "MyAuthServer"; // издатель токена
    public const string Audience = "MyAuthClient"; // потребитель токена
    private const string Key = "mysupersecret_secretkey!123"; // ключ для шифрации
    public const int Lifetime = 1; // время жизни токена - 1 минута

    public static SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
    }
}