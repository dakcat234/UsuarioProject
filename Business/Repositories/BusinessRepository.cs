using AutoMapper;
using Business.Base;
using Business.DTO;
using Business.Entities;
using Business.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories
{
    public class BusinessRepository : AplicationBase, IBusinessRepository
    {
        private readonly IUsuarioRepository _usuarioRepository;
        /// <summary><see cref="IJwtService"/> instancia.</summary>
        private readonly IJwtService _jwtService;
        public BusinessRepository(IMapper mapper, IUsuarioRepository usuarioRepository, IJwtService jwtService) : base(mapper)
        {
            _usuarioRepository = usuarioRepository;
            _jwtService = jwtService;
        }

        /// <summary>
        /// Metodo que borra un usuario por su id
        /// </summary>
        /// <param name="userId">id ingresada desde el swagger</param>
        /// <returns>Confirma que el usuario ha sido borrado</returns>
        public async Task<bool> DeleteUser(Guid userId)
        {
            Usuario deleteuser = await _usuarioRepository.GetUsuarioById(userId);

            if (deleteuser == null)
                return false;

            bool result = await _usuarioRepository.DeleteUsuario(userId);

            return result;
        }

        /// <summary>
        /// Metodo que trae la lista de todos los usuarios
        /// </summary>
        /// <returns>Lista de todos los usuarios</returns>
        public async Task<IEnumerable<Usuario>> GetAllUsers()
        {
            var allUsuario = await _usuarioRepository.GetAllUsuario();
            var mappedUsuario = _mapper.Map<IEnumerable<Usuario>>(allUsuario);

            return mappedUsuario;
        }

        /// <summary>
        /// Metodo que obtiene un usuario por id
        /// </summary>
        /// <param name="id">usuaior id</param>
        /// <returns>Objeto usuario por id</returns>
        public async Task<Usuario> GetUserById(Guid userId)
        {

            var usuario = await _usuarioRepository.GetUsuarioById(userId);

            if (usuario == null)
            {
                return null;
            }


            return usuario;
        }

        /// <summary>
        /// Metodo que crea un usuario
        /// </summary>
        /// <param name="usuario">datos ingresados en el swagger</param>
        /// <returns>Retorna un objeto creado</returns>
        async Task<ObjectCreated> IBusinessRepository.CreatedUser(UserDTO usuario)
        {
            Usuario usuariosave = base._mapper.Map<Usuario>(usuario);
            usuariosave.Clave = _jwtService.HashPassword(usuario.Clave);


            ObjectCreated result = await _usuarioRepository.CreateUsuario(usuariosave);

            return result;
        }
        /// <summary>
        /// Actualiza el usuario por su id
        /// </summary>
        /// <param name="usuario">id solicitada por swagger</param>
        /// <returns>confirma la actualización del usuario</returns>
        async Task<Boolean> IBusinessRepository.UpdateUser(UserUpdateDTO updatedUsuario, Guid userid)
        {
            Usuario updatedUser = base._mapper.Map<Usuario>(updatedUsuario);

            Boolean result = await _usuarioRepository.UpdateUsuario(updatedUser, userid);

            return result;
        }
    }
}
