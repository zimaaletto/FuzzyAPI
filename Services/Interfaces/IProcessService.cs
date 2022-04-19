using Domain.Entities;

namespace Services.Interfaces
{
    public interface IProcessService
    {
        List<SubsetVector> Fuzzification(string textParams);
        Task<double> ProcessData(string textParams, string resultTermName);
    }
}