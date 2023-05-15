namespace DevFreela.Infrastructure.CloudServices.Interfaces
{
    public interface IfileStorageService
    {
        void UploadFile(byte[] bytes, string fileName);
    }
}
