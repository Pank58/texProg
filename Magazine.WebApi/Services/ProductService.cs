using System;
using System.Threading.Tasks;
using Magazine.Core.Models;
using Magazine.Core.Services;
using Microsoft.EntityFrameworkCore;
using Magazine.WebApi.Data;

namespace Magazine.WebApi.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Product> AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> EditAsync(Product product)
        {

            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }
        public async Task<Product> RemoveAsync(Guid id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            return product!;
        }
        public async Task<Product?> SearchAsync(Guid id)
        {
            return await _context.Products.FindAsync(id);
        }
    }
}
