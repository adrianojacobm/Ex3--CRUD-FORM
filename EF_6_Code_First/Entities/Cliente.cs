using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_6_Code_First.Entities
{
   
    class Cliente : Pessoa
    {
        
        public string Cpf { get; set; }
        
        public int ClienteID { get; set; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }
        //public Carro Carro { get; set; }
        
    }
}
