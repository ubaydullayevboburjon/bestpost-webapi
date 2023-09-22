using BestPost.DataAccsess.Utils;
using BestPost.DataAccsess.ViewModels;
using BestPost.Domain.Entites.Posts;

namespace BestPost.DataAccsess.Interfaces.Posts;

public interface IPostRepository : IRepository<Post, PostViewModel>
{
    public Task<List<PostViewModel>> GetAll(PaginationParams @params);

    public Task<List<PostViewModel>> GetPostByUserId(int userId);

    public Task<long> CountLikeByPostId(long postId);

    public Task<List<Coment>> GetCommentByPostId(long postId);

    public Task<long> IsLiked(long userId);

    public Task<IList<PostViewModel>> SearchAsync(string search);
}
