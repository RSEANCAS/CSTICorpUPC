using GestionMatricula.BE;
using GestionMatricula.DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionMatricula.BL
{
    public class TipoMatriculaBL
    {
        public List<TipoMatriculaBE> Listar()
        {
            List<TipoMatriculaBE> lista = null;

            try
            {
                using (DbContextUPC dbContext = new DbContextUPC())
                {
                    var query = (from a in dbContext.TipoMatricula
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
