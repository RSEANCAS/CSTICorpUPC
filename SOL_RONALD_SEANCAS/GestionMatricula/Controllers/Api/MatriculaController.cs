using GestionMatricula.BE;
using GestionMatricula.BE.Custom;
using GestionMatricula.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GestionMatricula.Controllers.Api
{
    [RoutePrefix("api/matricula")]
    public class MatriculaController : ApiController
    {
        MatriculaBL matriculaBL = new MatriculaBL();

        [HttpGet]
        [Route("listar")]
        public HttpResponseMessage ListarPorNombre(string nombresAlumno, int pageNumber, int pageSize, string sortName, string sortOrder, int draw)
        {
            List<MatriculaBE> lista = matriculaBL.Listar(nombresAlumno, pageNumber, pageSize, sortName, sortOrder, out int totalRows);

            return Request.CreateResponse(HttpStatusCode.OK, new DataTableCustom<MatriculaBE> { data = lista, draw = draw, recordsFiltered = totalRows, recordsTotal = totalRows });
        }

        [HttpPost]
        [Route("guardar")]
        public HttpResponseMessage Guardar(MatriculaBE registro)
        {
            bool seGuardo = matriculaBL.Guardar(registro);

            return Request.CreateResponse(HttpStatusCode.OK, seGuardo);
        }
    }
}
