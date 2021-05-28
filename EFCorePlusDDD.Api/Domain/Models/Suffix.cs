using System.Linq;

namespace EFCorePlusDDD.Api.Domain.Models
{
    //Enumeration entity
    public class Suffix : Entity
    {
        public static readonly Suffix Jr = new Suffix(1, "Jr");
        public static readonly Suffix Sr = new Suffix(2, "Sr");

        public static readonly Suffix[] AllSuffixes = {Jr, Sr};

        protected Suffix(){}

        private Suffix(long id, string name) : base(id)
        {
            Name = name;
        }

        public string Name { get; }

        public static Suffix FromId(long id)
        {
            return AllSuffixes.SingleOrDefault(s => s.Id == id);
        }
    }
}