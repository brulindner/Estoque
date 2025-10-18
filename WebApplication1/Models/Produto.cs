using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Produto
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        [Required]
        public int Quantidade { get; set; } = 0;
        [Required]
        public int EstoqueMinimo { get; set; } = 0;
        public string? FotoUrl { get; set; } //Caminho da imagem salva
    }
}