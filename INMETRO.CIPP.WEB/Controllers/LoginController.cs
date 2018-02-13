using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using INMETRO.CIPP.WEB.Models;
using INMETRO.CIPP.WEB.ControleAcesso;
using System.Linq;

namespace INMETRO.CIPP.WEB.Controllers
{
    public class LoginController : Controller
    {

        // GET: Login
        public ActionResult Login()
        {
            if (Request.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();
           
        }

        [HttpPost]
        public ActionResult Login(LogonModel model, String returnUrl)
        {


            //if (!ModelState.IsValid)
            //    return ToJsonError("_DetalheLogin", model);

            String usuarioCorrente = "";
            if (ModelState.IsValid)
            {

                var autenticacao = new AutenticacaoServicoClient("BasicHttpBinding_IAutenticacaoServico");
                var token = ConfigurationManager.AppSettings["accessToken"];

                try
                {

                    var usuario = autenticacao.Autenticar(token,
                        new Login { UserName = model.Usuario, Senha = model.Senha });

                    if (usuario != null)
                    {
                        CriarCookie(usuario.Nome.Substring(0, usuario.Nome.IndexOf(" ")),
                            usuario.Perfis.Select(p => p.CodigoPerfil.Trim()).ToList());
                        Session["userLogin"] = model.Usuario;
                        Session["Usuario"] = usuario.Nome;
                        usuarioCorrente = usuario.Nome;

                        if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/") &&
                            !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                        {
                            return RedirectToAction(returnUrl);
                        }
                    }

                    return RedirectToAction("Download", "Download", usuarioCorrente);

                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            return View("Login", usuarioCorrente);
        }
      

        private void CriarCookie(String nomeUsuario, List<string> perfis)
        {
            bool persisteCookie = false;

            persisteCookie = true;

            string MinutesTime = ConfigurationManager.AppSettings.Get("TempoConexao");
            int connecttime = 240;
            if (!int.TryParse(MinutesTime, out connecttime)) connecttime = 240;
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                1, nomeUsuario, DateTime.Now, DateTime.Now.AddMinutes(connecttime), persisteCookie,
                string.Join(",", perfis), FormsAuthentication.FormsCookiePath);
            string hash = FormsAuthentication.Encrypt(ticket);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);
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