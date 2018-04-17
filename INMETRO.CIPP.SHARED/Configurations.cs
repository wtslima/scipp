using System.Configuration;
using System.Linq;

namespace INMETRO.CIPP.SHARED
{
    public class Configurations
    {
        private static string GetValue(string key)
        {
            if (!ConfigurationManager.AppSettings.AllKeys.Contains(key))
                throw new ConfigurationException($"App Settings não contem a chave {key}");

            var value = ConfigurationManager.AppSettings[key];

            if (string.IsNullOrWhiteSpace(value))
                throw new ConfigurationException("Valor no AppSettings está nulo");

            return value;
        }
        public static string EmailAdministrador()
        {
            return GetValue("EmailAdministrador");
        }
        public static string DiretorioRaiz()
        {
            return GetValue("DiretorioRaiz");
        }
        public static string HoraAgendada()
        {
            return GetValue("HoraAgendadaDownload");
        }
        public static string MinutosAgendados()
        {
            return GetValue("MinutosAgendadosDownload");
        }
        public static string HostEmail()
        {
          return GetValue("HostMail");

        }
        public static string CredentialMailUsername()
        {
            return GetValue("Credentials.UserName");
        }
        public static string CredentialMailPassword()
        {
            return GetValue("Credentials.Senha");
        }

        public static string EmailSCIPPAdministrativo()
        {
            return GetValue("EmailSCIPP");
        }
    }
}