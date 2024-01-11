using AutoMapper;
using Business.Base;
using Business.DTO.Login;
using Business.Entities;
using Business.Repositories;
using PopsyMarket;

using PopsyMarket.Objects;
using System.Security.Claims;
using UsuarioProject.Business;

namespace PopsyMarket.Business
{
    /// <summary>
    /// Clase que implementa los metodos de negeocio de <see cref="ILoginBusiness"/>
    /// </summary>
    public class LoginBusiness : AplicationBase, ILoginBusiness
    {
        /// <summary><see cref="IUsuariosRepository"/> instancia.</summary>
        private readonly IUsuarioRepository _repository;
        ///// <summary><see cref="IJwtService"/> instancia.</summary>
        private readonly IJwtService _jwtService;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="mapper"><see cref="IMapper"/> instancia.</param>
        /// <param name="repository"><see cref="IUsuariosRepository"/> instancia.</param>
        /// <param name="jwtService"><see cref="IJwtService"/> instancia.</param>
        public LoginBusiness(IMapper mapper,
             IUsuarioRepository repository,
             IJwtService jwtService) : base(mapper)
        {
            _repository = repository;
            _jwtService = jwtService;
        }

        #region Usuario
        async Task<LoginUserRead> ILoginBusiness.LoginAsync(LoginDTO loginSignIn)
        {

            if (await _repository.ValidateUserAsync(loginSignIn.correo) is LoginVerifyRead userVerify)
            {
                if (_jwtService.VerifyPassword(loginSignIn.clave, userVerify.Password))
                {
                    if (await _repository.GetUserClaimsAsync(userVerify.Usuario_id) is LoginUserRead userClaims)
                    {
                        List<Claim> claims = new List<Claim>
                        {
                            new Claim("id", userClaims.Id.ToString()),
                            new Claim("nombres_apellidos", userClaims.Nombres_Apellidos),
                            new Claim("correo", userClaims.Correo.ToLower()),

                        };
                        foreach (string rol in userClaims.Roles)
                        {
                            claims.Add(new Claim("roles", rol));
                        }
                      
                        userClaims.Token = _jwtService.BuildToken(claims);
                     
                        return userClaims;
                    }
                }
            }
            throw new InvalidOperationException("No se inicio Sesión.");

        }




        #endregion
    }
}
