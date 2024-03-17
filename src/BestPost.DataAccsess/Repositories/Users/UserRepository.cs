using BestPost.DataAccsess.Interfaces.Users;
using BestPost.DataAccsess.ViewModels;
using BestPost.Domain.Entites.Users;
using Dapper;

namespace BestPost.DataAccsess.Repositories.Users;
public class UserRepository : BaseRepository, IUserRepository
{
    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT count(*) from users ;";
            var result = await _connection.QuerySingleAsync<long>(query);
            return result;
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<long> CreateAsync(User entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "INSERT INTO users( first_name, last_name,username, email, email_confirmed, image_path, password_salt, password_hash, created_at, updated_at) " +
                "VALUES(@FirstName, @LastName, @Username, @Email, @EmailConfirmed, @ImagePath, @PasswordSalt, @PasswordHash,  @CreatedAt, @UpdatedAt); ";
            var result = await _connection.ExecuteAsync(query, entity);
            return result;
        }
        
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<long> DeleteAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"DELETE FROM users where Id={id}";
            var result = await _connection.ExecuteAsync(query, new { Id = id });
            return result;
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<List<UserViewModel>> GetAll()
    {

        try
        {
            await _connection.OpenAsync();
            string query = "select * from users  order by id desc ";
            var result = (await _connection.QueryAsync<UserViewModel>(query)).ToList();
            return result;

        }
        catch
        {

            return new List<UserViewModel>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT *FROM users WHERE email = @Email;";
            var data = await _connection.QuerySingleAsync<User>(query, new { Email = email });
            return data;
        }
        catch
        {
            return null;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<User?> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"select * from users where id = {id} ";
            var result = await _connection.QuerySingleAsync<User>(query);
            return result;
        }
        catch
        {
            return null;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<User?> GetByUsernamAsync(string username)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT *FROM users WHERE username = @Username;";
            var data = await _connection.QuerySingleAsync<User>(query, new { Username = username });
            return data;
        }
        catch
        {
            return null;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<long> UpdateAsync(long id, User entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "UPDATE users " +
                "SET first_name = @FirstName, last_name = @LastName,username=@username, email = @Email, email_confirmed = @EmailConfirmed, " +
                "image_path = @ImagePath, password_salt = @PasswordSalt, password_hash = @PasswordHash , " +
                " updated_at = @UpdatedAt " +
                $"WHERE id = {id};";
            var result = await _connection.ExecuteAsync(query, entity);
            return result;
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }
}

