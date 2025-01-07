using ClosedXML.Excel;
using LivrosMVC.Data;
using LivrosMVC.Interfaces.Emprestimos;
using LivrosMVC.Interfaces.Sessao;
using LivrosMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace LivrosMVC.Controllers
{
    public class EmprestimoController : Controller
    {

        private readonly IEmprestimosInterface _emprestimosInterface;
        private readonly ISessaoInterface _sessaoInterface;

        public EmprestimoController(IEmprestimosInterface emprestimosInterface , ISessaoInterface sessaoInterface)
        {
            _emprestimosInterface = emprestimosInterface;
            _sessaoInterface = sessaoInterface;
        }


        public async Task<IActionResult> Index()
        {

            var usuario = _sessaoInterface.BuscarSessao();
            if(usuario == null)
            {
                return RedirectToAction("Login", "Login");
            }

            var emprestimos = await _emprestimosInterface.BuscarEmprestimos();
            return View(emprestimos.Dados);
        }

        public IActionResult Cadastrar()
        {

            var usuario = _sessaoInterface.BuscarSessao();
            if (usuario == null)
            {
                return RedirectToAction("Login", "Login");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int? id)
        {

            var usuario = _sessaoInterface.BuscarSessao();
            if (usuario == null)
            {
                return RedirectToAction("Login", "Login");
            }

            var emprestimo = await _emprestimosInterface.BuscarEmprestimosPorId(id);

            return View(emprestimo.Dados);
        }

        [HttpGet]
        public async Task<IActionResult> Excluir(int? id)
        {

            var usuario = _sessaoInterface.BuscarSessao();
            if (usuario == null)
            {
                return RedirectToAction("Login", "Login");
            }

            var emprestimo = await _emprestimosInterface.BuscarEmprestimosPorId(id);

            return View(emprestimo.Dados);
        }

        [HttpGet]
        public async Task<IActionResult> Exportar()
        {

            var dados = await _emprestimosInterface.BuscaDadosEmprestimosExcel();

            using(XLWorkbook worKbook = new XLWorkbook())
            {
                worKbook.AddWorksheet(dados, "Dados Empréstimos");

                using(MemoryStream ms = new MemoryStream())
                {
                    worKbook.SaveAs(ms);
                    return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spredsheetml.sheet", "Emprestimo.xls");
                }
            }

        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(EmprestimosModel emprestimo)
        {
            if (ModelState.IsValid)
            {
                var emprestimoResult = await _emprestimosInterface.CadastrarEmprestimos(emprestimo);
                if(emprestimoResult.Status)
                {
                    TempData["MensagemSucesso"] = emprestimoResult.Mensagem;
                }
                else
                {
                    TempData["MensagemErro"] = "Algum erro ocorreu ao realizaro cadastro!";

                    return View(emprestimo);
                }

                return RedirectToAction("Index");
            }

            return View();
            
        }

        [HttpPost]
        public async Task<IActionResult> Editar(EmprestimosModel emprestimo)
        {
            if (ModelState.IsValid)
            {
                var emprestimoResult = await _emprestimosInterface.EditarEmprestimos(emprestimo);
                if(emprestimoResult.Status)
                {
                    TempData["MensagemSucesso"] = emprestimoResult.Mensagem;
                }
                else
                {
                    TempData["MensagemErro"] = "Algum erro ocorreu ao realizar a edição!";
                    return View(emprestimo);
                }
                
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Excluir(EmprestimosModel emprestimo)
        {

            if(emprestimo == null)
            {
                TempData["MensagemSucesso"] = "Empréstimo não localizado!";
                return View(emprestimo);
            }

            var emprestimoResult = await _emprestimosInterface.ExcluirEmprestimos(emprestimo);

            if(emprestimoResult.Status)
            {
                TempData["MensagemSucesso"] = emprestimoResult.Mensagem;
                
            }
            else
            {
                TempData["MensagemErro"] = emprestimoResult.Mensagem;
                return View(emprestimo);
            }

            
            return RedirectToAction("Index");
        }

    }
}
