using INMETRO.CIPP.DOMINIO.Interfaces.Repositorios;
using INMETRO.CIPP.WEB.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace INMETRO.CIPP.WEB.Controllers
{
    public class OrganismoController : Controller
    {
        private readonly IOrganismoRepositorio _repositorio;

        public OrganismoController(IOrganismoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        // GET: Organismo
        public ActionResult Index()
        {
            var organismos = ObterOrganismos();
            return View(organismos);
        }

       
        public ActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Adicionar(OrganismoModel model)
        {
            return View(model);
        }

        public ActionResult Editar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(OrganismoModel model)
        {
            return View(model);
        }

        public ActionResult Excluir()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Excluir(OrganismoModel model)
        {
            return View(model);
        }

        public ActionResult Detalhar()
        {
            return View();
        }

        private IList<OrganismoModel> ObterOrganismos()
        {
            var organismos =  _repositorio.BuscarTodosOrganismos().Result;

            return Conversao.Converter.ConverterParaModelo(organismos);
        }
    }
}