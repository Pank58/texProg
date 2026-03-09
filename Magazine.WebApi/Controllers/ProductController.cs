using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc; 
using Magazine.Core.Models;
using Magazine.Core.Services;

namespace Magazine.WebApi.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    // ссылка на сервис
    private readonly IProductService _productService;

    // Конструктор
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    // поиск
    [HttpGet("{id}")]
    public async Task<IActionResult> Search(Guid id)
    {
        try 
        {
            var product = await _productService.SearchAsync(id);
            if (product == null)
            {
                return NotFound($"Товар с ID {id} не найден."); // Возвращаем ошибку 404
            }
            return Ok(product);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ошибка при поиске товара: {ex.Message}");
        }
    }

    // добавление
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] Product product)
    {
        try
        {
            var createdProduct = await _productService.AddAsync(product);

            return Ok(createdProduct); 
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ошибка при добавлении товара: {ex.Message}");
        }
    }

    // изменение
    [HttpPut]
    public async Task<IActionResult> Edit([FromBody] Product product)
    {
        try
        {
            var updatedProduct = await _productService.EditAsync(product);
            return Ok(updatedProduct);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ошибка при обновлении товара: {ex.Message}");
        }
    }

    // удаление
    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(Guid id)
    {
        try
        {
            var deletedProduct = await _productService.RemoveAsync(id);

            if (deletedProduct == null)
            {
                return NotFound($"Товар с ID {id} для удаления не найден.");
            }

            return Ok(deletedProduct); 
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ошибка при удалении товара: {ex.Message}");
        }
    }
}