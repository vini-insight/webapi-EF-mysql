using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Models;
using Data;

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
        
        [HttpGet]
        public IActionResult Get()
        {
            var pessoas = context.Pessoa;
            return Ok(pessoas);
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
    }
}