using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
/*using Heracles.Validations;*/

namespace Heracles.Models
{
    public class Performance
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ExerciseId { get; set; }
        public DateTime Date { get; set; }
        public int Weight { get; set; }
        public int Reps { get; set; }
        public int Sets { get; set; }
        public int Score { get; set; }
/*        public int Volume { get; set; }
*/    }
}