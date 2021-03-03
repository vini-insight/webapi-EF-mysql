using Microsoft.AspNetCore.Mvc;
using Models;
using Data;
using System.Linq;
using MyValidations;
using Microsoft.Extensions.Logging;
using webapi_EF_mysql.Data;
using webapi_EF_mysql.Models;

namespace Controllers
{
    [ApiController] // PARA RODAR COM JSON NO POSTMAN
    [Route("[controller]")]
    public class PessoaController : ControllerBase
    {
        private readonly ILogger<PessoaController> _logger;
        
        private PessoaRepositorio pr { get; set; }
        
        public PessoaController(DataContext context, ILogger<PessoaController> logger)
        {            
            pr = new PessoaRepositorio(context);    // TIRAR DÚVIDA SE É UMA BOA PRÁTICA AQUI (FALAR SOBRE INJEÇÃO DE DEPENDENCIA E LOCALIZAÇÃO NO PACOTE DO PROJETO)
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
            var pessoas = PessoaRepositorio.GetListPessoa();            
            if(pessoas.Count() > 0)
            {
                StaticNLog.GerarLogSucesso("Lista de Pessoas foi Retornada");
                return Ok(pessoas);
            }                
            else
            {
                StaticNLog.GerarLogInfo("a lista de pessoas está vazia");
                return NotFound("LISTA VAZIA");
            }
        }

        [HttpPost]
        public IActionResult Post(Pessoa p)
        {
            if(ModelState.IsValid)            
            {
                StaticNLog.GerarLogSucesso("Nova Pessoa foi Gravada no BD");
                return Ok(PessoaRepositorio.InserirNoBancoDados(p));
            }
            else
            {
                StaticNLog.GerarLogInfo("aconteceu uma tentativa de gravar dados que não passou na validação");
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        public IActionResult Put(PessoaPut p)
        {
            if(ModelState.IsValid)
            {
                var pessoa = PessoaRepositorio.AtualizarNoBancoDados(p);
                if(pessoa != null)
                {
                    StaticNLog.GerarLogSucesso("Pessoa foi Atualizada");
                    return Ok(pessoa);
                }
                else                
                {                    
                    StaticNLog.GerarLogInfo("a pessoa com CPF número " + p.Cpf + " NÃO ENCONTRADA PARA ATUALIZAR SEUS DADOS");
                    return NotFound("NÃO ENCONTRADO");
                }
            }
            else
                return BadRequest(ModelState);
        }

        [HttpDelete("{cpf}")]
        public IActionResult Delete(string cpf)
        {
            if( ! CpfString.Validar(cpf) )
                return BadRequest("DIGITE APENAS OS NUMEROS, sem pontos, virugulas, traços, espaços nem letras");                        
            var pessoa = PessoaRepositorio.ApagarNoBancoDados(cpf);
            if(pessoa != null)
            {
                StaticNLog.GerarLogSucesso("Pessoa foi Excluída");
                return Ok(pessoa);
            }
            else
            {
                StaticNLog.GerarLogInfo("CPF " + cpf + " NÃO ENCONTRADO PARA EXCLUIR DA BASE");
                return NotFound("NÃO ENCONTRADO");
            }
        }

        [HttpGet("{cpf}")]
        public IActionResult GetSingle(string cpf)
        {   
            if( ! CpfString.Validar(cpf) )
                return BadRequest("DIGITE APENAS OS NUMEROS, sem pontos, virugulas, traços, espaços nem letras");            
            var pessoa = PessoaRepositorio.GetPessoa(cpf);
            if(pessoa != null)
            {
                StaticNLog.GerarLogSucesso("Pessoa foi Retornada");
                return Ok(pessoa);
            }
            else
            {
                StaticNLog.GerarLogInfo("o CPF " + cpf + " NÃO ENCONTRADO NA BASE DE DADOS");
                return NotFound("NÃO ENCONTRADO");
            }
        }
    }
}