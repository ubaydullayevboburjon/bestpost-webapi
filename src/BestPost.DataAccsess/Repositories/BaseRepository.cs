using Npgsql;

namespace BestPost.DataAccsess.Repositories;

public class BaseRepository
{
    protected readonly NpgsqlConnection _connection;
    public BaseRepository()
    {
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        this._connection = new NpgsqlConnection("Host=bestpost-db-do-user-18889018-0.h.db.ondigitalocean.com; Port=25060; Database=bestpost-db; User Id=doadmin; Password=AVNS_q_MJT7ZC7z89w7TNqIr;");
    }
}
