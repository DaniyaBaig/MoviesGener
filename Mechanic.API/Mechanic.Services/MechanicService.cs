using AutoMapper;
using Mechanic.Contracts.Repository;
using Mechanic.Contracts.Services;
using Mechanic.Models.Mechanic;
using Mechanic.ViewModels.Mechanic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mechanic.Services
{
    public class MechanicService : Service, IMechanicService
    {

        private readonly IMechanicRepository _mechanicRepository;
        

        public MechanicService(IMechanicRepository mechanicRepository, IMapper mapper) : base(mapper)
        {
            _mechanicRepository = mechanicRepository;
        }
        public async Task<IEnumerable<MechanicTypeViewModel>> GetAllMechanicType()
        {
            var result = await _mechanicRepository.GetAllMechanicType();
            return Mapper.Map<IEnumerable<MechanicTypeViewModel>>(result);
        }

        public async Task<IEnumerable<GenresViewModel>> GetAllGenres()
        {
            var result = await _mechanicRepository.GetGenres();
            return Mapper.Map<IEnumerable<GenresViewModel>>(result);
        }

        public async Task<GenresViewModel> AddGenres(Genres model)
        {

            // var account = new CreateGenres(id: model.Id, name: model.Name, description: model.Description );
            // var account = new AddGenres(Genres model);
            model.ID = await _mechanicRepository.AddGenres(model);
            return new GenresViewModel(model.ID);
        }
        public async Task<GenresViewModel> UpdateGenres(Genres model)
        {
            model.ID = await _mechanicRepository.UpdateGenres(model);
            return new GenresViewModel(model.ID);
        }
        public async Task<GenresViewModel> DeleteGenres(Genres model)
        {
            model.ID = await _mechanicRepository.DeleteGenres(model);
            return new GenresViewModel(model.ID);
        }

        public async Task<IEnumerable<MoviesViewModel>> GetAllMovies()
        {
            var result = await _mechanicRepository.GetMovies();
            return Mapper.Map<IEnumerable<MoviesViewModel>>(result);
        }
        public async Task<MoviesViewModel> AddMovies(Movies model)
        {

            // var account = new CreateGenres(id: model.Id, name: model.Name, description: model.Description );
            // var account = new AddGenres(Genres model);
            model.ID = await _mechanicRepository.AddMovies(model);
            return new MoviesViewModel(model.ID);
        }
        public async Task<MoviesViewModel> UpdateMovies(Movies model)
        {
            model.ID = await _mechanicRepository.UpdateMovies(model);
            return new MoviesViewModel(model.ID);
        }
        public async Task<MoviesViewModel> DeleteMovies(Movies model)
        {
            model.ID = await _mechanicRepository.DeleteMovies(model);
            return new MoviesViewModel(model.ID);
        }

    }
}
