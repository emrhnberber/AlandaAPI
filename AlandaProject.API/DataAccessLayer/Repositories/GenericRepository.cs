using AlandaProject.API.DataAccessLayer.Abstract;
using AlandaProject.API.Models;

namespace AlandaProject.API.DataAccessLayer.Repositories
{
    public class GenericRepository<T> : IGenericDal<T> where T : class
    {
        private readonly MyDbContext _Context;
        
        public GenericRepository(MyDbContext context)
        {
            _Context = context;
        }

        public void Delete(T t)
        {
            _Context.Remove(t);
            _Context.SaveChanges();
        }

        public T GetbyID(int id)
        {
            return _Context.Set<T>().Find(id);
        }

        public List<T> GetList()
        {
            return _Context.Set<T>().ToList();
        }

        public void Insert(T t)
        {
            _Context.Add(t);
            _Context.SaveChanges();
        }

        public void Update(T t)
        {
            _Context.Update(t);
            _Context.SaveChanges();
        }
    }
}
