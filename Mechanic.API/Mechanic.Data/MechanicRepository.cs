using Dapper;
using Mechanic.Contracts.Repository;
using Mechanic.Data.Helpers;
using Mechanic.Models.Mechanic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Mechanic.Data
{
    public class MechanicRepository : BaseRepository, IMechanicRepository
    {
        public MechanicRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {

        }
        public async Task<IEnumerable<MechanicType>> GetAllMechanicType()
        {
            DynamicParameters dynamicParam = new DynamicParameters();
            var result = await Get(async x => await x.QueryAsync<MechanicType>
            (
                "GetAllMechanicType",
                param: dynamicParam,
                commandType: System.Data.CommandType.StoredProcedure
            ));
            return result;
        }

        public async Task<IEnumerable<Genres>> GetGenres()
        {
            DynamicParameters dynamicParam = new DynamicParameters();
            var result = await Get(async x => await x.QueryAsync<Genres>
            (
                "GetAllGenres",
                param: dynamicParam,
                commandType: System.Data.CommandType.StoredProcedure
            ));
            return result;
        }
        
        public async Task<int> AddGenres(Genres account)
        {
            var dynamicParam = SetQueryParameters(account);

            await Get(async x => await x.QueryAsync<int>
            (
                "Genres_Add",
                dynamicParam,
                commandType: CommandType.StoredProcedure
            ));

            return dynamicParam.Get<int>("@GenresID");
        }

        private DynamicParameters SetQueryParameters(Genres account)
        {
            var dynamicParam = new DynamicParameters();
           

            dynamicParam.Add("ID", account.ID);
            dynamicParam.Add("NAME", account.NAME);
            dynamicParam.Add("DESCRIPTION", account.DESCRIPTION);
            dynamicParam.Add("CREATED_BY", account.CREATED_BY);
            dynamicParam.Add("CREATED_DATE", account.CREATED_DATE);
            dynamicParam.Add("MODIFIED_BY", account.MODIFIED_BY);
            dynamicParam.Add("MODIFIED_DATE", account.MODIFIED_DATE);
            dynamicParam.Add("DELETED", account.DELETED);
            return dynamicParam;
        }
        public async Task<int> UpdateGenres(Genres account)
        {
            var dynamicParam = SetQueryParameters(account);

            await Get(async x => await x.QueryAsync<int>
            (
                "Genres_Update",
                dynamicParam,
                commandType: CommandType.StoredProcedure
            ));

            return dynamicParam.Get<int>("@GenresID");
        }     
        public async Task<int> DeleteGenres(Genres account)
        {
            var dynamicParam = SetQueryParameters(account);

            await Get(async x => await x.QueryAsync<int>
            (
                "Genres_Delete",
                dynamicParam,
                commandType: CommandType.StoredProcedure
            ));

            return dynamicParam.Get<int>("@GenresID");
        }
        public async Task<IEnumerable<Genres>> GetMovies()
        {
            DynamicParameters dynamicParam = new DynamicParameters();
            var result = await Get(async x => await x.QueryAsync<Genres>
            (
                "GetAllMovies",
                param: dynamicParam,
                commandType: System.Data.CommandType.StoredProcedure
            ));
            return result;
        }
        public async Task<int> AddMovies(Movies account)
        {
            var dynamicParam = SetMoviesQueryParameters(account);

            await Get(async x => await x.QueryAsync<int>
            (
                "Movies_Add",
                dynamicParam,
                commandType: CommandType.StoredProcedure
            ));

            return dynamicParam.Get<int>("@MoviesID");
        }

        private DynamicParameters SetMoviesQueryParameters(Movies account)
        {
            var dynamicParam = new DynamicParameters();


            dynamicParam.Add("ID", account.ID);
            dynamicParam.Add("GENER_ID", account.GENER_ID);
            dynamicParam.Add("MOVIE_NAME", account.MOVIE_NAME);
            dynamicParam.Add("MOVIE_DESCRIPTION", account.MOVIE_DESCRIPTION);
            dynamicParam.Add("RELEASE_DATE", account.RELEASE_DATE);
            dynamicParam.Add("GENRES_ASSOCIATED", account.GENRES_ASSOCIATED);
            dynamicParam.Add("DURATION", account.DURATION);
            dynamicParam.Add("RATING", account.RATING);
            dynamicParam.Add("CREATED_BY", account.CREATED_BY);
            dynamicParam.Add("CREATED_DATE", account.CREATED_DATE);
            dynamicParam.Add("MODIFIED_BY", account.MODIFIED_BY);
            dynamicParam.Add("MODIFIED_DATE", account.MODIFIED_DATE);
            dynamicParam.Add("DELETED", account.DELETED);
            return dynamicParam;
        }
        public async Task<int> UpdateMovies(Movies account)
        {
            var dynamicParam = SetMoviesQueryParameters(account);

            await Get(async x => await x.QueryAsync<int>
            (
                "Movies_Update",
                dynamicParam,
                commandType: CommandType.StoredProcedure
            ));

            return dynamicParam.Get<int>("@MoviesID");
        }
        public async Task<int> DeleteMovies(Movies account)
        {
            var dynamicParam = SetMoviesQueryParameters(account);

            await Get(async x => await x.QueryAsync<int>
            (
                "Movies_Delete",
                dynamicParam,
                commandType: CommandType.StoredProcedure
            ));

            return dynamicParam.Get<int>("@MoviesID");
        }
    }
}
