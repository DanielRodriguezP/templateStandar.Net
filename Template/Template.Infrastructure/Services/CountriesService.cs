using System;
using Template.Application.Repositories;
using Template.Application.Services;
using Template.Domain;
using Template.Domain.DTOs;
using Template.Domain.Interfaces;
using Template.Domain.Responses;
using Template.Infrastructure.Repositories;

namespace Template.Infrastructure.Services
{
	public class CountriesService : GenericService<Country>, ICountriesService
	{
		private readonly ICountriesRepository _countries;

		public CountriesService(IGenericRepository<Country> generic, ICountriesRepository countries): base(generic)
		{
			_countries = countries;
		}

		public async Task<ActionResponse<Country>> GetAsync(Guid id) => await _countries.GetAsync(id);
        public override async Task<ActionResponse<IEnumerable<Country>>> GetAsync() => await _countries.GetAsync();
        public override async Task<ActionResponse<IEnumerable<Country>>> GetAsync(PaginationDTO pagination) => await _countries.GetAsync(pagination);

    }
}

