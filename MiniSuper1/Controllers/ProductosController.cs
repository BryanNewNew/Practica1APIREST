using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MiniSuper1.Dtos;
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

        public IActionResult Create([FromBody] ProductosCreateDto dto/*Se crea un dto para luego mapear a la entidad*/)
        {
            //Las verificaciones siempre van primero
             if(dto == null)//Se debe validar que el dto no sea nulo, ya que si es nulo no se puede mapear a la entidad
            //La variable producto es una variable local que se crea para mapear el dto a la entidad,
            //ya que el dto no tiene el mismo nombre de las propiedades que la entidad
            {
                return BadRequest("Los datos son obligatorios");
            }
            
            //Pasamos los datos del Dto a la entidad
            var producto = new ProductoEntity{
                Id = dto.Id,
                Nombre = dto.Nombre,
                Precio = dto.Precio,
                Stock = dto.Stock
            };//Se crea una variable local para mapear el dto a la entidad,
            //Siempre es obligatorio mapear la entidad
             _productos.Add(producto);

            return CreatedAtAction(nameof(GetAll)/*Esto solo es para indicar la acción que se debe llamar en la url */,
             new { id = producto.Id }/*Y este objeto contiene los parámetros para la acción, solo se lo indica a la url*/,
            producto/*Devuelve el producto creado*/);

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

        [HttpDelete("{id}")]

        public IActionResult Delete(int id)
        {
            var productoAnterior = _productos.FirstOrDefault(p=> p.Id == id);
            if(productoAnterior == null)
            {
                return NotFound();//Esto es para indicar que no se encontró el producto con el id especificado
            }
            _productos.Remove(productoAnterior);
            return NoContent(); //Esto es para indicar que la solicitud se ha procesado correctamente pero no hay contenido que devolver
        }
        
    }
}