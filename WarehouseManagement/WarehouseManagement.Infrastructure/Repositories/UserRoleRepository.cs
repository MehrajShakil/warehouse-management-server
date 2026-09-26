using WarehouseManagement.Application.Interfaces;
using WarehouseManagement.Domain.Entities;
using WarehouseManagement.Infrastructure.Data;

namespace WarehouseManagement.Infrastructure.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly AppDbContext _dbContext;

        public UserRoleRepository(
            AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public UserRoles AssignRole(UserRoles userRole)
        {
            _dbContext.UserRoles.Add(userRole);

            _dbContext.SaveChanges();
            return userRole;
        }

        public bool RemoveRole(Guid userId, Guid roleId)
        {
            var userRole = _dbContext.UserRoles
                .Where(ur => ur.UserId == userId && ur.RoleId == roleId)
                .FirstOrDefault();

            if (userRole == null)
            {
                return false;
            }

            _dbContext.UserRoles.Remove(userRole);

            return _dbContext.SaveChanges() > 0;
        }

        public bool HasRole(Guid userId, Guid roleId)
        {
            return _dbContext.UserRoles
                .Any(ur => ur.UserId == userId && ur.RoleId == roleId);
        }

        public List<Role> GetRolesByUserId(Guid userId)
        {
            return _dbContext.UserRoles
                .Where(ur => ur.UserId == userId)
                .Select(ur => ur.Role)
                .ToList();
        }

        public List<User> GetUsersByRoleId(Guid roleId)
        {
            return _dbContext.UserRoles
                .Where(ur => ur.RoleId == roleId)
                .Select(ur => ur.User)
                .ToList();
        }
    }
}
