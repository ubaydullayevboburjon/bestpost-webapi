using Npgsql;

namespace BestPost.DataAccsess.Repositories;

public class BaseRepository
{
    protected readonly NpgsqlConnection _connection;
    public BaseRepository()
    {
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        this._connection = new NpgsqlConnection("Host=db-postgresql-sgp1-00047-do-user-14589325-0.c.db.ondigitalocean.com; Port=25060; Database=bestpost-db; User Id=doadmin; Password=AVNS_UXdPJwHxhsHpv83yhcb;");
    }
}
