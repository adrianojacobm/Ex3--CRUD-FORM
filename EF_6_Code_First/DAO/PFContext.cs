using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF_6_Code_First.Entities;
using System.Data.Entity;


namespace EF_6_Code_First.DAO
{
    class PFContext : DbContext
    {
        public PFContext() : base("name = ConnectionString")
        {
            Database.SetInitializer<PFContext>(new DropCreateDatabaseIfModelChanges<PFContext>());
        }

        public DbSet<Cliente> PessoaFisicas { get; set; }
        
    }
}
