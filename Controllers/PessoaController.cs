using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Models;
using Data;
using System.Linq;
using MyValidations;
using System;

namespace Controllers
{
    [Controller]
    [Route("[controller]")]
    public class PessoaController : ControllerBase
    {
        private DataContext context { get; set; }
        public PessoaController(DataContext context)
        {
            this.context = context;
        }

        // PARTE 1 // https://medium.com/@gedanmagalhaes/criando-uma-api-rest-com-asp-net-core-3-1-entity-framework-mysql-423c00e3b58e
        // PARTE 2 // https://medium.com/@gedanmagalhaes/criando-uma-api-rest-com-asp-net-core-3-1-entity-framework-mysql-parte-2-e969e82e5d2f

        // EXEMPLO DO POST: https://github.com/GedanMagal/Api-Ef/blob/master/Controllers/ProductController.cs
        
        [HttpGet]
        public IActionResult Get()
        {
            var pessoas = context.Pessoa;
            if(context.Pessoa.Count() > 0)
                return Ok(pessoas);
            else
                return NotFound("LISTA VAZIA");
        }

        [HttpPost]
        public IActionResult Post(Pessoa p)
        {

            if (ModelState.IsValid)
            {
                context.Pessoa.Add(p);                
                context.SaveChanges();                
                return Ok(p);
            }
            else
                return BadRequest(ModelState);
        }

        [HttpPut]
        public IActionResult Put(PessoaPut p)
        {
            if (ModelState.IsValid)
            {
                // Pessoa aux = new Pessoa { Id = p.Id, Cpf = p.Cpf, DataNascimento = p.DataNascimento, Sexo = p.Sexo };
                // var pessoa = context.Pessoa.Where(link => link.Id == p.Id).FirstOrDefault<Pessoa>();
                var pessoa = context.Pessoa.Where(link => link.Cpf == p.Cpf).FirstOrDefault<Pessoa>();
                if (pessoa != null)
                {
                    // pessoa.Nome = p.Nome;
                    // if (p.Id > 0)
                    //     pessoa.Id = p.Id;
                    if (p.Nome != null)
                        pessoa.Nome = p.Nome;
                    if (p.Cpf != null)
                        pessoa.Cpf = p.Cpf;
                    if (p.DataNascimento != null)
                        pessoa.DataNascimento = p.DataNascimento;                
                    if (p.Sexo != null)
                        pessoa.Sexo = p.Sexo.ToUpper();
                    context.SaveChanges();
                    return Ok(pessoa);
                }
                else
                    // return NotFound(pessoa.MensagemErro); // SE NÃO ENCONTRADO    
                    return NotFound("NÃO ENCONTRADO");
            }
            else
                return BadRequest(ModelState);
        }

        [HttpDelete("{cpf}")]
        public IActionResult Delete(string cpf)
        {
            if ( ! CpfString.Validar(cpf) )
                return BadRequest("DIGITE APENAS OS NUMEROS, sem pontos, virugulas, traços, espaços nem letras");            
            var pessoa = context.Pessoa.Where(link => link.Cpf == cpf).FirstOrDefault<Pessoa>();
            if (pessoa != null)
            {
                context.Pessoa.Remove(pessoa);            
                context.SaveChanges();
                return Ok(pessoa);
            }
            else
                return NotFound("NÃO ENCONTRADO");
        }

        [HttpGet("{cpf}")]
        public IActionResult GetSingle(string cpf)
        {   
            if ( ! CpfString.Validar(cpf) )
                return BadRequest("DIGITE APENAS OS NUMEROS, sem pontos, virugulas, traços, espaços nem letras");            
            var pessoa = context.Pessoa.Where(link => link.Cpf == cpf).FirstOrDefault<Pessoa>();
            if (pessoa != null)
                return Ok(pessoa);
            else
                return NotFound("NÃO ENCONTRADO");
        }
    }
}