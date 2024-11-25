using Microsoft.AspNetCore.Mvc;
using MinhaApiComSQLite.Data;
using MinhaApiComSQLite.Models;
using Microsoft.EntityFrameworkCore;

namespace MinhaApiComSQLite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Produtos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
        {
            return await _context.Produtos.ToListAsync();
        }
    }
}
