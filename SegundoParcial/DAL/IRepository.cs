using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using SegundoParcial.DAL;
using SegundoParcial.Entidades;
using SegundoParcial.BLL;

namespace SegundoParcial.DAL
{
    public interface IRepository<T> where T : class
    {
        List<T> GetList(Expression<Func<T, bool>> expression);
        T Buscar(int id);
        
    }
}
