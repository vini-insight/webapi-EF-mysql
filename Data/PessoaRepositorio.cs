using System.Linq;
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

        public static Pessoa GetPessoa(string cpf)
        {
            return _context.Pessoa.Where(link => link.Cpf == cpf).FirstOrDefault<Pessoa>();
        }

        public static Pessoa InserirNoBancoDados(Pessoa p)
        {
            p.Sexo = p.Sexo.ToUpper();
            _context.Pessoa.Add(p);
            _context.SaveChanges();
            return p;
        }

        public static Pessoa AtualizarNoBancoDados(PessoaPut p)
        {
            var pessoa = _context.Pessoa.Where(link => link.Cpf == p.Cpf).FirstOrDefault<Pessoa>();
            if (pessoa != null)
            {
                if (p.Nome != null)
                    pessoa.Nome = p.Nome;
                if (p.Cpf != null)
                    pessoa.Cpf = p.Cpf;
                if (p.DataNascimento != null)
                    pessoa.DataNascimento = p.DataNascimento;                
                if (p.Sexo != null)
                    pessoa.Sexo = p.Sexo.ToUpper();
                _context.SaveChanges();
                return pessoa;
            }
            else
                return null;            
        }

        public static Pessoa ApagarNoBancoDados(string cpf)
        {
            var pessoa = _context.Pessoa.Where(link => link.Cpf == cpf).FirstOrDefault<Pessoa>();
            if (pessoa != null)
            {
                _context.Pessoa.Remove(pessoa);
                _context.SaveChanges();
                return pessoa;
            }
            else
                return null;
        }
    }
}