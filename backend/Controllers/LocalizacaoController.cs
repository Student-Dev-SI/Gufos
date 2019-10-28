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
    public class LocalizacaoController : ControllerBase
    {
        //Estamos distanciando
        GufosContext _contexto = new GufosContext();

        //GET: api/Localizacao
        [HttpGet]
        public async Task<ActionResult<List<Localizacao>>> Get()
        {
            //ToListAsync() = É um método de framework que esta embutido Select*From
            //Ele e uma tarefa, pois estamos pegando as informações dentro do Molder
            var localizacao = await _contexto.Localizacao.ToListAsync();
                
            //condicional Simples. Um retorno vazio

            if( localizacao == null){
                return
                 NotFound();
            }

            return localizacao;
        }
        //GET: api/Localizacao/2
        [HttpGet("{id}")]
        public async Task<ActionResult<Localizacao>> Get(int id)
        {
            //FindAsync = procura algo especifico no banco
            var localizacoes = await _contexto.Localizacao.FindAsync(id);
                
            if( localizacoes == null){
                return NotFound();
            }

            return localizacoes;
        }
        //Post api/Localizacao
        // Post= Enviar
        [HttpPost]

        //Objeto:Localizacao var:categoria
        public async Task<ActionResult<Localizacao>> Post( Localizacao localizacao )
        {
            try{
                //Tratamos contra ataques de SQL injection
                await _contexto.AddAsync(localizacao);
                //parametros, para não sofre nenhum tipo de ataques
                //Salvamos efetiamente o nosso objeto no banco de dados
                await _contexto.SaveChangesAsync();
            } catch(DbUpdateConcurrencyException){
                throw;
            }
            return localizacao;
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put( int id, Localizacao localizacao)
        {
            //SE o Id objeto não existir ele retorna erro 401
            if( id != localizacao.Idlocalizacao){
                return BadRequest();
            }
            //Comparamos os atribuitos  que foram modificados através do EF
            _contexto.Entry(localizacao).State = EntityState.Modified;

            try{
                await _contexto.SaveChangesAsync();
            }catch(DbUpdateConcurrencyException){
                
                //Verificamos se o objeto  inserido realmente existe  no banco
                var localizacao_valido = await _contexto.Localizacao.FindAsync(id);

                if(localizacao_valido == null){
                    return NotFound();
                }else{
                    throw;
                }
            }
            //Nocontent = return 204, sem nada
            return NoContent();
        }
        // Delete api/Localizacao/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<Localizacao>> Delete(int id){

            var localizacao = await _contexto.Localizacao.FindAsync(id);
            if(localizacao == null){
                return NotFound();
            }

            //Removendo defitivamente
            _contexto.Localizacao.Remove(localizacao);
            await _contexto.SaveChangesAsync();

            return localizacao;
        }
    }
}