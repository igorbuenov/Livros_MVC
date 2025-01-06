using LivrosMVC.Data;
using LivrosMVC.DTO.Usuario;
using LivrosMVC.Interfaces.Login;
using LivrosMVC.Interfaces.Senha;
using LivrosMVC.Interfaces.Sessao;
using LivrosMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace LivrosMVC.Services.Login
{
    public class LoginService : ILoginInterface
    {

        private readonly AppDbContext _context;
        private readonly ISenhaInterface _senhaInterface;
        private readonly ISessaoInterface _sessaoInterface;

        public LoginService(AppDbContext context, ISenhaInterface senhaInterface, ISessaoInterface sessaoInterface)
        {
            _context = context;
            _senhaInterface = senhaInterface;
            _sessaoInterface = sessaoInterface;
        }

        public async Task<ResponseModel<UsuarioModel>> Login(UsuarioLoginDTO usuarioLoginDTO)
        {

            ResponseModel<UsuarioModel> response = new ResponseModel<UsuarioModel>();

            try
            {

                var usuario = GetUsuarioByEmail(usuarioLoginDTO.Email);
                if (usuario == null)
                {
                    response.Mensagem = "Credenciais Inválidas!";
                    response.Status = false;
                    return response;
                }

                if(!_senhaInterface.VerificarSenhaAsync(usuarioLoginDTO.Senha, usuario.SenhaHash, usuario.SenhaSalt))
                {
                    response.Mensagem = "Credenciais Inválidas!";
                    response.Status = false;
                    return response;
                }

                // Criação de sessão para usuário logado
                _sessaoInterface.CriarSessao(usuario);


                response.Mensagem = "Usuário logado com sucesso!";
                return response;

            } 
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<UsuarioModel>> Registrar(UsuarioCadastroDTO usuarioCadastroDTO)
        {

            ResponseModel<UsuarioModel> response = new ResponseModel<UsuarioModel>();

            try
            {

                // Email já cadastrado
                if (VerificarEmailAsync(usuarioCadastroDTO.Email))
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
        private bool VerificarEmailAsync(string email)
        {
            return _context.Usuarios.FirstOrDefault(usuario => usuario.Email == email) != null ? true : false;
        }

        private UsuarioModel GetUsuarioByEmail(string email)
        {
            return _context.Usuarios.FirstOrDefault(usuario => usuario.Email == email);
        }

        


    }
}
