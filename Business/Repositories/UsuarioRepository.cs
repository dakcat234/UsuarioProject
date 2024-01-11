using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Business.DTO;
using Business.Entities;
using infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PopsyMarket.Objects;
using UsuarioProject.Business;

namespace Business.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly UsuarioContext _context;

        public UsuarioRepository(UsuarioContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Metodo que crea un usuario
        /// </summary>
        /// <param name="usuario">datos ingresados en el swagger</param>
        /// <returns>Retorna un objeto creado</returns>
        async Task<ObjectCreated> IUsuarioRepository.CreateUsuario(Usuario usuario)
        {
            if (!await ((IUsuarioRepository)this).ExistEmail(usuario.Correo))
                throw new InvalidOperationException("El correo electrónico ya está en uso.");

            usuario.UsuarioId = Guid.NewGuid(); 
            await _context.AddAsync(usuario);
            _context.SaveChangesAsync();

            return new ObjectCreated
            {
                Id = usuario.UsuarioId,
                CreationDate = DateTime.Now,
            };
        }
        /// <summary>
        /// Metodo que trae la lista de todos los usuarios
        /// </summary>
        /// <returns>Lista de todos los usuarios</returns>
         async Task<List<Usuario>> IUsuarioRepository.GetAllUsuario()
        {
            var allUsers = await _context.Usuarios.ToListAsync();
            return allUsers;
        }

        /// <summary>
        /// Metodo que borra un usuario por su id
        /// </summary>
        /// <param name="userId">id ingresada desde el swagger</param>
        /// <returns>Confirma que el usuario ha sido borrado</returns>
         async Task<bool> IUsuarioRepository.DeleteUsuario(Guid userId)
        {
            var deleteuser = await _context.Usuarios.FindAsync(userId);

            if (deleteuser == null)
                return false;

            _context.Usuarios.Remove(deleteuser);
            await _context.SaveChangesAsync();

            return true; 
        }

        /// <summary>
        /// Metodo que obtiene un usuario por id
        /// </summary>
        /// <param name="id">usuaior id</param>
        /// <returns>Objeto usuario por id</returns>
         async Task<Usuario?> IUsuarioRepository.GetUsuarioById(Guid usuarioid)
        {
            Usuario user = await _context.Usuarios.Where(x => x.UsuarioId.Equals(usuarioid)).FirstOrDefaultAsync();
            if (user == null)
                return null;
            return user;
        }

        /// <summary>
        /// Actualiza el usuario por su id
        /// </summary>
        /// <param name="usuario">id solicitada por swagger</param>
        /// <returns>confirma la actualización del usuario</returns>
         async Task<Boolean> IUsuarioRepository.UpdateUsuario(Usuario usuario, Guid userid)
        {
            Usuario existing = await ((IUsuarioRepository)this).GetUsuarioById(userid);

            if (existing == null)       
                return false;

            existing.FullName = usuario.FullName;
            existing.Description = usuario.Description;
            existing.Cargo = usuario.Cargo;

            _context.Entry(existing).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }

        async Task<bool> IUsuarioRepository.ExistEmail(string correo)
        {
            var email = await _context.Usuarios.Where(x => x.Correo.Equals(correo)).FirstOrDefaultAsync();

            if (email == null)
                return true;

            return false;
        }

        async Task<LoginUserRead?> IUsuarioRepository.GetUserClaimsAsync(Guid usuario_id)
      => await _context.Usuarios.Where(x => x.UsuarioId.Equals(usuario_id))
              .Select(x => new LoginUserRead
              {
                  Id = usuario_id,
                  Nombres_Apellidos = x.FullName,
                  Correo = x.Correo,
                  Token = String.Empty

              }).FirstOrDefaultAsync();

        async Task<LoginVerifyRead?> IUsuarioRepository.ValidateUserAsync(string email)
       => await _context.Usuarios.Where(x => x.Correo.ToUpper().Equals(email.ToUpper()))
               .Select(x => new LoginVerifyRead
               {
                   Usuario_id = x.UsuarioId,
                   Correo = x.Correo,
                   Password = x.Clave
               }).FirstOrDefaultAsync();

    }
}
