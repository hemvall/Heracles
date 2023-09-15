using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
/*using Heracles.Validations;
*/
namespace Heracles.Models
{
    public class Goal
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? ExerciseId { get; set; }
        public string? Label { get; set; }
        public int? Data { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime Deadline { get; set; }
        public bool IsActive { get; set; }
    }
}