using ItCareerExam.Data.Models;
using ItCareerExam.Data.Repositories;
using ItCareerExam.Services.Mapping;
using ItCareerExam.Web.DTOs.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static ItCareerExam.Common.GlobalConstants;

namespace ItCareerExam.Services.Data.Users
{
    public class UsersService : IUsersService
    {
        private readonly IRepository<ApplicationUser> _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersService(IRepository<ApplicationUser> userRepository, UserManager<ApplicationUser> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task CreateUserAsync(CreateUserDTO createDTO)
        {
            var user = AutoMapperConfig.MapperInstance.Map<ApplicationUser>(createDTO);

            var result = await _userManager.CreateAsync(user, createDTO.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, UserRole);
            }
            else
            {
                throw new InvalidOperationException("User already exists");
            }
        }

        public async Task DeleteUserAsync(string id)
        {
            var user = await _userRepository.All().FirstAsync(u => u.Id == id);
            _userRepository.Delete(user);
            await _userRepository.SaveChangesAsync();
        }

        public async Task EditUserAsync(EditUserDTO editDTO)
        {
            var user = await _userRepository.All().FirstAsync(u => u.Id == editDTO.Id);

            user.UserName = editDTO.Username;
            user.FirstName = editDTO.FirstName;
            user.LastName = editDTO.LastName;
            user.Email = editDTO.Email;

            var removedPassword = await _userManager.RemovePasswordAsync(user);

            // set new password only if current one is successfully removed
            if (removedPassword.Succeeded)
            {
                var addNewPassword = await _userManager.AddPasswordAsync(user, editDTO.Password);

                if (addNewPassword.Succeeded)
                {
                    await _userManager.UpdateAsync(user);
                }
            }
        }

        public Task<bool> ExistsByIdAsync(string id) => _userRepository.AllAsNoTracking().AnyAsync(u => u.Id == id);

        public async Task<UserDetailsDTO> GetUserDetailsAsync(string id)
        {
            var user = await _userRepository.AllAsNoTracking().FirstAsync(u => u.Id == id);
            var detailsDTO = AutoMapperConfig.MapperInstance.Map<UserDetailsDTO>(user);
            var roles = await _userManager.GetRolesAsync(user);
            detailsDTO.Role = roles[0];
            return detailsDTO;
        }

        public async Task<EditUserDTO> GetUserEditDTO(string id)
        {
            var user = await _userRepository.AllAsNoTracking().FirstAsync(u => u.Id == id);
            return AutoMapperConfig.MapperInstance.Map<EditUserDTO>(user);
        }

        public async Task<IEnumerable<UserDTO>> GetUsersAsync(string name)
        {
            var users = await _userRepository.AllAsNoTracking()
                .To<UserDTO>()
                .ToListAsync();

            if (!string.IsNullOrEmpty(name))
            {
                users = users.Where(u => u.Username.Contains(name.Trim(), StringComparison.InvariantCultureIgnoreCase)).ToList();
            }

            return users;
        }

        public int GetUsersCount() => _userRepository.AllAsNoTracking().Count();
    }
}
