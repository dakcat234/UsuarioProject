using Business.DTO;
using Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories
{
    public interface IBusinessRepository
    {
        /// <summary>
        /// Metodo que crea un usuario
        /// </summary>
        /// <param name="usuario">datos ingresados en el swagger</param>
        /// <returns>Retorna un objeto creado</returns>
        Task<ObjectCreated> CreatedUser(UserDTO usuario);
        Task<IEnumerable<Usuario>> GetAllUsers();
        Task<Usuario> GetUserById(Guid userId);
        Task<bool> UpdateUser(UserUpdateDTO updatedUser, Guid userid);
        Task<bool> DeleteUser(Guid userId);
    }
}
