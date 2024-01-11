namespace UsuarioProject.Settings
{
    /// <summary>
    /// Settings para los tokens que son enviados en los headers.
    /// </summary>
    public class TokenSettings
    {
        /// <summary>
        /// Tiempo de vida inicial del token.
        /// </summary>
        public String TiempoVidaMinutos { get; set; } = default!;
        /// <summary>
        /// Tiempo de vida extra para el token.
        /// </summary>
        public String TiempoVidaExtra { get; set; } = default!;
        /// <summary>
        /// Clave para generar tokens.
        /// </summary>
        public String jwt { get; set; } = default!;
    }
}
