using Microsoft.AspNetCore.Mvc;
using ProductApi.Data;
using ProductApi.Models;

namespace ProductApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }


    [HttpGet]
    public IActionResult GetAll()
    {
        var products = _context.Products.ToList();
       return Ok(products);
    }

    [HttpGet("{id}")]
public IActionResult GetById(int id)
{
    var product = _context.Products.Find(id);

    if (product == null)
        return NotFound();

    return Ok(product);
}

    [HttpPost]
public IActionResult Create(Product product)
{
    _context.Products.Add(product);
    _context.SaveChanges();

    return CreatedAtAction(
        nameof(GetById),
        new { id = product.Id },
        product
    );
}


        [HttpPut("{id}")]
public IActionResult Update(int id, Product product)
{
    var existing = _context.Products.Find(id);

    if (existing == null)
        return NotFound();

    existing.Name = product.Name;
    existing.Price = product.Price;

    _context.SaveChanges();

    return Ok(existing);
}

        [HttpDelete("{id}")]
public IActionResult Delete(int id)
{
    var product = _context.Products.Find(id);

    if (product == null)
        return NotFound();

    _context.Products.Remove(product);
    _context.SaveChanges();

    return Ok();
}

    }
}