using INMETRO.CIPP.DOMINIO.Modelos;
using System.Data.Entity.Migrations;

namespace INMETRO.CIPP.INFRA.ENTITYFRAMEWORK.Migrations
{
   
    internal sealed class Configuration : DbMigrationsConfiguration<CippContexto>

    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CippContexto context)
        {
            #region Organismos

            //context.Organismos.AddOrUpdate(
            //    x => x.Id,
            //    new Organismo() { CodigoOIA = 0016, Nome = "QUALITY INSPE��O VEICULAR LTDA. - EPP." },
            //    new Organismo() { CodigoOIA = 0082, Nome = "TRANSTECH IVESUR BRASIL LTDA." },
            //    new Organismo() { CodigoOIA = 0112, Nome = "Tiririca Inspe��o e Seguran�a Veicular Ltda" },
            //    new Organismo() { CodigoOIA = 0142, Nome = "Inscape Inspe��es Ltda" },
            //    new Organismo() { CodigoOIA = 0224, Nome = "Tecsul Inspe��o Veicular Ltda." },
            //    new Organismo() { CodigoOIA = 0257, Nome = "ITTRAN - Instituto Tecnol�gico de Transporte Ltda." },
            //    new Organismo() { CodigoOIA = 0269, Nome = "CENTRAL PORTO ALEGRE DE INSPE��ES VEICULARES LTDA." },
            //    new Organismo() { CodigoOIA = 0279, Nome = "NACIONAL INSPE��ES LTDA." },
            //    new Organismo() { CodigoOIA = 0289, Nome = "EMBRAVEC - EMPRESA BRASILEIRA DE INSPE��O VEICULAR LTDA. - EPP." },
            //    new Organismo() { CodigoOIA = 0294, Nome = "WEST ENGENHARIA DE INSPE��O LTDA." },
            //    new Organismo() { CodigoOIA = 0307, Nome = "SBI-RS - INSPE��ES DE TANQUES E RESERVAT�RIOS LTDA." },
            //    new Organismo() { CodigoOIA = 0319, Nome = "SBI - SERRANA INSPE��ES DE PRODUTOS PERIGOSOS LTDA." },
            //    new Organismo() { CodigoOIA = 0322, Nome = "BBI SERVI�OS DE INSPE��O VEICULAR LTDA." },
            //    new Organismo() { CodigoOIA = 0328, Nome = "INSPECENTRO INSPE��O VEICULAR EIRELI - ME." },
            //    new Organismo() { CodigoOIA = 0351, Nome = "ITESV - Instituto Tecnologia de Engenharia e Seguran�a Veicular Ltda." },
            //    new Organismo() { CodigoOIA = 0368, Nome = "BUREAU BRASILEIRO DE INSPE��O LTDA" },
            //    new Organismo() { CodigoOIA = 0420, Nome = "ATIVE OURINHOS INSPE��ES VEICULARES LTDA." },
            //    new Organismo() { CodigoOIA = 0424, Nome = "IVECAR CENTRO INSPE��O VEICULAR LTDA.	" },
            //    new Organismo() { CodigoOIA = 0426, Nome = "SINTEV - Sistema de Inspe��o T�cnica Veicular Ltda" },
            //    new Organismo() { CodigoOIA = 0427, Nome = "GCL - INSPE��ES VEICULAR LTDA" },
            //    new Organismo() { CodigoOIA = 0430, Nome = "CTV - Centro de Tecnologia Veicular Ltda." },
            //    new Organismo() { CodigoOIA = 0431, Nome = "Monitor Engenharia - Inspe��o T�cnica de Ve�culos Ltda" },
            //    new Organismo() { CodigoOIA = 0434, Nome = "TRIVELATO & QUEIROZ INSPE��ES VEICULARES LTDA." },
            //    new Organismo() { CodigoOIA = 0439, Nome = "N�vel Inspe��es Ltda." },
            //    new Organismo() { CodigoOIA = 0440, Nome = "FIT INSPE��O VEICULAR LTDA." },
            //    new Organismo() { CodigoOIA = 0443, Nome = "AVAL SOROCABA INSPE��ES VEICULARES LTDA." },
            //    new Organismo() { CodigoOIA = 0450, Nome = "CPI Veicular Ltda" },
            //    new Organismo() { CodigoOIA = 0455, Nome = "�POTIGUAR INSPE��ES DE SEGURAN�A VEICULAR LTDA. - ME." },
            //    new Organismo() { CodigoOIA = 0463, Nome = "CBI - Centro Brasileiro de Inspe��o Veicular Ltda. - EPP." },
            //    new Organismo() { CodigoOIA = 0465, Nome = "LAUDOCENTER INSPE��O VEICULAR LTDA. - ME." },
            //    new Organismo() { CodigoOIA = 0467, Nome = "FIT INSPE��O VEICULAR LTDA." },
            //    new Organismo() { CodigoOIA = 0469, Nome = "CARDOSO DE PAULA VISTORIA DE SEGURAN�A AUTOMOBILISTICA LTDA" },
            //    new Organismo() { CodigoOIA = 0471, Nome = "MARTINS & CAETANO INSPE��ES VEICULARES LTDA. - ME." },
            //    new Organismo() { CodigoOIA = 0474, Nome = "GOI�NIA INSPE��ES LTDA" },
            //    new Organismo() { CodigoOIA = 0476, Nome = "NATAN - CAPACITA��O PARA TRANSPORTE DE PRODUTOS PERIGOSOS LTDA - ME." },
            //    new Organismo() { CodigoOIA = 0479, Nome = "INSPECIONAR - INSPE��O VEICULAR LTDA." },
            //    new Organismo() { CodigoOIA = 0481, Nome = "Ita Center Inspe��o Veicular Ltda." },
            //    new Organismo() { CodigoOIA = 0482, Nome = "VITRAN ENGENHARIA LTDA - ME" },
            //    new Organismo() { CodigoOIA = 0485, Nome = "EIVA INSPE��O VEICULAR LTDA" },
            //    new Organismo() { CodigoOIA = 0493, Nome = "INSPECENTRO INSPE��O VEICULAR EIRELI - ME." },
            //    new Organismo() { CodigoOIA = 0498, Nome = "SIMON INSPE��O VEICULAR LTDA." },
            //    new Organismo() { CodigoOIA = 0501, Nome = "Tecmetro Inspe��es Juiz de Fora Ltda" },
            //    new Organismo() { CodigoOIA = 0503, Nome = "VISTO-CAR PAUL�NIA INSPE��O VEICULAR LTDA." },
            //    new Organismo() { CodigoOIA = 0504, Nome = "BRASIL MG INSPE��ES VEICULARES JF LTDA." },
            //    new Organismo() { CodigoOIA = 0506, Nome = "VISTO-CAR Piracicaba Centro de Inspe��o Veicular Ltda" },
            //    new Organismo() { CodigoOIA = 0512, Nome = "ATIVE MARING� INSPE��ES VEICULARES LTDA. - ME" },
            //    new Organismo() { CodigoOIA = 0516, Nome = "SETA INSTITUI��O TECNICA DE INSPE��O VEICULAR LTDA" },
            //    new Organismo() { CodigoOIA = 0518, Nome = "RAMAF INSPE��ES LTDA. - ME." },
            //    new Organismo() { CodigoOIA = 0523, Nome = "DE PAULA VILAS BOAS - VISTORIA DE SEGURAN�A AUTOMOBILISTICA LTDA" },
            //    new Organismo() { CodigoOIA = 0524, Nome = "ENTRO DE INSPE��O VEICULAR DE ITABAIANA LTDA. - ME." },
            //    new Organismo() { CodigoOIA = 0525, Nome = "Nasa Inspe��o Veicular Ltda" },
            //    new Organismo() { CodigoOIA = 0526, Nome = "CIAA - CENTRO INSPE��O AUTOMOTIVA AMERICANA LTDA. - EPP." },
            //    new Organismo() { CodigoOIA = 0530, Nome = "Costa Fernandes Inspe��es Veiculares Ltda." },
            //    new Organismo() { CodigoOIA = 0531, Nome = "ADAR INSPE��O VEICULAR E CARGAS PERIGOSAS LTDA. - ME." },
            //    new Organismo() { CodigoOIA = 0532, Nome = "JABOAT�O DESCONTAMINA��O E INSPE��ES VEICULARES LTDA." },
            //    new Organismo() { CodigoOIA = 0536, Nome = "NIVEL - N�CLEO DE INSPE��ES VEICULARES LTDA. - ME." },
            //    new Organismo() { CodigoOIA = 0540, Nome = "LARISSA FIRMINO DA SILVA - EPP." },
            //    new Organismo() { CodigoOIA = 0542, Nome = "IVECAR CENTRO INSPE��O VEICULAR LTDA. - ME." },
            //    new Organismo() { CodigoOIA = 0545, Nome = "PAES DE OLIVEIRA & GOMES LTDA" },
            //    new Organismo() { CodigoOIA = 0549, Nome = "CENTRAL CHAPEC� DE INSPE��ES VEICULARES LTDA." },
            //    new Organismo() { CodigoOIA = 0551, Nome = "Central Pelotas de Inspe��es Veiculares Ltda" },
            //    new Organismo() { CodigoOIA = 0552, Nome = "ITAMETRO INSPE��ES VEICULARES - ME." },
            //    new Organismo() { CodigoOIA = 0554, Nome = "Atlantida Inspe��o Veicular Ltda-EPP." },
            //    new Organismo() { CodigoOIA = 0556, Nome = "VISTEC - Vistoria T�cnica Ltda" },
            //    new Organismo() { CodigoOIA = 0559, Nome = "Visto-Car S�o Jos� Inspe��o Veicular Ltda." },
            //    new Organismo() { CodigoOIA = 0562, Nome = "CETEM - Centro Tecnol�gico Mec�nico Ltda." },
            //    new Organismo() { CodigoOIA = 0563, Nome = "ARTE REAL INSPE��O VEICULAR LTDA. - ME." },
            //    new Organismo() { CodigoOIA = 0565, Nome = "�SERITRAN - SERVI�O DE INSPE��O EM TRANSPORTES LTDA." },
            //    new Organismo() { CodigoOIA = 0568, Nome = "INSPETRO DE MACA� LTDA." },
            //    new Organismo() { CodigoOIA = 0569, Nome = "INSPEVEL - INSPE��O VEICULAR DE CASCAVEL LTDA." },
            //    new Organismo() { CodigoOIA = 0570, Nome = "SETA INSTITUI��O T�CNICA DE INSPE��O VEICULAR LTDA. - EPP." },
            //    new Organismo() { CodigoOIA = 0574, Nome = "VISTO-CAR ANCHIETA INSPE��O VEICULAR LTDA. - EPP" },
            //    new Organismo() { CodigoOIA = 0575, Nome = "SETA REALENGO INSTITUI��O T�CNICA DE INSPE��O VEICULAR LTDA." },
            //    new Organismo() { CodigoOIA = 0576, Nome = "Inspeguar� Inspe��o Veicular Guarapuava Ltda." },
            //    new Organismo() { CodigoOIA = 0579, Nome = "ARAUC�RIA INSPE��ES LTDA." },
            //    new Organismo() { CodigoOIA = 0583, Nome = "SINAU - SERVI�O DE INSPE��O AUTOMOTIVA LTDA. - ME." },
            //    new Organismo() { CodigoOIA = 0584, Nome = "SANTO ANDR� INSPE��O VEICULAR LTDA. - EPP." },
            //    new Organismo() { CodigoOIA = 0586, Nome = "LIPP - LIMEIRA INSPE��ES LTDA." },
            //    new Organismo() { CodigoOIA = 0587, Nome = "GUARU INSPE��ES LTDA. - EPP." },
            //    new Organismo() { CodigoOIA = 0588, Nome = "SEVEL - Seguran�a Veicular Paran� Ltda. - EPP." },
            //    new Organismo() { CodigoOIA = 0589, Nome = "JCP Inspe��es Veiculares LTDA." },
            //    new Organismo() { CodigoOIA = 0590, Nome = "ARA PP INSPE��O VEICULAR LTDA." },
            //    new Organismo() { CodigoOIA = 0591, Nome = "GMAP INSPE��O VEICULAR LTDA. - EPP." },
            //    new Organismo() { CodigoOIA = 0593, Nome = "ECV - EMPRESA CAPIXABA DE VISTORIAS LTDA." },
            //    new Organismo() { CodigoOIA = 0594, Nome = "CRIVE - CENTRO REG. DE INSPE��O VEICULAR LTDA." },
            //    new Organismo() { CodigoOIA = 0598, Nome = "ITV INSPE��O T�CNICA VEICULAR LTDA." },
            //    new Organismo() { CodigoOIA = 0601, Nome = "MAXY ENGENHARIA DE INSPE��O LTDA." },
            //    new Organismo() { CodigoOIA = 0603, Nome = "IVETU INSPE��O VEICULAR TUBAR�O LTDA. - ME." },
            //    new Organismo() { CodigoOIA = 0606, Nome = "INSPENORTE INSPE��O VEICULAR LTDA." },
            //    new Organismo() { CodigoOIA = 0610, Nome = "CASCAVEL INSPE��ES LTDA. - ME." },
            //    new Organismo() { CodigoOIA = 0611, Nome = "ITV - Inspe��o T�cnica Veicular Ltda." },
            //    new Organismo() { CodigoOIA = 0612, Nome = "CIAUTO - CENTRO DE INSPE��O AUTOMOTIVA LTDA." },
            //    new Organismo() { CodigoOIA = 0613, Nome = "ETESUL PLANALTO INSPE��O VEICULAR LTDA ME" },
            //    new Organismo() { CodigoOIA = 0614, Nome = "NECAVA - Inspe��o e Pesquisa em Transporte Ltda. - ME." },
            //    new Organismo() { CodigoOIA = 0616, Nome = "DELTA SERVI�OS INSPE��O LTDA. - EPP." },
            //    new Organismo() { CodigoOIA = 0622, Nome = "ECIV EMPRESA CRICIUMENSE DE INSPE��ES VEICULARES LTDA" },
            //    new Organismo() { CodigoOIA = 0623, Nome = "PANTANAL INSPE��ES LTDA ME" },
            //    new Organismo() { CodigoOIA = 0624, Nome = "CEI - CENTRO ESPECIALIZADO DE INSPE��ES LTDA. - EPP." },
            //    new Organismo() { CodigoOIA = 0625, Nome = "MINAS INSPE��ES EM VE�CULOS E EQUIPAMENTOS LTDA. - ME." },
            //    new Organismo() { CodigoOIA = 0627, Nome = "VISAVERO- Vistorias e Inspe��es de Seguran�a e Ambiental em Ve�culos Rodovi�rios LTDA." },
            //    new Organismo() { CodigoOIA = 0628, Nome = "CIC - CENTRO DE INSPE��ES E CONSULTORIAS LTDA. EPP." },
            //    new Organismo() { CodigoOIA = 0630, Nome = "VIU VEICULAR INSPE��ES UBERL�NDIA LTDA. - ME." },
            //    new Organismo() { CodigoOIA = 0631, Nome = "SERITRAM - SERVI�O DE INSPE��O EM TRANSPORTES DE MARAB� LTDA. - EPP." },
            //    new Organismo() { CodigoOIA = 0632, Nome = "PARAN� INSPE��ES - EIRELLI - ME." },
            //    new Organismo() { CodigoOIA = 0633, Nome = "INSPESAN INSPE��ES MURIA� LTDA. - ME." },
            //    new Organismo() { CodigoOIA = 0634, Nome = "F�NIX INSPE��ES VEICULARES LTDA." },
            //    new Organismo() { CodigoOIA = 0635, Nome = "Central Santa Maria de Inspe��es Veiculares Ltda. - EPP." },
            //    new Organismo() { CodigoOIA = 0636, Nome = "POLO CAPUAVA - INSPE��O VEICULAR LTDA. - EPP." },
            //    new Organismo() { CodigoOIA = 0637, Nome = "CIVIC 1 - Inspe��o Veicular EIRELI - EPP." },
            //    new Organismo() { CodigoOIA = 0640, Nome = "WEST ENGENHARIA DE INSPE��O LTDA." },
            //    new Organismo() { CodigoOIA = 0641, Nome = "VITRAN ENGENHARIA LTDA. - EPP." },
            //    new Organismo() { CodigoOIA = 0645, Nome = "WEST ENGENHARIA DE INSPE��O LTDA. - FILIAL MARITUBA/PA" },
            //    new Organismo() { CodigoOIA = 0646, Nome = "WEST ENGENHARIA DE INSPE��O LTDA. - GUARULHOS" },
            //    new Organismo() { CodigoOIA = 0647, Nome = "METROPOLITAN INSPE��ES LTDA - EPP" },
            //    new Organismo() { CodigoOIA = 0652, Nome = "MULT CEAR� INSPE��O LTDA. - ME." },
            //    new Organismo() { CodigoOIA = 0656, Nome = "Master Inspe��es Veiculares Ltda - EPP" },
            //    new Organismo() { CodigoOIA = 0657, Nome = "SETA - REALENGO INSTITUI��O T�CNICA DE INSPE��O VEICULAR LTDA." },
            //    new Organismo() { CodigoOIA = 0659, Nome = "SETA NORTE T�CNICA INSPE��O VEICULAR LTDA. - EPP." },
            //    new Organismo() { CodigoOIA = 0663, Nome = "AVAL LONDRINA INSPE��ES VEICULARES LTDA." },
            //    new Organismo() { CodigoOIA = 0665, Nome = "NIVEL N�CLEO DE INSPE��ES VEICULARES DE AN�POLIS LTDA - ME" },
            //    new Organismo() { CodigoOIA = 0668, Nome = "BR VALE ENGENHARIA DE INSPE��O LTDA." },
            //    new Organismo() { CodigoOIA = 0672, Nome = "BETIM INSPE��ES LTDA - ME" },
            //    new Organismo() { CodigoOIA = 0674, Nome = "L�DER SUL INSPE��ES LTDA. - ME." },
            //    new Organismo() { CodigoOIA = 0676, Nome = "Paraiba Servi�o de Inspe��o Veicular Ltda - EPP" },
            //    new Organismo() { CodigoOIA = 0677, Nome = "CETEI - CENTRO T�CNICO DE ENGENHARIA DE INSPE��O LTDA." },
            //    new Organismo() { CodigoOIA = 0679, Nome = "CINSPEMAR - Centro Especializado de Inspe��es do Maranh�o Ltda. - ME." },
            //    new Organismo() { CodigoOIA = 0680, Nome = "SETA - Institui��o T�cnica de Inspe��o Veicular Ltda. - EPP." },
            //    new Organismo() { CodigoOIA = 0684, Nome = "SHERLOCKAR INSPE��O VEICULAR EIRELLI." },
            //    new Organismo() { CodigoOIA = 0687, Nome = "NORTEKAR - INSPE��O VEICULAR LTDA EPP" },
            //    new Organismo() { CodigoOIA = 0689, Nome = "DCX Inspe�ao e Engenharia Ltda" },
            //    new Organismo() { CodigoOIA = 0691, Nome = "CIAP CENTRO DE INSPE��O AUTOMOTIVA PAUL�NIA LTDA" },
            //    new Organismo() { CodigoOIA = 0692, Nome = "Seta Institui��o T�cnica de Inspe��o Veicular Ltda. - EPP." },
            //    new Organismo() { CodigoOIA = 0696, Nome = "ITAJA� INSPE��ES VEICULARES LTDA" },
            //    new Organismo() { CodigoOIA = 0698, Nome = "BUREAU ITAJAIENSE DE INSPE��O VEICULAR LTDA - ME" },
            //    new Organismo() { CodigoOIA = 0699, Nome = "FIT INSPE��O VEICULAR LTDA." },
            //    new Organismo() { CodigoOIA = 0702, Nome = "MAYARA LETICIA BALESTERO. - ME." },
            //    new Organismo() { CodigoOIA = 0704, Nome = "SIVIC SEGURAN�A E INSPE��O VEICULAR LTDA." },
            //    new Organismo() { CodigoOIA = 0705, Nome = "MENDON�A, MENDON�A & SCHUNKE LTDA. - ME." },
            //    new Organismo() { CodigoOIA = 0706, Nome = "SETA S�O LU�S INSTITUI��O T�CNICA DE INSPE��O VEICULAR LTDA. - EPP." },
            //    new Organismo() { CodigoOIA = 0712, Nome = "ATIVE PARANAGU� INSPE��ES VEICULARES LTDA. - ME." },
            //    new Organismo() { CodigoOIA = 0714, Nome = "Otimiza Inspe��o Veicular Ltda. - EPP." },
            //    new Organismo() { CodigoOIA = 0716, Nome = "ESTA��O INSPE��O VEICULAR LTDA." },
            //    new Organismo() { CodigoOIA = 0722, Nome = "N�cleo de Inspe��o Veicular Ltda. - ME." },
            //    new Organismo() { CodigoOIA = 0723, Nome = "PANTANAL INSPE��O VEICULAR LTDA .- EPP." },
            //    new Organismo() { CodigoOIA = 0724, Nome = "CAIBI INSPE��ES VEICULARES LTDA." },
            //    new Organismo() { CodigoOIA = 0727, Nome = "CIVI - Centro de Inspe��o Veicular de Arapiraca Ltda. - ME." },
            //    new Organismo() { CodigoOIA = 0731, Nome = "CONRADO E SILVA LTDA. - ME." },
            //    new Organismo() { CodigoOIA = 0734, Nome = "INSPECAMPO INSPE��O DE SEGURAN�A VEICULAR DE CAMPO LARGO LTDA." },
            //    new Organismo() { CodigoOIA = 0735, Nome = "EJB - SERVI�OS DE INSPE��O T�CNICA VEICULAR LTDA." },
            //    new Organismo() { CodigoOIA = 0738, Nome = "NOVA INSPE��O VEICULAR LTDA. - EPP." },
            //    new Organismo() { CodigoOIA = 0740, Nome = "ISTO MAIS VISTORIA VEICULAR LTDA." },
            //    new Organismo() { CodigoOIA = 0742, Nome = "BR INSPE��O T�CNICA VEICULAR - ME." },
            //    new Organismo() { CodigoOIA = 0752, Nome = "JULIANA DE BRITO PEREIRA - ME." },
            //    new Organismo() { CodigoOIA = 0754, Nome = "Maring� Inspe��o Veicular Ltda. - ME." },
            //    new Organismo() { CodigoOIA = 0755, Nome = "AFD INSPE��ES VEICULARES EIRELI - ME." },
            //     new Organismo() { CodigoOIA = 7777, Nome = "TEST ORGANISMO - 2", EhAtivo = true },
            //     new Organismo() { CodigoOIA = 0001, Nome = "TESTE-ORGANISMO" });

            #endregion

            #region FtpInfo

            //context.IntegracaoInfo.AddOrUpdate(

            //new FTPInfo()
            //{
            //    OrganismoId = 96,
            //    HostURI = $"ftpq.ibratan.com.br/",
            //    Usuario = $"diois",
            //    Senha = $"gtd3NRzVjk9pJLfr3Cjshkqn",
            //    DiretorioInspecao = $"/Inspecoes/",
            //    DiretorioInspecaoLocal = $"622\\",
            //    TipoIntegracao = 1
            //});

            //    new FTPInfo() { OrganismoId = 156, HostURI = $"ftp://184.168.109.66:2112/", Usuario = $"nakoa_ftp", Senha = $"n@k@412!", DiretorioInspecao = $"/new-orders/", DiretorioInspecaoLocal = $"7777\\", DiretorioInspecaoRemoto = $"/New/" },
            //new FTPInfo() { OrganismoId = 156, HostURI = $"ftp://200.20.212.163:2112/", Usuario = $"diois_ftp", Senha = $"d!@15#admin", DiretorioInspecao = $"/Inspecoes/", DiretorioInspecaoLocal = $"0001\\", DiretorioInspecaoRemoto = $"/New/" });
            //new FTPInfo()
            //{
            //    OrganismoId = 39,
            //    HostURI = $"35.170.103.199",
            //    Usuario = $"eivainmetro",
            //    Senha = $"cAkaP7egu",
            //    DiretorioInspecao = $"/INSPECOES/",
            //    DiretorioInspecaoLocal = $"485\\",
            //    TipoIntegracao = 2
            //},

            ////    new FTPInfo() { OrganismoId = 156, HostURI = $"ftp://184.168.109.66:2112/", Usuario = $"nakoa_ftp", Senha = $"n@k@412!", DiretorioInspecao = $"/new-orders/", DiretorioInspecaoLocal = $"7777\\", DiretorioInspecaoRemoto = $"/New/" },
            //new FTPInfo()
            //{
            //    OrganismoId = 92,
            //    HostURI = $"35.170.103.199",
            //    Usuario = $"ciautoinmetro",
            //    Senha = $"otmws@123",
            //    DiretorioInspecao = $"/INSPECOES/",
            //    DiretorioInspecaoLocal = $"612\\",
            //    TipoIntegracao = 2
            //});


            #endregion


        }
    }
}
