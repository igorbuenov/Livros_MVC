using LivrosMVC.Models;
using System.Data;

namespace LivrosMVC.Interfaces.Emprestimos
{
    public interface IEmprestimosInterface
    {
        Task<ResponseModel<List<EmprestimosModel>>> BuscarEmprestimos();
        Task<ResponseModel<EmprestimosModel>> BuscarEmprestimosPorId(int? id);
        Task<ResponseModel<EmprestimosModel>> CadastrarEmprestimos(EmprestimosModel emprestimoModel);
        Task<ResponseModel<EmprestimosModel>> EditarEmprestimos(EmprestimosModel emprestimoModel);
        Task<ResponseModel<EmprestimosModel>> ExcluirEmprestimos(EmprestimosModel emprestimoModel);
        Task<DataTable> BuscaDadosEmprestimosExcel();
        
    }
}
