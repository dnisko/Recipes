using Common.Responses;
using DataAccess.Interfaces;
using DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataAccess.Implementations
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly RecipeDbContext _context; 
        private readonly DbSet<T> _table;
        //private readonly ILogger<Repository<T>> _logger;

        public Repository(RecipeDbContext context)//, ILogger<Repository<T>> logger)
        {
            _context = context;
            _table = _context.Set<T>();
            //_logger = logger;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                return await _table.AsNoTracking().ToListAsync();
            }
            catch (Exception)//(Exception ex)
            {
                //_logger.LogError(ex.InnerException, "Error");
                //throw new InvalidOperationException(ex.Message);
                throw;
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            try
            {
                //var maxId = await _table.MaxAsync(x => x.Id);
                //if (id < 0 || id > maxId)
                //{
                //    throw new KeyNotFoundException($"Entity with id: {id} is not found.");
                //}

                //return (await _table.FindAsync(id))!;
                return await _table.FindAsync(id);
            }
            catch (Exception)//(InvalidOperationException ex)
            {
                //_logger.LogError(ex.InnerException, "Error");
                //throw new InvalidOperationException(ex.Message);
                throw;
            }
        }

        public async Task AddAsync(T entity)
        {
            try
            {
                //_logger.LogInformation($"Calling AddAsync from {typeof(T).Name} Repository.");
                _table.Add(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception)//(InvalidOperationException ex)
            {
                //throw new InvalidOperationException(ex.Message);
                throw;
            }
        }

        public async Task UpdateAsync(T entity)
        {
            try
            {
                //_logger.LogInformation($"Calling UpdateAsync from {typeof(T).Name} Repository.");
                _table.Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception)//(InvalidOperationException ex)
            {
                //throw new InvalidOperationException(ex.Message);
                throw;
            }
        }

        public async Task DeleteAsync(T entity)
        {
            try
            {
                //_logger.LogInformation($"Calling DeleteAsync from {typeof(T).Name} Repository.");
                //var entity = await _table.FindAsync(id);
                //if (entity != null)
                //{
                //    _table.Remove(entity);
                //    await _context.SaveChangesAsync();
                //}
                //else
                //{
                //    //throw new KeyNotFoundException($"Entity with id: {id} is not found.");
                //    throw new Exception();
                //}
                _table.Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception)//(InvalidOperationException ex)
            {
                //throw new InvalidOperationException(ex.Message);
                throw;
            }
        }

        public async Task<PaginatedResult<T>> GetPagedAsync(IQueryable<T> query, int pageNumber, int pageSize)
        {
            var totalRecords = await query.CountAsync();
            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return new PaginatedResult<T>(items, totalRecords, pageNumber, pageSize);
        }
    }
}
