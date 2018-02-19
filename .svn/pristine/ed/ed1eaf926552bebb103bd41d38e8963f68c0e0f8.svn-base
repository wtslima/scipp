using System;
using System.Web;
using System.Web.Services.Description;
using System.Web.WebPages;

namespace INMETRO.CIPP.WEB.Helpers
{
    public static class HttpContextBaseExtensions
    {
        private const string chavePerfil = "PERFIL_USUARIO";
        private const string chaveUsuarioExterno = "IS_USUARIO_EXTERNO";
        private const string chaveOrganismo = "ORGANISMO_USUARIO";
        private const string chavePermiteAprovar = "PERMITE_APROVAR";
        private const string chavePermitePublicar = "PERMITE_PUBLICAR";

        public static string GetRole(this HttpContextBase context)
        {
            return context.Session[chavePerfil] == null ? null : context.Session[chavePerfil].ToString();
        }

        public static void SetRole(this HttpContextBase context, string value)
        {
            context.Session[chavePerfil] = value;
        }

        public static string GetRole(this HttpContext context)
        {
            return context.Session[chavePerfil] == null ? null : context.Session[chavePerfil].ToString();
        }

        public static void SetRole(this HttpContext context, string value)
        {
            context.Session[chavePerfil] = value;
        }

        public static string GetNameUsuarioAutenticado(this HttpContext context, string login)
        {
            if (!context.Request.IsAuthenticated)
                return String.Empty;

            //var response = new UsuarioService().ObterUsuario(new Service.Dto.FiltroUsuarioRequest(), login).Where(r => r.Login == login).FirstOrDefault();
            //return response == null ? login : response.Nome;
            return  string.Empty;
        }

        public static bool? GetUsuarioExterno(this HttpContextBase context)
        {
            return context.Session[chaveUsuarioExterno] == null ? default(bool?) : (bool)context.Session[chaveUsuarioExterno];
        }

        public static void SetUsuarioExterno(this HttpContextBase context, bool value)
        {
            context.Session[chaveUsuarioExterno] = value;
        }

        public static bool? GetUsuarioExterno(this HttpContext context)
        {
            return context.Session[chaveUsuarioExterno] == null ? default(bool?) : (bool)context.Session[chaveUsuarioExterno];
        }

        public static void SetUsuarioExterno(this HttpContext context, bool value)
        {
            context.Session[chaveUsuarioExterno] = value;
        }

        public static int? GetOrganismo(this HttpContext context)
        {
            return context.Session[chaveOrganismo] == null ? default(int?) : (int)context.Session[chaveOrganismo];
        }

        public static void SetOrganismo(this HttpContext context, int organismo)
        {
            context.Session[chaveOrganismo] = organismo;
        }

        public static bool? GetPermiteAprovar(this HttpContext context)
        {
            return context.Session[chavePermiteAprovar] == null ? default(bool?) : (bool)context.Session[chavePermiteAprovar];
        }

        public static void SetPermiteAprovar(this HttpContext context, bool permiteAprovar)
        {
            context.Session[chavePermiteAprovar] = permiteAprovar;
        }

        public static bool? GetPermitePublicar(this HttpContext context)
        {
            return context.Session[chavePermitePublicar] == null ? default(bool?) : (bool)context.Session[chavePermitePublicar];
        }

        public static void SetPermitePublicar(this HttpContext context, bool permiteAutorizarPublicacao)
        {
            context.Session[chavePermitePublicar] = permiteAutorizarPublicacao;
        }
    }
}