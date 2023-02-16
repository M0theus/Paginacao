using BuscaPaginada.Context;
using BuscaPaginada.Dto;
using BuscaPaginada.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BuscaPaginada.Controllers;

[ApiController]
[Route("api/v1/busca_paginada")]
public class ProductController : ControllerBase
{
    private readonly TesteContext _testeContext;

    public ProductController(TesteContext testeContext)
    {
        _testeContext = testeContext;
    }

    [HttpPost("Create-Product")]
    public async Task<ActionResult> Create([FromBody] Product product)
    {
        _testeContext.Products.Add(product);
        await _testeContext.SaveChangesAsync();

        return Ok(await _testeContext.Products.ToListAsync());
    }

    [HttpGet("{page}")]
    public async Task<ActionResult<List<Product>>> GetProdutcs(int page)
    {
        if (_testeContext.Products == null)
        {
            return NotFound();
        }

        var pageResults = 3f;
        var pageCount = Math.Ceiling(_testeContext.Products.Count() / pageResults);
        
        var productsForPage = await _testeContext.Products
            .Skip((page - 1) * (int)pageResults)
            .Take((int)pageResults)
            .ToListAsync();

        var response = new ProductResponseDto
        {
            Products = productsForPage,
            CurrentPage = page,
            Pages = (int)pageCount
        };
        
        return Ok(response);
    }
}