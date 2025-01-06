using LivrosMVC.DTO.Usuario;
using LivrosMVC.Interfaces.Login;
using LivrosMVC.Interfaces.Sessao;
using Microsoft.AspNetCore.Mvc;

namespace LivrosMVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginInterface _loginInterface;
        private readonly ISessaoInterface _sessaoInterface;

        public LoginController(ILoginInterface loginInterface, ISessaoInterface sessaoInterface)
        {
            _loginInterface = loginInterface;
            _sessaoInterface = sessaoInterface;
        }

        [HttpGet]
        public IActionResult Login()
        {
            var usuario = _sessaoInterface.BuscarSessao();
            if (usuario != null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            _sessaoInterface.EncerrarSessao();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(UsuarioCadastroDTO usuarioCadastroDTO)
        {

            if (ModelState.IsValid)
            {

                var usuario = await _loginInterface.Registrar(usuarioCadastroDTO);

                if (usuario.Status)
                {
                    TempData["MensagemSucesso"] = usuario.Mensagem;
                } else
                {
                    TempData["MensagemErro"] = usuario.Mensagem;
                    return View(usuarioCadastroDTO);
                }

                return RedirectToAction("Index");

            }
            else
            {
                return View(usuarioCadastroDTO);
            }

            
        }

        [HttpPost]
        public async Task<IActionResult> Login(UsuarioLoginDTO usuarioLoginDTO)
        {

            if (ModelState.IsValid)
            {
                var usuario = await _loginInterface.Login(usuarioLoginDTO);
                if (usuario.Status)
                {
                    TempData["MensagemSucesso"] = usuario.Mensagem;
                    return RedirectToAction("Index", "Home");
                } 
                else
                {
                    TempData["MensagemErro"] = usuario.Mensagem;
                    return View(usuarioLoginDTO);
                }

            }
            else
            {
                return View(usuarioLoginDTO);
            }
        }
    }
}
