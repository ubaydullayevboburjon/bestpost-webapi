using Npgsql;

namespace BestPost.DataAccsess.Repositories;

public class BaseRepository
{
    protected readonly NpgsqlConnection _connection;
    public BaseRepository()
    {
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        this._connection = new NpgsqlConnection("Host=best-post-do-user-14588306-0.b.db.ondigitalocean.com; Port=25060; Database=bestpost-db; User Id=doadmin; Password=AVNS_VTygUTcouxOHKX0EYwa;");
    }
}
