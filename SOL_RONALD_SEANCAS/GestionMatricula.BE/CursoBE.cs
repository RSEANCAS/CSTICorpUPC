using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionMatricula.BE
{
    [Table("UPC_CURSO")]
    public class CursoBE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("CURSOID")]
        public int CursoId {get;set;}
        [Column("DESCRIPCION")]
        public string Descripcion { get; set; }
        [Column("CANTIDADCREDITOS")]
        public int CantidadCreditos { get; set; }
        [Column("ESTADO")]
        public short Estado { get; set; }
    }
}
