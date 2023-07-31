using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Heracles.Validations
{
    public class ImageAttribute : ValidationAttribute
    {
        private readonly List<string> extensions = new List<string>{"png", "jpeg", "jpg"};
         protected override ValidationResult IsValid(object? value, ValidationContext context)
         {
            if(!string.IsNullOrEmpty(value?.ToString())){
                string extensao = value?.ToString().Split('.')[1];

                if(!extensions.Contains(extensao) || extensao is null)
                    return new ValidationResult("O formato de extensão da foto não é permitido. Apenas aceitamos png, jpeg ou jpg.");

                return ValidationResult.Success;
            }

            return new ValidationResult("É necessário informar a foto e sua extensão");
         }
    }
}