using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace RpgApi.Models
{
    public class Arma
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty; //Declarar valor inicial vazio
        public int Dano { get; set; }
    }
}