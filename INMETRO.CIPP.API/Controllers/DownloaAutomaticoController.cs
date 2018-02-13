using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using INMETRO.CIPP.SERVICOS.Interfaces;

namespace INMETRO.CIPP.API.Controllers
{
    public class DownloaAutomaticoController : ApiController
    {
        private readonly IDownloadServico _servico;
        public DownloaAutomaticoController(IDownloadServico servico)
        {
            _servico = servico;
        }

        [HttpGet]
        [Route("api/DownloadInspecoesPorRotinaAutomatica")]

        public IHttpActionResult DownloadInspecaoPorCipp()
        {
            try
            {

                if (!ModelState.IsValid)
                    return StatusCode(HttpStatusCode.BadRequest);


                var resultado = _servico.DownloadInspecoesPorRotinaAutomatica();

                return StatusCode(HttpStatusCode.OK);

            }
            catch
            {
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }


    }
}
