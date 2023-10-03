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
    [RoutePrefix("api/tipomatricula")]
    public class TipoMatriculaController : ApiController
    {
        TipoMatriculaBL tipoMatriculaBL = new TipoMatriculaBL();

        [HttpGet]
        [Route("listar")]
        public HttpResponseMessage Listar()
        {
            List<TipoMatriculaBE> lista = tipoMatriculaBL.Listar();

            return Request.CreateResponse(HttpStatusCode.OK, lista);
        }
    }
}
