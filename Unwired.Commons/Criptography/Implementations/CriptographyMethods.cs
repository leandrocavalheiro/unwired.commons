using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;
using Unwired.Commons.Criptography.Interfaces;
using Unwired.Commons.Extensions;

namespace Unwired.Commons.Criptography.Implementations;

public class CriptographyMethods : ICriptographyMethods
{
    private readonly string _salt;
    private readonly string _cryptoKey;
    private readonly int _interations;    

    public CriptographyMethods(IConfiguration configuration)
    {
        _salt = configuration.GetSection("Criptography:Salt").Value.GetValueOrDefault("83ba7e67-4d39-4227-a12d-0657be79e689");
        _cryptoKey = configuration.GetSection("Criptography:Key").Value.GetValueOrDefault("822b6cd1-9368-4b36-8815-8006e23098d2");
        _interations = configuration.GetSection("Criptography:Interations").Value.GetValueOrDefault("1000").ToInt();
    }

    public string Encrypt(string text)
    {
        var key = new Rfc2898DeriveBytes(_cryptoKey, Encoding.UTF8.GetBytes(_salt), _interations, HashAlgorithmName.SHA512).GetBytes(32);

        using (var aesAlg = Aes.Create())
        {
            aesAlg.Mode = CipherMode.ECB;

            using (var encryptor = aesAlg.CreateEncryptor(key, aesAlg.IV))
            {
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    using (var swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(text);
                    }
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }
    }
    public bool CompareText(string text, string encryptedText)
    {
        text = Encrypt(text);
        return text == encryptedText;
    }
}
