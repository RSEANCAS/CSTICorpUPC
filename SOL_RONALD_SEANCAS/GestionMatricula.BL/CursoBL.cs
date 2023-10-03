using GestionMatricula.BE;
using GestionMatricula.DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionMatricula.BL
{
    public class CursoBL
    {
        public List<CursoBE> ListarPorDescripcion(string descripcion)
        {
            List<CursoBE> lista = null;

            try
            {
                if (descripcion != null) if (descripcion.Length < 3) return lista;

                using (DbContextUPC dbContext = new DbContextUPC())
                {
                    var query = (from a in dbContext.Curso
                                 where (a.Descripcion ?? "").ToLower().Contains(descripcion.ToLower())
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
    }
}
