namespace HotelsCalifornia.Services;

using System.Buffers;
using HotelsCalifornia.Data;
using HotelsCalifornia.DTOs;
using HotelsCalifornia.Models;
public interface IUserService
{
    Task<IEnumerable<ReturnUserDTO>> GetUsersAsync();
    Task<ReturnUserDTO> GetUserAsync(int userId);
    Task<User> CreateUserAsync(NewUserDTO newUser);
    Task<User> UpdateUserAsync(UpdateUserDTO updateUser);
    Task<Member> IncrementMemberRewards(int id, int points);
    Task<User> DeleteUserAsync(int userId);
    
}

public class UserService(IUserRepository repo, IHotelService hotelService) : IUserService
{
    private readonly IUserRepository _repo = repo;
    private readonly IHotelService HotelService = hotelService;
    public async Task<IEnumerable<ReturnUserDTO>> GetUsersAsync()
    {
        return await _repo.GetUsersAsync();
    }

    public async Task<ReturnUserDTO> GetUserAsync(int userId)
    {
        if (userId < 1)
            throw new ArgumentOutOfRangeException("User ID must be a positive number");
        User user = await _repo.GetUserByIdAsync(userId);

        var returnUser = user switch
        {
            Admin a => new ReturnUserDTO
            {
                Id = a.Id,
                userType = UserType.Admin,
                Username = a.Username,
                PasswordHash = a.PasswordHash
            },
            Manager m => new ReturnUserDTO
            {
                Id = m.Id,
                userType = UserType.Manager,
                Username = m.Username,
                PasswordHash = m.PasswordHash,
                HotelId = m.HotelId
            },
            Member mem => new ReturnUserDTO
            {
                Id = mem.Id,
                userType = UserType.Member,
                Username = mem.Username,
                PasswordHash = mem.PasswordHash,
                LicenseNumber = mem.LicenseNumber,
                Email = mem.Email,
                PhoneNumber = mem.PhoneNumber,
                RewardPoints = mem.RewardPoints,
                InBlocklist = mem.InBlocklist
            },
            _ => throw new Exception("Unknown user type")
        };

        return returnUser;
    }

    public async Task<User> CreateUserAsync(NewUserDTO newUser)
    {
        if (newUser.Username.Length < 1)
            throw new ArgumentException("Username cannot be empty");
        if (newUser.Username.Length > 20)
            throw new ArgumentException("Username must be less than 20 characters");
        if (newUser.PasswordHash.Length < 1)
            throw new ArgumentException("Password cannot be empty");
        if (newUser.PasswordHash?.Length > 50)
            throw new ArgumentException("Password cannot exceed 50 characters");

        if (newUser is NewManagerDTO newManager)
        {
            if (newManager.HotelId < 1)
                throw new ArgumentException("Manager must have a hotel, Id cannot be less than 1");
            if (await hotelService.GetHotelAsync(newManager.HotelId) is null)
                throw new ArgumentException("Manager must have a hotel, No hotel exists with the provided hotel Id");
        }

        if (newUser is NewMemberDTO newMember)
        {
            if (newMember.LicenseNumber.Length < 9 || newMember.LicenseNumber.Length > 14) 
                throw new ArgumentException("Licence number must be bewteen 9 and 14 characters");
            if (newMember.Email.Length < 1)
                throw new ArgumentException("Email must not be empty");
            if (newMember.Email.Length > 50) 
                throw new ArgumentException("Email must not exceed 50 characters");
            if (newMember.PhoneNumber.Length != 10)
                throw new ArgumentException("PhoneNumber should be exactly 10 digits");
        }

        return await _repo.CreateUserAsync(newUser);
    }

    public async Task<User> UpdateUserAsync(UpdateUserDTO updateUser)
    {
        if (updateUser.Id < 1)
            throw new ArgumentOutOfRangeException("User Id must be a positive number");
        if (updateUser.Username.Length > 20)
            throw new ArgumentException("Username must be less than 20 characters");
        if (updateUser.PasswordHash?.Length > 50)
            throw new ArgumentException("Password cannot exceed 50 characters");

        if (updateUser is UpdateMemberDTO member)
        {
            if ((member.LicenseNumber.Length < 9 || member.LicenseNumber.Length > 14) && member.LicenseNumber is not null) 
                throw new ArgumentException("Licence number must be bewteen 9 and 14 characters");
            if (member.Email.Length > 50) 
                throw new ArgumentException("Email must not exceed 50 characters");
            if (member.PhoneNumber.Length != 10 && member.PhoneNumber.Length > 0)
                throw new ArgumentException("PhoneNumber should be exactly 10 digits");
        }
        return await _repo.UpdateUserAsync(updateUser);
    }

    public async Task<Member> IncrementMemberRewards(int id, int points)
    {
        return await _repo.IncrementMemberRewards(id, points);
    }


    public async Task<User> DeleteUserAsync(int userId)
    {
        if (userId < 1)
            throw new ArgumentOutOfRangeException("User ID must be a positive number");
        return await _repo.DeleteUserAsync(userId);
    }
}