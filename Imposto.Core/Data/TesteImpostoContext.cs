﻿using Imposto.Core.Domain;
using System.Data.Entity;

namespace Imposto.Core.Data
{
    public class TesteImpostoContext : DbContext
    {
        public TesteImpostoContext() : base("Data Source=DESKTOP-S518OET;DataBase=teste;initial Catalog=teste;Integrated Security=SSPI;")
        {
            Database.SetInitializer<TesteImpostoContext>(null);
        }

        public DbSet<NotaFiscal> NotaFiscal { get; set; }
        public DbSet<NotaFiscalCFOP> NotaFiscalCFOP { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
        }

    }
}
