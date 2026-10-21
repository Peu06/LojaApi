using LojaApi.Data;
using LojaApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LojaApi.Controllers;

[ApiController]
[Route("api/[controller]")] // -> api/produtos
public class ProdutosController : ControllerBase
{
    // O DbContext chega pelo construtor (injeção de dependência)
    private readonly LojaDbContext _context;
    public ProdutosController(LojaDbContext context) => _context = context;

    // GET api/produtos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Produto>>> GetAll()
        => Ok(await _context.Produtos.ToListAsync());

    // GET api/produtos/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Produto>> GetById(int id)
    {
        var produto = await _context.Produtos.FindAsync(id);
        return produto is null ? NotFound() : Ok(produto);
    }

    // POST api/produtos
    [HttpPost]
    public async Task<ActionResult<Produto>> Create(Produto novo)
    {
        // Se chegou aqui, o modelo JÁ foi validado pelo [ApiController]
        novo.Id = 0; // o banco gera o Id; ignora o que o cliente mandou
        _context.Produtos.Add(novo);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = novo.Id }, novo);
    }

    // PUT api/produtos/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, Produto atualizado)
    {
        var produto = await _context.Produtos.FindAsync(id);
        if (produto is null) return NotFound();

        produto.Nome = atualizado.Nome;
        produto.Preco = atualizado.Preco;
        await _context.SaveChangesAsync(); // estado Modified -> UPDATE
        return NoContent();
    }

    // DELETE api/produtos/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var produto = await _context.Produtos.FindAsync(id);
        if (produto is null) return NotFound();

        _context.Produtos.Remove(produto);
        await _context.SaveChangesAsync(); // estado Deleted -> DELETE
        return NoContent();
    }
}