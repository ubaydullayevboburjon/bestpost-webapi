using BestPost.DataAccsess.Utils;
using BestPost.DataAccsess.ViewModels;
using BestPost.Domain.Entites.Posts;
using BestPost.Service.Dtos.Posts;

namespace BestPost.Service.Interfaces.Posts;

public interface IPostService
{
    public Task<bool> DeleteAsync(long postId);

    public Task<long> CountAsync();

    public Task<IList<PostViewModel>> GetAllAsync(PaginationParams @params);

    public Task<Post> GetByIdAsync(long postId);

    public Task<bool> UpdateAsync(PostUpdateDto dto);

    public Task<List<PostViewModel>> SearchAsync(string search);

    public Task<bool> CreateAsync(PostCreateDto dto);
}
