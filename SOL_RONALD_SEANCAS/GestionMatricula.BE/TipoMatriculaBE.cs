using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionMatricula.BE
{
    [Table("UPC_TIPOMATRICULA")]
    public class TipoMatriculaBE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("TIPOMATRICULAID")]
        public int TipoMatriculaId { get; set; }
        [Column("NOMBRE")]
        public string Nombre { get; set; }
    }
}
