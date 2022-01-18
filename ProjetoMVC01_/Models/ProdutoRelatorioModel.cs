using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoMVC01_.Models
{
    public class ProdutoRelatorioModel
    {
        [Required(ErrorMessage = "Por favor, informe a Data de Início.")]
        public DateTime? DataMin { get; set; }

        [Required(ErrorMessage = "Por favor, informe a Data de Término.")]
        public DateTime? DataMax { get; set; }

        [Required(ErrorMessage = "Por favor, informe o Tipo de Relatório.")]
        public string TipoRelatorio { get; set; }
    }
}
