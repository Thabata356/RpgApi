using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace RpgApi.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty; //Declarar valor inicial vazio
        public byte[]? PasswordHash { get; set; } // ? --> Indica que o dado pode ser nulo
        public byte[]? PasswordSalt { get; set; }
        public byte[]? Foto { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public DateTime? DataAcesso { get; set; }  // using System;

        //NotMapped --> Especifica que a propriedade bão vai gerar tabela no Banco
        [NotMapped] // using System.ComponentModel.DataAnnotations.Schema

        public string PasswordString { get; set; } = string.Empty;
        // A lista torna possível que o Usuário possua vários personagens
        public List<Personagem> Personagens { get; set; } = new List<Personagem>(); // using System.Collections.Generic;
        public string? Perfil { get; set; }
        public string? Email { get; set; } = string.Empty;

    }
}