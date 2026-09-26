using System;
using System.Collections.Generic;
using System.Text;
using WarehouseManagement.Domain.Entities;

namespace WarehouseManagement.Application.Interfaces
{
    public interface IUserRepository
    {
        User AddUser(User user);

        User UpdateUser(User user);

        bool DeleteUser(User user);

        User? GetUserById(Guid id);

        User? GetUserByUserNameOrEmail(string userNameOrEmail);

        List<User> GetAllUsers(List<Guid>? userIds);
    }
}
