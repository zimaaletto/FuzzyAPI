using FuzzyLogicApi.Models.FuzzyLogicAreaDTOs;

namespace Services.Interfaces
{
    public interface IFuzzyLogicAreaService
    {
        Task<List<FuzzyLogicAreaDTO>> GetAllFuzzyLogicAreasAsync();
        Task<FuzzyLogicAreaDTO> CreateFuzzyLogicAreaAsync(CreateFuzzyLogicAreaDTO fuzzyLogicAreaDTO);
    }
}
