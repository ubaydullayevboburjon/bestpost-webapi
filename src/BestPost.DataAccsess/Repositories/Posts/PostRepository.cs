using BestPost.DataAccsess.Interfaces;
using BestPost.DataAccsess.Interfaces.Posts;
using BestPost.DataAccsess.Utils;
using BestPost.DataAccsess.ViewModels;
using BestPost.Domain.Entites.Posts;
using Dapper;

namespace BestPost.DataAccsess.Repositories.Posts;

public class PostRepository : BaseRepository, IPostRepository
{
    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT count(*) from posts ;";
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

    public async Task<long> CountLikeByPostId(long postId)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"select count(is_liked=true) from likes where post_id = @Id";
            var result = await _connection.QuerySingleAsync<long>(query, new { Id = postId });
            return result;
        }
        catch
        {
            return 0;
        }
        finally
        {
            _connection?.Close();
        }

    }

    public async Task<long> CreateAsync(Post entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "INSERT INTO public.posts(title, description, image_path, user_id, created_at, updated_at) VALUES (@Title, @Description, @ImagePath,@UserId, @CreatedAt, @UpdatedAt);";
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
            string query = $"DELETE FROM posts WHERE id=@Id";
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

    public async Task<List<PostViewModel>> GetAll(PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM posts order by id desc " +

                $"offset {@params.GetSkipCount()} limit {@params.PageSize}";

            var result = (await _connection.QueryAsync<PostViewModel>(query)).ToList();
            return result;
        }
        catch
        {
            return new List<PostViewModel>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<Post?> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM posts where id=@Id";
            var result = await _connection.QuerySingleAsync<Post>(query, new { Id = id });
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

    public Task<List<Coment>> GetCommentByPostId(long postId)
    {
        throw new NotImplementedException();
    }

    public Task<List<PostViewModel>> GetPostByUserId(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<long> IsLiked(long userId)
    {
        throw new NotImplementedException();
    }

    public async Task<IList<PostViewModel>> SearchAsync(string search)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"Select *From posts where title ilike '%{search}%' or description ilike '%{search}%'";
            var result = (await _connection.QueryAsync<PostViewModel>(query)).ToList();
            return result;
        }
        catch
        {
            return new List<PostViewModel>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<long> UpdateAsync(long id, Post entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"UPDATE public.blog SET title=@Title, description=@Description, image_path=@ImagePath,user_id = @UserId created_at=@CreatedAt, updated_at=@UpdatedAt WHERE <condition> " +
             $"WHERE id={id};";

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
