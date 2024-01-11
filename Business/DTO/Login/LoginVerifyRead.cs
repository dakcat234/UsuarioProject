namespace UsuarioProject.Business
{
    public class LoginVerifyRead
    {
        public Guid Usuario_id { get; set; }
        public String Correo { get; set; } = default!;
        public String Password { get; set; } = default!;
    }
}
