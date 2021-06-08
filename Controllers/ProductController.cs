using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;

namespace Shop.Controllers
{
    [Route("products")]
    public class ProductController :ControllerBase
    {
        [HttpGet]
        [Route("")]
        [AllowAnonymous]
       public async Task<ActionResult<List<Produt>>> Get([FromServices]DataContext context)
        {
            var products = await context.Products.Include(x => x.Category).AsNoTracking().ToListAsync(); 
            return Ok(products);
        }

        [HttpGet] //products/categories/1
        [Route("{id:int}")]
         [AllowAnonymous]
        public async Task<ActionResult<Produt>> GetById([FromServices]DataContext context, int id)
        {
            var product = await context
                .Products
                .Include(x => x.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
                
            return product;
        } 
        [HttpGet] //products/categories/1
        [Route("categories/{id:int}")]
        [Authorize(Roles = "employee")]
        public async Task<ActionResult<List<Produt>>> GetByIdCategories([FromServices]DataContext context, int id)
        {
            var products = await context
                .Products
                .Include(x => x.Category)
                .AsNoTracking()
                .Where(x => x.CategoryId ==id)
                .ToListAsync();
                
            return products;
        } 

        [HttpPost]
        [Route("")]
        [Authorize(Roles = "employee")]
        public async Task<ActionResult<Produt>> Post([FromServices]DataContext context,
                                                      [FromBody]Produt model)
        {
            if(ModelState.IsValid)
            {
                context.Products.Add(model);
                await context.SaveChangesAsync();
                return Ok(model);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}