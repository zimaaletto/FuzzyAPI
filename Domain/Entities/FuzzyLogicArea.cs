
namespace Domain.Entities
{
    public class FuzzyLogicArea
    {
        public int Id { get; set; }
        public string AreaName { get; set; }
        public virtual List<Term> Terms { get; set; }
        public virtual List<Rule> Rules { get; set; }
    }
}