using System;
using System.ComponentModel.DataAnnotations;

namespace CadastroProfessores.Model
{
    public class Professor
    {
        public int? Id { get; set; } = 0;

        [Required(ErrorMessage = "O nome do professor é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Nome do Professor")]
        public string Nome { get; set; }
    }
}
