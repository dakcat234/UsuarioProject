namespace Business.Repositories
{
    public interface IEncryptService
    {
        String Encrypt(String plainText);
        String Decrypt(String cipherText);
    }
}
