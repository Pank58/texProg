using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Magazine.Core.Models;

namespace Magazine.Core.Services
{
    public interface IProductService
    {
        /// <summary>
        /// Добавление в базу данных нового элемента.
        /// </summary>
        /// <param name="product">Товар для добавления</param>
        /// <returns>Сущность, которая была создана</returns>
        Task<Product> AddAsync(Product product);

        /// <summary>
        /// Изменение элемента в базе данных.
        /// </summary>
        /// <param name="product">Товар с обновленными данными</param>
        /// <returns>Сущность, которая была изменена</returns>
        Task<Product> EditAsync(Product product);

        /// <summary>
        /// Удаляет из базы данных элемент по его ID.
        /// </summary>
        /// <param name="id">Уникальный идентификатор товара</param>
        /// <returns>Сущность, которая была удалена</returns>
        Task<Product> RemoveAsync(Guid id);

        /// <summary>
        /// Поиск элемента по ID. В случае отсутствия возвращает null.
        /// </summary>
        /// <param name="id">Идентификатор для поиска</param>
        /// <returns>Найденная сущность или null</returns>
        Task<Product?> SearchAsync(Guid id);
    }
}
