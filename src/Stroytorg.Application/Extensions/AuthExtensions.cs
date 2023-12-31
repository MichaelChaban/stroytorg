using System.Security.Cryptography;

namespace Stroytorg.Application.Extensions;

public static class AuthExtensions
{
    public static bool VerifyPassword(this string providedPassword, string password)
    {
        byte[] hashBytes = Convert.FromBase64String(password);
        byte[] salt = GetSaltFromHashBytes(hashBytes);
        byte[] computedHash = ComputePbkdf2Hash(providedPassword, salt);

        return CompareHashes(hashBytes, computedHash);
    }

    private static byte[] GetSaltFromHashBytes(byte[] hashBytes)
    {
        byte[] salt = new byte[16];
        Array.Copy(hashBytes, 0, salt, 0, 16);
        return salt;
    }

    private static byte[] ComputePbkdf2Hash(string password, byte[] salt)
    {
        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations: 10000, HashAlgorithmName.SHA256);
        return pbkdf2.GetBytes(20);
    }

    private static bool CompareHashes(byte[] storedHashBytes, byte[] computedHash)
    {
        for (int i = 0; i < 20; i++)
        {
            if (storedHashBytes[i + 16] != computedHash[i])
            {
                return false;
            }
        }

        return true;
    }
}
