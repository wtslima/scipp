using INMETRO.CIPP.DOMINIO.Modelos;

namespace INMETRO.CIPP.INFRA.ENTITYFRAMEWORK.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<CIPP.INFRA.ENTITYFRAMEWORK.Contexto>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Contexto context)
        {
            //context.Organismos.AddOrUpdate(
            //    x => x.Id,
            //new Organismo() { CodigoOIA = "7777", Nome = "TEST ORGANISMO - 2", EhAtivo = true},
            //new Organismo() { CodigoOIA = "8888", Nome = "TEST ORGANISMO - 2", EhAtivo = true },
            //new Organismo() { CodigoOIA = "9999", Nome = "TEST ORGANISMO - 4", EhAtivo = true});


            //context.IntegracaoInfo.AddOrUpdate(
            //    new FTPInfo() { OrganismoId = 1002, HostURI = $"ftp://rappdes01s:2112", Usuario = $"diois_ftp", Senha = $"d!@15#admin", DiretorioInspecao = $"/new-orders/", DiretorioInspecaoLocal = $"7777\\", DiretorioInspecaoRemoto = $"/New/" },
            //    new FTPInfo() { OrganismoId = 1003, HostURI = $"ftp://184.168.109.66:2112/", Usuario = $"nakoa_ftp", Senha = $"n@k@412!", DiretorioInspecao = $"/new-orders/", DiretorioInspecaoLocal = $"8888\\", DiretorioInspecaoRemoto = $"/New/" },
            //    new FTPInfo() { OrganismoId = 1004, HostURI = $"ftp://184.168.109.66:2111/", Usuario = $"cocomoon_ftp", Senha = $"c3e#22F!", DiretorioInspecao = $"/new-orders/", DiretorioInspecaoLocal = $"9999\\", DiretorioInspecaoRemoto = $"/New/" }
            //    );
        }
    }
}
