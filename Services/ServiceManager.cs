using AutoMapper;
using Domain.RepositoryInterfaces;
using Services.Interfaces;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public IFuzzyLogicAreaService FuzzyLogicAreaService => new FuzzyLogicAreaService(_repositoryManager, _mapper);
        public IRuleService RuleService => new RuleService(_repositoryManager, _mapper);
        public ISubsetService SubsetService => new SubsetService(_repositoryManager, _mapper);
        public ITermService TermService => new TermService(_repositoryManager, _mapper);
        public IProcessService ProcessService => new ProcessService(_repositoryManager, _mapper);
    }
}
