namespace WebApi.Data.Repositories
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using WebApi.Models;

    public class UserRepository
    {
        internal ApplicationDbContext context;
        internal DbSet<ApplicationUser> dbSet;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<ApplicationUser>();
        }

        public ApplicationUser GetById(string id)
        {
            return this.dbSet.Find(id);
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            return this.dbSet.AsQueryable();
        }

        public virtual void Update(ApplicationUser entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
