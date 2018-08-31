using INMETRO.CIPP.DOMINIO.Interfaces;
using INMETRO.CIPP.DOMINIO.Modelos;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace INMETRO.CIPP.WEB.Controllers
{
    public class IntegracaoController : Controller
    {
        private readonly IOrganismoDominioService _servico;
        private readonly IIntegracaoInfo _integracaoServico;
        public IntegracaoController(IOrganismoDominioService servico, IIntegracaoInfo integracao)
        {
            _servico = servico;
            _integracaoServico = integracao;
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

            return View();
        }
        [HttpPost]
        public ActionResult Adicionar(IntegracaoInfos model)
        {
            //todo: corrigir retorno do codigo oia 
            var c = _servico.ObetrPorId(model.OrganismoId);
            model.DiretorioInspecaoLocal = c.CodigoOIA.Trim();
            model.DiretorioInspecao = "INSPECOES";


            var resultado = _integracaoServico.Adicionar(model);

            if (resultado)
            {
                var organismos = _servico.BuscarTodos().Where(s => s.IntegracaoInfo == null).OrderBy(s => s.Id).ToList();
                organismos.Insert(0, new Organismo()
                {
                    Id = model.OrganismoId,
                    CodigoOIA = model.DiretorioInspecaoLocal
                });
                ViewBag.Organismos = new SelectList(organismos, "Id", "CodigoOIA");
                return View(model);
            }

            return View(model);
        }

        public ActionResult Editar(int id)
        {
            if (id <= 0) { return HttpNotFound(); }
             var organismos = _servico.BuscarTodos().Where(s => s.IntegracaoInfo == null).OrderBy(s => s.Id).ToList();
            var i = _integracaoServico.ObterPorId(id);
            var x = new Organismo()
            {
                Id = i.OrganismoId,
                CodigoOIA = i.DiretorioInspecaoLocal
            };
            ViewBag.Organismos = new SelectList(organismos,"Id", "CodigoOIA");

            return View(i);
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
        public ActionResult Excluir(int id)
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