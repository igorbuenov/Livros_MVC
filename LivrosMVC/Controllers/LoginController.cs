using LivrosMVC.DTO.Usuario;
using LivrosMVC.Interfaces.Login;
using Microsoft.AspNetCore.Mvc;

namespace LivrosMVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginInterface _loginInterface;

        public LoginController(ILoginInterface loginInterface)
        {
            _loginInterface = loginInterface;
        }


        public IActionResult Index()
        {
            return View();
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
            return View();
        }
    }
}
