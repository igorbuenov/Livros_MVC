using LivrosMVC.Models;

namespace LivrosMVC.Interfaces.Sessao
{
    public interface ISessaoInterface
    {
        UsuarioModel BuscarSessao();
        void CriarSessao(UsuarioModel usuarioModel);
        void EncerrarSessao();
    }
}
