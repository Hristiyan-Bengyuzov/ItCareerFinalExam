using ItCareerExam.Data.Models;
using ItCareerExam.Data.Repositories;
using ItCareerExam.Infrastructure.Extensions;
using ItCareerExam.Services.Mapping;
using ItCareerExam.Web.DTOs.Bars;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace ItCareerExam.Services.Data.Bars
{
    public class BarsService : IBarsService
    {
        private readonly IRepository<Bar> _barRepository;

        public BarsService(IRepository<Bar> barRepository)
        {
            _barRepository = barRepository;
        }

        public async Task CreateBarAsync(CreateBarDTO createDTO)
        {
            var bar = AutoMapperConfig.MapperInstance.Map<Bar>(createDTO);
            await _barRepository.AddAsync(bar);
            await _barRepository.SaveChangesAsync();
        }

        public async Task DeleteBarAsync(int id)
        {
            var bar = await _barRepository.All().FirstAsync(b => b.Id == id);
            _barRepository.Delete(bar);
            await _barRepository.SaveChangesAsync();
        }

        public async Task EditBarAsync(EditBarDTO editDTO)
        {
            var bar = await _barRepository.All().FirstAsync(b => b.Id == editDTO.Id);

            bar.Name = editDTO.Name;
            bar.Description = editDTO.Description;
            bar.Photo = editDTO.PhotoInput.ToBase64String();

            _barRepository.Update(bar);
            await _barRepository.SaveChangesAsync();
        }

        public Task<bool> ExistsByIdAsync(int id) => _barRepository.AllAsNoTracking().AllAsync(b => b.Id == id);

        public async Task<BarDTO> GetBarDetailsAsync(int id)
        {
            var bar = await _barRepository.AllAsNoTracking().FirstAsync(b => b.Id == id);
            return AutoMapperConfig.MapperInstance.Map<BarDTO>(bar);
        }

        public async Task<EditBarDTO> GetBarEditDTO(int id)
        {
            var bar = await _barRepository.AllAsNoTracking().FirstAsync(b => b.Id == id);
            return AutoMapperConfig.MapperInstance.Map<EditBarDTO>(bar);
        }

        public async Task<IEnumerable<BarDTO>> GetBarsAsync(string name)
        {
            var bars = await _barRepository.AllAsNoTracking()
                .To<BarDTO>()
                .ToListAsync();

            if (!string.IsNullOrEmpty(name))
            {
                bars = bars.Where(b => b.Name.Contains(name.Trim(), StringComparison.InvariantCultureIgnoreCase)).ToList();
            }

            return bars;
        }

        public int GetBarsCount() => _barRepository.AllAsNoTracking().Count();
    }
}
