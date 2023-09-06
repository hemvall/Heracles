using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
/*using Heracles.Validations;*/

namespace Heracles.Models
{
    public class ExerciseType
    {        
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Label { get; set; }
    }
}