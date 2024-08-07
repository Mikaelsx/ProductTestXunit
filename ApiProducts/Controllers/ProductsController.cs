using Microsoft.AspNetCore.Mvc;
using ApiProducts.Interface;
using ApiProducts.Domains;
using System.Threading.Tasks;
using System.Linq;
using ApiProducts.Services;
using MongoDB.Driver;

[Route("api/products")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductsRepository _productsRepository;
    private readonly IMongoCollection<Products> _product;

    //public ProductsController(IProductsRepository productsRepository)
    public ProductsController(MongoDbServiceImplementation mongoDbServiceImplementation)
    {
        //_productsRepository = productsRepository;
        _product = mongoDbServiceImplementation.GetDatabase.GetCollection<Products>("products");
    }




    // GET: api/products
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Products>>> GetAll()
    {
        try
        {
            var products = await _productsRepository.ListarAsync();
            return Ok(products);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

    }

    // GET: api/products/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Products>> GetById(Guid id)
    {
        var product = await _productsRepository.BuscarPorIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    // POST: api/products
    [HttpPost]
    public async Task<ActionResult<Products>> Create(Products product)
    {
        await _productsRepository.CadastrarAsync(product);
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }

    // PUT: api/products/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, Products product)
    {
        if (product.Id != id)
        {
            return BadRequest();
        }

        await _productsRepository.AtualizarPorIdAsync(id, product);
        return NoContent();
    }

    // DELETE: api/products/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var product = await _productsRepository.DeletarPorIdAsync(id, new Products());
        if (product == null)
        {
            return NotFound();
        }
        return NoContent();
    }
}
