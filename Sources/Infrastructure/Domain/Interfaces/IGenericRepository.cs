﻿namespace Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Get(object id);
        Task<IEnumerable<T>> GetAll();
        Task Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}