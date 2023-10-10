using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
/*using Heracles.Validations;*/

namespace Heracles.Models
{
    public class Set
    {
        public int Id { get; set; }
        public int PerfId { get; set; }
        public int UserId { get; set; }
        public int Weight { get; set; }
        public int Reps { get; set; }
    }
}