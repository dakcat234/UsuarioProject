using Business.DTO;
using Business.Entities;
using PopsyMarket.Objects;
using UsuarioProject.Business;

namespace Business.Repositories
{
    public interface IUsuarioRepository
    {
        Task<List<Usuario>> GetAllUsuario();
        Task<Boolean> UpdateUsuario(Usuario usuario, Guid userid);
        Task<Boolean> DeleteUsuario(Guid usuarioid);
        Task<Boolean> ExistEmail(String correo);
        Task<Usuario> GetUsuarioById(Guid usuarioid);
        Task<ObjectCreated> CreateUsuario(Usuario usuario);
        /// <summary>
        /// Devuelve los claims de un usuario validado.
        /// </summary>
        /// <param name="usuario_id">Usuario id.</param>
        /// <returns><see cref="LoginUserRead"/> objeto.</returns>
        Task<LoginUserRead?> GetUserClaimsAsync(Guid usuario_id);
        /// <summary>
        /// Valida si existe un usuario con ese correo.
        /// </summary>
        /// <param name="email">Email.</param>
        /// <returns>Verdadero si existe, de otra manera retorna falso.</returns>
        Task<LoginVerifyRead?> ValidateUserAsync(String email);
    }
}
