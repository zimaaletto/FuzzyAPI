
namespace Domain.Entities
{
    public class Term
    {
        public int Id { get; set; }
        public string TermName { get; set; }
        public virtual List<Subset> Subsets { get; set; }
        public int? FuzzyLogicAreaId { get; set; }
        public virtual FuzzyLogicArea FuzzyLogicArea { get; set; }
    }
}