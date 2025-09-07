using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WinFormsEF6Demo.Models
{
    [Table("USUARIO")]                       
    public class Usuario
    {
        [Key]                                
        [Column("USU_LOGIN")]
        [StringLength(30)]
        public string Login { get; set; } = "";

        [Required]
        [Column("USU_PASSWORD")]
        [StringLength(200)]                 
        public string PasswordHash { get; set; } = "";

        [Required]
        [Column("USU_ESTADO")]
        [StringLength(1)]
        public string Estado { get; set; } = "A";  

        [NotMapped]
        public bool Activo => Estado == "A";
    }
}
