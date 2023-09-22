using Microsoft.AspNetCore.Http;

namespace BestPost.Service.Interfaces.Common;

public interface IFileService
{
    public Task<string> UploadImageAsync(IFormFile image, string folderName);

    public Task<bool> DeleteImageAsync(string subpath);

    public Task<string> UploadAvatarAsync(IFormFile avatar, string folderName);

    public Task<bool> DeleteAvatarAsync(string subpath);
}
