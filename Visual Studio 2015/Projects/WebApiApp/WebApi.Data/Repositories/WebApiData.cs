namespace WebApi.Data.Repositories
{
    using System;
    using System.Reflection.Emit;
    using WebApi.Data.Interfaces;
    using WebApi.Models;
    using WebApi.Models.Interfaces;

    public class WebApiData : IDisposable, IWebApiData
    {
        private ApplicationDbContext context;
        private UserRepository userRepository;
        private EntityRepository<Painting> paintingRepository;
        private EntityRepository<Exibition> exibitionRepository;
        private EntityRepository<Gallery> galleryRepository;

        public WebApiData(ApplicationDbContext dbContext)
        {
            this.context = dbContext;
        }

        public UserRepository UserRepository
        {
            get
            {
                if (this.userRepository == null)
                {
                    this.userRepository = new UserRepository(this.context);
                }
                return this.userRepository;
            }
        }

        public EntityRepository<Painting> PaintingRepository => this.GetRepository(ref this.paintingRepository);
        public EntityRepository<Exibition> ExibitionRepository => this.GetRepository(ref this.exibitionRepository);
        public EntityRepository<Gallery> GalleryRepository => this.GetRepository(ref this.galleryRepository);
       

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private EntityRepository<TEntity> GetRepository<TEntity>(ref EntityRepository<TEntity> repository)
            where TEntity : class, IDentificatable
        {
            if (repository == null)
            {
                repository = new EntityRepository<TEntity>(this.context);
            }

            return repository;
        }
    }
}
