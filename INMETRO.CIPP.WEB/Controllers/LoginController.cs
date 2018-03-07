using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using INMETRO.CIPP.WEB.Models;
using INMETRO.CIPP.WEB.ControleAcesso;


namespace INMETRO.CIPP.WEB.Controllers
{
    public class LoginController : Controller
    {

        // GET: Login
        public ActionResult Login()
        {

            var user = HttpContext.Session["Usuario"];
            if (user != null)
                return RedirectToAction("Index", "Home");

            return View();
           
        }

        [HttpPost]
        public ActionResult Login(LogonModel model, string returnUrl)
        {

            var usuarioCorrente = "";
            if (ModelState.IsValid)
            {

                var autenticacao = new AutenticacaoServicoClient("BasicHttpBinding_IAutenticacaoServico");
                var token = ConfigurationManager.AppSettings["accessToken"];

                try
                {
                    //var usuario = autenticacao.Autenticar(token,
                    //    new Login { UserName = model.Usuario, Senha = model.Senha });

                    //if (usuario != null)
                    //{
                    //    CriarCookie(usuario.Nome.Substring(0, usuario.Nome.IndexOf(" ")),
                    //        usuario.Perfis.Select(p => p.CodigoPerfil.Trim()).ToList());
                    //    Session["userLogin"] = model.Usuario;
                    //    Session["Usuario"] = usuario.Nome;


                    //    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/") &&
                    //        !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    //    {
                    //        return RedirectToAction(returnUrl);
                    //    }
                    //}

                    Session["Usuario"] = "Wellington";
                    usuarioCorrente = Session["Usuario"].ToString();
                    return RedirectToAction("Download", "Download", usuarioCorrente);

                }
                catch (Exception ex)
                {
                    var exception = new ExceptionSystem();
                    if (ex.InnerException != null) exception.Mensagem = ex.InnerException.ToString();
                    return PartialView("_Error", exception);
                }
            }

            return View("Login", usuarioCorrente);
        }
      
        private void CriarCookie(string nomeUsuario, IEnumerable<string> perfis)
        {
            var minutesTime = ConfigurationManager.AppSettings.Get("TempoConexao");
            if (!int.TryParse(minutesTime, out var connecttime)) connecttime = 240;
            var ticket = new FormsAuthenticationTicket(
                1, nomeUsuario, DateTime.Now, DateTime.Now.AddMinutes(connecttime), true,
                string.Join(",", perfis), FormsAuthentication.FormsCookiePath);
            var hash = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);
            if (ticket.IsPersistent) cookie.Expires = ticket.Expiration;

            Response.Cookies.Add(cookie);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            HttpContext.Session.Clear();

            return RedirectToAction("Login", "Login");
        }
    }
}