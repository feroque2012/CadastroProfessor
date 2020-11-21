using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CadastroProfessores.Model
{
    public class Aluno
    {
        public int? Id { get; set; } = 0;

        [Required(ErrorMessage = "O nome do Aluno é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Nome do Aluno")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O Valor da Mensalidade é obrigatório")]
        [Display(Name = "Valor Mensalidade")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public float? VlrMensalidade { get; set; }

        [Required(ErrorMessage = "A Data de Vencimento é obrigatória")]
        [Display(Name = "Data de Vencimento")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DtVencimento { get; set; }

        public int IdProfessor { get; set; }

        [Display(Name = "Nome do Professor")]
        public string NomeProfessor { get; set; }
    }
}
