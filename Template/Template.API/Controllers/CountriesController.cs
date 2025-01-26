using System;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Services;
using Template.Domain;
using Template.Domain.DTOs;

namespace Template.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountriesController: GenericController<Country>
	{
		private readonly ICountriesService _countries;
		public CountriesController(ICountriesService countries, IGenericService<Country> service): base(service)
		{
			_countries = countries;
		}

        [HttpGet]
        public override async Task<IActionResult> GetAsync(PaginationDTO pagination)
        {
            var response = await _countries.GetAsync(pagination);
            if (response.Success)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }

        [HttpGet("full")]
        public override async Task<IActionResult> GetAsync()
        {
            var response = await _countries.GetAsync();
            if (response.Success)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var response = await _countries.GetAsync(id);
            if (response.Success)
            {
                return Ok(response.Result);
            }
            return NotFound(response.Message);
        }

    }
}

