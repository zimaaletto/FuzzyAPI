using FuzzyLogicApi.Models.SubsetDTOs;

namespace Services.Interfaces
{
    public interface ISubsetService
    {
        Task<List<SubsetDTO>> GetAllSubsetsByTermIdAsync(int termId);
        Task<SubsetDTO> CreateSubsetAsync(CreateSubsetDTO createSubsetDTO);
    }
}
