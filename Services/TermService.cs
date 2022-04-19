using Domain.Exceptions;
using AutoMapper;
using Services.Interfaces;
using FuzzyLogicApi.Models.TermDTOs;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class TermService : ITermService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly ITermRepository _termRepository;

        public TermService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _mapper = mapper;
            _repositoryManager = repositoryManager;
            _termRepository = _repositoryManager.Term;
        }

        public async Task<List<TermDTO>> GetAllTermsByFuzzyLogicAreaIdAsync(int fuzzyLogicAreaId)
        {
            var fuzzyLogicArea = _repositoryManager.FuzzyLogicArea
                .FindByCondition(x => x.Id.Equals(fuzzyLogicAreaId));

            if (fuzzyLogicArea == null)
            {
                throw new ItemNotFoundException("Fuzzy logic area not found!");
            }

            var terms = await _termRepository
                .FindByCondition(x => x.FuzzyLogicAreaId.Equals(fuzzyLogicAreaId))
                .ToListAsync();

            if (terms == null)
            {
                throw new ItemNotFoundException("Terms not found!");
            }

            var termDTOs = _mapper.Map<List<TermDTO>>(terms);

            return termDTOs;
        }

        public async Task<TermDTO> CreateTermAsync(CreateTermDTO createTermDTO)
        {
            var fuzzyLogicArea = _repositoryManager.FuzzyLogicArea
                .FindByCondition(x => x.Id.Equals(createTermDTO.FuzzyLogicAreaId));

            if (fuzzyLogicArea == null)
            {
                throw new ItemNotFoundException("Fuzzy logic area not found!");
            }

            var termToCreate = _mapper.Map<Term>(createTermDTO);
            await _termRepository.Create(termToCreate);

            await _repositoryManager.SaveAsync();

            return GetLastTermByFuzzyLogicAreaIdAsync(Convert.ToInt32(termToCreate.FuzzyLogicAreaId));
        }

        private TermDTO GetLastTermByFuzzyLogicAreaIdAsync(int fuzzyLogicAreaId)
        {
            var term = _termRepository
                .FindByCondition(x => x.FuzzyLogicAreaId.Equals(fuzzyLogicAreaId))
                .AsEnumerable()
                .LastOrDefault();

            var termDTO = _mapper.Map<TermDTO>(term);

            return termDTO;
        }
    }
}
