using Domain.Exceptions;
using AutoMapper;
using Services.Interfaces;
using FuzzyLogicApi.Models.RuleDTOs;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class RuleService : IRuleService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly IRuleRepository _ruleRepository;

        public RuleService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _mapper = mapper;
            _repositoryManager = repositoryManager;
            _ruleRepository = _repositoryManager.Rule;
        }

        public async Task<List<RuleDTO>> GetAllRulesByFuzzyLogicAreaIdAsync(int fuzzyLogicAreaId)
        {
            var fuzzyLogicArea = _repositoryManager.FuzzyLogicArea
                .FindByCondition(x => x.Id.Equals(fuzzyLogicAreaId));

            if (fuzzyLogicArea == null)
            {
                throw new ItemNotFoundException("Fuzzy logic area not found!");
            }

            var rules = await _ruleRepository
                .FindByCondition(x => x.FuzzyLogicAreaId.Equals(fuzzyLogicAreaId))
                .ToListAsync();

            if (rules == null)
            {
                throw new ItemNotFoundException("Rules not found!");
            }

            var ruleDTOs = _mapper.Map<List<RuleDTO>>(rules);

            return ruleDTOs;
        }

        public async Task<RuleDTO> CreateRuleAsync(CreateRuleDTO createRuleDTO)
        {
            var fuzzyLogicArea = _repositoryManager.FuzzyLogicArea
                .FindByCondition(x => x.Id.Equals(createRuleDTO.FuzzyLogicAreaId));

            if (fuzzyLogicArea == null)
            {
                throw new ItemNotFoundException("Fuzzy logic area not found!");
            }

            var ruleToCreate = _mapper.Map<Rule>(createRuleDTO);
            await _ruleRepository.Create(ruleToCreate);

            await _repositoryManager.SaveAsync();

            return GetLastRuleByFuzzyLogicAreaIdAsync(Convert.ToInt32(ruleToCreate.FuzzyLogicAreaId));
        }

        private RuleDTO GetLastRuleByFuzzyLogicAreaIdAsync(int fuzzyLogicAreaId)
        {
            var rule = _ruleRepository
                .FindByCondition(x => x.FuzzyLogicAreaId.Equals(fuzzyLogicAreaId))
                .AsEnumerable()
                .LastOrDefault();

            var ruleDTO = _mapper.Map<RuleDTO>(rule);

            return ruleDTO;
        }
    }
}
