using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Data;
using UserManagement.Domain.Models;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;

namespace UserManagement.Services.Domain.Implementations;

/// <summary>
/// Implements the IUserService interface to provide user management functionality.
/// </summary>
public class UserService(IDataContext dataAccess, IMappingService mapper) : IUserService
{
    /// <summary>
    /// Returns users by their active state.
    /// </summary>
    /// <param name="isActive">The active state to filter by.</param>
    /// <returns>A collection of users that match the active state.</returns>
    public IEnumerable<UserModel> FilterByActive(bool isActive)
    {
        // Fetch all users from the data access layer.
        var users = dataAccess.GetAll<User>();
        // Filter the users by their active state.
        var filteredUsers = users.Where(x => x.IsActive == isActive);
        // Map the filtered users to the UserModel class.
        return filteredUsers.Select(x => mapper.Map<User, UserModel>(x));
    }

    /// <summary>
    /// Retrieves all users.
    /// </summary>
    /// <returns>A collection of all users.</returns>
    public IEnumerable<UserModel> GetAll()
    {
        // Fetch all users from the data access layer.
        var users = dataAccess.GetAll<User>();
        // Map each user to the UserModel class.
        return users.Select(x => mapper.Map<User, UserModel>(x));
    }

    /// <summary>
    /// Retrieves a user by their ID.
    /// </summary>
    /// <param name="id">The user ID.</param>
    /// <returns>The user if found, null otherwise.</returns>
    public async ValueTask<UserModel?> GetByIdAsync(long id)
    {
        // Fetch the user by their ID from the data access layer.
        var result = await dataAccess.GetByIdAsync<User>(id);
        // If the user is not found, return null.
        if (result == null) return null;
        // Map the user to the UserModel class.
        return mapper.Map<User, UserModel>(result);
    }


    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="user">The user to create.</param>
    /// <returns>The created user with its ID.</returns>
    public async ValueTask<UserModel> CreateAsync(UserModel user)
    {
        // Map the user model to the User entity.
        var userEntity = mapper.Map<UserModel, User>(user);
        // Create the user in the data access layer.
        var result = await dataAccess.CreateAsync(userEntity);
        // Map the created user entity to the UserModel class.
        return mapper.Map<User, UserModel>(result);
    }


    /// <summary>
    /// Updates an existing user.
    /// </summary>
    /// <param name="user">The updated user information.</param>
    /// <returns>The updated user.</returns>
    public async ValueTask<UserModel> UpdateAsync(UserModel user)
    {
        // Map the user model to the User entity.
        var userEntity = mapper.Map<UserModel, User>(user);
        // Update the user in the data access layer.
        var result = await dataAccess.UpdateAsync(userEntity);
        // Map the updated user entity to the UserModel class.
        return mapper.Map<User, UserModel>(result);
    }


    /// <summary>
    /// Deletes a user by their ID.
    /// </summary>
    /// <param name="id">The user ID.</param>
    /// <returns>True if the deletion was successful, false otherwise.</returns>
    public async ValueTask<bool> DeleteAsync(long id)
    {
        // Fetch the user by their ID from the data access layer.
        var userEntity = await dataAccess.GetByIdAsync<User>(id);
        // If the user is not found, return false.
        if (userEntity == null) return false;
        // Delete the user in the data access layer.
        var result = await dataAccess.DeleteAsync(userEntity);
        // Return the result of the deletion.
        return result;
    }

}
