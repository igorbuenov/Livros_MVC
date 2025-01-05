using LivrosMVC.Data;
using LivrosMVC.DTO;
using LivrosMVC.Interfaces.Login;
using LivrosMVC.Interfaces.Senha;
using LivrosMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace LivrosMVC.Services.Login
{
    public class LoginService : ILoginInterface
    {

        private readonly AppDbContext _context;
        private readonly ISenhaInterface _senhaInterface;

        public LoginService(AppDbContext context, ISenhaInterface senhaInterface)
        {
            _context = context;
            _senhaInterface = senhaInterface;
        }


        public async Task<ResponseModel<UsuarioModel>> Registrar(UsuarioCadastroDTO usuarioCadastroDTO)
        {

            ResponseModel<UsuarioModel> response = new ResponseModel<UsuarioModel>();

            try
            {

                // Email já cadastrado
                if (verificarEmailAsync(usuarioCadastroDTO.Email))
                {
                    response.Mensagem = "Email já cadastrado!";
                    response.Status = false;
                    return response;
                }

                // Cripto SENHA e SALT
                _senhaInterface.CriarSenhaHash(usuarioCadastroDTO.Senha, out byte[] senhaHash, out byte[] senhaSalt);

                // Envio de dados para o banco
                var usuario = new UsuarioModel() {

                    Nome = usuarioCadastroDTO.Nome,
                    Sobrenome = usuarioCadastroDTO.Sobrenome,
                    Email = usuarioCadastroDTO.Email,
                    SenhaHash = senhaHash,
                    SenhaSalt = senhaSalt

                };

                _context.Add(usuario);
                await _context.SaveChangesAsync();

                //Retorno de dados para o Client
                response.Mensagem = "Usuário cadastrado com sucesso!";
                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }

        }




        // UTILITÁRIOS
        private bool verificarEmailAsync(string email)
        {
            return _context.Usuarios.FirstOrDefault(usuario => usuario.Email == email) != null ? true : false;
        }


    }
}
