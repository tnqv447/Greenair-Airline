using System.Threading.Tasks;
using System;
using System.Linq;
using System.Linq.Expressions;
using ApplicationCore.Interfaces;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repos
{
    public class Repository<T> : IRepository<T> where T : class, IAggregateRoot
    {
        protected GreenairContext Context { get; private set; }

        public Repository(GreenairContext context)
        {
            Context = context;
        }

        public void Add(T entity)
        {
            try
            {
                Context.Add(entity);
            }
            catch (Exception e)
            {
                Console.WriteLine("Add() Unexpected: " + e);
            }
        }

        public void AddRange(IEnumerable<T> entities)
        {

            try
            {
                Context.AddRange(entities);
            }
            catch (Exception e)
            {
                Console.WriteLine("AddRange() Unexpected: " + e);
            }
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {

            try
            {
                return Context.Set<T>().Where(predicate).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine("Find() Unexpected: " + e);
                return null;
            }
        }

        public IEnumerable<T> GetAll()
        {
            try
            {
                return Context.Set<T>().AsNoTracking().ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine("GetAll() Unexpected: " + e);
                return null;
            }
        }

        public T GetBy(string id)
        {
            try
            {
                return Context.Set<T>().Find(id);
            }
            catch (Exception e)
            {
                Console.WriteLine("GetBy() Unexpected: " + e);
                return null;
            }
        }

        public void Remove(T entity)
        {

            try
            {
                Context.Set<T>().Remove(entity);
            }
            catch (Exception e)
            {
                Console.WriteLine("Remove() Unexpected: " + e);
            }
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            try
            {
                Context.Set<T>().RemoveRange(entities);
            }
            catch (Exception e)
            {
                Console.WriteLine("RemoveRange() Unexpected: " + e);
            }
        }
        public void Update(T entity)
        {

            try
            {
                Context.Set<T>().Update(entity);
            }
            catch (Exception e)
            {
                Console.WriteLine("Update() Unexpected: " + e);
            }
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            try
            {
                Context.Set<T>().UpdateRange(entities);
            }
            catch (Exception e)
            {
                Console.WriteLine("UpdateRange() Unexpected: " + e);
            }
        }


        // Async    =========================================================================================

        public async Task AddAsync(T entity)
        {
            try
            {
                await Context.AddAsync(entity);
            }
            catch (Exception e)
            {
                Console.WriteLine("Add() Unexpected: " + e);
            }
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            try
            {
                await Context.AddRangeAsync(entities);
            }
            catch (Exception e)
            {
                Console.WriteLine("AddRange() Unexpected: " + e);
            }

        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await Context.Set<T>().AsNoTracking().Where(predicate).ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("FindAsync() Unexpected: " + e);
                return null;
            }
        }

        public async Task<IEnumerable<T>> ClientFindAsync(Func<T, bool> predicate)
        {
            try
            {
                var res = await Context.Set<T>().AsNoTracking().ToListAsync();
                return res.Where(predicate).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine("ClientFindAsync() Unexpected: " + e);
                return null;
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                return await Context.Set<T>().AsNoTracking().ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("GetAll() Unexpected: " + e);
                return null;
            }
        }

        public async Task<T> GetByAsync(string id)
        {
            try
            {
                return await Context.Set<T>().FindAsync(id);
            }
            catch (Exception e)
            {
                Console.WriteLine("GetBy() Unexpected: " + e);
                return null;
            }
        }

        public async Task RemoveAsync(T entity)
        {

            try
            {
                await Task.Run(() => Context.Set<T>().Remove(entity));
            }
            catch (Exception e)
            {
                Console.WriteLine("Remove() Unexpected: " + e);
            }
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            try
            {
                await Task.Run(() => Context.Set<T>().RemoveRange(entities));
            }
            catch (Exception e)
            {
                Console.WriteLine("RemoveRange() Unexpected: " + e);
            }

        }

        public async Task UpdateAsync(T entity)
        {

            try
            {
                await Task.Run(() => Context.Set<T>().Update(entity));
            }
            catch (Exception e)
            {
                Console.WriteLine("UpdateAsync() Unexpected: " + e);
            }
        }

        public async Task UpdateRangeAsync(IEnumerable<T> entities)
        {
            try
            {
                await Task.Run(() => Context.Set<T>().UpdateRange(entities));
            }
            catch (Exception e)
            {
                Console.WriteLine("UpdateRangeAsync() Unexpected: " + e);
            }
        }
    }
}