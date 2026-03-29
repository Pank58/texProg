using System;
using System.Threading.Tasks;
using Magazine.Core.Models;
using Magazine.Core.Services;
using Microsoft.EntityFrameworkCore;
using Magazine.WebApi.Data;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Text.Json;
using System.Collections.Generic; 

namespace Magazine.WebApi.Services
{

    public class ProductService : IProductService
    {
        private readonly string _filePath;
        //private readonly ApplicationDbContext _context;
        private Dictionary<Guid, Product> _products;
        private readonly Mutex _mutex = new Mutex();

        public ProductService(IConfiguration config)
        {
           
            _filePath = config["DataBaseFilePath"]!;

            InitFromFile();
        }

        private void InitFromFile()
        {
            if (File.Exists(_filePath))
            {
                var text = File.ReadAllText(_filePath);

                if (!string.IsNullOrWhiteSpace(text))
                {
                    _products = JsonSerializer.Deserialize<Dictionary<Guid, Product>>(text)!;
                }
                else
                {
                    _products = new Dictionary<Guid, Product>();
                }
            }
            else
            {
                _products = new Dictionary<Guid, Product>();
            }
        }

        private void WriteToFile()
        {
            _mutex.WaitOne();

            try
            {
                var text = JsonSerializer.Serialize(_products);
                File.WriteAllText(_filePath, text);
            }
            finally
            {
                _mutex.ReleaseMutex();
            }
        }

        public Task<Product> AddAsync(Product product)
        {
            
            if (product.Id == Guid.Empty)
            {
                product.Id = Guid.NewGuid();
            }

            _products[product.Id] = product;
            WriteToFile();

            return Task.FromResult(product);
        }

        public Task<Product> EditAsync(Product product)
        {
            if (_products.ContainsKey(product.Id))
            {
                _products[product.Id] = product;

                WriteToFile();
            }
            else
            {
                throw new Exception($"Не удалось обновить: Товар с ID {product.Id} не найден.");
            }

            return Task.FromResult(product);
        }
        public Task<Product> RemoveAsync(Guid id)
        {
            _products.TryGetValue(id, out var product);

            if (product != null)
            {
                _products.Remove(id);
                WriteToFile();
            }

            return Task.FromResult(product!);
        }
        public Task<Product?> SearchAsync(Guid id)
        {
            _products.TryGetValue(id, out var product);
            return Task.FromResult(product);
        }
    }
}
