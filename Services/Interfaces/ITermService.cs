using FuzzyLogicApi.Models.TermDTOs;

namespace Services.Interfaces
{
    public interface ITermService
    {
        Task<List<TermDTO>> GetAllTermsByFuzzyLogicAreaIdAsync(int fuzzyLogicAreaId);
        Task<TermDTO> CreateTermAsync(CreateTermDTO createTermDTOs);
    }
}
