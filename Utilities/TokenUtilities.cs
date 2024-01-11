using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using UsuarioProject.Settings;

namespace PopsyMarket.Utilities
{
    /// <summary>
    /// Clase que maneja las utilidades de los tokens.
    /// </summary>
    public static class TokenUtilities
    {
        /// <summary>
        /// Metodo que extiende la vida de un token.
        /// </summary>
        /// <param name="controller"><see cref="ControllerBase"/> instancia.</param>
        /// <param name="settings"><see cref="TokenSettings"/> objeto.</param>
        public static void ExtenderTokenHeader(this ControllerBase controller, TokenSettings settings)
        {
            try
            {
                String? token = controller.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                if (!String.IsNullOrEmpty(token))
                {
                    JwtSecurityToken jwtToken = new JwtSecurityToken(token);
                    DateTime expirationDate = jwtToken.ValidTo;
                    Int32 tiempoDeVidaExtra;
                    if (!Int32.TryParse(settings.TiempoVidaExtra, out tiempoDeVidaExtra))
                        tiempoDeVidaExtra = 5;
                    DateTime newExpirationDate = expirationDate.AddMinutes(tiempoDeVidaExtra);
                    Double newExpirationDateUnixEpoch = newExpirationDate.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                    jwtToken.Payload["exp"] = newExpirationDateUnixEpoch;
                    String updatedToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
                    controller.Response.Headers.Add("Authorization", "Bearer " + updatedToken);
                }
            }
            catch (Exception) { }
        }
    }
}
