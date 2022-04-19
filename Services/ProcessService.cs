using AutoMapper;
using Services.Interfaces;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Domain.Statics;

namespace Services
{
    public class ProcessService : IProcessService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public ProcessService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _mapper = mapper;
            _repositoryManager = repositoryManager;
        }

        public async Task<double> ProcessData(string textParams, string resultTermName)
        {
            var rules = new List<Dictionary<string, string>>();
            var subsetVectors = Fuzzification(textParams);

            if (ProcessVariables.LastRules == null)
            {
                rules = await GetAllRules();
                ProcessVariables.LastRules = rules.GetRange(0, rules.Count);
            }
            else
            {
                rules = ProcessVariables.LastRules;
            }

            var resultValues = new List<double>();

            foreach (var rule in rules)
            {
                var min = Double.MaxValue;
                for(int i = 0; i < rule.Count() - 1; i++)
                {
                    var logicItem = rule.ElementAt(i);

                    var correspondingSubset = new SubsetVector();
                    foreach(var subsetVector in subsetVectors)
                    {
                        if (subsetVector.Name == logicItem.Key)
                        {
                            correspondingSubset = subsetVector;
                            break;
                        }
                    }
                    var fuzzySubsetValue = correspondingSubset.FuzzificationResults[logicItem.Value];
                    if (fuzzySubsetValue == 0)
                    {
                        min = 0;
                        break;
                    }
                    if (fuzzySubsetValue < min)
                    {
                        min = fuzzySubsetValue;
                    }
                }
                if (min != 0 && min != double.MaxValue)
                {
                    resultValues.Add(min);
                }
            }

            var resultAverage = resultValues.Average();

            var resultTerm = _repositoryManager
                    .Term
                    .FindByCondition(x => x.TermName.Equals(resultTermName)).ToList();

            var resultTermSubset = resultTerm.FirstOrDefault().Subsets.OrderBy(x => x.Value).ToList();

            var diff = Math.Abs(resultTermSubset.LastOrDefault().Value - resultTermSubset.FirstOrDefault().Value);
            var result = resultTermSubset.FirstOrDefault().Value + resultAverage * diff;            

            return result;
        }

        public List<SubsetVector> Fuzzification(string textParams)
        {
            var subsetVectors = new List<SubsetVector>();

            var inputParams = ParseParams(textParams);

            foreach(var inputParam in inputParams)
            {
                var subsetVector = new SubsetVector();
                subsetVector.Name = inputParam.Key.ToString();

                var term = _repositoryManager
                    .Term
                    .FindByCondition(x => x.TermName.Equals(inputParam.Key)).ToList();

                var subsets = term.FirstOrDefault().Subsets.OrderBy(x => x.Value).ToList();

                var minimizedSubsetVectors = MinimizeSubsetVector(inputParam.Value, subsets);

                subsetVector.FuzzificationResults = minimizedSubsetVectors
                    .ToDictionary(entry => entry.Key, entry => entry.Value);

                subsetVectors.Add(subsetVector);
            }

            return subsetVectors;
        }

        private async Task<List<Dictionary<string, string>>> GetAllRules()
        {
            var rulesResult = new List<Dictionary<string, string>>();

            var rules = await _repositoryManager.Rule.FindAll().ToListAsync();
            foreach(var rule in rules)
            {
                rulesResult.Add(rule.RuleValue
                            .Trim()
                            .Split('|')
                            .Select(x => x.Trim().Split(':').Select(y => y.Trim()).ToArray())
                            .ToDictionary(y => y[0], y => y[1]));
            }

            return rulesResult;
        }

        private Dictionary<string, double> MinimizeSubsetVector(double value, List<Subset> subsets)
        {
            var minimizedSubsetVector = new Dictionary<string, double>();

            for(int i = 0; i < subsets.Count - 1; i++)
            {
                if (i == 0 && value < subsets.ElementAt(i).Value)//beyond left border
                {
                    minimizedSubsetVector.Add(subsets.ElementAt(i).Key, 1.0);
                    continue;
                }

                if (minimizedSubsetVector.Count - 1 == i)//added on previous
                {
                    continue;
                }

                if (i == subsets.Count - 2 && value >= subsets.ElementAt(i + 1).Value)//beyond right border
                {
                    minimizedSubsetVector.Add(subsets.ElementAt(i).Key, 0.0);
                    minimizedSubsetVector.Add(subsets.ElementAt(i + 1).Key, 1.0);

                    break;
                }

                if (value >= subsets.ElementAt(i + 1).Value ||
                    value < subsets.ElementAt(i).Value)
                {
                    minimizedSubsetVector.Add(subsets.ElementAt(i).Key, 0.0);
                    continue;
                }

                var step_diff = Math.Abs(subsets.ElementAt(i + 1).Value - subsets.ElementAt(i).Value);
                var cost = 100 / step_diff;

                var valuePercent_1 = (100 - (Math.Abs(value - subsets.ElementAt(i).Value)) * cost) / 100;
                minimizedSubsetVector.Add(subsets.ElementAt(i).Key, valuePercent_1);

                var valuePercent_2 = (100 - (Math.Abs(subsets.ElementAt(i + 1).Value - value)) * cost) / 100;
                minimizedSubsetVector.Add(subsets.ElementAt(i + 1).Key, valuePercent_2);
            }

            if (subsets.Count - minimizedSubsetVector.Count == 1)
            {
                minimizedSubsetVector.Add(subsets.LastOrDefault().Key, 0.0);
            }

            return minimizedSubsetVector;
        }

        private Dictionary<string, double> ParseParams(string textParams)
        {
            var paramsDict = textParams
                            .Trim()
                            .Split('|')
                            .Select(x => x.Trim().Split(':').Select(y => y.Trim()).ToArray())
                            .ToDictionary(y => y[0], y => y[1]);

            var resultDict = new Dictionary<string, double>();
            foreach(var dictItem in paramsDict)
            {
                resultDict.Add(dictItem.Key, Double.Parse(dictItem.Value, CultureInfo.InvariantCulture));
            }

            return resultDict;
        }
    }
}
