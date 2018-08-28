using INMETRO.CIPP.DOMINIO.Interfaces;
using INMETRO.CIPP.DOMINIO.Interfaces.Repositorios;
using INMETRO.CIPP.DOMINIO.Modelos;
using INMETRO.CIPP.WEB.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace INMETRO.CIPP.WEB.Controllers
{
    public class OrganismoController : Controller
    {
        private readonly IOrganismoDominioService _servico;

        public OrganismoController(IOrganismoDominioService servico)
        {
            _servico = servico;
        }
        // GET: Organismo
        public ActionResult Index()
        {
            var organismos =  _servico.BuscarTodos();
            //var convertido = Conversao.Converter.ConverterParaModelo(organismos);
            return View(organismos);
        }

       
        public ActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Adicionar(Organismo organismo)
        {
            if (organismo == null) return HttpNotFound();

            var resultado = _servico.Adicionar(organismo);
            if (resultado)
                return RedirectToAction("Index");

            return View(organismo);
            
        }

        public ActionResult Editar(int id)
        {
            if (id == null) { return HttpNotFound(); }

            var organismo = _servico.ObetrPorId(id);
            return View(organismo);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Organismo organismo)
        {
            if (organismo.Id <= 0) return HttpNotFound();

            var resultado = _servico.Atualizar(organismo);
            if(resultado)
                return RedirectToAction("Index");

            return View(organismo);
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

        //private async Task<List<OrganismoModel>> ObterOrganismos()
        //{
        //    var organismos = await _servico.BuscarTodosOrganismos();
            
        //    return  Conversao.Converter.ConverterParaModelo(organismos);
        //}
    }
}