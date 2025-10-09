using FileEncryptor.Lib.Crypt;

namespace FileEncryptor.Tests;

public class CryptTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestCrypt_EncryptAndDecrypt_SameData()
    {
        // Assign
        byte[] data = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
        
        byte[] cryptTable1 = CryptExtensions.CreateCryptTable(100);
        byte[] cryptTable2 = CryptExtensions.CreateCryptTable(300);
        byte[] decryptTable1 = CryptExtensions.CreateDecryptTable(100);
        byte[] decryptTable2 = CryptExtensions.CreateDecryptTable(300);

        byte[] cryptedData = data.ToArray();
        cryptedData.CryptBytes(cryptTable1, data.Length);
        cryptedData.CryptBytes(cryptTable2, data.Length);
        cryptedData.CryptBytes(decryptTable2, data.Length);
        cryptedData.CryptBytes(decryptTable1, data.Length);
        Assert.That(data, Is.EqualTo(cryptedData));
    }
}