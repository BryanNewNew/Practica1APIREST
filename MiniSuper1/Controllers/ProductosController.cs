using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MiniSuper1.Entities;

namespace MiniSuper1.Controllers
{
    [Route("api/productos")]
        [ApiController]

    public class ProductosController : ControllerBase
    {
        private static List<ProductoEntity> _productos = new()
        {
            new ProductoEntity { Id = 000001, Nombre = "Leche", Precio = 2.50m, Stock = 100},
            new ProductoEntity { Id = 000002, Nombre = "Pan", Precio = 1.20m, Stock = 50}
        };

        [HttpGet]

        public IActionResult GetAll()
        {
            return Ok(_productos);
        }

        [HttpGet("{id}")]

        public IActionResult GetbyId(int id)
        {
            var producto = _productos.FirstOrDefault(p=> p.Id == id);
            if(producto == null)
            {
                return NotFound();
            }

            return Ok(producto);
        }

        [HttpPost]

        public IActionResult Create([FromBody] ProductoEntity producto/*Esta es una variable local*/)
        {
            if(producto == null)
            {
                return BadRequest();
            }
            
            _productos.Add(producto);

            return CreatedAtAction(nameof(GetAll), new { id = producto.Id }, producto);

        }

        [HttpPut("{id}")]

        public IActionResult Update(int id, [FromBody] ProductoEntity producto)
        {
            var productoAnterior = _productos.FirstOrDefault(p=> p.Id == id);
            if(productoAnterior == null)
            {
                return NotFound();//Esto es para indicar que no se encontró el producto con el id especificado
            }
            if(producto == null)
            {
                return BadRequest(); //Esto es para indicar que la solicitud no es válida ya que no existe un producto con ese id
            }
            _productos.Remove(productoAnterior);
            _productos.Add(producto);
            //return NoContent(); Esto es para indicar que la solicitud se ha procesado correctamente pero no hay contenido que devolver
            return Ok(producto);

        }
    }

}