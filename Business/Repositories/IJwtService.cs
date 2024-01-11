using infrastructure.Enum;
using System.Security.Claims;

namespace Business.Repositories
{
    public interface IJwtService
    {

        /// <summary>
        /// Construye un token.
        /// </summary>
        /// <param name="claims">Lista de claims a añadir.</param>
        /// <returns>Bearer token.</returns>
        String BuildToken(List<Claim> claims);
        /// <summary>
        /// Construye un token.
        /// </summary>
        /// <param name="password">Contraseña configurada.</param>
        /// <param name="userType">Tipo de usuario.</param>
        /// <returns>Bearer token.</returns>
        String BuildToken(String password, UserType userType);
        /// <summary>
        /// Cifra una contraseña.
        /// </summary>
        /// <param name="password">Contraseña.</param>
        /// <returns>Contraseña cifrada.</returns>
        String HashPassword(String password);
        /// <summary>
        /// Compara una contraseña con otra cifrada para verificar si son iguales.
        /// </summary>
        /// <param name="password">Contraseña.</param>
        /// <param name="passwordEncrypted">Contraseña encriptada.</param>
        /// <returns>Verdadero si son iguales, de otra manera retorna falso.</returns>
        Boolean VerifyPassword(String password, String passwordEncrypted);
        /// <summary>
        /// Construye una contraseña random de x números.
        /// </summary>
        /// <param name="limit">Límite de longitud.</param>
        /// <returns>Contraseña random.</returns>
        String GetRandomPassword(Int32 limit);
        /// <summary>
        /// Extiende la vida de un token.
        /// </summary>
        /// <param name="token">Token actual.</param>
        /// <returns>Token nuevo.</returns>
        String ExtendToken(String token);
    }
}
