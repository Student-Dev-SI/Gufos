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
    public class TipoUsuarioController : ControllerBase
    {
        //Estamos distanciando
        GufosContext _contexto = new GufosContext();

        //GET: api/TipoUsuario
        [HttpGet]
        public async Task<ActionResult<List<TipoUsuario>>> Get()
        {
            //ToListAsync() = É um método de framework que esta embutido Select*From
            //Ele e uma tarefa, pois estamos pegando as informações dentro do Molder
            var tipousuario = await _contexto.TipoUsuario.ToListAsync();
                
            //condicional Simples. Um retorno vazio

            if( tipousuario == null){
                return
                 NotFound();
            }

            return tipousuario;
        }
        //GET: api/TipoUsuario/2
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoUsuario>> Get(int id)
        {
            //FindAsync = procura algo especifico no banco
            var tipousuarios = await _contexto.TipoUsuario.FindAsync(id);
                
            if( tipousuarios == null){
                return NotFound();
            }

            return tipousuarios;
        }
        //Post api/TipoUsuario
        // Post= Enviar
        [HttpPost]

        //Objeto:TipoUsuario var:categoria
        public async Task<ActionResult<TipoUsuario>> Post( TipoUsuario tipousuario )
        {
            try{
                //Tratamos contra ataques de SQL injection
                await _contexto.AddAsync(tipousuario);
                //parametros, para não sofre nenhum tipo de ataques
                //Salvamos efetiamente o nosso objeto no banco de dados
                await _contexto.SaveChangesAsync();
            } catch(DbUpdateConcurrencyException){
                throw;
            }
            return tipousuario;
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put( int id, TipoUsuario tipousuario)
        {
            //SE o Id objeto não existir ele retorna erro 401
            if( id != tipousuario.IdtipoUsuario){
                return BadRequest();
            }
            //Comparamos os atribuitos  que foram modificados através do EF
            _contexto.Entry(tipousuario).State = EntityState.Modified;

            try{
                await _contexto.SaveChangesAsync();
            }catch(DbUpdateConcurrencyException){
                
                //Verificamos se o objeto  inserido realmente existe  no banco
                var tipousuario_valido = await _contexto.TipoUsuario.FindAsync(id);

                if(tipousuario_valido == null){
                    return NotFound();
                }else{
                    throw;
                }
            }
            //Nocontent = return 204, sem nada
            return NoContent();
        }
        // Delete api/TipoUsuario/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<TipoUsuario>> Delete(int id){

            var tipousuario = await _contexto.TipoUsuario.FindAsync(id);
            if(tipousuario == null){
                return NotFound();
            }

            //Removendo defitivamente
            _contexto.TipoUsuario.Remove(tipousuario);
            await _contexto.SaveChangesAsync();

            return tipousuario;
        }
    }
}