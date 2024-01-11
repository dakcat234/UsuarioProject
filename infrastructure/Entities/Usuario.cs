using infrastructure.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace Business.Entities
{
    public class Usuario : TblCreationBaseEntity
    {
        [Key]
        public Guid UsuarioId { get; set; }
        public String FullName { get; set; }
        public String? Description { get; set; }
        public String Cargo { get; set; }
        public String Correo { get; set; } = default!;
        public String Clave { get; set; } = default!;
    }
}
