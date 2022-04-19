
namespace Domain.Entities
{
    public class Subset
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public double Value { get; set; }
        public int? TermId { get; set; }
        public virtual Term Term { get; set; }
    }
}