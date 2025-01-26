using System;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Services;
using Template.Domain.DTOs;

namespace Template.API.Controllers
{
	public class GenericController<T> : Controller where T: class
	{
		private readonly IGenericService<T> _service;

		public GenericController(IGenericService<T> service)
		{
			_service = service;
		}

        [HttpGet("full")]
        public virtual async Task<IActionResult> GetAsync()
        {
            var action = await _service.GetAsync();
            if (action.Success)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var action = await _service.GetAsync(pagination);
            if (action.Success)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }

        [HttpGet("totalPages")]
        public virtual async Task<IActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)
        {
            var action = await _service.GetTotalPagesAsync(pagination);
            if (action.Success)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }


        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetAsync(int id)
        {
            var action = await _service.GetAsync(id);
            if (action.Success)
            {
                return Ok(action.Result);
            }
            return NotFound();
        }

        [HttpPost]
        public virtual async Task<IActionResult> PostAsync(T model)
        {
            var action = await _service.AddAsync(model);
            if (action.Success)
            {
                return Ok(action.Result);
            }
            return BadRequest(action.Message);
        }

        [HttpPut]
        public virtual async Task<IActionResult> PutAsync(T model)
        {
            var action = await _service.UpdateAsync(model);
            if (action.Success)
            {
                return Ok(action.Result);
            }
            return BadRequest(action.Message);
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> DeleteAsync(int id)
        {
            var action = await _service.DeleteAsync(id);
            if (action.Success)
            {
                return NoContent();
            }
            return BadRequest(action.Message);
        }

    }
}

