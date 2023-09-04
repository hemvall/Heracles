using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
/*using Heracles.Validations;
*/
namespace Heracles.Models
{
    public class Unit
    {
        public int Id { get; set; }
        public int? LanguageId { get; set; }
        public string? Label { get; set; }
        public string? Code { get; set; }
    }
}