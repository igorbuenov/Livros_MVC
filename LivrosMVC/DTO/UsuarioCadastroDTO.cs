using System.ComponentModel.DataAnnotations;

namespace LivrosMVC.DTO
{
    public class UsuarioCadastroDTO
    {
        [Required(ErrorMessage = "Digite o Nome!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite o Sobrenome!")]
        public string Sobrenome { get; set; }

        [Required(ErrorMessage = "Digite o Email!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Digite a Senha!")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Digite a confirmação da senha!"), Compare("Senha", ErrorMessage = "As senhas não são iguais!")]
        public string ConfirmaSenha { get; set; }

    }
}
