namespace WebApi.Data.Interfaces
{
    using WebApi.Data.Repositories;
    using WebApi.Models;

    public interface IWebApiData
    {
        UserRepository UserRepository { get; }
        EntityRepository<Painting> PaintingRepository { get; }
        EntityRepository<Exibition> ExibitionRepository { get; }
        EntityRepository<Gallery> GalleryRepository { get; }
       
        void Save();
        void Dispose();
    }
}
