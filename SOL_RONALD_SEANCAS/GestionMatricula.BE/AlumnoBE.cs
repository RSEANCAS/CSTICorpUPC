using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionMatricula.BE
{
    [Table("UPC_ALUMNO")]
    public class AlumnoBE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ALUMNOID")]
        public int AlumnoId { get; set; }
        [Column("NUMERODOCUMENTOIDENTIDAD")]
        public string NumeroDocumentoIdentidad { get; set; }
        [Column("CODIGO")]
        public string Codigo { get; set; }
        [Column("NOMBRES")]
        public string Nombres { get; set; }
        [Column("APELLIDOS")]
        public string Apellidos { get; set; }
        [Column("ESTADO")]
        public short Estado { get; set; }
    }
}
