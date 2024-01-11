using Business.DTO;
using Business.Entities;
using Business.Repositories;
using infrastructure.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UsuarioProject.Controllers
{
    [Route("usuario/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IBusinessRepository _BusinessRepository;

        public UserController(IBusinessRepository usuarioRepository)
        {
            _BusinessRepository = usuarioRepository;
        }

        /// <summary>
        /// Metodo que crea un usuario
        /// </summary>
        /// <param name="usuario">datos ingresados en el swagger</param>
        /// <returns>Retorna un objeto creado</returns>
        [HttpPost("Create")]
        [Authorize]
        public async Task<ObjectCreated> Post([FromBody] UserDTO usuario)
        {
            var result = await _BusinessRepository.CreatedUser(usuario);

            if (result == null)
                return null;

            return result;
        }

        /// <summary>
        /// Metodo que obtiene un usuario por id
        /// </summary>
        /// <param name="id">usuaior id</param>
        /// <returns>Objeto usuario por id</returns>
        [HttpGet("GetId/{id}")]
        public async Task<ActionResult<Usuario>> Get(Guid id)
        {
            Usuario usuario = await _BusinessRepository.GetUserById(id);

            if (usuario == null)
                return NotFound();


            return Ok(usuario);
        }

        /// <summary>
        /// Metodo que borra un usuario por su id
        /// </summary>
        /// <param name="userId">id ingresada desde el swagger</param>
        /// <returns>Confirma que el usuario ha sido borrado</returns>
        [HttpDelete("Delete/{userId}")]
        public async Task<ActionResult<Usuario>> Delete(Guid userId)
        {
            var deleteusuario = await _BusinessRepository.DeleteUser(userId);

            if (deleteusuario == null)
                return NotFound();

            return Ok(deleteusuario);
        }

        /// <summary>
        /// Actualiza el usuario por su id
        /// </summary>
        /// <param name="usuario">id solicitada por swagger</param>
        /// <returns>confirma la actualización del usuario</returns>
        [HttpPut("Update/{userid}")]
        public async Task<ActionResult<bool>> Put(Guid userid, [FromBody] UserUpdateDTO usuario)
        {


            bool updatedUsuario = await _BusinessRepository.UpdateUser(usuario, userid);

            if (!updatedUsuario)
                return BadRequest();

            return Ok(updatedUsuario);
        }
    }
}
