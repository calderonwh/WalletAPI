using System.ComponentModel.DataAnnotations;

namespace WalletAPI.Domain.Entities
{
    public class Wallet
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El documento es obligatorio."), MaxLength(20)]
        public string DocumentId { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "El balance no puede ser negativo.")]
        public decimal Balance { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
