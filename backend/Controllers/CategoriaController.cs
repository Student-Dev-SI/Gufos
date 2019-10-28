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
    public class CategoriaController : ControllerBase
    {
        //Estamos distanciando
        GufosContext _contexto = new GufosContext();

        //GET: api/Categoria
        [HttpGet]
        public async Task<ActionResult<List<Categoria>>> Get()
        {
            //ToListAsync() = É um método de framework que esta embutido Select*From
            //Ele e uma tarefa, pois estamos pegando as informações dentro do Molder
            var categorias = await _contexto.Categoria.ToListAsync();
                
            //condicional Simples. Um retorno vazio

            if( categorias == null){
                return
                 NotFound();
            }

            return categorias;
        }
        //GET: api/Categoria/2
        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> Get(int id)
        {
            //FindAsync = procura algo especifico no banco
            var categoria = await _contexto.Categoria.FindAsync(id);
                
            if( categoria == null){
                return NotFound();
            }

            return categoria;
        }
        //Post api/Categoria
        // Post= Enviar/ cadastrar
        [HttpPost]

        //Objeto:Categoria var:categoria
        public async Task<ActionResult<Categoria>> Post( Categoria categoria )
        {
            try{
                //Tratamos contra ataques de SQL injection
                await _contexto.AddAsync(categoria);
                //parametros, para não sofre nenhum tipo de ataques
                //Salvamos efetiamente o nosso objeto no banco de dados
                await _contexto.SaveChangesAsync();
            } catch(DbUpdateConcurrencyException){
                throw;
            }
            return categoria;
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put( int id, Categoria categoria)
        {
            //SE o Id objeto não existir ele retorna erro 401
            if( id != categoria.Idcategoria){
                return BadRequest();
            }
            //Comparamos os atribuitos  que foram modificados através do EF
            _contexto.Entry(categoria).State = EntityState.Modified;

            try{
                await _contexto.SaveChangesAsync();
            }catch(DbUpdateConcurrencyException){
                
                //Verificamos se o objeto  inserido realmente existe  no banco
                var categoria_valido = await _contexto.Categoria.FindAsync(id);

                if(categoria_valido == null){
                    return NotFound();
                }else{
                    throw;
                }
            }
            //Nocontent = return 204, sem nada
            return NoContent();
        }
        // Delete api/Categoria/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<Categoria>> Delete(int id){

            var categoria = await _contexto.Categoria.FindAsync(id);
            if(categoria == null){
                return NotFound();
            }

            //Removendo defitivamente
            _contexto.Categoria.Remove(categoria);
            await _contexto.SaveChangesAsync();

            return categoria;
        }
    }
}