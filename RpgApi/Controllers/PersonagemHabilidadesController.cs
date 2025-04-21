using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RpgApi.Data;
using RpgApi.Models;
using RpgApi.Models.Enuns;

namespace RpgApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonagensHabilidadesController : ControllerBase
    {
        //Codificação geral dentro do corpo da controller.
        private readonly DataContext _context;

        public PersonagensHabilidadesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("GetHabilidadesDoPersonagem/{id}")]
        public async Task<IActionResult> GetHabilidadesDoPersonagem(int id)
        {
            try{

                // Ou apenas var h = await...
                List<PersonagemHabilidade> ph = await _context.TB_PERSONAGENS_HABILIDADES
                    .Include(h => h.Habilidade)
                    .Where(p => p.PersonagemId == id)
                    .ToListAsync();

                return Ok(ph);

            } catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetHabilidades")]
        public async Task<IActionResult> GetHabilidades()
        {
            try{
                List<Habilidade> listaHabilidades = await _context.TB_HABILIDADES.ToListAsync();

                return Ok(listaHabilidades);

            } catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddPersonagemHabilidadedAsync(PersonagemHabilidade novoPersonagemHabilidade)
        {
            try
            {
                Personagem personagem = await _context.TB_PERSONAGENS
                    .Include(p => p.Arma)
                    .Include(p => p.PersonagemHabilidades).ThenInclude(ps => ps.Habilidade)
                    .FirstOrDefaultAsync(p => p.Id == novoPersonagemHabilidade.PersonagemId);
                
                if (personagem == null)
                    throw new System.Exception("Personagem não encontardo para o Id informado.");

                Habilidade habilidade = await _context.TB_HABILIDADES
                                    .FirstOrDefaultAsync(h => h.Id == novoPersonagemHabilidade.HabilidadeId);

                if (habilidade == null)
                    throw new System.Exception("Habilidade não encontrada.");

                PersonagemHabilidade ph = new PersonagemHabilidade();
                ph.Personagem = personagem;
                ph.Habilidade = habilidade;
                await _context.TB_PERSONAGENS_HABILIDADES.AddAsync(ph);
                int linhaAfetadas = await _context.SaveChangesAsync();

                return Ok (linhaAfetadas);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("DeletePersonagemHabilidade")]
        public async Task<IActionResult> DeletePersonagemHabilidade(PersonagemHabilidade ph)
        {
            try{
                var personagem = await _context.TB_PERSONAGENS
                    .FirstOrDefaultAsync(p => p.Id == ph.PersonagemId);

                if (personagem == null){
                    return NotFound("O Id fornecido não está vinculado a um Personagem");
                }

                var habilidade = await _context.TB_HABILIDADES
                    .FirstOrDefaultAsync(h => h.Id == ph.HabilidadeId);

                if (habilidade == null){
                    return NotFound("O Id fornecido não está vinculado a uma Habilidade.");
                }

                var phRemover = await _context.TB_PERSONAGENS_HABILIDADES
                    .FirstOrDefaultAsync(p => p.PersonagemId == ph.PersonagemId && p.HabilidadeId == ph.HabilidadeId);
                
                if (phRemover == null){
                    return NotFound("O Personagem e a Habilidade fornecidos não estão vinculados entre si.");
                }

                 _context.TB_PERSONAGENS_HABILIDADES.Remove(phRemover);
                await _context.SaveChangesAsync();

                return Ok("Habilidade removida com sucesso do Personagem.");

            } catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}