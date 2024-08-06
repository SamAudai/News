using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using News.Server.Models;
using News.Server.Repositories.Interfaces;

namespace News.Server.Repositories
{
    public class GenericRepository<T> : GenericInterface<T> where T : class
    {
        private readonly NewsDbContext _context;
        private DbSet<T> table;

        public GenericRepository(NewsDbContext context)
        {
            _context = context;
            table = _context.Set<T>();
        }

        public IEnumerable<T> GetAllData(string value)
        {
            IQueryable<T> query = table;
            if (value != "")
            {
                query = query.Include(value);
            }
            return query.ToList();
        }

        public T GetDataById(object id)
        {
            return table.Find(id)!;
        }

        public T UpdateData(T value)
        {
            //DbSet<T> table = _context.Set<T>();
            //table.Attach(value);
            table.Attach(value);
            _context.Entry(value).State = EntityState.Modified;
            _context.SaveChanges();
            return value;
        }

        public T AddData(T value)
        {
            table.Add(value);
            _context.SaveChanges();
            return value;
        }

        public void DeleteData(object id)
        {
            var val = GetDataById(id);
            table.Remove(val);
            _context.SaveChanges();
        }
    }
}
