﻿using System;
using Template.Application.Services;
using Template.Domain.DTOs;
using Template.Domain.Interfaces;
using Template.Domain.Responses;

namespace Template.Infrastructure.Services
{
	public class GenericService<T> : IGenericService<T> where T: class
	{
        private readonly IGenericRepository<T> _repository;

		public GenericService(IGenericRepository<T> repository)
		{
            _repository = repository;
		}

        public virtual async Task<ActionResponse<T>> AddAsync(T model) => await _repository.AddAsync(model);

        public virtual async Task<ActionResponse<T>> DeleteAsync(int id) => await _repository.DeleteAsync(id);

        public virtual async Task<ActionResponse<IEnumerable<T>>> GetAsync() => await _repository.GetAsync();

        public virtual async Task<ActionResponse<T>> GetAsync(int id) => await _repository.GetAsync(id);

        public virtual async Task<ActionResponse<T>> UpdateAsync(T model) => await _repository.UpdateAsync(model);

        public virtual async Task<ActionResponse<IEnumerable<T>>> GetAsync(PaginationDTO pagination) => await _repository.GetAsync(pagination);

        public virtual async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination) => await _repository.GetTotalPagesAsync(pagination);

    }
}

