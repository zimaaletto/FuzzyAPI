
namespace Domain.Entities
{
    public class Rule
    {
        public int Id { get; set; }
        public string RuleValue { get; set; }
        public int? FuzzyLogicAreaId { get; set; }
        public virtual FuzzyLogicArea FuzzyLogicArea { get; set; }
    }
}