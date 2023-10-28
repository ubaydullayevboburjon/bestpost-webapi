using BestPost.DataAccsess.Interfaces.Posts;
using BestPost.DataAccsess.Utils;
using BestPost.DataAccsess.ViewModels;
using BestPost.Domain.Entites.Posts;
using BestPost.Domain.Exceptions.Posts;
using BestPost.Domain.Exceptionsl;
using BestPost.Service.Common.Helpers;
using BestPost.Service.Dtos.Posts;
using BestPost.Service.Interfaces.Auth;
using BestPost.Service.Interfaces.Common;
using BestPost.Service.Interfaces.Posts;

namespace BestPost.Service.Services.Posts;

public class PostService : IPostService
{
    private readonly IPostRepository _repository;
    private readonly IFileService _fileService;
    private readonly IPaginator _paginator;
    private readonly IIdentityService _identity;

    public PostService(IPostRepository postRepository,
        IFileService fileService,
        IPaginator paginator,
        IIdentityService identityService)
    {
        _repository = postRepository;
        _fileService = fileService;
        _paginator = paginator;
        _identity = identityService;
    }

    public Task<long> CountAsync() => _repository.CountAsync();

    public async Task<bool> CreateAsync(PostCreateDto dto)
    {
        string imagePath = await _fileService.UploadImageAsync(dto.Image, "posts");
        long userId = _identity.UserId;
        Post post = new Post()
        {
            ImagePath = imagePath,
            Title = dto.Title,
            Description = dto.Description,
            UserId = userId,
            CreatedAt = TimeHelper.GetDateTime(),
            UpdatedAt = TimeHelper.GetDateTime()
        };
        var result = await _repository.CreateAsync(post);
        return result > 0;
    }

    public async Task<bool> DeleteAsync(long postId)
    {
        var blog = await _repository.GetByIdAsync(postId);
        if (blog == null) throw new PostNotFoundException();

        var result = await _fileService.DeleteImageAsync(blog.ImagePath);
        if (result == false) throw new ImageNotFoundExceptions();

        var dbResult = await _repository.DeleteAsync(postId);
        return dbResult > 0;
    }

    public async Task<IList<PostViewModel>> GetAllAsync(PaginationParams @params)
    {
        var post = await _repository.GetAll(@params);
        var count = await _repository.CountAsync();
        _paginator.Paginate(count, @params);
        return (List<PostViewModel>)post;
    }

    public async Task<Post> GetByIdAsync(long postId)
    {
        var post = await _repository.GetByIdAsync(postId);
        if (post is null) throw new PostNotFoundException();
        return post;
    }

    public async Task<List<PostViewModel>> SearchAsync(string search)
    {
        var searche = (await _repository.SearchAsync(search)).ToList();
        return searche;
    }

    public async Task<bool> UpdateAsync(PostUpdateDto dto)
    {
        var blog = await _repository.GetByIdAsync(dto.Id);
        if (blog == null) throw new PostNotFoundException();

        blog.Title = dto.Title;
        blog.Description = dto.Description;
        blog.UpdatedAt = DateTime.Now;

        var result = await _repository.UpdateAsync(dto.Id, blog);
        return result > 0;
    }



}
