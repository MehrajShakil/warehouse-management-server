using WarehouseManagement.Application.Interfaces;
using WarehouseManagement.Domain.Entities;
using WarehouseManagement.Infrastructure.Data;

namespace WarehouseManagement.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _dbContext;

        public RoleRepository(
            AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Role AddRole(Role role)
        {
            _dbContext.Roles.Add(role);

            _dbContext.SaveChanges();
            return role;
        }

        public Role UpdateRole(Role role)
        {
            _dbContext.Roles.Update(role);

            _dbContext.SaveChanges();
            return role;
        }

        public Role DeleteRole(Role role)
        {
            _dbContext.Roles.Remove(role);

            _dbContext.SaveChanges();
            return role;
        }

        public Role? GetRoleById(Guid id)
        {
            return _dbContext.Roles
                .Where(r => r.Id == id)
                .FirstOrDefault();
        }

        public List<Role> GetAllRoles(List<Guid>? roleIds)
        {
            return _dbContext.Roles
                .Where(r => roleIds == null || roleIds.Count == 0 || roleIds.Contains(r.Id))
                .ToList();
        }
    }
}
