using System;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Services;
using Template.Domain;

namespace Template.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CategoriesController : GenericController<Category>
	{
		public CategoriesController(IGenericService<Category> service) : base(service)
		{
		}
	}
}

