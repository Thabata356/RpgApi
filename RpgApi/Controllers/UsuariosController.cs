using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using RpgApi.Data;
using RpgApi.Models;
using RpgApi.Utils;

namespace RpgApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly DataContext _context;

        public UsuariosController(DataContext context){
            _context = context;
        }

        private async Task<bool> UsuarioExistente(string username){
            if(await _context.TB_USUARIOS.AnyAsync(x => x.Username.ToLower() == username.ToLower())){
                return true;
            }
            return false;
        }

        [HttpPost("Registrar")]
        public async Task<IActionResult> RegistraUsuario(Usuario user){
            try
            {
                if(await UsuarioExistente(user.Username)){
                    throw new System.Exception("Nome de usuário já existe");
                }

                Criptografia.CriarPasswordHash(user.PasswordString, out byte[] hash, out byte[] salt);
                user.PasswordString = string.Empty;
                user.PasswordHash = hash;
                user.PasswordSalt = salt;
                await _context.TB_USUARIOS.AddAsync(user);
                await _context.SaveChangesAsync();

                return Ok(user.Id);
            }
            catch(System.Exception ex){
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Autenticar")]
        public async Task<IActionResult> AutenticarUsuario(Usuario credenciais){
            try{
                Usuario? usuario = await _context.TB_USUARIOS
                    .FirstOrDefaultAsync(x => x.Username.ToLower().Equals(credenciais.Username.ToLower()));

                if(usuario == null){
                    throw new System.Exception("Usuário não encontrado.");
                }
                else if(!Criptografia.VerificarPasswordHash(credenciais.PasswordString, usuario.PasswordHash, usuario.PasswordSalt)){
                    throw new System.Exception("Senha incorreta.");
                }
                else{

                    //var date = DateTime.UtcNow();
                    //var date = DateTime.Now;
                    //Console.WriteLine(date);
                    //usuario.DataAcesso = date;
                    usuario.DataAcesso = DateTime.Now;
                    _context.TB_USUARIOS.Update(usuario);
                    await _context.SaveChangesAsync();

                    usuario.PasswordHash = null; // Remoção do Hash/Salt para não transitar no retorno da requisição
                    usuario.PasswordSalt = null;

                    return Ok(usuario);
                }
            }
            catch(System.Exception ex){
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("AlterarSenha")]
        public async Task<IActionResult> AlterarSenha(Usuario credenciais){
            
            try
            {
            Usuario? usuario = await _context.TB_USUARIOS // Busca usuário no BD por login
                .FirstOrDefaultAsync(u => u.Username.ToLower().Equals(credenciais.Username.ToLower()));
                // Ou simplesmente u => u.Username.ToLower() == credenciais.Username.ToLower()

            if(usuario == null){
                throw new System.Exception("Usuário não encontrado.");
            }
                Criptografia.CriarPasswordHash(credenciais.PasswordString, out byte[] hash, out byte[] salt); 
                //usuario.PasswordString = string.Empty;
                usuario.PasswordHash = hash; // Caso o usuário exista, executa a crptografia
                usuario.PasswordSalt = salt; // Guarda a Hash e Salt nas propriedades do usuário

                _context.TB_USUARIOS.Update(usuario);
                int linhasAfetadas = await _context.SaveChangesAsync(); // Confirma alteração no BD

                return Ok("Senha alterada" + linhasAfetadas);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(){

            try
            {
                List<Usuario> lista = await _context.TB_USUARIOS.ToListAsync();
                return Ok(lista);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id){

            try
            {
                Usuario? usuario = await _context.TB_USUARIOS
                        .FirstOrDefaultAsync(u => u.Id == id);

                if(usuario == null){
                        throw new System.Exception("Usuário não encontrado.");
                    }
                
                return Ok(usuario);
            }

            catch(System.Exception ex)
            {
                    return BadRequest(ex.Message);
            }
        }

        [HttpGet("{usuarioId}")] 
        public async Task<IActionResult> GetUsuario(int usuarioId) 
        { 
            try 
            { 
                //List exigirá o using System.Collections.Generic 
                Usuario usuario = await _context.TB_USUARIOS //Busca o usuário no banco através do Id 
                   .FirstOrDefaultAsync(x => x.Id == usuarioId); 
 
                return Ok(usuario); 
            } 
            catch (System.Exception ex) 
            { 
                return BadRequest(ex.Message); 
            } 
        }

        [HttpGet("GetByLogin/{login}")] 
        public async Task<IActionResult> GetUsuario(string login) 
        { 
            try 
            { 
                //List exigirá o using System.Collections.Generic 
                Usuario usuario = await _context.TB_USUARIOS //Busca o usuário no banco através do login 
                   .FirstOrDefaultAsync(x => x.Username.ToLower() == login.ToLower()); 
 
                return Ok(usuario); 
            } 
            catch (System.Exception ex) 
            { 
                return BadRequest(ex.Message); 
            } 
        }

         //Método para alteração da geolocalização 
        [HttpPut("AtualizarLocalizacao")] 
        public async Task<IActionResult> AtualizarLocalizacao(Usuario u) 
        { 
            try 
            { 
                Usuario usuario = await _context.TB_USUARIOS //Busca o usuário no banco através do Id 
                   .FirstOrDefaultAsync(x => x.Id == u.Id); 
 
                usuario.Latitude = u.Latitude; 
                usuario.Longitude = u.Longitude; 
 
                var attach = _context.Attach(usuario); 
                attach.Property(x => x.Id).IsModified = false; 
                attach.Property(x => x.Latitude).IsModified = true; 
                attach.Property(x => x.Longitude).IsModified = true; 
 
                int linhasAfetadas = await _context.SaveChangesAsync(); //Confirma a alteração no banco 
                return Ok(linhasAfetadas); //Retorna as linhas afetadas (Geralmente sempre 1 linha msm) 
            } 
            catch (System.Exception ex) 
            { 
                return BadRequest(ex.Message); 
            } 
        }

        [HttpPut("AtualizarEmail")] 
        public async Task<IActionResult> AtualizarEmail(Usuario u) 
        { 
            try 
            { 
                Usuario usuario = await _context.TB_USUARIOS //Busca o usuário no banco através do Id 
                   .FirstOrDefaultAsync(x => x.Id == u.Id); 
 
                usuario.Email = u.Email;                 
 
                var attach = _context.Attach(usuario); 
                attach.Property(x => x.Id).IsModified = false; 
                attach.Property(x => x.Email).IsModified = true;                 
 
                int linhasAfetadas = await _context.SaveChangesAsync(); //Confirma a alteração no banco 
                return Ok(linhasAfetadas); //Retorna as linhas afetadas (Geralmente sempre 1 linha msm) 
            } 
            catch (System.Exception ex) 
            { 
                return BadRequest(ex.Message); 
            } 
        }

         //Método para alteração da foto 
        [HttpPut("AtualizarFoto")] 
        public async Task<IActionResult> AtualizarFoto(Usuario u) 
        { 
            try 
            { 
                Usuario usuario = await _context.TB_USUARIOS  
                   .FirstOrDefaultAsync(x => x.Id == u.Id); 
 
                usuario.Foto = u.Foto;                 
 
                var attach = _context.Attach(usuario); 
                attach.Property(x => x.Id).IsModified = false; 
                attach.Property(x => x.Foto).IsModified = true;                 
 
                int linhasAfetadas = await _context.SaveChangesAsync();  
                return Ok(linhasAfetadas);  
            } 
            catch (System.Exception ex) 
            { 
                return BadRequest(ex.Message); 
            } 
        }
    }
}