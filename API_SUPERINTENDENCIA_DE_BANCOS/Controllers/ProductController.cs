using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_SUPERINTENDENCIA_DE_BANCOS.Controllers
{
    public class ProductController : Controller
    {

        private static List<products> _Products = new List<products>
        {
            new products {  productsId = 1, NombreProducto = "Carro" },
            new products { productsId = 2, NombreProducto = "Apartamento"},
            new products { productsId = 3, NombreProducto = "Silla de playa" },
        };


        [HttpGet]
        public IActionResult Get()
        {
            var Pd = _Products.ToList();
            return Ok(Pd);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _Products.FirstOrDefault(p => p.productsId == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public IActionResult Create([FromBody] products product)
        {
            product.productsId = _Products.Count + 1;
            _Products.Add(product);

            return CreatedAtAction(nameof(GetById), new { id = product.productsId }, product);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] products product)
        {
            var existingProduct = _Products.FirstOrDefault(p => p.productsId == id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            existingProduct.NombreProducto = product.NombreProducto;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _Products.FirstOrDefault(p => p.productsId == id);
            if (product == null)
            {
                return NotFound();
            }
            _Products.Remove(product);
            return NoContent();
        }





    }
}
