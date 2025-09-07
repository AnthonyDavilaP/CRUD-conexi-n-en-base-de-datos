using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsEF6Demo.Models
{
    [Table("PRODUCTO")]
    public class Producto
    {      
        [Key]
        [Column("PRO_CODIGO")]
        public int Codigo { get; set; } 

        [Required]
        [Column("PRO_DESCRIPCION")]
        [StringLength(200)]
        public string Descripcion { get; set; } = "";

        [Required]
        [Column("PRO_ESTADO")]
        [StringLength(1)]
        public string Estado { get; set; } 

        [Required]
        [Column("PRO_PVP")]
        
        public decimal pvp { get; set; } 

        [Required]
        [Column("PRO_IVA")]
        
        public int iva { get; set; } 
    }
}
