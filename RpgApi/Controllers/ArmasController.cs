using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RpgApi.Data;
using RpgApi.Models;
using System.Linq;
using System.Threading.Tasks;

namespace RpgApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ArmasController : ControllerBase
    {
        private readonly DataContext _context;

        public ArmasController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(){

            try
            {
                //using System.Collections.Generic;
                List<Arma> armas = await _context.TB_ARMAS.ToListAsync();
                return Ok(armas);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message + " - " + ex.InnerException);
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> Add(Arma novaArma)
        {
            try
            {               
                if(novaArma.Dano == 0)
                  throw new Exception("O Dano da arma não pode ser 0.");

                await _context.TB_ARMAS.AddAsync(novaArma);
                await _context.SaveChangesAsync();

                return Ok(novaArma.Id);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message + " - " + ex.InnerException);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(Arma novaArma)
        {
            try
            {
                _context.TB_ARMAS.Update(novaArma);
                int linhaAfetadas = await _context.SaveChangesAsync();

                return Ok(linhaAfetadas);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message + " - " + ex.InnerException);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Arma aRemover = await _context.TB_ARMAS.FirstOrDefaultAsync(p => p.Id == id);

                _context.TB_ARMAS.Remove(aRemover);
                int linhaAfetadas = await _context.SaveChangesAsync();

                return Ok(linhaAfetadas);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message + " - " + ex.InnerException);
            }
        }
    }
}