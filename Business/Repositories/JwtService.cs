using Business.Repositories;
using infrastructure.Enum;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using UsuarioProject.Settings;

namespace Business.Repositories
{
    /// <summary>
    /// Clase de integración que expone los métodos de <see cref="IJwtService"/>
    /// </summary>
    public class JwtService : IJwtService
    {
        private readonly byte[] _jwt;
        private readonly TokenSettings _settings;
        private static readonly string Characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private static readonly Random Random = new Random();

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="jwt">Jwt Key.</param>
        /// <param name="settings">Settings.</param>
        public JwtService(TokenSettings settings)
        {
            _settings = settings;

            // Generate a random 256-bit key
            _jwt = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(_jwt);
            }
        }

        string IJwtService.BuildToken(List<Claim> claims)
            => BuildToken(claims);

        string IJwtService.BuildToken(string password, UserType userType)
        {
            if (password.Equals(_settings.jwt))
            {
                string rolSeleccionado = string.Empty;
                switch (userType)
                {
                    case UserType.admin:
                        rolSeleccionado = "admin";
                        break;
                    default:
                        break;
                }
                List<Claim> userInformation = new List<Claim>
                {
                    new Claim("id", "A77D2BE0-EEF1-ED11-892D-34735A9C3F28"),
                    new Claim("nombres", "Gerente"),
                    new Claim("apellidos", "Canal"),
                    new Claim("correo", "GerenteCanal@usuario.com"),
                    new Claim("roles", rolSeleccionado),
                };
                return BuildToken(userInformation);
            }
            else
            {
                return "Acceso denegado.";
            }
        }

        string IJwtService.GetRandomPassword(int limit)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < limit; i++)
            {
                result.Append(Characters[Random.Next(Characters.Length)]);
            }
            return result.ToString();
        }

        string IJwtService.HashPassword(string password)
            => BCrypt.Net.BCrypt.HashPassword(password, 10);

        bool IJwtService.VerifyPassword(string password, string passwordEncrypted)
            => BCrypt.Net.BCrypt.Verify(password, passwordEncrypted);

        string IJwtService.ExtendToken(string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                JwtSecurityToken jwtToken = new JwtSecurityToken(token);
                return BuildToken(jwtToken.Claims.ToList());
            }
            else
            {
                return string.Empty;
            }
        }

        private string BuildToken(List<Claim> claims)
        {
            try
            {
                SymmetricSecurityKey key = new SymmetricSecurityKey(_jwt);
                SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                int keySizeInBits = key.KeySize;

                if (keySizeInBits != 256)
                {
                    throw new InvalidOperationException($"Invalid key size. Expected 256 bits, but got {keySizeInBits} bits.");
                }

                Int32.TryParse(_settings.TiempoVidaMinutos, out int tiempoVidaToken);
                DateTime expiration = DateTime.UtcNow.AddMinutes(tiempoVidaToken);
                DateTime issuedAt = DateTime.UtcNow;

                JwtSecurityToken securityToken = new JwtSecurityToken(
                    claims: claims,
                    issuer: null,
                    audience: null,
                    notBefore: issuedAt,
                    expires: expiration,
                    signingCredentials: credentials
                );

                return new JwtSecurityTokenHandler().WriteToken(securityToken);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
