namespace HotelsCalifornia.Services;

using System.Buffers;
using System.Text.RegularExpressions;
using HotelsCalifornia.Data;
using HotelsCalifornia.DTOs;
using HotelsCalifornia.Models;
public interface IUserService
{
    Task<IEnumerable<ReturnUserDTO>> GetUsersAsync();
    Task<IEnumerable<ReturnManagerDTO>> GetManagersAsync();
    Task<IEnumerable<ReturnMemberDTO>> GetMembersAsync();
    Task<ReturnUserDTO> GetUserAsync(int userId);
    Task<User> CreateUserAsync(NewUserDTO newUser);
    Task<User> UpdateUserAsync(UpdateUserDTO updateUser);
    Task<Member> IncrementMemberRewards(int id, int points);
    Task<User> DeleteUserAsync(int userId);
    
}

public class UserService(IUserRepository repo, IHotelRepository hotelRepository) : IUserService
{
    private readonly IUserRepository _repo = repo;
    private readonly IHotelRepository _hotelRepo = hotelRepository;
    public async Task<IEnumerable<ReturnUserDTO>> GetUsersAsync()
    {
        var users = await _repo.GetUsersAsync();
        List<ReturnUserDTO> userDTOs = [];
        foreach (User user in users)
            userDTOs.Add(toDTO(user));
        return userDTOs;
    }

    public async Task<IEnumerable<ReturnManagerDTO>> GetManagersAsync()
    {
        var managers = await _repo.GetManagersAsync();
        List<ReturnManagerDTO> managerDTOs = [];
        foreach (Manager manager in managers)
            managerDTOs.Add(new ReturnManagerDTO()
            {
                Id = manager.Id,
                Username = manager.Username,
                HotelId = manager.HotelId,
                userType = UserType.Manager
            });
        return managerDTOs;
    }

    public async Task<IEnumerable<ReturnMemberDTO>> GetMembersAsync()
    {
        var members = await _repo.GetMembersAsync();
        List<ReturnMemberDTO> memberDTOs = [];
        foreach (Member member in members)
            memberDTOs.Add(new ReturnMemberDTO()
            {
                Id = member.Id,
                Username = member.Username,
                userType = UserType.Member,
                LicenseNumber = member.LicenseNumber,
                PhoneNumber = member.PhoneNumber,
                Email = member.Email,
                RewardPoints = member.RewardPoints,
                InBlocklist = member.InBlocklist
            });
        return memberDTOs;
    }

    public async Task<ReturnUserDTO> GetUserAsync(int userId)
    {
        if (userId < 1)
            throw new ArgumentOutOfRangeException("User ID must be a positive number");
        User user = await _repo.GetUserByIdAsync(userId);

        return toDTO(user);
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
                throw new ArgumentOutOfRangeException("Id cannot be less than 1");
            if (await _hotelRepo.GetHotelByIdAsync(newManager.HotelId) is null)
                throw new ArgumentException("No hotel exists with the provided hotel Id");
        }

        if (newUser is NewMemberDTO newMember)
        {
            if (newMember.LicenseNumber.Length < 7 || newMember.LicenseNumber.Length > 31) 
                throw new ArgumentException("License number must be between 7 and 31 characters");
            if (!IsValidEmail(newMember.Email))
                throw new ArgumentException("Invalid email");
            if (!IsValidPhoneNumber(newMember.PhoneNumber))
                throw new ArgumentException("Invalid phone number");
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
            if (member.Email is not null && !IsValidEmail(member.Email))
                throw new ArgumentException("Invalid email");
            if (member.PhoneNumber is not null && !IsValidPhoneNumber(member.PhoneNumber))
                throw new ArgumentException("Invalid phone number");
            if (member.LicenseNumber is not null && (member.LicenseNumber.Length < 7 || member.LicenseNumber.Length > 31)) 
                throw new ArgumentException("Licence number must be bewteen 7 and 31 characters");
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

    private bool IsValidEmail(string email)
    {
        Regex pattern = new(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$");
        return pattern.IsMatch(email);
    }

    private bool IsValidPhoneNumber(string phoneNumber)
    {
        Regex pattern = new("^\\+?\\d{1,4}?[-.\\s]?\\(?\\d{1,3}?\\)?[-.\\s]?\\d{1,4}[-.\\s]?\\d{1,4}[-.\\s]?\\d{1,9}$");
        return pattern.IsMatch(phoneNumber);
    }

    private ReturnUserDTO toDTO(User user)
    {
        return user switch
        {
            Admin a => new ReturnUserDTO()
            {
                Id = a.Id,
                Username = a.Username,
                userType = UserType.Admin
            },
            Manager m => new ReturnUserDTO()
            {
                Id = m.Id,
                Username = m.Username,
                HotelId = m.HotelId,
                userType = UserType.Manager
            },
            Member mr => new ReturnUserDTO()
            {
                Id = mr.Id,
                Username = mr.Username,
                userType = UserType.Member,
                LicenseNumber = mr.LicenseNumber,
                Email = mr.Email,
                PhoneNumber = mr.PhoneNumber,
                RewardPoints = mr.RewardPoints,
                InBlocklist = mr.InBlocklist
            },
            _ => throw new Exception("Unknown user type")
        };
    }
}