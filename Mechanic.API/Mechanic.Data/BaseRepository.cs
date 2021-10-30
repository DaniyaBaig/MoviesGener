using Dapper;
using Mechanic.Contracts.Repository;
using Mechanic.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Mechanic.Data
{
    public class BaseRepository : IRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public BaseRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        protected void Execute(Action<IDbConnection> query)
        {
            using IDbConnection db = _connectionFactory.GetConnection();
            query.Invoke(db);
        }

        protected async Task<T> Get<T>(Func<IDbConnection, Task<T>> query)
        {
            using IDbConnection db = _connectionFactory.GetConnection();
            return await query.Invoke(db);
        }

        protected async Task<Tuple<IEnumerable<T1>, IEnumerable<T2>>> GetMultiple<T1, T2>(string sql,
                                        object parameters,
                                        Func<GridReader, IEnumerable<T1>> func1,
                                        Func<GridReader, IEnumerable<T2>> func2)
        {
            var objs = await GetMultipleResults(sql, parameters, func1, func2);
            return Tuple.Create(objs[0] as IEnumerable<T1>, objs[1] as IEnumerable<T2>);
        }

        protected async Task<Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>>> GetMultiple<T1, T2, T3>(string sql,
                                        object parameters,
                                        Func<GridReader, IEnumerable<T1>> func1,
                                        Func<GridReader, IEnumerable<T2>> func2,
                                        Func<GridReader, IEnumerable<T3>> func3)
        {
            var objs = await GetMultipleResults(sql, parameters, func1, func2, func3);
            return Tuple.Create(objs[0] as IEnumerable<T1>, objs[1] as IEnumerable<T2>, objs[2] as IEnumerable<T3>);
        }

        protected async Task<Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>>> GetMultiple<T1, T2, T3, T4>(string sql,
                                        object parameters,
                                        Func<GridReader, IEnumerable<T1>> func1,
                                        Func<GridReader, IEnumerable<T2>> func2,
                                        Func<GridReader, IEnumerable<T3>> func3,
                                        Func<GridReader, IEnumerable<T4>> func4)
        {
            var objs = await GetMultipleResults(sql, parameters, func1, func2, func3, func4);
            return Tuple.Create(objs[0] as IEnumerable<T1>, objs[1] as IEnumerable<T2>, objs[2] as IEnumerable<T3>, objs[3] as IEnumerable<T4>);
        }

        protected async Task<Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>>> GetMultiple<T1, T2, T3, T4, T5>(string sql,
                                        object parameters,
                                        Func<GridReader, IEnumerable<T1>> func1,
                                        Func<GridReader, IEnumerable<T2>> func2,
                                        Func<GridReader, IEnumerable<T3>> func3,
                                        Func<GridReader, IEnumerable<T4>> func4,
                                        Func<GridReader, IEnumerable<T5>> func5)
        {
            var objs = await GetMultipleResults(sql, parameters, func1, func2, func3, func4, func5);
            return Tuple.Create(objs[0] as IEnumerable<T1>, objs[1] as IEnumerable<T2>, objs[2] as IEnumerable<T3>, objs[3] as IEnumerable<T4>, objs[4] as IEnumerable<T5>);
        }

        protected async Task<Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>>> GetMultiple<T1, T2, T3, T4, T5, T6>(string sql,
                                        object parameters,
                                        Func<GridReader, IEnumerable<T1>> func1,
                                        Func<GridReader, IEnumerable<T2>> func2,
                                        Func<GridReader, IEnumerable<T3>> func3,
                                        Func<GridReader, IEnumerable<T4>> func4,
                                        Func<GridReader, IEnumerable<T5>> func5,
                                        Func<GridReader, IEnumerable<T6>> func6)
        {
            var objs = await GetMultipleResults(sql, parameters, func1, func2, func3, func4, func5, func6);
            return Tuple.Create(objs[0] as IEnumerable<T1>, objs[1] as IEnumerable<T2>, objs[2] as IEnumerable<T3>, objs[3] as IEnumerable<T4>, objs[4] as IEnumerable<T5>, objs[5] as IEnumerable<T6>);
        }

        protected async Task<Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>>> GetMultiple<T1, T2, T3, T4, T5, T6, T7>(string sql,
                                        object parameters,
                                        Func<GridReader, IEnumerable<T1>> func1,
                                        Func<GridReader, IEnumerable<T2>> func2,
                                        Func<GridReader, IEnumerable<T3>> func3,
                                        Func<GridReader, IEnumerable<T4>> func4,
                                        Func<GridReader, IEnumerable<T5>> func5,
                                        Func<GridReader, IEnumerable<T6>> func6,
                                        Func<GridReader, IEnumerable<T7>> func7)
        {
            var objs = await GetMultipleResults(sql, parameters, func1, func2, func3, func4, func5, func6, func7);
            return Tuple.Create(objs[0] as IEnumerable<T1>, objs[1] as IEnumerable<T2>, objs[2] as IEnumerable<T3>, objs[3] as IEnumerable<T4>, objs[4] as IEnumerable<T5>, objs[5] as IEnumerable<T6>, objs[6] as IEnumerable<T7>);
        }





        protected async Task<Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, Tuple<IEnumerable<T7>, IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>, IEnumerable<T11>, IEnumerable<T12>>>> GetMultiple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string sql,
                                     object parameters,
                                     Func<GridReader, IEnumerable<T1>> func1,
                                     Func<GridReader, IEnumerable<T2>> func2,
                                     Func<GridReader, IEnumerable<T3>> func3,
                                     Func<GridReader, IEnumerable<T4>> func4,
                                     Func<GridReader, IEnumerable<T5>> func5,
                                     Func<GridReader, IEnumerable<T6>> func6,
                                     Func<GridReader, IEnumerable<T7>> func7,
                                     Func<GridReader, IEnumerable<T8>> func8,
                                     Func<GridReader, IEnumerable<T9>> func9,
                                     Func<GridReader, IEnumerable<T10>> func10,
                                     Func<GridReader, IEnumerable<T11>> func11,
                                     Func<GridReader, IEnumerable<T12>> func12

                                     )
        {
            var objs = await GetMultipleResults(sql, parameters, func1, func2, func3, func4, func5, func6, func7, func8, func9, func10, func11, func12);
            return Tuple.Create(objs[0] as IEnumerable<T1>, objs[1] as IEnumerable<T2>, objs[2] as IEnumerable<T3>, objs[3] as IEnumerable<T4>, objs[4] as IEnumerable<T5>, objs[5] as IEnumerable<T6>, Tuple.Create(objs[6] as IEnumerable<T7>, objs[7] as IEnumerable<T8>, objs[8] as IEnumerable<T9>, objs[9] as IEnumerable<T10>, objs[10] as IEnumerable<T11>, objs[11] as IEnumerable<T12>));
        }

        private async Task<List<object>> GetMultipleResults(string sql, object parameters, params Func<GridReader, object>[] readerFunction)
        {
            var returnResults = new List<object>();
            using (IDbConnection db = _connectionFactory.GetConnection())
            {
                var gridReader = await db.QueryMultipleAsync(sql, parameters, commandType: CommandType.StoredProcedure);

                foreach (var readerFunc in readerFunction)
                {
                    var obj = readerFunc(gridReader);
                    returnResults.Add(obj);
                }
            }

            return returnResults;
        }
    }
}
