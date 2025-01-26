using System;
using Microsoft.EntityFrameworkCore;
using Template.Application.Repositories;
using Template.Domain;
using Template.Domain.DTOs;
using Template.Domain.Helpers;
using Template.Domain.Responses;
using Template.Infrastructure.Data;

namespace Template.Infrastructure.Repositories
{
	public class CountriesRepository : GenericRepository<Country>, ICountriesRepository
	{
		private readonly DataContext _context;

		public CountriesRepository(DataContext context) : base(context)
		{
			_context = context;
		}

        public override async Task<ActionResponse<IEnumerable<Country>>> GetAsync()
        {
            var countries = await _context.Countries
                .OrderBy(x => x.Name)
                .ToListAsync();
            return new ActionResponse<IEnumerable<Country>>
            {
                Success = true,
                Result = countries
            };
        }

        public override async Task<ActionResponse<IEnumerable<Country>>> GetAsync(PaginationDTO pagination)
        {
            var queryable = _context.Countries
                .Include(c => c.States)
                .AsQueryable();

            return new ActionResponse<IEnumerable<Country>>
            {
                Success = true,
                Result = await queryable
                    .OrderBy(x => x.Name)
                    .Paginate(pagination)
                    .ToListAsync()
            };
        }

        public override async Task<ActionResponse<Country>> GetAsync(Guid id)
        {
            var country = await _context.Countries
                 .Include(c => c.States!)
                 .Include(s => s.Cities)
                 .FirstOrDefaultAsync(c => c.Id == id);

            if (country == null)
            {
                return new ActionResponse<Country>
                {
                    Success = false,
                    Message = "País no existe"
                };
            }

            return new ActionResponse<Country>
            {
                Success = true,
                Result = country
            };
        }
    }
}

