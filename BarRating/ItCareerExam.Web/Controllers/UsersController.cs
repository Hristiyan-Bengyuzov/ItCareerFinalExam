using ItCareerExam.Services.Data.Users;
using ItCareerExam.Web.DTOs.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static ItCareerExam.Common.GlobalConstants;

namespace ItCareerExam.Web.Controllers
{
    [Authorize(Roles = AdministratorRole)]
    public class UsersController : Controller
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string name)
        {
            var users = await _usersService.GetUsersAsync(name);
            return View(users);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserDTO createDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(createDTO);
            }

            try
            {
                await _usersService.CreateUserAsync(createDTO);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(createDTO);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            if (!await _usersService.ExistsByIdAsync(id))
            {
                return NoContent();
            }

            var detailsDTO = await _usersService.GetUserDetailsAsync(id);
            return View(detailsDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (!await _usersService.ExistsByIdAsync(id))
            {
                return NoContent();
            }

            var editDTO = await _usersService.GetUserEditDTO(id);
            return View(editDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id,  EditUserDTO editDTO)
        {
            if (!await _usersService.ExistsByIdAsync(id))
            {
                return NoContent();
            }

            if (!ModelState.IsValid)
            {
                return View(editDTO);
            }

            await _usersService.EditUserAsync(editDTO);
            return RedirectToAction(nameof(Index));
        }


        // this action will be called through ajax
        [HttpDelete]
        public async Task Delete(string id)
        {
            if (!await _usersService.ExistsByIdAsync(id))
            {
                throw new InvalidOperationException("User not found");
            }

            await _usersService.DeleteUserAsync(id);
        }
    }
}
