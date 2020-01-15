using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Models;

namespace ToDoApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly TodoContext _context;

        public ProductsController(TodoContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns the available products
        /// </summary>
        /// <returns></returns>
        ///  /// <response code="200">If product found, Returns the product</response>
        /// 
        // GET: api/Products
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Products>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        /// <summary>
        /// Returns the product from the specified URI
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">If product found, Returns the product</response>
        /// <response code="404">If product not found</response>         
        /// 
        // GET: api/Products/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Products>> GetProducts(int id)
        {
            var products = await _context.Products.FindAsync(id);

            if (products == null)
            {
                return NotFound();
            }

            return products;
        }

        /// <summary>
        /// Update or creates product at specified URI
        /// </summary>
        /// <param name="id"></param>
        /// <param name="products"></param>
        /// <returns></returns>
        /// 
        /// <response code="200">If product found, Returns the product</response>
        /// <response code="404">If product not found</response>         
        /// 
        // PUT: api/Products/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutProducts(int id, Products products)
        {
            if (id != products.Id)
            {
                return BadRequest();
            }

            _context.Entry(products).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Adds the products and provides new URI
        /// </summary>
        /// /// <summary>
        /// Creates a TodoItem.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        /// POST api/Products
        /// {
        ///    "Id": 1,
        ///    "ProductCode": 001,
        ///    "Name": "Lavender Heart",
        ///    "Price": 19.99
        ///  }
        ///
        /// </remarks>
        /// <param name="products"></param>
        /// <returns></returns>
        /// <response code="201">Returns the newly created product</response>
        /// <response code="400">If the item is null</response>            
        /// <response code="500">If the duplicate product code exists</response>            
        /// 
        // POST: api/Products

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Products>> PostProducts(Products products)
        {
            _context.Products.Add(products);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProducts", new { id = products.Id }, products);
        }

        /// <summary>
        /// Deletes the product from the URI
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">If product found, Returns the product</response>
        /// <response code="404">If product not found</response>         
        /// 
        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Products>> DeleteProducts(int id)
        {
            var products = await _context.Products.FindAsync(id);
            if (products == null)
            {
                return NotFound();
            }

            _context.Products.Remove(products);
            await _context.SaveChangesAsync();

            return products;
        }

        private bool ProductsExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
