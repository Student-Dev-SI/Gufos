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
    public class PresencaController : ControllerBase
    {
        //Estamos distanciando
        GufosContext _contexto = new GufosContext();

        //GET: api/Presenca
        [HttpGet]
        public async Task<ActionResult<List<Presenca>>> Get()
        {
            //ToListAsync() = É um método de framework que esta embutido Select*From
            //Ele e uma tarefa, pois estamos pegando as informações dentro do Molder
            var presenca = await _contexto.Presenca.ToListAsync();
                
            //condicional Simples. Um retorno vazio

            if( presenca == null){
                return
                 NotFound();
            }

            return presenca;
        }
        //GET: api/Presenca/2
        [HttpGet("{id}")]
        public async Task<ActionResult<Presenca>> Get(int id)
        {
            //FindAsync = procura algo especifico no banco
            var presencas = await _contexto.Presenca.FindAsync(id);
                
            if( presencas == null){
                return NotFound();
            }

            return presencas;
        }
        //Post api/Presenca
        // Post= Enviar
        [HttpPost]

        //Objeto:Presenca var:categoria
        public async Task<ActionResult<Presenca>> Post( Presenca presenca )
        {
            try{
                //Tratamos contra ataques de SQL injection
                await _contexto.AddAsync(presenca);
                //parametros, para não sofre nenhum tipo de ataques
                //Salvamos efetiamente o nosso objeto no banco de dados
                await _contexto.SaveChangesAsync();
            } catch(DbUpdateConcurrencyException){
                throw;
            }
            return presenca;
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put( int id, Presenca presenca)
        {
            //SE o Id objeto não existir ele retorna erro 401
            if( id != presenca.Idpresenca){
                return BadRequest();
            }
            //Comparamos os atribuitos  que foram modificados através do EF
            _contexto.Entry(presenca).State = EntityState.Modified;

            try{
                await _contexto.SaveChangesAsync();
            }catch(DbUpdateConcurrencyException){
                
                //Verificamos se o objeto  inserido realmente existe  no banco
                var presenca_valido = await _contexto.Presenca.FindAsync(id);

                if(presenca_valido == null){
                    return NotFound();
                }else{
                    throw;
                }
            }
            //Nocontent = return 204, sem nada
            return NoContent();
        }
        // Delete api/Presenca/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<Presenca>> Delete(int id){

            var presenca = await _contexto.Presenca.FindAsync(id);
            if(presenca == null){
                return NotFound();
            }

            //Removendo defitivamente
            _contexto.Presenca.Remove(presenca);
            await _contexto.SaveChangesAsync();

            return presenca;
        }
    }
}