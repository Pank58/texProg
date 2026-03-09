using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magazine.Core.Models
{
    public class Product
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Definition { get; set; }

 
        public decimal Price { get; set; }

        //хранить лучше не картинку а путь к ней
        public string? Image { get; set; }
    }
}
