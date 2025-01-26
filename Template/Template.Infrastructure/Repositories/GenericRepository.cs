
using Microsoft.EntityFrameworkCore;
using Template.Domain.DTOs;
using Template.Domain.Helpers;
using Template.Domain.Interfaces;
using Template.Domain.Responses;
using Template.Infrastructure.Data;

namespace Template.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T: class
	{
        private readonly DataContext _context;
        private readonly DbSet<T> _entity;
        public GenericRepository(DataContext context)
		{
            _context = context;
            _entity = context.Set<T>();
		}

        public virtual async Task<ActionResponse<IEnumerable<T>>> GetAsync(PaginationDTO pagination)
        {
            var queryable = _entity.AsQueryable();

            return new ActionResponse<IEnumerable<T>>
            {
                Success = true,
                Result = await queryable
                    .Paginate(pagination)
                    .ToListAsync()
            };
        }

        public virtual async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination)
        {
            var queryable = _entity.AsQueryable();
            double count = await queryable.CountAsync();
            int totalPages = (int)Math.Ceiling(count / pagination.RecordsNumber);
            return new ActionResponse<int>
            {
                Success = true,
                Result = totalPages
            };
        }


        public virtual async Task<ActionResponse<T>> AddAsync(T entity)
        {
            _context.Add(entity);
            try
            {
                await _context.SaveChangesAsync();
                return new ActionResponse<T>
                {
                    Success = true,
                    Result = entity
                };
            }
            catch (DbUpdateException)
            {
                return DbUpdateExceptionActionResponse();
            }
            catch (Exception exception)
            {
                return ExceptionActionResponse(exception);
            }

        }

        public virtual async Task<ActionResponse<T>> DeleteAsync(int id)
        {
            var row = await _entity.FindAsync(id);
            if (row == null)
            {
                return new ActionResponse<T>
                {
                    Success = false,
                    Message = "Registro no encontrado"
                };

            }

            try
            {
                _entity.Remove(row);
                await _context.SaveChangesAsync();
                return new ActionResponse<T>
                {
                    Success = true,
                };
            }
            catch
            {
                return new ActionResponse<T>
                {
                    Success = false,
                    Message = "No se puede borrar, porque tiene registros duplicados"
                };

            }
        }

        public virtual async Task<ActionResponse<T>> GetAsync(int id)
        {
            var row = await _entity.FindAsync(id);
            if (row != null)
            {
                return new ActionResponse<T>
                {
                    Success = true,
                    Result = row
                };
            }

            return new ActionResponse<T>
            {
                Success = false,
                Message = "Registro no encontrado"
            };

        }

        public virtual async Task<ActionResponse<IEnumerable<T>>> GetAsync()
        {
            return new ActionResponse<IEnumerable<T>>
            {
                Success = true,
                Result = await _entity.AsNoTracking().ToListAsync()
            };

        }

        public virtual async Task<ActionResponse<T>> UpdateAsync(T entity)
        {
            try
            {
                _context.Update(entity);
                await _context.SaveChangesAsync();
                return new ActionResponse<T>
                {
                    Success = true,
                    Result = entity
                };
            }
            catch (DbUpdateException)
            {
                return DbUpdateExceptionActionResponse();
            }
            catch (Exception exception)
            {
                return ExceptionActionResponse(exception);
            }

        }

        private ActionResponse<T> ExceptionActionResponse(Exception exception)
        {
            return new ActionResponse<T>
            {
                Success = false,
                Message = exception.Message
            };
        }

        private ActionResponse<T> DbUpdateExceptionActionResponse()
        {
            return new ActionResponse<T>
            {
                Success = false,
                Message = "Ya existe el registro que estas intentando crear."
            };
        }
    }
}

