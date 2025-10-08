using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data
{
    public class AppDbCoontext : DbContext
    {
        public AppDbCoontext(DpContextOptions option) : base(options) { }

        public DbSet<Produto> Estoque { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
    }
}