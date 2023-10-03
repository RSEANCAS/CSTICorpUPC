using GestionMatricula.BE;
using GestionMatricula.DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionMatricula.BL
{
    public class SeccionBL
    {
        public List<SeccionCursoBE> ListarPorCurso(int? cursoId)
        {
            List<SeccionCursoBE> lista = null;

            try
            {
                if (!cursoId.HasValue) return lista;

                using (DbContextUPC dbContext = new DbContextUPC())
                {
                    var query = (from a in dbContext.SeccionCurso
                                 join b in dbContext.Seccion on a.SeccionId equals b.SeccionId
                                 where a.CursoId == cursoId.Value
                                 && a.CantidadVacantesDisponibles > 0
                                 && b.Estado == 1
                                 select new
                                 {
                                     a.SeccionId,
                                     SeccionNombre = a.Seccion.Nombre,
                                     SeccionEstado = a.Seccion.Estado,
                                     a.CursoId,
                                     CursoDescripcion = a.Curso.Descripcion,
                                     CursoCantidadCreditos = a.Curso.CantidadCreditos,
                                     CursoEstado = a.Curso.Estado,
                                     a.CantidadVacantes,
                                     a.CantidadVacantesDisponibles,
                                     a.CantidadVacantesUsadas,
                                 }
                        );

                    lista = query
                        .ToList()
                        .Select(x => new SeccionCursoBE
                        {
                            SeccionId = x.SeccionId,
                            Seccion = new SeccionBE
                            {
                                SeccionId = x.SeccionId,
                                Nombre = x.SeccionNombre,
                                Estado = x.SeccionEstado
                            },
                            CursoId = x.CursoId,
                            Curso = new CursoBE
                            {
                                CursoId = x.CursoId,
                                Descripcion = x.CursoDescripcion,
                                CantidadCreditos = x.CursoCantidadCreditos,
                                Estado = x.CursoEstado
                            },
                            CantidadVacantes = x.CantidadVacantes,
                            CantidadVacantesDisponibles = x.CantidadVacantesDisponibles,
                            CantidadVacantesUsadas = x.CantidadVacantesUsadas,
                        }).ToList();
                }
            }
            catch (Exception ex)
            {
                lista = null;
            }

            return lista;
        }
    }
}
