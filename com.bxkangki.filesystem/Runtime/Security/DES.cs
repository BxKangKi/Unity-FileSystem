// 한번사는인생. (2020, January 9). 웃으면 1류다.
// [C#] 암호화/복호화 한방에 해결하자.(Feat.DESCryptoServiceProvider).
// https://im-first-rate.tistory.com/

using System;
using System.Security.Cryptography;
using System.IO;
using System.Text;

public enum DesType {
    Encrypt = 0,    // 암호화
    Decrypt = 1     // 복호화
}

public class DES {
        
    // Key values must be 8 digits.
    private byte[] Key { get; set; }
 
    // encryption / decryption method
    public string Result(DesType type, string input) {
        DESCryptoServiceProvider des = new DESCryptoServiceProvider() { 
            Key = Key,
            IV = Key
        };
 
        MemoryStream ms = new MemoryStream();
 
        // define transform / data as anonymous type
        var property = new {
            transform = type.Equals(DesType.Encrypt) ? des.CreateEncryptor() : des.CreateDecryptor(),
            data = type.Equals(DesType.Encrypt) ?  Encoding.UTF8.GetBytes(input.ToCharArray()) : Convert.FromBase64String(input)
        };
 
        CryptoStream cryStream = new CryptoStream(ms, property.transform, CryptoStreamMode.Write);
        var data = property.data;
 
        cryStream.Write(data, 0, data.Length);
        cryStream.FlushFinalBlock();
 
        return type.Equals(DesType.Encrypt) ? Convert.ToBase64String(ms.ToArray()) : Encoding.UTF8.GetString(ms.GetBuffer());
    }
 
    // constructor
    public DES(string key) {
        Key = ASCIIEncoding.ASCII.GetBytes(key);
    }
}
