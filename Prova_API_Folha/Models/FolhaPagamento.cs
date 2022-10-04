using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class FolhaPagamento
    {
        [Key()]
        public int Id { get; set; }

        [ForeignKey("Funcionario")]
        public int FuncionarioId { get; set; }
        public virtual Funcionario Funcionario { get; set; }

        public int Mes { get; set; }

        public int Ano { get; set; }
        public double ValorHora { get; set; }

        public int Quantidade { get; set; }

        public double Bruto { get; set; }

        public double ImpostoRenda { get; set; }

        public double Inss { get; set; }

        public double ImpostoFgts { get; set; }

        public double  Liquido { get; set; }

    }
}