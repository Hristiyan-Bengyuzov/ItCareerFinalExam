using ItCareerExam.Web.DTOs.Bars;

namespace ItCareerExam.Services.Data.Bars
{
    public interface IBarsService
    {
        Task CreateBarAsync(CreateBarDTO createDTO);
        Task<IEnumerable<BarDTO>> GetBarsAsync(string name);
        Task<EditBarDTO> GetBarEditDTO(int id);
        Task EditBarAsync(EditBarDTO editDTO);
        Task DeleteBarAsync(int id);
        Task<BarDTO> GetBarDetailsAsync(int id);
        Task<bool> ExistsByIdAsync(int id);
        int GetBarsCount();
    }
}
