using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionMatricula.BE
{
    [Table("UPC_MATRICULA")]
    public class MatriculaBE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("MATRICULAID")]
        public int MatriculaId { get; set; }
        [Column("SECCIONID")]
        public int SeccionId { get; set; }
        [ForeignKey("SeccionId")]
        public virtual SeccionBE Seccion { get; set; }
        [Column("CURSOID")]
        public int CursoId { get; set; }
        [ForeignKey("CursoId")]
        public virtual CursoBE Curso { get; set; }
        [Column("ALUMNOID")]
        public int AlumnoId { get; set; }
        [ForeignKey("AlumnoId")]
        public virtual AlumnoBE Alumno { get; set; }
        [Column("TIPOMATRICULAID")]
        public int TipoMatriculaId { get; set; }
        [Column("TipoMatriculaId")]
        public virtual TipoMatriculaBE TipoMatricula { get; set; }
        [Column("FECHAMATRICULA")]
        public DateTime FechaMatricula { get; set; }
        [Column("FLAGANULADO")]
        public short FlagAnulado { get; set; }
        [Column("FECHAANULADO")]
        public DateTime? FechaAnulado { get; set; }
    }
}
