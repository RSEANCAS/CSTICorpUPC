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
    [RoutePrefix("api/curso")]
    public class CursoController : ApiController
    {
        CursoBL cursoBL = new CursoBL();

        [HttpGet]
        [Route("listarpornombre")]
        public HttpResponseMessage ListarPorNombre(string descripcion)
        {
            List<CursoBE> lista = cursoBL.ListarPorDescripcion(descripcion);

            return Request.CreateResponse(HttpStatusCode.OK, lista);
        }
    }
}