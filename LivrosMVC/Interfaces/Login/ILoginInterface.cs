using LivrosMVC.DTO.Usuario;
using LivrosMVC.Models;

namespace LivrosMVC.Interfaces.Login
{
    public interface ILoginInterface
    {
        Task<ResponseModel<UsuarioModel>> Registrar(UsuarioCadastroDTO usuarioCadastroDTO);
        Task<ResponseModel<UsuarioModel>> Login(UsuarioLoginDTO usuarioLoginDTO);

    }
}
