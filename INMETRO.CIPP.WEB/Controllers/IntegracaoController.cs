using INMETRO.CIPP.DOMINIO.Interfaces;
using INMETRO.CIPP.DOMINIO.Modelos;
using INMETRO.CIPP.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace INMETRO.CIPP.WEB.Controllers
{
    public class IntegracaoController : Controller
    {
        private readonly IOrganismoDominioService _servico;
        public IntegracaoController(IOrganismoDominioService servico)
        {
            _servico = servico;
        }
        // GET: Integracao
        public ActionResult Index()
        {
            var organismos = _servico.BuscarTodos().
                Where(s => s.IntegracaoInfo != null).
                Select(s => s.IntegracaoInfo).
                OrderBy(s => s.DiretorioInspecaoLocal).
                ToList();
           
            return View(organismos);
        }

        public ActionResult Adicionar()
        {
            var organismos = _servico.BuscarTodos().Where(s => s.IntegracaoInfo == null).OrderBy(s => s.Id).ToList();

            organismos.Insert(0, new Organismo()
            {
                Id = 0,
                CodigoOIA = "- Selecione -"
            });
            ViewBag.Organismos = new SelectList(organismos, "Id", "CodigoOIA");
            var t = TipoLista();
            t.Insert(0,  new Tipo()
            {
                Id = 0,
                TipoIntegracao = "- Selecione -"
            });
            ViewBag.Tipo = new SelectList(t, "Id", "TipoIntegracao");
            // ViewBag.Organismos = o;
            return View();
        }
        [HttpPost]
        public ActionResult Adicionar(IntegracaoInfos model)
        {
            return View(model);
        }

        public ActionResult Editar(int id)
        {
            if (id == null) { return HttpNotFound(); }
            var organismo = _servico.ObetrPorId(id).IntegracaoInfo;

            var x =new Organismo()
            {
                Id = organismo.OrganismoId,
                CodigoOIA = organismo.DiretorioInspecaoLocal
            };
            ViewBag.Organismos = new SelectList("Id", "CodigoOIA");
            
            var t = new Tipo()
            {
                Id = organismo.TipoIntegracao,
                TipoIntegracao = ""
            };
            ViewBag.Tipo = new SelectList( "Id", "TipoIntegracao");

            
            return View(organismo);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Organismo organismo)
        {
            if (organismo.Id <= 0) return HttpNotFound();

            var resultado = _servico.Atualizar(organismo);
            if (resultado)
                return RedirectToAction("Index");

            return View(organismo);
        }

      

        public ActionResult Excluir()
        {
            return View();
        }
        [HttpPut]
        public ActionResult Excluir(int  id)
        {
            return View();
        }

        public ActionResult Detalhar(int id)
        {
            return View();
        }

        private List<Tipo> TipoLista()
        {
            var item = new List<Tipo>();

            item.Add(new Tipo { Id = 1, TipoIntegracao = "FTP" });

            item.Add(new Tipo { Id = 2, TipoIntegracao = "SFTP" });

            return item;

        }


        public class Tipo
        {
            public Tipo()
            {

            }

            public int Id { get; set; }
            public string TipoIntegracao { get; set; }
        }

    }
}