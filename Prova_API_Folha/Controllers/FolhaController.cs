using System;
using System.Collections.Generic;
using System.Linq;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/folha")]

    public class FolhaController : ControllerBase
    {
        private const int V = 0;
        private readonly DataContext _context;

        public FolhaController(DataContext context) => _context = context; 

        [HttpPost]
        [Route("cadastrar")]
        public IActionResult Cadastrar([FromBody] FolhaPagamento folha) {
            folha.Bruto = folha.Quantidade * folha.ValorHora; 

            if (folha.Bruto <= 1903.98) {
                 return 0;
                    }
            else if (folha.Bruto <= 2826.65){
                return (folha.ImpostoRenda = folha.Bruto * .075) - 142.8;
            }
            else if (Bruto <= 3751.05) {
                return (folha.ImpostoRenda = folha.Bruto * .15) - 354.8;
                    }
            else if (folha.Bruto <= 4664.68) {
                return (folha.ImpostoRenda = folha.Bruto * .225) - 636.13;
                }
                return (folha.ImpostoRenda = folha.Bruto * .275) - 869.39; 


            if (folha.Bruto <= 1693.72){
                return folha.Inss = folha.Bruto * .08;
            }
            else if (Bruto <= 2822.9) {
                return folha.Inss = folha.Bruto * .09;
                    }
            else if (folha.Bruto <= 5645.8) {
                return folha.Inss = folha.Bruto * 0.11; 
                }
                return folha.Inss = 621.03; 

            
            folha.ImpostoFgts = folha.Bruto * 0.08; 
            folha.Liquido = folha.Bruto - folha.ImpostoRenda - folha.Inss; 
            folha.Mes = DateTime.Now.Month;
            Funcionario funcionario = _context.Funcionarios.Find(folha.FuncionarioId); 
            folha.Funcionario = funcionario; 

            if(funcionario != null) {
                _context.Folhas.Add(folha); 
                _context.SaveChanges(); 
                return Created("", folha);  
            } else {
                return NotFound("código de status 404"); 
            }
        }

        [HttpGet]
        [Route("listar")]
        public IActionResult Listar() {
            List<FolhaPagamento> folhas = new List<FolhaPagamento>(); 
            List<FolhaPagamento> folhasDB = _context.Folhas.ToList(); 

            foreach(var folha in folhasDB) {
                Funcionario funcionario = _context.Funcionarios.Find(folha.FuncionarioId); 
                folha.Funcionario = funcionario; 

                folhas.Add(folha); 
            }

           if(folhasDB == null) {
               return NotFound("código de status 404");
           } else {
               return Ok(folhas); 
           }
        }

        [HttpGet]
        [Route("buscar/{cpf}/{mes}/{ano}")]
        public IActionResult Buscar([FromRoute] string cpf, [FromRoute] int mes, [FromRoute] int ano) {
            Funcionario funcionario = _context.Funcionarios.FirstOrDefault(f => f.Cpf.Equals(cpf)); 
            FolhaPagamento folha = _context.Folhas.FirstOrDefault(f => f.FuncionarioId.Equals(funcionario.Id)); 

            if(mes == folha.Mes && ano == folha.Ano) {
                return Ok(folha);
            } else {
                return NotFound("código de status 404");
            }
        }

    }

}
    
