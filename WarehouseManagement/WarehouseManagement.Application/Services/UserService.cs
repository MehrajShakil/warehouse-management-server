using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarehouseManagement.Application.Dtos;
using WarehouseManagement.Application.Interfaces;
using WarehouseManagement.Application.Security;
using WarehouseManagement.Domain.Entities;

namespace WarehouseManagement.Application.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;

        public UserService(
            IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserDto CreateUser(CreateUserDto createUserDto)
        {
            var user = new User
            {
                UserName = createUserDto.UserName,
                Email = createUserDto.Email,
                Password = string.Empty,
                PasswordHash = PasswordHasher.Hash(createUserDto.Password),
                IsActive = createUserDto.IsActive
            };

            var createdUser = _userRepository.AddUser(user);

            return ToDto(createdUser);
        }

        public UserDto UpdateUser(UpdateUserDto updateUserDto)
        {
            var user = _userRepository.GetUserById(updateUserDto.Id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with id '{updateUserDto.Id}' was not found.");
            }

            user.UserName = updateUserDto.UserName;
            user.Email = updateUserDto.Email;
            if (!string.IsNullOrEmpty(updateUserDto.Password))
            {
                user.PasswordHash = PasswordHasher.Hash(updateUserDto.Password);
            }
            user.IsActive = updateUserDto.IsActive;

            var updatedUser = _userRepository.UpdateUser(user);

            return ToDto(updatedUser);
        }

        public UserDto DeleteUser(Guid id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with id '{id}' was not found.");
            }

            var deleted = _userRepository.DeleteUser(user);
            if (!deleted)
            {
                throw new InvalidOperationException($"User with id '{id}' could not be deleted.");
            }

            return ToDto(user);
        }

        public UserDto GetUser(Guid id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with id '{id}' was not found.");
            }

            return ToDto(user);
        }

        public UserDto ChangePassword(Guid id, ChangePasswordDto changePasswordDto)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with id '{id}' was not found.");
            }

            if (!PasswordHasher.Verify(changePasswordDto.CurrentPassword, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Current password is incorrect.");
            }

            user.PasswordHash = PasswordHasher.Hash(changePasswordDto.NewPassword);

            var updatedUser = _userRepository.UpdateUser(user);

            return ToDto(updatedUser);
        }

        public UserDto SetUserActive(Guid id, SetUserActiveDto setUserActiveDto)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with id '{id}' was not found.");
            }

            user.IsActive = setUserActiveDto.IsActive;

            var updatedUser = _userRepository.UpdateUser(user);

            return ToDto(updatedUser);
        }

        public List<UserDto> GetAllUsers(List<Guid>? userIds)
        {
            var users = _userRepository.GetAllUsers(userIds);

            return users.Select(ToDto).ToList();
        }

        private static UserDto ToDto(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Password = string.Empty,
                PasswordHash = string.Empty,
                IsActive = user.IsActive
            };
        }
    }
}
