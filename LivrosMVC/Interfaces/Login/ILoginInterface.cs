using LivrosMVC.DTO;
using LivrosMVC.Models;

namespace LivrosMVC.Interfaces.Login
{
    public interface ILoginInterface
    {
        Task<ResponseModel<UsuarioModel>> Registrar(UsuarioCadastroDTO usuarioCadastroDTO);

    }
}
