using GameStore.BLL.DTO;
using System.Collections.Generic;

namespace GameStore.BLL.Interfaces
{
    public interface IUserEditor
    {
        UserDTO GetUser(int? id);
        IEnumerable<UserDTO> GetUsers();
        IEnumerable<RoleDTO> GetRoles();
        void ChangeRole(int id, string role);
        void Dispose();
    }
}
