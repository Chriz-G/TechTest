using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Domain.Models;

namespace UserManagement.Services.Domain.Interfaces;

public interface IUserService
{
    /// <summary>
    /// Return users by active state
    /// </summary>
    /// <param name="isActive"></param>
    /// <returns></returns>
    IEnumerable<UserModel> FilterByActive(bool isActive);

    /// <summary>
    /// Retrieves all users
    /// </summary>
    /// <returns>A collection of all users</returns>

    IEnumerable<UserModel> GetAll();

    /// <summary>
    /// Retrieves a user by their ID
    /// </summary>
    /// <param name="id">The user ID</param>
    /// <returns>The user if found, null otherwise</returns>

    ValueTask<UserModel?> GetByIdAsync(long id);

    /// <summary>
    /// Creates a new user
    /// </summary>
    /// <param name="user">The user to create</param>
    /// <returns>The User containing the ID of the newly created user</returns>
    ValueTask<UserModel> CreateAsync(UserModel user);

    /// <summary>
    /// Updates an existing user
    /// </summary>
    /// <param name="user">The user with updated information</param>
    /// <returns>The updated User </returns>
    ValueTask<UserModel> UpdateAsync(UserModel user);

    /// <summary>
    /// Deletes a user
    /// </summary>
    /// <param name="id">The user ID</param>
    /// <returns>True if deletion was successful, false otherwise</returns>
    ValueTask<bool> DeleteAsync(long id);
}
