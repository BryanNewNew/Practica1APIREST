using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniSuper1.Entities
{
    public class ProductoEntity
    {
        public Guid Id { get; set; }
        public string? Nombre { get; set; }
        public decimal Precio { get; set; }

        public int Stock { get; set; }
    }
}