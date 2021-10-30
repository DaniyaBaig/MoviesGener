using Mechanic.Models.Mechanic;
using Mechanic.ViewModels.Mechanic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mechanic.Contracts.Services
{
    public interface IMechanicService
    {
       // Task<IEnumerable<MechanicTypeViewModel>> GetAllMechanicType();
        Task<IEnumerable<GenresViewModel>> GetAllGenres();
        Task<GenresViewModel> AddGenres(Genres model);
        Task<GenresViewModel>  UpdateGenres(Genres model);
        Task<GenresViewModel> DeleteGenres(Genres model);
        Task<IEnumerable<MoviesViewModel>> GetAllMovies();
        Task<MoviesViewModel> AddMovies(Movies model);
        Task<MoviesViewModel> UpdateMovies(Movies model);
        Task<MoviesViewModel> DeleteMovies(Movies model);
    }
}
