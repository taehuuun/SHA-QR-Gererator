using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

using ZXing;
using ZXing.QrCode;
using System.IO;

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

    /// <summary>
    /// QR코드 데이터 세팅 및 생성 함수
    /// </summary>
    /// <param name="data">QR코드에 들어갈 데이터</param>
    /// <param name="width">QR코드 너비</param>
    /// <param name="height">QR코드 높이</param>
    /// <returns></returns>
    private static Color32[] Encode(string data, int width, int height)
    {
        // QR코드 생성 기본 세팅
        var writer = new BarcodeWriter
        {
            // 포맷은 QR코드
            Format = BarcodeFormat.QR_CODE,

            // 옵션 프로퍼티 설정
            Options = new QrCodeEncodingOptions
            {
                Height = height,
                Width = width
            }
        };

        return writer.Write(data);
    }
}
