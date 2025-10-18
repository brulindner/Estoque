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
            if (foto != null)
            {
                var uploads = Path.Combine(_env.WebRootPath, "imagens");
                if (!Directory.Exists(uploads))
                    Directory.CreateDirectory(uploads);

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
        public async Task<IActionResult> PutProduto(int id, [FromBody] Produto produto, IFormFile? foto)
        {
            if (id != produto.Id)
                return BadRequest();

            var produtoExistente = await _appDbContext.Produtos.FindAsync(id);
            if (produtoExistente == null)
                return NotFound("Produto não encontrado");

            //Para atualizar os campos
            produtoExistente.Nome = produto.Nome;
            produtoExistente.Descricao = produto.Descricao;
            produtoExistente.Quantidade = produto.Quantidade;
            produtoExistente.EstoqueMinimo = produto.EstoqueMinimo;

            //Se uma foto já foi enviada
            if ( foto != null )
            {
                var uploads = Path.Combine(_env.WebRootPath, "imagens");
                if (!Directory.Exists(uploads))
                    Directory.CreateDirectory(uploads);

                var fileName = $"{Guid.NewGuid()}_{foto.FileName}";
                var filePath = Path.Combine(uploads, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await foto.CopyToAsync(stream);
                }

                //Se já tiver foto, pode remover se quiser
                if (!string.IsNullOrEmpty(produtoExistente.FotoUrl))
                {
                    var oldPath = Path.Combine(_env.WebRootPath, produtoExistente.FotoUrl.TrimStart('/'));
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