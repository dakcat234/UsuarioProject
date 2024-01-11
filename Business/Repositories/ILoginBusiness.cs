using Business.DTO.Login;
using PopsyMarket.Objects;

namespace UsuarioProject.Business
{
    public interface ILoginBusiness
    {
        /// <summary>
        /// Genera un token por usuario.
        /// </summary>
        /// <param name="loginSignIn">Credenciales.</param>
        /// <returns><see cref="LoginUserRead"/> objeto.</returns>
        Task<LoginUserRead> LoginAsync(LoginDTO loginSignIn);
        /// <summary>
        /// Extiende la vida de un token.
        /// </summary>
        /// <param name="token">Token actual.</param>
        /// <returns>Token nuevo.</returns>
        //RefreshTokenObject ExtendToken(String token);
        ///// <summary>
        ///// Construye un token.
        ///// </summary>
        ///// <param name="password">Contraseña configurada.</param>
        ///// <param name="userType">Tipo de usuario.</param>
        ///// <returns>Bearer token.</returns>
        //String BuildToken(String password, UserType userType);
        ///// <summary>
        ///// Crea un registro de cierre de sesion.
        ///// </summary>
        ///// <param name="password">Contraseña configurada.</param>
        ///// <param name="userType">Tipo de usuario.</param>
        ///// <returns>Bearer token.</returns>
        //Task LogOut(Guid User_Id);
    }
}
