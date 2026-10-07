using LojaApi.Models;
using Microsoft.AspNetCore.Mvc;
namespace LojaApi.Controllers;

[ApiController]
[Route("api/[controller]")] // -> api/produtos
public class ProdutosController : ControllerBase
{
    // "Banco" em memória: static para sobreviver entre requisições
    private static readonly List<Produto> _db = new();
    private static int _proximoId = 1;

    // GET api/produtos
    [HttpGet]
    public ActionResult<IEnumerable<Produto>> GetAll() => Ok(_db);

    // GET api/produtos/5
    [HttpGet("{id:int}")]
    public ActionResult<Produto> GetById(int id)
    {
        var produto = _db.FirstOrDefault(p => p.Id == id);
        return produto is null ? NotFound() : Ok(produto);
    }

    // POST api/produtos
    [HttpPost]
    public ActionResult<Produto> Create(Produto novo)
    {
        // Se chegou aqui, o modelo JÁ foi validado pelo [ApiController]
        novo.Id = _proximoId++;
        _db.Add(novo);
        return CreatedAtAction(nameof(GetById), new { id = novo.Id }, novo);
    }

    // PUT api/produtos/5
    [HttpPut("{id:int}")]
    public IActionResult Update(int id, Produto atualizado)
    {
        var produto = _db.FirstOrDefault(p => p.Id == id);
        if (produto is null) return NotFound();

        produto.Nome = atualizado.Nome;
        produto.Preco = atualizado.Preco;
        return NoContent();
    }

    // DELETE api/produtos/5
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var removidos = _db.RemoveAll(p => p.Id == id);
        return removidos == 0 ? NotFound() : NoContent();
    }
}