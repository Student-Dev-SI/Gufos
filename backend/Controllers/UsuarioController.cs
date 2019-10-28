using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    //Definimos nossra rota do controller e dizemos que é um controller de API
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        //Estamos distanciando
        GufosContext _contexto = new GufosContext();

        //GET: api/Usuario
        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> Get()
        {
            //ToListAsync() = É um método de framework que esta embutido Select*From
            //Ele e uma tarefa, pois estamos pegando as informações dentro do Molder
            var usuario = await _contexto.Usuario.ToListAsync();
                
            //condicional Simples. Um retorno vazio

            if( usuario == null){
                return
                 NotFound();
            }

            return usuario;
        }
        //GET: api/Usuario/2
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> Get(int id)
        {
            //FindAsync = procura algo especifico no banco
            var usuarios = await _contexto.Usuario.FindAsync(id);
                
            if( usuarios == null){
                return NotFound();
            }

            return usuarios;
        }
        //Post api/Usuario
        // Post= Enviar
        [HttpPost]
        //Objeto:Usuario var:categoria
        public async Task<ActionResult<Usuario>> Post( Usuario usuario )
        {
            try{
                //Tratamos contra ataques de SQL injection
                await _contexto.AddAsync(usuario);
                //parametros, para não sofre nenhum tipo de ataques
                //Salvamos efetiamente o nosso objeto no banco de dados
                await _contexto.SaveChangesAsync();
            } catch(DbUpdateConcurrencyException){
                throw;
            }
            return usuario;
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put( int id, Usuario usuario)
        {
            //SE o Id objeto não existir ele retorna erro 401
            if( id != usuario.IdtipoUsuario){
                return BadRequest();
            }
            //Comparamos os atribuitos  que foram modificados através do EF
            _contexto.Entry(usuario).State = EntityState.Modified;

            try{
                await _contexto.SaveChangesAsync();
            }catch(DbUpdateConcurrencyException){
                
                //Verificamos se o objeto  inserido realmente existe  no banco
                var usuario_valido = await _contexto.Usuario.FindAsync(id);

                if(usuario_valido == null){
                    return NotFound();
                }else{
                    throw;
                }
            }
            //Nocontent = return 204, sem nada
            return NoContent();
        }
        // Delete api/Usuario/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<Usuario>> Delete(int id){

            var usuario = await _contexto.Usuario.FindAsync(id);
            if(usuario == null){
                return NotFound();
            }
            //Removendo defitivamente
            _contexto.Usuario.Remove(usuario);
            await _contexto.SaveChangesAsync();

            return usuario;
        }
    }
}