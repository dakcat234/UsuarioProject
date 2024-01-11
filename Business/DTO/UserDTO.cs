using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTO
{
    public class UserDTO
    {
        public string FullName { get; set; } = default!;
        public string Description { get; set;} = default!;
        public string Cargo { get; set; } = default!;
        public String Correo { get; set; } = default!;
        public String Clave { get; set; } = default!;   

    }
}
