using Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace webapi_EF_mysql.Data
{
    public class PessoaRepositorio
    {
        private static DataContext _context { get; set; }

        public PessoaRepositorio(DataContext context)
        {
            _context = context;
        }


        public static DbSet<Pessoa> GetListPessoa()
        {
            return _context.Pessoa;
        }
    }
}