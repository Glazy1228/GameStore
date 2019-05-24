using GameStore.BLL.Interfaces;
using GameStore.DAL.Interfaces;
using GameStore.BLL.DTO;
using GameStore.DAL.Entities;
using System.Linq;

namespace GameStore.BLL.Services
{
    public class AccountService : IAccountService
    {
        IUnitOfWork rep { get; set; }

        public AccountService(IUnitOfWork uow)
        {
            rep = uow;
        }

        /// <summary>
        /// Проверка существования mail в БД
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        public bool CheckDBEmail(string mail)
        {
            User us = rep.Users.Find(u => u.Email == mail).FirstOrDefault();
            if (us == null)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Проверка существования логина в БД
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public bool CheckDBLogin(string login)
        {
            User us = rep.Users.Find(u => u.Login == login).FirstOrDefault();
            if (us == null)
                return true;
            else
                return false;
        }

        public void CreateUser(UserDTO model)
        {
            rep.Users.Create(new User { Email = model.Email, Login = model.Login
                , Password = model.Password, RoleId = 2 });
            rep.Save();
        }
        
        /// <summary>
        /// Проверка логина и пароля перед авторизацией
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool CheckUserLogin(string login, string password)
        {
            try
            {
                User user = rep.Users.Find(u => u.Login == login).First();
                if (user.Password == password)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public void Dispose()
        {
            rep.Dispose();
        }
    }
}
