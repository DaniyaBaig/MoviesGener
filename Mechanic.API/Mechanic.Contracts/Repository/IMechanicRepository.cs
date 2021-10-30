using Mechanic.Models.Mechanic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Mechanic.Contracts.Repository
{
    public interface IMechanicRepository
    {
        Task<IEnumerable<MechanicType>> GetAllMechanicType();

        Task<IEnumerable<Genres>> GetGenres();

        Task<int> AddGenres(Genres genres);
        Task<int> UpdateGenres(Genres genres);
        Task<int> DeleteGenres(Genres genres);
        Task<IEnumerable<Genres>> GetMovies();
        Task<int> AddMovies(Movies movies);
        Task<int> UpdateMovies(Movies movies);
        Task<int> DeleteMovies(Movies movies);

    }
}
