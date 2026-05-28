using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace investment_service.Models
{
    [Table("SecUsuarios")]
    public class SecUsuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int nIdUsuario { get; set; }
        public int? nIdPersona { get; set; }
        [Required]
        [MaxLength(60)]
        public string cContrasena { get; set; } = string.Empty;
        [Required]
        public int nEstado { get; set; }
        [Required]
        public int nIdPerfil { get; set; }
        [MaxLength(20)]
        public string? cAuditoria { get; set; }
    }
}