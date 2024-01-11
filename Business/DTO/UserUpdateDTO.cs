using Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTO
{
    public class UserUpdateDTO
    {
        public string FullName { get; set; }
        public string Description { get; set; }
        public string Cargo { get; set; }
    }
}
