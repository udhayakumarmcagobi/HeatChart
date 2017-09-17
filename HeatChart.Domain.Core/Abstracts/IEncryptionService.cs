namespace HeatChart.Domain.Core.Abstracts
{
    public interface IEncryptionService
    {
        string CreateSalt();
        string EncryptPassword(string password, string salt);
    }
}
