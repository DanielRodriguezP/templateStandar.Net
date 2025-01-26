using System;
namespace Template.Domain.DTOs
{
	public class PaginationDTO
	{
		public Guid Id { get; set; }
		public int Page { get; set; } = 1;
		public int RecordsNumber { get; set; } = 10;
	}
}

