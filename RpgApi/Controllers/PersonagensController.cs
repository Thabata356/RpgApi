using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RpgApi.Data;
using RpgApi.Models;
using System.Linq;
using System.Threading.Tasks;
using RpgApi.Models.Enuns;
using System.Collections.Generic;

namespace RpgApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class PersonagensController : ControllerBase
    {
        private readonly DataContext _context;

        public PersonagensController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            try
            {
                Personagem p = await _context.TB_PERSONAGENS
                    .Include(ar => ar.Arma) //Carrega a propriedade Arma do objeto p
                    .Include(u => u.Usuario) //Carrega os dados do usuário correspondente
                    .Include(ph => ph.PersonagemHabilidades)
                        .ThenInclude(h => h.Habilidade) //Carrega a lista de PersonagemHabilidade de p
                    .FirstOrDefaultAsync(pBusca => pBusca.Id == id);

                return Ok(p);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<Personagem> lista = await _context.TB_PERSONAGENS.ToListAsync();
                return Ok(lista);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(Personagem novoPersonagem)
        {
            try
            {
                if (novoPersonagem.PontosVida > 100)
                {
                    throw new Exception("Pontos de vida não pode ser maior que 100");     
                }
                await _context.TB_PERSONAGENS.AddAsync(novoPersonagem);
                await _context.SaveChangesAsync();

                return Ok(novoPersonagem.Id);
            }
            catch(System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(Personagem novoPersonagem)
        {
            try
            {
                if(novoPersonagem.PontosVida > 100)
                {
                    throw new System.Exception("Pontos de vida não pode ser maior que 100");
                }
                _context.TB_PERSONAGENS.Update(novoPersonagem);
                int linhasAfetadas = await _context.SaveChangesAsync();

                return Ok(linhasAfetadas);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Personagem pRemover = await _context.TB_PERSONAGENS.FirstOrDefaultAsync(p => p.Id == id);

                _context.TB_PERSONAGENS.Remove(pRemover);
                int linhasAfetadas = await _context.SaveChangesAsync();
                return Ok(linhasAfetadas);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("RestaurarPontosVida")] 
        public async Task<IActionResult> RestaurarPontosVidaAsync(Personagem p) 
        { 
            try 
            { 
                int linhasAfetadas = 0; 
                Personagem? pEncontrado = 
                await _context.TB_PERSONAGENS.FirstOrDefaultAsync(pBusca => pBusca.Id == p.Id); 
                pEncontrado.PontosVida = 100; 
 
                bool atualizou = await TryUpdateModelAsync<Personagem>(pEncontrado, "p", 
                    pAtualizar => pAtualizar.PontosVida); 
                // EF vai detectar e atualizar apenas as colunas que foram alteradas. 
                if (atualizou) 
                    linhasAfetadas = await _context.SaveChangesAsync(); 
 
                return Ok(linhasAfetadas); 
            } 
            catch (System.Exception ex) 
            { 
                return BadRequest(ex.Message); 
            } 
        }

        //Método para alteração da foto 
        [HttpPut("AtualizarFoto")] 
        public async Task<IActionResult> AtualizarFotoAsync(Personagem p) 
        { 
            try 
            { 
                Personagem personagem = await _context.TB_PERSONAGENS  
                   .FirstOrDefaultAsync(x => x.Id == p.Id); 
                personagem.FotoPersonagem = p.FotoPersonagem;    
                var attach = _context.Attach(personagem); 
                attach.Property(x => x.Id).IsModified = false; 
                attach.Property(x => x.FotoPersonagem).IsModified = true;    
                int linhasAfetadas = await _context.SaveChangesAsync();  
                return Ok(linhasAfetadas);  
            } 
            catch (System.Exception ex) 
            { 
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("ZerarRanking")] 
        public async Task<IActionResult> ZerarRankingAsync(Personagem p) 
        { 
            try 
            { 
                Personagem pEncontrado =  
                  await _context.TB_PERSONAGENS.FirstOrDefaultAsync(pBusca => pBusca.Id == p.Id); 
 
                pEncontrado.Disputas = 0; 
                pEncontrado.Vitorias = 0; 
                pEncontrado.Derrotas = 0; 
                int linhasAfetadas = 0; 
 
                bool atualizou = await TryUpdateModelAsync<Personagem>(pEncontrado, "p", 
                    pAtualizar => pAtualizar.Disputas, 
                    pAtualizar => pAtualizar.Vitorias, 
                    pAtualizar => pAtualizar.Derrotas); 
 
                // EF vai detectar e atualizar apenas as colunas que foram alteradas. 
                if (atualizou) 
                    linhasAfetadas = await _context.SaveChangesAsync(); 
 
                return Ok(linhasAfetadas); 
            } 
            catch (System.Exception ex) 
            { 
                return BadRequest(ex.Message); 
            } 
        }

        [HttpPut("ZerarRankingRestaurarVidas")] 
        public async Task<IActionResult> ZerarRankingRestaurarVidasAsync() 
        { 
            try 
            { 
                List<Personagem> lista = 
                await _context.TB_PERSONAGENS.ToListAsync(); 
 
                foreach (Personagem p in lista) 
                { 
                    await ZerarRankingAsync(p); 
                    await RestaurarPontosVidaAsync(p); 
                } 
                return Ok(); 
            } 
            catch (System.Exception ex) 
            { 
                return BadRequest(ex.Message); 
            } 
        }

        [HttpGet("GetByUser/{userId}")] 
        public async Task<IActionResult> GetByUserAsync(int userId) 
        { 
            try 
            { 
                List<Personagem> lista = await _context.TB_PERSONAGENS 
                            .Where(u => u.Usuario.Id == userId) 
                            .ToListAsync(); 
 
                return Ok(lista); 
            } 
            catch (System.Exception ex) 
            { 
                return BadRequest(ex.Message); 
            } 
        }

        [HttpGet("GetByPerfil/{userId}")] 
        public async Task<IActionResult> GetByPerfilAsync(int userId) 
        { 
            try 
            { 
                Usuario usuario = await _context.TB_USUARIOS 
                   .FirstOrDefaultAsync(x => x.Id == userId); 
 
                List<Personagem> lista = new List<Personagem>(); 
                if (usuario.Perfil == "Admin") 
                    lista = await _context.TB_PERSONAGENS.ToListAsync(); 
                else 
                    lista = await _context.TB_PERSONAGENS 
                            .Where(p => p.Usuario.Id == userId).ToListAsync(); 
                return Ok(lista); 
            } 
            catch (System.Exception ex) 
            { 
                return BadRequest(ex.Message); 
            } 
        }

        [HttpGet("GetByNomeAproximado/{nomePersonagem}")] 
        public async Task<IActionResult> GetByNomeAproximado(string nomePersonagem) 
        { 
            try 
            { 
                List<Personagem> lista = await _context.TB_PERSONAGENS 
                    .Where(p => p.Nome.ToLower().Contains(nomePersonagem.ToLower())) 
                    .ToListAsync(); 
 
                return Ok(lista); 
            } 
            catch (System.Exception ex) 
            { 
                return BadRequest(ex.Message); 
            } 
        }


    }
}