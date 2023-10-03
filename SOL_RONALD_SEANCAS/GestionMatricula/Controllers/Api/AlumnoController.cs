using GestionMatricula.BE;
using GestionMatricula.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GestionMatricula.Controllers.Api
{
    [RoutePrefix("api/alumno")]
    public class AlumnoController : ApiController
    {
        AlumnoBL alumnoBL = new AlumnoBL();

        [HttpGet]
        [Route("listarpornombre")]
        public HttpResponseMessage ListarPorNombre(string nombres)
        {
            List<AlumnoBE> lista = alumnoBL.ListarPorNombre(nombres);

            return Request.CreateResponse(HttpStatusCode.OK, lista);
        }

        [HttpGet]
        [Route("existematricula")]
        public HttpResponseMessage ExisteMatricula(int alumnoId, int? cursoId, int? seccionId)
        {
            bool existe = alumnoBL.ExisteMatricula(alumnoId, cursoId, seccionId);

            return Request.CreateResponse(HttpStatusCode.OK, existe);
        }
    }
}
