using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using INMETRO.CIPP.WEB.Helpers;

namespace INMETRO.CIPP.WEB.Controllers
{
    public class CippController : Controller
    {
        //protected List<ContatoModel> ContatosListSession
        //{
        //    get
        //    {
        //        HttpContext.Session["ContatoListSession"] = HttpContext.Session["ContatoListSession"] ?? new List<ContatoModel>();
        //        return HttpContext.Session["ContatoListSession"] as List<ContatoModel>;

        //    }
        //    set
        //    {
        //        HttpContext.Session["ContatoListSession"] = value;

        //    }
        //}

        protected JsonResult ToJsonSuccess()
        {
            return ToJsonError(null, true, null, null, null);
        }

        protected JsonResult ToJsonSuccess(string content)
        {
            return ToJsonError(content, true, null, null, null);
        }

        protected JsonResult ToJsonError(string content)
        {
            return ToJsonError(content, false, null, null, null);
        }

        protected JsonResult ToJsonSuccess(string content, string viewName, object model)
        {
            return ToJsonError(content, true, viewName, model, null);
        }

        protected JsonResult ToJsonError(string content, string viewName, object model)
        {
            return ToJsonError(content, false, viewName, model, null);
        }

        protected JsonResult ToJsonSuccess(string content, string viewName, object model, object other)
        {
            return ToJsonError(content, true, viewName, model, other);
        }

        protected JsonResult ToJsonError(string content, string viewName, object model, object other)
        {
            return ToJsonError(content, false, viewName, model, other);
        }

        protected JsonResult ToJsonSuccess(string viewName, object model)
        {
            return ToJsonError(null, true, viewName, model, null);
        }

        protected JsonResult ToJsonError(string viewName, object model)
        {
            return ToJsonError(null, false, viewName, model, null);
        }

        protected JsonResult ToJsonError(string content, bool success, string viewName, object model, object other)
        {
            var renderedView = string.IsNullOrEmpty(viewName) ? null : PartialView(viewName, model).RenderToString();

            return Json(new { Success = success, Content = content, View = renderedView, Other = other });
        }

        protected IEnumerable<SelectListItem> ToSelectList<T>(Func<T, int> valor, Func<T, string> texto, bool possuiSelecione = true)
        {
            return ToSelectList<int, T>(new List<T>(), valor, texto, possuiSelecione);
        }

        protected IEnumerable<SelectListItem> ToSelectList<T>(IEnumerable<T> repositorio, Func<T, int> valor, Func<T, string> texto, bool possuiSelecione = true)
        {
            return ToSelectList<int, T>(repositorio, valor, texto, possuiSelecione);
        }

        protected IEnumerable<SelectListItem> ToSelectList<TKey, T>(IEnumerable<T> repositorio, Func<T, TKey> valor, Func<T, string> texto, bool possuiSelecione = true)
        {
            var itens = new List<SelectListItem>();

            if (repositorio != null)
            {
                foreach (var entidade in repositorio)
                {
                    var x = valor(entidade).ToString();
                    var y = texto(entidade);

                    var item = new SelectListItem { Text = y, Value = x };

                    itens.Add(item);
                }

                itens = itens.OrderBy(x => x.Text).ToList();
            }

            if (itens.Count != 1 && possuiSelecione)
                itens.Insert(0, new SelectListItem { Value = "", Text = "Selecione" });
            else if (itens.Any())
                itens.First().Selected = true;

            if (itens.Count == 1 && itens[0].Value == String.Empty)
                itens[0].Text = "Nenhum item disponível";

            return itens;
        }

        protected IEnumerable<SelectListItem> ToSelectList<TKey, T>(T repositorio, Func<T, TKey> valor, Func<T, string> texto)
            where T : class
        {
            var itens = new List<SelectListItem>();

            if (repositorio != null)
            {
                var x = valor(repositorio).ToString();
                var y = texto(repositorio);

                var item = new SelectListItem { Text = y, Value = x };

                itens.Add(item);
            }

            return itens;
        }

        protected IEnumerable<SelectListItem> ToEmptySelectList(string textoSelecione)
        {
            var lista = new List<SelectListItem>();
            lista.Add(new SelectListItem() { Value = "", Text = textoSelecione, Selected = true });
            return lista;
        }

        protected string NomeUsuario
        {
            get { return Session["NOME_USUARIO"] == null ? null : Session["NOME_USUARIO"].ToString(); }
            set { HttpContext.Session["NOME_USUARIO"] = value; }
        }

        //protected bool PermiteAprovar
        //{
        //    get { return SecurityHelper.GetPermiteAprovar(); }
        //}

        //protected bool PermitePublicar
        //{
        //    get { return SecurityHelper.GetPermitePublicar(); }
        //}

        protected string PerfilUsuario
        {
            get { return HttpContext.GetRole(); }
            set { HttpContext.SetRole(value); }
        }

        protected string LoginUsuario
        {
            get { return HttpContext.User.Identity.Name; }
        }

        protected bool PermiteAcesso(IEnumerable<string> perfis)
        {
            return perfis.Any(p => p.Equals(PerfilUsuario, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}