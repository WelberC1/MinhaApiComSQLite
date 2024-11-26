using Microsoft.AspNetCore.Mvc;
using MinhaApiComSQLite.Models;
using MinhaApiComSQLite.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinhaApiComSQLite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    //códigos de resposta http implementados
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutosController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        // GET: api/Produtos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetAllProdutos()
        {
            try
            {
                var produtos = await _produtoRepository.GetAllProdutos();
                return Ok(produtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // POST: api/Produtos
        [HttpPost]
        public async Task<ActionResult<Produto>> AddProduto([FromBody] Produto produto)
        {
            try
            {
                var novoProduto = await _produtoRepository.AddProduto(produto);
                return CreatedAtAction(nameof(AddProduto), novoProduto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message }); // Erro de validação
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message }); // Erro no servidor
            }
        }

        // PUT: api/Produtos/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduto([FromRoute] int id, [FromBody] Produto produto)
        {
            try
            {
                var produtoAtualizado = await _produtoRepository.UpdateProduto(id, produto);

                if (produtoAtualizado == null)
                {
                    return NotFound(new { error = "Produto não encontrado." });
                }

                return Ok(produtoAtualizado); 
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message }); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }


        // DELETE: api/Produtos/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduto(int id)
        {
            try
            {
                var sucesso = await _produtoRepository.DeleteProduto(id);

                if (!sucesso)
                    return NotFound(new { error = "Produto não encontrado." });

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
