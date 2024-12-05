using System.ComponentModel.DataAnnotations;

namespace ProjetoGaragemAS.Dtos
{
    public class CarroUpdateDto
    {
        [Required(ErrorMessage = "A marca é obrigatória.")]
        public string Marca { get; set; } = string.Empty;

        [Required(ErrorMessage = "O modelo é obrigatório.")]
        public string Modelo { get; set; } = string.Empty;

        [Range(1900, int.MaxValue, ErrorMessage = "O ano deve ser 1900 ou maior.")]
        public int Ano { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
        public decimal Preco { get; set; }
    }
}
