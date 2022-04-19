using Domain.Exceptions;
using AutoMapper;
using Services.Interfaces;
using FuzzyLogicApi.Models.SubsetDTOs;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class SubsetService : ISubsetService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly ISubsetRepository _subsetRepository;

        public SubsetService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _mapper = mapper;
            _repositoryManager = repositoryManager;
            _subsetRepository = _repositoryManager.Subset;
        }

        public async Task<List<SubsetDTO>> GetAllSubsetsByTermIdAsync(int termId)
        {
            var term = _repositoryManager.Term
                .FindByCondition(x => x.Id.Equals(termId));

            if (term == null)
            {
                throw new ItemNotFoundException("Term not found!");
            }

            var subsets = await _subsetRepository
                .FindByCondition(x => x.TermId.Equals(termId))
                .ToListAsync();

            if (subsets == null)
            {
                throw new ItemNotFoundException("Subsets not found!");
            }

            var subsetDTOs = _mapper.Map<List<SubsetDTO>>(subsets);

            return subsetDTOs;
        }

        public async Task<SubsetDTO> CreateSubsetAsync(CreateSubsetDTO createSubsetDTO)
        {
            var term = _repositoryManager.Term
                .FindByCondition(x => x.Id.Equals(createSubsetDTO.TermId));

            if (term == null)
            {
                throw new ItemNotFoundException("Term not found!");
            }

            var subsetToCreate = _mapper.Map<Subset>(createSubsetDTO);
            await _subsetRepository.Create(subsetToCreate);

            await _repositoryManager.SaveAsync();

            return GetLastSubsetByTermIdAsync(Convert.ToInt32(subsetToCreate.TermId));
        }

        private SubsetDTO GetLastSubsetByTermIdAsync(int termId)
        {
            var subset = _subsetRepository
                .FindByCondition(x => x.TermId.Equals(termId))
                .AsEnumerable()
                .LastOrDefault();

            var subsetDTO = _mapper.Map<SubsetDTO>(subset);

            return subsetDTO;
        }
    }
}
