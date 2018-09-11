using INMETRO.CIPP.DOMINIO.Interfaces;
using INMETRO.CIPP.DOMINIO.Modelos;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using INMETRO.CIPP.WEB.Models;

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

            var user = HttpContext.Session["Usuario"];
            if (user == null)
                return RedirectToAction("Login", "Login");

            var organismos = _servico.BuscarTodos().
                Where(s => s.IntegracaoInfo != null).
                Select(s => s.IntegracaoInfo).
                OrderBy(s => s.DiretorioInspecaoLocal).
                ToList();

            return View(organismos);
        }

        public ActionResult Adicionar()
        {

            var user = HttpContext.Session["Usuario"];
            if (user == null)
                return RedirectToAction("Login", "Login");

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
        [ValidateAntiForgeryToken]
        public ActionResult Adicionar(IntegracaoInfoModel model)
        {
            //todo: corrigir retorno do codigo oia 

            if(!ModelState.IsValid)
            {
                model.Mensagem = new MensagemModel { ExisteExcecao = false, Mensagem = "Um erro ocorreu ao gravar a Integração." };
                return RedirectToAction("Adicionar");
            }
           
                var c = _servico.ObetrPorId(model.OrganismoId);

                var dominio = new IntegracaoInfos
                {
                    DiretorioInspecao = "INSPECOES",
                    DiretorioInspecaoLocal = c.CodigoOIA.Trim(),
                    HostURI = model.HostURI,
                    Senha = model.Senha,
                    Porta = model.Porta,
                    OrganismoId = model.OrganismoId,
                    TipoIntegracao = model.TipoIntegracao,
                    Usuario = model.Usuario
                };


                var resultado = _integracaoServico.Adicionar(dominio);

                if (resultado)
                {
                    var organismos = _servico.BuscarTodos().Where(s => s.IntegracaoInfo == null).OrderBy(s => s.Id).ToList();
                    organismos.Insert(0, new Organismo()
                    {
                        Id = model.OrganismoId,
                        CodigoOIA = model.DiretorioInspecaoLocal
                    });
                    ViewBag.Organismos = new SelectList(organismos, "Id", "CodigoOIA");
                    model.Mensagem = new MensagemModel { ExisteExcecao = resultado, Mensagem = "Integração gravada com sucesso." };
                    return View(model);
                }
                model.Mensagem = new MensagemModel { ExisteExcecao = resultado, Mensagem = "Um erro ocorreu ao gravar a Intergração." };
                return View(model);
           
            
        }

        public ActionResult Editar(int id)
        {

            var user = HttpContext.Session["Usuario"];
            if (user == null)
                return RedirectToAction("Login", "Login");

            if (id <= 0) { return HttpNotFound(); }
            var organismos = _servico.BuscarTodos().Where(s => s.IntegracaoInfo == null).OrderBy(s => s.Id).ToList();
            var i = _integracaoServico.ObterPorId(id);

            var model = new IntegracaoInfoModel
            {

                Id = i.Id,
                DiretorioInspecao = i.DiretorioInspecao,
                DiretorioInspecaoLocal = i.DiretorioInspecaoLocal,
                HostURI = i.HostURI,
                Porta = i.Porta,
                TipoIntegracao = i.TipoIntegracao,
                Usuario = i.Usuario,
                Senha = i.Senha,
                OrganismoId = i.OrganismoId

            };



            organismos.Insert(0,new Organismo()
            {
                Id = i.OrganismoId,
                CodigoOIA = i.DiretorioInspecaoLocal
            });
            ViewBag.Organismos = new SelectList(organismos, "Id", "CodigoOIA");

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(IntegracaoInfoModel i)
        {
            if (i.Id <= 0) return HttpNotFound();

          

            var dominio = new IntegracaoInfos
            {

                Id = i.Id,
                DiretorioInspecao = i.DiretorioInspecao,
                HostURI = i.HostURI,
                Porta = i.Porta,
                TipoIntegracao = i.TipoIntegracao,
                Usuario = i.Usuario,
                Senha = i.Senha,

            };
            var c = _servico.ObetrPorId(i.OrganismoId);
            dominio.DiretorioInspecaoLocal = c.CodigoOIA.Trim();
            dominio.OrganismoId = c.Id;

            var resultado = _integracaoServico.Atualizar(dominio);

            if (resultado)
            {
                var organismos = _servico.BuscarTodos().Where(s => s.IntegracaoInfo == null).OrderBy(s => s.Id).ToList();
                organismos.Insert(0, new Organismo()
                {
                    Id = dominio.OrganismoId,
                    CodigoOIA = dominio.DiretorioInspecaoLocal
                });
                ViewBag.Organismos = new SelectList(organismos, "Id", "CodigoOIA");
                i.Mensagem = new MensagemModel { ExisteExcecao = resultado, Mensagem = "A integração foi alterada com sucesso." };
                return View(i);
            }

            i.Mensagem = new MensagemModel { ExisteExcecao = resultado, Mensagem = "Houve um erro durante a alteração." };
            return View(i);


        }

        public ActionResult Excluir(int id)
        {
            var user = HttpContext.Session["Usuario"];
            if (user == null)
                return RedirectToAction("Login", "Login");

            var x = _integracaoServico.ObterPorId(id);
            var o = new IntegracaoInfos
            {
                Id = x.Id,
                DiretorioInspecaoLocal = x.DiretorioInspecaoLocal,
                HostURI = x.HostURI
            };

            return View(o);
        }

        [HttpPost]
        public ActionResult Excluir(IntegracaoInfos model)
        {
            var x = _integracaoServico.Desativar(model.Id);
            return View();
        }

        public ActionResult Detalhar(int id)
        {
            return View();
        }

       

       

    }
}