using Domain.Exceptions;
using AutoMapper;
using Services.Interfaces;
using FuzzyLogicApi.Models.FuzzyLogicAreaDTOs;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class FuzzyLogicAreaService : IFuzzyLogicAreaService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly IFuzzyLogicAreaRepository _fuzzyLogicAreaRepository;

        public FuzzyLogicAreaService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _mapper = mapper;
            _repositoryManager = repositoryManager;
            _fuzzyLogicAreaRepository = _repositoryManager.FuzzyLogicArea;
        }

        public async Task<List<FuzzyLogicAreaDTO>> GetAllFuzzyLogicAreasAsync()
        {
            var fuzzyLogicAreas = await _fuzzyLogicAreaRepository.FindAll().ToListAsync();

            if (fuzzyLogicAreas == null)
            {
                throw new ItemNotFoundException("Fuzzy logic areas not found!");
            }

            return _mapper.Map<List<FuzzyLogicAreaDTO>>(fuzzyLogicAreas);
        }

        public async Task<FuzzyLogicAreaDTO> CreateFuzzyLogicAreaAsync(CreateFuzzyLogicAreaDTO fuzzyLogicAreaDTO)
        {
            var fuzzyLogicAreaToCreate = _mapper.Map<FuzzyLogicArea>(fuzzyLogicAreaDTO);
            await _fuzzyLogicAreaRepository.Create(fuzzyLogicAreaToCreate);

            await _repositoryManager.SaveAsync();

            return await GetLastFuzzyLogicArea();
        }

        private async Task<FuzzyLogicAreaDTO> GetLastFuzzyLogicArea()
        {
            var fuzzyLogicAreas = await _fuzzyLogicAreaRepository
                .FindAll().ToListAsync();

            if (fuzzyLogicAreas == null || fuzzyLogicAreas.Count == 0)
            {
                throw new ItemNotFoundException("Fuzzy logic areas not found!");
            }

            var fuzzyLogicAreaDTO = _mapper.Map<FuzzyLogicAreaDTO>(fuzzyLogicAreas.Last());

            return fuzzyLogicAreaDTO;
        }
    }
}
