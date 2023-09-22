using BestPost.DataAccsess.Utils;

namespace BestPost.Service.Interfaces.Common;

public interface IPaginator
{
    public void Paginate(long itemsCount, PaginationParams @params);

}
