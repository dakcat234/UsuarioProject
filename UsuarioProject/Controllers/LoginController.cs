using Business.DTO;
using Business.DTO.Login;
using Business.Entities;
using Business.Repositories;
using infrastructure.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PopsyMarket.Objects;
using System.Threading;
using UsuarioProject.Business;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UsuarioProject.Controllers
{
    [Route("usuario/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IBusinessRepository _BusinessRepository;
        private readonly ILoginBusiness _loginBusiness;

        public LoginController(IBusinessRepository usuarioRepository,ILoginBusiness loginBusiness)
        {
            _BusinessRepository = usuarioRepository;
            _loginBusiness = loginBusiness;
        }


        [HttpPost("")]
        //[PermisosAttribute("NOVALIDAR")]
        public async Task<ActionResult<LoginUserRead>> LoginAsync([FromBody] LoginDTO loginSignIn)
        {
            var result = await _loginBusiness.LoginAsync(loginSignIn);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }
        /// <summary>
        /// Metodo que trae la lista de todos los usuarios
        /// </summary>
        /// <returns>Lista de todos los usuarios</returns>

    }
}
