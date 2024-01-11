using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.Entities.Base
{
    public class TblCreationBaseEntity
    {
        public DateTime fecha_creacion { get; protected set; }
        public DateTime fecha_modificacion { get; set; }
    }
}
