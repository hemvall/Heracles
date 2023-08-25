using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
/*using Heracles.Validations;*/

namespace Heracles.Models
{
    public class Exercise
    {        
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? PR { get; set; }
        public int? DisplayPosition { get; set; }
        public string? Label { get; set; }
    }
}