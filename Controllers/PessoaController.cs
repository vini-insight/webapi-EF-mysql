using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Models;
using Data;
using System.Linq;
using MyValidations;
using System;
using Microsoft.Extensions.Logging;

namespace Controllers
{
    [Controller]
    [Route("[controller]")]
    public class PessoaController : ControllerBase
    {
        private readonly ILogger<PessoaController> _logger;
        private DataContext context { get; set; }
        public PessoaController(DataContext context, ILogger<PessoaController> logger)
        {
            this.context = context;
            _logger = logger;
            _logger.LogDebug(1, "NLog injected into HomeController");
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Hello, this is the index!");
            // return View();
            return Ok();
        }
        
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
                var pessoa = context.Pessoa.Where(link => link.Cpf == p.Cpf).FirstOrDefault<Pessoa>();
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
                    context.SaveChanges();
                    return Ok(pessoa);
                }
                else
                // return NotFound(pessoa.MensagemErro); // SE NÃO ENCONTRADO    
                {
                    var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
                    try
                    {
                        logger.Debug("a pessoa com CPF número " + p.Cpf + " NÃO ENCONTRADA PARA ATUALIZAR SEUS DADOS");                        
                    }
                    catch (Exception exception)
                    {                        
                        logger.Error(exception, "ALGUMA EXCEÇÃO ACONTECEU E NÃO FOI POSSIVEL GERAR LOG.");
                        throw;
                    }
                    finally
                    {
                        NLog.LogManager.Shutdown();
                    }
                    return NotFound("NÃO ENCONTRADO");
                }
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
            {
                var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
                try
                {
                    logger.Debug("CPF " + cpf + " NÃO ENCONTRADO PARA EXCLUIR");                        
                }
                catch (Exception exception)
                {                        
                    logger.Error(exception, "ALGUMA EXCEÇÃO ACONTECEU E NÃO FOI POSSIVEL GERAR LOG.");
                    throw;
                }
                finally
                {
                    NLog.LogManager.Shutdown();
                }
                return NotFound("NÃO ENCONTRADO");
            }
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
            {
                var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
                try
                {
                    logger.Debug("CPF " + cpf + " NÃO ENCONTRADO");                        
                }
                catch (Exception exception)
                {                        
                    logger.Error(exception, "ALGUMA EXCEÇÃO ACONTECEU E NÃO FOI POSSIVEL GERAR LOG.");
                    throw;
                }
                finally
                {
                    NLog.LogManager.Shutdown();
                }
                return NotFound("NÃO ENCONTRADO");
            }
        }
    }
}