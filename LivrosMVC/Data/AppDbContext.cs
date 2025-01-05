using LivrosMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace LivrosMVC.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<EmprestimosModel> Emprestimos { get; set; }
        public DbSet<UsuarioModel> Usuarios { get; set; }
    }
}
