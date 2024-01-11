using Business.Entities;

namespace UsuarioProject.Services
{
    public interface IUsuarioService
    {
        Task<List<Usuario>> GetAllUsuario();
        Task<Usuario> UpdateUsuario(Usuario usuario);
        Task<bool> DeleteUsuario(int usuarioid);
        Task<Usuario> GetUsuarioById(int usuarioid);
        Task<Usuario> CreateUsuario(Usuario usuario);
    }
}
