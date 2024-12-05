using System.ComponentModel.DataAnnotations;

namespace ProjetoGaragemAS.Dtos
{
    public class PessoaCreateDto
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O nome pode ter no máximo 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O email deve ser válido.")]
        public string Email { get; set; } = string.Empty;
    }
}
