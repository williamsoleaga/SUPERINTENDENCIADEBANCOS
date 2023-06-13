using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_SUPERINTENDENCIA_DE_BANCOS.Controllers
{
    public class UsersController : Controller
    {

         private static List<User> _users = new List<User>
        {
            new User { Id = 1, Nombre = "Juan Perez", Correo = "Perez@example.com", Perfil="Admin" },
            new User { Id = 2, Nombre = "Manuel Batista", Correo = "Batista@example.com", Perfil="Super Admin" },
            new User { Id = 3, Nombre = "Maria Rosario", Correo = "Rosario@example.com", Perfil="Contador" },
        };


        [HttpGet]
        public IActionResult Get()
        {
            var users = _users.ToList();
            return Ok(users);
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public IActionResult Create([FromBody] User user)
        {
            user.Id = _users.Count + 1;
            _users.Add(user);

            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] User user)
        {
            var existingUser = _users.FirstOrDefault(u => u.Id == id);
            if (existingUser == null)
            {
                return NotFound();
            }

            existingUser.Nombre = user.Nombre;
            existingUser.Correo = user.Correo;
            existingUser.Perfil = user.Perfil;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            _users.Remove(user);
            return NoContent();
        }


    }
}
