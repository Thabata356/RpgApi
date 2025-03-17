using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RpgApi.Models;
using RpgApi.Models.Enuns;

//Luiza Gonçalves e Thabata Vitoria

namespace RpgApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonagensExercicioController : ControllerBase
    {
        private static List<Personagem> personagens = new List<Personagem>()
        {
            //Colar os objetos da lista do chat aqui
            new Personagem() { Id = 1, Nome = "Frodo", PontosVida=100, Forca=17, Defesa=23, Inteligencia=33, Classe=ClasseEnum.Cavaleiro},
            new Personagem() { Id = 2, Nome = "Sam", PontosVida=100, Forca=15, Defesa=25, Inteligencia=30, Classe=ClasseEnum.Cavaleiro},
            new Personagem() { Id = 3, Nome = "Galadriel", PontosVida=100, Forca=18, Defesa=21, Inteligencia=35, Classe=ClasseEnum.Clerigo },
            new Personagem() { Id = 4, Nome = "Gandalf", PontosVida=100, Forca=18, Defesa=18, Inteligencia=37, Classe=ClasseEnum.Mago },
            new Personagem() { Id = 5, Nome = "Hobbit", PontosVida=100, Forca=20, Defesa=17, Inteligencia=31, Classe=ClasseEnum.Cavaleiro },
            new Personagem() { Id = 6, Nome = "Celeborn", PontosVida=100, Forca=21, Defesa=13, Inteligencia=34, Classe=ClasseEnum.Clerigo },
            new Personagem() { Id = 7, Nome = "Radagast", PontosVida=100, Forca=25, Defesa=11, Inteligencia=35, Classe=ClasseEnum.Mago }
        };

        
        [HttpGet("GetByNome/{nome}")]
        public IActionResult GetByNome(string nome)
        {
            List<Personagem> listaBusca = personagens.FindAll(p => p.Nome.Contains(nome));

            if (listaBusca.Count == 0){
                return NotFound("Nenhum personagem com esse nome foi encontrado.");
            } else{
                return Ok(listaBusca);
            }
        }

        [HttpGet("GetClerigoMago")]
        public IActionResult GetClerigoMago()
        {
            List<Personagem> listaBusca = personagens.FindAll(p => p.Classe != ClasseEnum.Cavaleiro);
            List<Personagem> listaFinal = listaBusca.OrderByDescending(p => p.PontosVida).ToList();
            return Ok(listaFinal);
        }

        [HttpGet("GetEstatisticas")]
        public IActionResult GetEstatisticas()
        {
            return Ok($"Quantidade de personagens: {personagens.Count}\n" + 
            $"Soma da inteligência dos personagens: {personagens.Sum(p => p.Inteligencia)}");             
        }

        [HttpPost("PostValidacao")]
        public IActionResult PostValidacao(Personagem novoPersonagem)
        {
            if(novoPersonagem.Defesa < 10 && novoPersonagem.Inteligencia > 30){
                return BadRequest("O personagem não pode ter defesa menor que 10 ou inteligência maior que 30.");
            }
            else if(novoPersonagem.Defesa < 10){
                return BadRequest("O personagem não pode ter defesa menor que 10.");
            }
            else if(novoPersonagem.Inteligencia > 30){
                return BadRequest("O personagem não pode ter inteligência maior que 30.");
            }

            personagens.Add(novoPersonagem);
            return Ok(personagens);
        }

        [HttpPost("PostValidacaoMago")]
        public IActionResult PostValidacaoMago(Personagem novoPersonagem)
        {
            if(novoPersonagem.Classe == ClasseEnum.Mago && novoPersonagem.Inteligencia < 35)
            {
                return BadRequest("Magos não podem ter inteligencia menor que 35.");
            }

            personagens.Add(novoPersonagem);
            return Ok(personagens);
        }

        [HttpGet("GetByClasse/{classe}")]
        public IActionResult GetByClasse(int classe)
        {
            //ClasseEnum tipoEnum = (ClassEnum)classe -> Força a variável a ser um Enum
            //List<Personagem> listaFinal = personagens.FindAll(p => p.Classe == tipoEnum)

            List<Personagem> listaFinal = personagens.Where(p => (int)p.Classe == classe).ToList();

            return Ok(listaFinal);
        }
    }
}