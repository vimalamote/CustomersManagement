using CustomerManagementApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagementApi.Repositories
{
    public class GenericRepository<T>: IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<T>> GetAllRecords()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetRecordById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task AddRecord(T entity)
        {
                await _context.AddAsync(entity);
                await _context.SaveChangesAsync();
            
        }

        public async Task UpdateRecord(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRecord(int id)
        {
            var recordToDelete = await _context.Set<T>().FindAsync(id);
            if (recordToDelete != null)
            {
                _context.Set<T>().Remove(recordToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
