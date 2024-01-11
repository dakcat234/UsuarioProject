namespace PopsyMarket.Objects
{
    public class LoginUserRead
    {
        public Guid Id { get; set; }
        public String Nombres_Apellidos { get; set; } = default!;
        public String Correo { get; set; } = default!;
        public List<String> Roles { get; set; } = new List<String>();
        public String Token { get; set; } = default!;
        public Int16 tipoUsuario { get; set; } = default!;
        public String identificacion { get; set; } = default!;

    }
}
