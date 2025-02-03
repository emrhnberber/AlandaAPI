using AlandaProject.API.BussinessLayer.Abstract;
using AlandaProject.API.DataAccessLayer.Abstract;
using AlandaProject.API.Models;

namespace AlandaProject.API.BussinessLayer.Concrete
{
    public class UsersManager : IUsersService
    {
        private readonly IUsersDal _userdal;

        public UsersManager(IUsersDal userdal)
        {
            _userdal = userdal;
        }

        public void TDelete(User t)
        {
            _userdal.Delete(t);
        }

        public User TGetbyID(int ID)
        {
            return _userdal.GetbyID(ID);
        }

        public List<User> TGetList()
        {
            return _userdal.GetList();
        }

        public void TInsert(User t)
        {
            _userdal.Insert(t);
        }

        public void TUpdate(User t)
        {
            _userdal.Update(t);
        }
    }
}
