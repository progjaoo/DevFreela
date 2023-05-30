namespace DevFreela.Coree.Services
{
    public interface IAuthService
    {
        //Adicionei a interface que vai ter esse método para ser implementado na camada infraestructure...
        string GenerateJwtToken(string email, string role);
        string ComputeSha256Hash(string password);
    }
}
