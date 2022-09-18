using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CryptoQR
{
    /// <summary>
    /// SHA-256 알고리즘으로 데이터 암호화
    /// </summary>
    /// <param name="data">암호화할 데이터</param>
    /// <returns></returns>
    private static string EncryptSHA(string data)
    {
        var bytes = Encoding.UTF8.GetBytes(data);
        var hash = new SHA256CryptoServiceProvider().ComputeHash(bytes);
        var encryptedData = new StringBuilder();

        foreach(var h in hash) encryptedData.AppendFormat("{0:x2}",h);

        return encryptedData.ToString();
    }
}
