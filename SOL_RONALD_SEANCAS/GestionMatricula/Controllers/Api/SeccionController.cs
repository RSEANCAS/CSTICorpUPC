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
    [RoutePrefix("api/seccion")]
    public class SeccionController : ApiController
    {
        SeccionBL seccionBL = new SeccionBL();

        [HttpGet]
        [Route("listarporcurso")]
        public HttpResponseMessage ListarPorCurso(int? cursoId)
        {
            List<SeccionCursoBE> lista = seccionBL.ListarPorCurso(cursoId);

            return Request.CreateResponse(HttpStatusCode.OK, lista);
        }
    }
}
