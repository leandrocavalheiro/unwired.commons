namespace Unwired.Commons.Criptography.Interfaces;

public interface ICriptographyMethods
{
    string Encrypt(string text);
    bool CompareText(string text, string encryptedText);
}
