using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionMatricula.BE
{
    [Table("UPC_SECCION")]
    public class SeccionBE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("SECCIONID")]
        public int SeccionId { get; set; }
        [Column("NOMBRE")]
        public string Nombre { get; set; }
        [Column("ESTADO")]
        public short Estado { get; set; }
    }
}
