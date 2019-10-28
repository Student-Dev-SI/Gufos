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
    public class EventoController : ControllerBase
    {
        //Estamos distanciando
        GufosContext _contexto = new GufosContext();

        //GET: api/Evento
        [HttpGet]
        public async Task<ActionResult<List<Evento>>> Get()
        {
            //ToListAsync() = É um método de framework que esta embutido Select*From
            //Ele e uma tarefa, pois estamos pegando as informações dentro do Molder
            var evento = await _contexto.Evento.ToListAsync();
                
            //condicional Simples. Um retorno vazio

            if( evento == null){
                return
                 NotFound();
            }

            return evento;
        }
        //GET: api/Evento/2
        [HttpGet("{id}")]
        public async Task<ActionResult<Evento>> Get(int id)
        {
            //FindAsync = procura algo especifico no banco
            var eventos = await _contexto.Evento.FindAsync(id);
                
            if( eventos == null){
                return NotFound();
            }

            return eventos;
        }
        //Post api/Evento
        // Post= Enviar
        [HttpPost]

        //Objeto:Evento var:categoria
        public async Task<ActionResult<Evento>> Post( Evento evento )
        {
            try{
                //Tratamos contra ataques de SQL injection
                await _contexto.AddAsync(evento);
                //parametros, para não sofre nenhum tipo de ataques
                //Salvamos efetiamente o nosso objeto no banco de dados
                await _contexto.SaveChangesAsync();
            } catch(DbUpdateConcurrencyException){
                throw;
            }
            return evento;
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put( int id, Evento evento)
        {
            //SE o Id objeto não existir ele retorna erro 401
            if( id != evento.Idevento){
                return BadRequest();
            }
            //Comparamos os atribuitos  que foram modificados através do EF
            _contexto.Entry(evento).State = EntityState.Modified;

            try{
                await _contexto.SaveChangesAsync();
            }catch(DbUpdateConcurrencyException){
                
                //Verificamos se o objeto  inserido realmente existe  no banco
                var evento_valido = await _contexto.Evento.FindAsync(id);

                if(evento_valido == null){
                    return NotFound();
                }else{
                    throw;
                }
            }
            //Nocontent = return 204, sem nada
            return NoContent();
        }
        // Delete api/Evento/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<Evento>> Delete(int id){

            var evento = await _contexto.Evento.FindAsync(id);
            if(evento == null){
                return NotFound();
            }

            //Removendo defitivamente
            _contexto.Evento.Remove(evento);
            await _contexto.SaveChangesAsync();

            return evento;
        }
    }
}