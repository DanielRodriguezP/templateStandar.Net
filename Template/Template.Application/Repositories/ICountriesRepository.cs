using System;
using Template.Domain;
using Template.Domain.DTOs;
using Template.Domain.Responses;

namespace Template.Application.Repositories
{
	public interface ICountriesRepository
	{
        Task<ActionResponse<Country>> GetAsync(Guid id);

        Task<ActionResponse<IEnumerable<Country>>> GetAsync();

        Task<ActionResponse<IEnumerable<Country>>> GetAsync(PaginationDTO pagination);

    }
}

