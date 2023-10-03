using GestionMatricula.BE;
using GestionMatricula.DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionMatricula.BL
{
    public class AlumnoBL
    {
        public List<AlumnoBE> ListarPorNombre(string nombres)
        {
            List<AlumnoBE> lista = null;

            try
            {
                if (nombres != null) if (nombres.Length < 3) return lista;

                using (DbContextUPC dbContext = new DbContextUPC())
                {
                    var query = (from a in dbContext.Alumno
                                 where ((a.Nombres ?? "").ToLower().Contains(nombres.ToLower()) || (a.Apellidos ?? "").ToLower().Contains(nombres.ToLower()) || (a.NumeroDocumentoIdentidad??"").ToLower().Contains(nombres))
                                 && a.Estado == 1
                                 select a
                        );

                    lista = query.ToList();
                }
            }
            catch (Exception ex)
            {
                lista = null;
            }

            return lista;
        }

        public bool ExisteMatricula(int alumnoId, int? cursoId, int? seccionId)
        {
            bool existe = false;

            try
            {
                using (DbContextUPC dbContext = new DbContextUPC())
                {
                    var query = (from a in dbContext.Matricula
                                 where a.AlumnoId == alumnoId
                                 && a.CursoId == a.CursoId
                                 && a.SeccionId == a.SeccionId
                                 && a.FlagAnulado == 0
                                 select a
                        );

                    existe = query.Count() > 0;
                }
            }
            catch (Exception ex)
            {
                existe = false;
            }

            return existe;
        }
    }
}