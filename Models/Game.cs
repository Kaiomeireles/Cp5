using System.ComponentModel.DataAnnotations;

namespace GameStoreMVC.Models
{
    public class Game
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O título é obrigatório")]
        [Display(Name = "Título")]
        public string Titulo { get; set; } = string.Empty;

        [Display(Name = "Descrição")]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "O gênero é obrigatório")]
        [Display(Name = "Gênero")]
        public string Genero { get; set; } = string.Empty;

        [Required(ErrorMessage = "O preço é obrigatório")]
        [Range(0.01, 9999.99, ErrorMessage = "Preço deve ser entre 0.01 e 9999.99")]
        [Display(Name = "Preço")]
        public decimal Preco { get; set; }

        [Display(Name = "URL da Imagem")]
        public string? ImagemUrl { get; set; }

        [Required(ErrorMessage = "A plataforma é obrigatória")]
        [Display(Name = "Plataforma")]
        public string Plataforma { get; set; } = string.Empty;

        [Display(Name = "Data de Lançamento")]
        [DataType(DataType.Date)]
        public DateTime? DataLancamento { get; set; }
    }
}

