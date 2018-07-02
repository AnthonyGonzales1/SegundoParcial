using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace SegundoParcial.DAL
{
    
    public class Repositorio<T> : IDisposable, IRepository<T> where T : class
    {
        internal Contexto _contexto;

        public Repositorio(Contexto contexto)
        {
            _contexto = contexto;
        }

        public T Buscar(int id)
        {
            T entity;
            try
            {
                entity = _contexto.Set<T>().Find(id);
            }
            catch (Exception)
            {
                throw;
            }
            return entity;
        }

        public List<T> GetList(Expression<Func<T, bool>> expression)
        {
            List<T> Lista = new List<T>();
            try
            {
                Lista = _contexto.Set<T>().Where(expression).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return Lista;
        }

        public void Dispose()
        {
            _contexto.Dispose();
        }
    }
}

