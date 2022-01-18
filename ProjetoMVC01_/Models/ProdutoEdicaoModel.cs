using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoMVC01_.Models
{
    public class ProdutoEdicaoModel
    {
        // campo oculto no formulário
        public Guid IdProduto { get; set; }

        [MinLength(6, ErrorMessage = "Por favor, informe no mínimo {1} caracteres")]
        [MaxLength(100, ErrorMessage = "Por favor, informe no máximo {1} caracteres")]
        [Required(ErrorMessage = "Por favor, informe o Nome do Produto.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Por favor, informe o Preço do Produto.")]
        public decimal? Preco { get; set; }

        [Required(ErrorMessage = "Por favor, informe a Quantidade do Produto.")]
        public int? Quantidade { get; set; }
    }
}
