using ItCareerExam.Web.DTOs.Users;

namespace ItCareerExam.Services.Data.Users
{
    public interface IUsersService
    {
        Task CreateUserAsync(CreateUserDTO createDTO);
        Task<IEnumerable<UserDTO>> GetUsersAsync(string name);
        Task<UserDetailsDTO> GetUserDetailsAsync(string id);
        Task<EditUserDTO> GetUserEditDTO(string id);
        Task EditUserAsync(EditUserDTO editDTO);
        Task DeleteUserAsync(string id);
        Task<bool> ExistsByIdAsync(string id);
        int GetUsersCount();
    }
}
