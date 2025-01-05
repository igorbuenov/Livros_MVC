using LivrosMVC.Interfaces.Sessao;
using LivrosMVC.Models;

namespace LivrosMVC.Services.Sessao
{
    public class SessaoService : ISessaoInterface
    {


        private readonly IHttpContextAccessor _contextAccessor;

        public SessaoService(IHttpContextAccessor contextAcessor)
        {
            _contextAccessor = contextAcessor;
        }


        public UsuarioModel BuscarSessao()
        {
            throw new NotImplementedException();
        }

        public void CriarSessao(UsuarioModel usuarioModel)
        {
            
        }

        public void EncerrarSessao()
        {
            throw new NotImplementedException();
        }
    }
}
