using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Base
{
    public class AplicationBase
    {
        protected readonly IMapper _mapper;

        public AplicationBase(IMapper mapper)
        {
            _mapper = mapper;
        }
    }
}
