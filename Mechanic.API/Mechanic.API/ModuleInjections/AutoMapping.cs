using AutoMapper;
using Mechanic.Models.Mechanic;
using Mechanic.ViewModels.Mechanic;

namespace Mechanic.API.ModuleInjections
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<MechanicType, MechanicTypeViewModel>();
            CreateMap<Genres, GenresViewModel>();
            CreateMap<Movies, MoviesViewModel>();
        }
    }
}
