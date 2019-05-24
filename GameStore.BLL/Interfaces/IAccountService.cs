using GameStore.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.BLL.Interfaces
{
    public interface IAccountService
    {
        bool CheckDBLogin(string login);
        bool CheckDBEmail(string mail);
        void CreateUser(UserDTO model);
        bool CheckUserLogin(string login, string password);
        void Dispose();
    }
}
