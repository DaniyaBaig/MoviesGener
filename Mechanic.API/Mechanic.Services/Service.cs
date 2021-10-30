using AutoMapper;
using Mechanic.Contracts.Services;

namespace Mechanic.Services
{
    public class Service : IService
    {
        protected readonly IMapper Mapper;
        public Service(IMapper mapper)
        {
            Mapper = mapper;
        }
    }
}
