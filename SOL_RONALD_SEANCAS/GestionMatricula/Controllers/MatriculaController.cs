using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionMatricula.Controllers
{
    [RoutePrefix("")]
    public class MatriculaController : Controller
    {
        [Route("index")]
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("registro")]
        public ActionResult Registro()
        {
            return View();
        }
    }
}