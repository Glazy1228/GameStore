using System.Collections.Generic;
using GameStore.BLL.Infrastructure;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Interfaces;
using GameStore.BLL.DTO;
using GameStore.DAL.Entities;
using AutoMapper;
using System.Linq;

namespace GameStore.BLL.Services
{
    class UserEditor : IUserEditor
    {
        IUnitOfWork rep { get; set; }

        public UserEditor(IUnitOfWork uow)
        {
            rep = uow;
        }

        public void Dispose()
        {
            rep.Dispose();
        }

        public UserDTO GetUser(int? id)
        {
            if (id == null)
            {
                throw new ValidationException("Не установлено id пользователя", "");
            }
            var user = rep.Users.Get(id.Value);
            if (user == null)
            {
                throw new ValidationException("Пользователь не найден", "");
            }
            return new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                Login = user.Login,
                Password = user.Password,
                RoleId = user.RoleId
            };
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<User>, List<UserDTO>>(rep.Users.GetAll());
        }

        public IEnumerable<RoleDTO> GetRoles()
        {
            var mapper = new MapperConfiguration(cfg=> cfg.CreateMap<Role, RoleDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Role>, IEnumerable<RoleDTO>>(rep.Roles.GetAll());
        }

        public void ChangeRole(int id,string role)
        {
            Role rol = rep.Roles.GetAll().FirstOrDefault(r=> r.Name.Equals(role));
            User user = rep.Users.Get(id);
            user.RoleId = rol.Id;
            rep.Users.Update(user);
            rep.Save();
        }
    }
}
