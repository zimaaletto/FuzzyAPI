using AutoMapper;
using Domain.Entities;
using FuzzyLogicApi.Models.FuzzyLogicAreaDTOs;
using FuzzyLogicApi.Models.RuleDTOs;
using FuzzyLogicApi.Models.SubsetDTOs;
using FuzzyLogicApi.Models.TermDTOs;

namespace Services.Mappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CreateFuzzyLogicAreaDTO, FuzzyLogicArea>();
            CreateMap<FuzzyLogicArea, FuzzyLogicAreaDTO>();

            CreateMap<CreateRuleDTO, Rule>();
            CreateMap<Rule, RuleDTO>();

            CreateMap<CreateSubsetDTO, Subset>();
            CreateMap<Subset, SubsetDTO>();

            CreateMap<CreateTermDTO, Term>();
            CreateMap<Term, TermDTO>();
        }
    }
}
