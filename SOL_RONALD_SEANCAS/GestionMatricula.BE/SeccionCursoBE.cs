using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionMatricula.BE
{
    [Table("UPC_SECCIONCURSO")]
    public class SeccionCursoBE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("SECCIONID", Order = 0)]
        public int SeccionId { get; set; }
        [ForeignKey("SeccionId")]
        public virtual SeccionBE Seccion { get; set; }
        [Column("CURSOID", Order = 1)]
        public int CursoId { get; set; }
        [ForeignKey("CursoId")]
        public virtual CursoBE Curso { get; set; }
        [Column("CANTIDADVACANTES")]
        public int CantidadVacantes { get; set; }
        [Column("CANTIDADVACANTESDISPONIBLES")]
        public int CantidadVacantesDisponibles { get; set; }
        [Column("CANTIDADVACANTESUSADAS")]
        public int CantidadVacantesUsadas { get; set; }
    }
}
