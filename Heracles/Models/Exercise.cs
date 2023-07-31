using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Heracles.Validations;

namespace Heracles.Models
{
    public class Exercise
    {        
        public int Id { get; set; }
        public string? Label { get; set; }
        public string? ImagePath { get; set; }
    }
}