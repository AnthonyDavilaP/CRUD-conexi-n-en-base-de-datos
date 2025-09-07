using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WinFormsEF6Demo.Models
{
    [Table("CLIENTE")]
    public class Cliente
    {
        [Column("CLI_CODIGO")]
        public int ClienteId { get; set; }

        [Column("CLI_NOMBRE")]
        [Required, StringLength(120)]
        public string Nombre { get; set; } = string.Empty;

        [Column("CLI_EMAIL")]
        [StringLength(120)]
        public string Email { get; set; } = string.Empty;
    }
}
