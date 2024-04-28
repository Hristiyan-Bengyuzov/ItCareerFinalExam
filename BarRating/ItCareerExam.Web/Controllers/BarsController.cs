using ItCareerExam.Services.Data.Bars;
using ItCareerExam.Web.DTOs.Bars;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static ItCareerExam.Common.GlobalConstants;

namespace ItCareerExam.Web.Controllers
{
    [Authorize]
    public class BarsController : Controller
    {
        private readonly IBarsService _barsService;

        public BarsController(IBarsService barsService)
        {
            _barsService = barsService;
        }

        public async Task<IActionResult> Index(string name)
        {
            var barDTOs = await _barsService.GetBarsAsync(name);
            return View(barDTOs);
        }

        [HttpGet]
        [Authorize(Roles = AdministratorRole)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = AdministratorRole)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBarDTO createDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(createDTO);
            }

            await _barsService.CreateBarAsync(createDTO);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Roles = AdministratorRole)]
        public async Task<IActionResult> Edit(int id)
        {
            if (!await _barsService.ExistsByIdAsync(id))
            {
                return NoContent();
            }

            var editDTO = await _barsService.GetBarEditDTO(id);
            return View(editDTO);
        }

        [HttpPost]
        [Authorize(Roles = AdministratorRole)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditBarDTO editDTO)
        {
            if (!await _barsService.ExistsByIdAsync(id))
            {
                return NoContent();
            }

            if (!ModelState.IsValid)
            {
                return View(editDTO);
            }

            await _barsService.EditBarAsync(editDTO);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if (!await _barsService.ExistsByIdAsync(id))
            {
                return NoContent();
            }

            var details = await _barsService.GetBarDetailsAsync(id);
            return View(details);
        }

        // this will be fired through ajax
        [HttpDelete]
        [Authorize(Roles = AdministratorRole)]
        public async Task Delete(int id)
        {
            if (!await _barsService.ExistsByIdAsync(id))
            {
                throw new InvalidOperationException("Bar not found");
            }

            await _barsService.DeleteBarAsync(id);
        }
    }
}
