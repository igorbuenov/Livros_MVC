using LivrosMVC.Data;
using LivrosMVC.Interfaces.Emprestimos;
using LivrosMVC.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace LivrosMVC.Services.Emprestimos
{
    public class EmprestimosService : IEmprestimosInterface
    {

        private readonly AppDbContext _context;
        
        public EmprestimosService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<DataTable> BuscaDadosEmprestimosExcel()
        {
            DataTable dataTable = new DataTable();

            dataTable.TableName = "Dados empréstimos";

            dataTable.Columns.Add("Recebedor", typeof(string));
            dataTable.Columns.Add("Fornecedor", typeof(string));
            dataTable.Columns.Add("Livro", typeof(string));
            dataTable.Columns.Add("Data empréstimo", typeof(DateTime));

            var emprestimos = await BuscarEmprestimos();

            if (emprestimos.Dados.Count > 0)
            {
                emprestimos.Dados.ForEach(emprestimo =>
                {
                    dataTable.Rows.Add(emprestimo.Recebedor, emprestimo.Fornecedor, emprestimo.LivroEmprestado, emprestimo.DataEmprestimo);
                });
            }

            return dataTable;
        }

        public async Task<ResponseModel<List<EmprestimosModel>>> BuscarEmprestimos()
        {

            ResponseModel<List<EmprestimosModel>> response = new ResponseModel<List<EmprestimosModel>>();

            try
            {

                var emprestimos = await _context.Emprestimos.ToListAsync();

                if (emprestimos == null)
                {
                    response.Mensagem = "Registros não encontrados!";
                    return response;
                }

                response.Dados = emprestimos;
                response.Mensagem = "Dados coletados com sucesso!";
                return response;

            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }

        }

        public async Task<ResponseModel<EmprestimosModel>> BuscarEmprestimosPorId(int? id)
        {
            ResponseModel<EmprestimosModel> response = new ResponseModel<EmprestimosModel>();

            try
            {

                if (id == null)
                {
                    response.Mensagem = "Empréstimo não localizado!";
                    response.Status = false;
                    return response;
                }

                var emprestimo = await _context.Emprestimos.FirstOrDefaultAsync(emp => emp.Id == id);

                if (emprestimo == null)
                {
                    response.Mensagem = "Empréstimo não localizado!";
                    return response;
                }

                response.Dados = emprestimo;
                response.Mensagem = "Dados coletados com sucesso!";
                return response;

            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<EmprestimosModel>> CadastrarEmprestimos(EmprestimosModel emprestimoModel)
        {
            ResponseModel<EmprestimosModel> response = new ResponseModel<EmprestimosModel>();

            try
            {

                _context.Add(emprestimoModel);
                await _context.SaveChangesAsync();

                response.Mensagem = "Cadastro realizado com sucesso!";
                return response;


            }
            catch(Exception ex)
            {

                response.Mensagem = ex.Message;
                response.Status = false;
                return response;

            }
        }

        public async Task<ResponseModel<EmprestimosModel>> EditarEmprestimos(EmprestimosModel emprestimoModel)
        {
            ResponseModel<EmprestimosModel> response = new ResponseModel<EmprestimosModel>();

            try
            {

                var emprestimo = await BuscarEmprestimosPorId(emprestimoModel.Id);

                if (emprestimo.Status == false)
                {
                    return emprestimo;
                }

                emprestimo.Dados.LivroEmprestado = emprestimoModel.LivroEmprestado;
                emprestimo.Dados.Fornecedor = emprestimoModel.Fornecedor;
                emprestimo.Dados.Recebedor = emprestimoModel.Recebedor;

                _context.Update(emprestimo.Dados);
                await _context.SaveChangesAsync();

                response.Mensagem = "Edição realizada com sucesso!";
                return response;


            }
            catch(Exception ex)
            {
                response.Mensagem= ex.Message;
                response.Status = false;
                return response;
            }

        }

        public async Task<ResponseModel<EmprestimosModel>> ExcluirEmprestimos(EmprestimosModel emprestimoModel)
        {

            ResponseModel<EmprestimosModel> response = new ResponseModel<EmprestimosModel>();

            try
            {
                _context.Remove(emprestimoModel);
                await _context.SaveChangesAsync();

                response.Mensagem = "Registro excluído com sucesso!";
                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }


        }
    }
}
