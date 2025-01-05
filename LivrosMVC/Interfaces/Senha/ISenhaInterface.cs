using LivrosMVC.DTO.Usuario;

namespace LivrosMVC.Interfaces.Senha
{
    public interface ISenhaInterface
    {
        void CriarSenhaHash(string senha, out byte[] senhaHash, out byte[] senhaSalt);
        bool VerificarSenhaAsync(string senha, byte[] senhaHash, byte[] senhaSalt);
    }
}
