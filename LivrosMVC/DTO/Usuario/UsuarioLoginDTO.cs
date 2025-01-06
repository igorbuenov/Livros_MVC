using System.ComponentModel.DataAnnotations;

namespace LivrosMVC.DTO.Usuario
{
    public class UsuarioLoginDTO
    {
        
        [Required(ErrorMessage = "Digite o Email!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Digite a Senha!")]
        public string Senha { get; set; }
    }
}
