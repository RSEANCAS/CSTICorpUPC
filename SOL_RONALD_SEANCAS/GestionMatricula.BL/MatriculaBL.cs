using GestionMatricula.BE;
using GestionMatricula.DA;
using GestionMatricula.UT;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionMatricula.BL
{
    public class MatriculaBL
    {
        public List<MatriculaBE> Listar(string nombresAlumno, int pageNumber, int pageSize, string sortName, string sortOrder, out int totalRows)
        {
            totalRows = 0;
            int pageOffSet = (pageNumber - 1) * pageSize;

            List<MatriculaBE> lista = null;

            try
            {
                using (DbContextUPC dbContext = new DbContextUPC())
                {
                    var query = (
                        from a in dbContext.Matricula
                        join b in dbContext.Seccion on a.SeccionId equals b.SeccionId
                        join c in dbContext.Curso on a.CursoId equals c.CursoId
                        join d in dbContext.Alumno on a.AlumnoId equals d.AlumnoId
                        join e in dbContext.TipoMatricula on a.TipoMatriculaId equals e.TipoMatriculaId
                        where ((d.Nombres ?? "").ToLower().Contains(nombresAlumno.ToLower()) || (d.Apellidos ?? "").ToLower().Contains(nombresAlumno.ToLower()) || string.IsNullOrEmpty(nombresAlumno))
                        select new
                        {
                            MatriculaId = a.MatriculaId,
                            SeccionId = a.SeccionId,
                            SeccionNombre = b.Nombre,
                            SeccionEstado = b.Estado,
                            CursoId = a.CursoId,
                            CursoDescripcion = c.Descripcion,
                            CursoCantidadCreditos = c.CantidadCreditos,
                            CursoEstado = c.Estado,
                            AlumnoId = a.AlumnoId,
                            AlumnoNombres = d.Nombres,
                            AlumnoApellidos = d.Apellidos,
                            AlumnoNumeroDocumentoIdentidad = d.NumeroDocumentoIdentidad,
                            AlumnoCodigo = d.Codigo,
                            AlumnoEstado = d.Estado,
                            TipoMatriculaId = a.TipoMatriculaId,
                            TipoMatriculaNombre = e.Nombre,
                            FechaMatricula = a.FechaMatricula,
                            FlagAnulado = a.FlagAnulado,
                            FechaAnulado = a.FechaAnulado,
                        }
                        );

                    totalRows = query.Count();

                    query = query
                        .OrderBy(sortName, sortOrder)
                        .Skip(pageOffSet)
                        .Take(pageSize);

                    lista = query
                        .ToList()
                        .Select(x => new MatriculaBE
                        {
                            MatriculaId = x.MatriculaId,
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
                            AlumnoId = x.AlumnoId,
                            Alumno = new AlumnoBE
                            {
                                AlumnoId = x.AlumnoId,
                                Nombres = x.AlumnoNombres,
                                Apellidos = x.AlumnoApellidos,
                                NumeroDocumentoIdentidad = x.AlumnoNumeroDocumentoIdentidad,
                                Codigo = x.AlumnoCodigo,
                                Estado = x.AlumnoEstado
                            },
                            TipoMatriculaId = x.TipoMatriculaId,
                            TipoMatricula = new TipoMatriculaBE
                            {
                                TipoMatriculaId = x.TipoMatriculaId,
                                Nombre = x.TipoMatriculaNombre
                            },
                            FechaMatricula = x.FechaMatricula,
                            FlagAnulado = x.FlagAnulado,
                            FechaAnulado = x.FechaAnulado
                        }).ToList();
                }
            }
            catch (EntitySqlException ex)
            {
                lista = null;
            }
            catch (Exception ex)
            {
                lista = null;
            }

            return lista;
        }

        public bool Guardar(MatriculaBE registro)
        {
            bool seGuardo = false;

            registro.FechaMatricula = DateTime.Now;

            try
            {
                using (DbContextUPC dbContext = new DbContextUPC())
                {
                    using (DbContextTransaction transaction = dbContext.Database.BeginTransaction())
                    {
                        DbRawSqlQuery<int> query = dbContext.Database.SqlQuery<int>("SELECT SEQ_UPC_MATRICULA.NEXTVAL FROM DUAL");

                        registro.MatriculaId = query.First();
                        registro = dbContext.Matricula.Add(registro);

                        SeccionCursoBE seccionCurso = (
                                                    from a in dbContext.SeccionCurso
                                                    where a.SeccionId == registro.SeccionId
                                                    && a.CursoId == registro.CursoId
                                                    select a
                                                  ).FirstOrDefault();

                        if (seccionCurso != null)
                        {
                            if (seccionCurso.CantidadVacantesUsadas < seccionCurso.CantidadVacantes)
                            {
                                seccionCurso.CantidadVacantesDisponibles -= 1;
                                seccionCurso.CantidadVacantesUsadas += 1;

                                seGuardo = true;
                            }
                            else seGuardo = false;
                        }
                        else seGuardo = false;

                        if (seGuardo)
                        {
                            transaction.Commit();
                        }
                        else transaction.Rollback();
                    }
                    dbContext.SaveChanges();
                }
            }
            catch (DbUpdateException ex)
            {
                seGuardo = false;
            }
            catch (EntitySqlException ex)
            {
                seGuardo = false;
            }
            catch (Exception ex)
            {
                seGuardo = false;
            }

            return seGuardo;
        }
    }
}
