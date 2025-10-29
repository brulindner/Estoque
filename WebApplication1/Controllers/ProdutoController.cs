using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Estoque.Data;
using Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _env;

        public ProdutoController(AppDbContext appDbContext, IWebHostEnvironment env)
        {
            _appDbContext = appDbContext;
            _env = env;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
        {
            return await _appDbContext.Produtos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> GetProduto(int id)
        {
            var produto = await _appDbContext.Produtos.FindAsync(id);
            if (produto == null)
                return NotFound();

            return produto;
        }

        [HttpPost]
        public async Task<ActionResult<Produto>> PostProduto([FromForm] Produto produto, IFormFile? foto)
        {
            // Define o caminho da pasta wwwroot/imagens, com fallback
            var webRootPath = _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var uploads = Path.Combine(webRootPath, "imagens");
            Directory.CreateDirectory(uploads); // cria a pasta se não existir

            if (foto != null)
            {
                var fileName = $"{Guid.NewGuid()}_{foto.FileName}";
                var filePath = Path.Combine(uploads, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await foto.CopyToAsync(stream);
                }

                produto.FotoUrl = $"/imagens/{fileName}";
            }

            _appDbContext.Produtos.Add(produto);
            await _appDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduto), new { id = produto.Id }, produto);
        }


       [HttpPut("{id}")]
public async Task<IActionResult> PutProduto(int id, [FromForm] Produto produto, IFormFile? foto)
        {
    
    
    if (id != produto.Id)
        return BadRequest();

    var produtoExistente = await _appDbContext.Produtos.FindAsync(id);
    if (produtoExistente == null)
        return NotFound("Produto não encontrado");

    // Atualiza os campos
    produtoExistente.Nome = produto.Nome;
    produtoExistente.Descricao = produto.Descricao;
    produtoExistente.Quantidade = produto.Quantidade;
    produtoExistente.EstoqueMinimo = produto.EstoqueMinimo;

    // Define caminho da pasta wwwroot/imagens com fallback
    var webRootPath = _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
    var uploads = Path.Combine(webRootPath, "imagens");
    Directory.CreateDirectory(uploads); // cria se não existir

    if (foto != null)
    {
        var fileName = $"{Guid.NewGuid()}_{foto.FileName}";
        var filePath = Path.Combine(uploads, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await foto.CopyToAsync(stream);
        }

        // Remove foto antiga, se existir
        if (!string.IsNullOrEmpty(produtoExistente.FotoUrl))
        {
            var oldPath = Path.Combine(webRootPath, produtoExistente.FotoUrl.TrimStart('/'));
            if (System.IO.File.Exists(oldPath))
                System.IO.File.Delete(oldPath);
        }

        produtoExistente.FotoUrl = $"/imagens/{fileName}";
    }

    await _appDbContext.SaveChangesAsync();
    return Ok(produtoExistente);
}


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduto(int id)
        {
            var produto = await _appDbContext.Produtos.FindAsync(id);
            if (produto == null)
                return NotFound();

            _appDbContext.Produtos.Remove(produto);
            await _appDbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("{id}/entrada")]
        public async Task<IActionResult> AdicionarEstoque(int id, [FromQuery] int quantidade)
        {
            var produto = await _appDbContext.Produtos.FindAsync(id);
            if (produto == null)
                return NotFound();

            produto.Quantidade += quantidade;
            await _appDbContext.SaveChangesAsync();
            return Ok(produto);
        }

        [HttpPost("{id}/saida")]
        public async Task<IActionResult>RemoverEstoque(int id, [FromQuery] int quantidade)
        {
            var produto = await _appDbContext.Produtos.FindAsync(id);
            if (produto == null)
                return NotFound();

            if (produto.Quantidade < quantidade)
                return BadRequest("Quantidade insuficiente em estoque");

            produto.Quantidade -= quantidade;
            await _appDbContext.SaveChangesAsync();

            return Ok(produto);
        }
       
    }
}