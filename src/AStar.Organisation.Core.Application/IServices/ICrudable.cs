﻿namespace AStar.Organisation.Core.Application.IServices
{
    public interface ICrudable<T>
    {
        public Task<IEnumerable<T>> GetAll();
        public Task<T> GetById(int id);
        public Task Create(T dto);
        public Task Update(T dto);
        public Task Delete(int id);
    }
}