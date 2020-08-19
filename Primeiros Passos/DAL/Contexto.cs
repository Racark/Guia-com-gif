using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Primeiros_Passos.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Primeiros_Passos.DAL
{
    public class Contexto :DbContext
    {
        public Contexto() : base("GuiaConnectionString") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Guia> GuiaDb { get; set; }
    }
}