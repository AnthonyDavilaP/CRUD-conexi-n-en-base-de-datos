using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsEF6Demo.Models
{
    
    [Table("BODEGA")]
    public class Bodega
    {
        [Key]
        [Column("BOD_CODIGO")]
        public int Codigo { get; set; }

        [Required]
        [Column("BOD_DESCRIPCION")]
        [StringLength(50)]
        public string Descripcion { get; set; } = "";

        [Required]
        [Column("BOD_UBICACION")]
        [StringLength(100)]
        public string Ubicacion { get; set; } = "";

        
        [Column("BOD_RESPONSABLE")]
        [StringLength(120)]
        public string Responsable { get; set; } = "";
        
    }
}
