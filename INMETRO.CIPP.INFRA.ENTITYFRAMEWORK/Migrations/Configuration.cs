
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
            //    new Organismo() { CodigoOIA = 0016, Nivel_Li = "", Nome = "QUALITY INSPEÇÃO VEICULAR LTDA. - EPP." },
            //    new Organismo() { CodigoOIA = 0082, Nivel_Li = "", Nome = "TRANSTECH IVESUR BRASIL LTDA." },
            //    new Organismo() { CodigoOIA = 0112, Nivel_Li = "01", Nome = "Tiririca Inspeção e Segurança Veicular Ltda" },
            //    new Organismo() { CodigoOIA = 0142, Nivel_Li = "", Nome = "Inscape Inspeções Ltda" },
            //    new Organismo() { CodigoOIA = 0224, Nivel_Li = "", Nome = "Tecsul Inspeção Veicular Ltda." },
            //    new Organismo() { CodigoOIA = 0257, Nivel_Li = "", Nome = "ITTRAN - Instituto Tecnológico de Transporte Ltda." },
            //    new Organismo() { CodigoOIA = 0269, Nivel_Li = "", Nome = "CENTRAL PORTO ALEGRE DE INSPEÇÕES VEICULARES LTDA." },
            //    new Organismo() { CodigoOIA = 0279, Nivel_Li = "", Nome = "NACIONAL INSPEÇÕES LTDA." },
            //    new Organismo() { CodigoOIA = 0289, Nivel_Li = "", Nome = "EMBRAVEC - EMPRESA BRASILEIRA DE INSPEÇÃO VEICULAR LTDA. - EPP." },
            //    new Organismo() { CodigoOIA = 0294, Nivel_Li = "", Nome = "WEST ENGENHARIA DE INSPEÇÃO LTDA." },
            //    new Organismo() { CodigoOIA = 0307, Nivel_Li = "", Nome = "SBI-RS - INSPEÇÕES DE TANQUES E RESERVATÓRIOS LTDA." },
            //    new Organismo() { CodigoOIA = 0319, Nivel_Li = "", Nome = "SBI - SERRANA INSPEÇÕES DE PRODUTOS PERIGOSOS LTDA." },
            //    new Organismo() { CodigoOIA = 0322, Nivel_Li = "", Nome = "BBI SERVIÇOS DE INSPEÇÃO VEICULAR LTDA." },
            //    new Organismo() { CodigoOIA = 0328, Nivel_Li = "", Nome = "INSPECENTRO INSPEÇÃO VEICULAR EIRELI - ME." },
            //    new Organismo() { CodigoOIA = 0351, Nivel_Li = "", Nome = "ITESV - Instituto Tecnologia de Engenharia e Segurança Veicular Ltda." },
            //    new Organismo() { CodigoOIA = 0368, Nivel_Li = "", Nome = "BUREAU BRASILEIRO DE INSPEÇÃO LTDA" },
            //    new Organismo() { CodigoOIA = 0420, Nivel_Li = "", Nome = "ATIVE OURINHOS INSPEÇÕES VEICULARES LTDA." },
            //    new Organismo() { CodigoOIA = 0424, Nivel_Li = "", Nome = "IVECAR CENTRO INSPEÇÃO VEICULAR LTDA.	" },
            //    new Organismo() { CodigoOIA = 0426, Nivel_Li = "", Nome = "SINTEV - Sistema de Inspeção Técnica Veicular Ltda" },
            //    new Organismo() { CodigoOIA = 0427, Nivel_Li = "", Nome = "GCL - INSPEÇÕES VEICULAR LTDA" },
            //    new Organismo() { CodigoOIA = 0430, Nivel_Li = "", Nome = "CTV - Centro de Tecnologia Veicular Ltda." },
            //    new Organismo() { CodigoOIA = 0431, Nivel_Li = "", Nome = "Monitor Engenharia - Inspeção Técnica de Veículos Ltda" },
            //    new Organismo() { CodigoOIA = 0434, Nivel_Li = "", Nome = "TRIVELATO & QUEIROZ INSPEÇÕES VEICULARES LTDA." },
            //    new Organismo() { CodigoOIA = 0439, Nivel_Li = "", Nome = "Nível Inspeções Ltda." },
            //    new Organismo() { CodigoOIA = 0440, Nivel_Li = "", Nome = "FIT INSPEÇÃO VEICULAR LTDA." },
            //    new Organismo() { CodigoOIA = 0443, Nivel_Li = "", Nome = "AVAL SOROCABA INSPEÇÕES VEICULARES LTDA." },
            //    new Organismo() { CodigoOIA = 0450, Nivel_Li = "", Nome = "CPI Veicular Ltda" },
            //    new Organismo() { CodigoOIA = 0455, Nivel_Li = "", Nome = " POTIGUAR INSPEÇÕES DE SEGURANÇA VEICULAR LTDA. - ME." },
            //    new Organismo() { CodigoOIA = 0463, Nivel_Li = "", Nome = "CBI - Centro Brasileiro de Inspeção Veicular Ltda. - EPP." },
            //    new Organismo() { CodigoOIA = 0465, Nivel_Li = "", Nome = "LAUDOCENTER INSPEÇÃO VEICULAR LTDA. - ME." },
            //    new Organismo() { CodigoOIA = 0467, Nivel_Li = "", Nome = "FIT INSPEÇÃO VEICULAR LTDA." },
            //    new Organismo() { CodigoOIA = 0469, Nivel_Li = "", Nome = "CARDOSO DE PAULA VISTORIA DE SEGURANÇA AUTOMOBILISTICA LTDA" },
            //    new Organismo() { CodigoOIA = 0471, Nivel_Li = "", Nome = "MARTINS & CAETANO INSPEÇÕES VEICULARES LTDA. - ME." },
            //    new Organismo() { CodigoOIA = 0474, Nivel_Li = "", Nome = "GOIÂNIA INSPEÇÕES LTDA" },
            //    new Organismo() { CodigoOIA = 0476, Nivel_Li = "", Nome = "NATAN - CAPACITAÇÃO PARA TRANSPORTE DE PRODUTOS PERIGOSOS LTDA - ME." },
            //    new Organismo() { CodigoOIA = 0479, Nivel_Li = "", Nome = "INSPECIONAR - INSPEÇÃO VEICULAR LTDA." },
            //    new Organismo() { CodigoOIA = 0481, Nivel_Li = "", Nome = "Ita Center Inspeção Veicular Ltda." },
            //    new Organismo() { CodigoOIA = 0482, Nivel_Li = "", Nome = "VITRAN ENGENHARIA LTDA - ME" },
            //    new Organismo() { CodigoOIA = 0485, Nivel_Li = "", Nome = "EIVA INSPEÇÃO VEICULAR LTDA" },
            //    new Organismo() { CodigoOIA = 0493, Nivel_Li = "", Nome = "INSPECENTRO INSPEÇÃO VEICULAR EIRELI - ME." },
            //    new Organismo() { CodigoOIA = 0498, Nivel_Li = "", Nome = "SIMON INSPEÇÃO VEICULAR LTDA." },
            //    new Organismo() { CodigoOIA = 0501, Nivel_Li = "", Nome = "Tecmetro Inspeções Juiz de Fora Ltda" },
            //    new Organismo() { CodigoOIA = 0503, Nivel_Li = "", Nome = "VISTO-CAR PAULÍNIA INSPEÇÃO VEICULAR LTDA." },
            //    new Organismo() { CodigoOIA = 0504, Nivel_Li = "", Nome = "BRASIL MG INSPEÇÕES VEICULARES JF LTDA." },
            //    new Organismo() { CodigoOIA = 0506, Nivel_Li = "", Nome = "VISTO-CAR Piracicaba Centro de Inspeção Veicular Ltda" },
            //    new Organismo() { CodigoOIA = 0512, Nivel_Li = "", Nome = "ATIVE MARINGÁ INSPEÇÕES VEICULARES LTDA. - ME" },
            //    new Organismo() { CodigoOIA = 0516, Nivel_Li = "", Nome = "SETA INSTITUIÇÃO TECNICA DE INSPEÇÃO VEICULAR LTDA" },
            //    new Organismo() { CodigoOIA = 0518, Nivel_Li = "", Nome = "RAMAF INSPEÇÕES LTDA. - ME." },
            //    new Organismo() { CodigoOIA = 0523, Nivel_Li = "", Nome = "DE PAULA VILAS BOAS - VISTORIA DE SEGURANÇA AUTOMOBILISTICA LTDA" },
            //    new Organismo() { CodigoOIA = 0524, Nivel_Li = "", Nome = "ENTRO DE INSPEÇÃO VEICULAR DE ITABAIANA LTDA. - ME." },
            //    new Organismo() { CodigoOIA = 0525, Nivel_Li = "", Nome = "Nasa Inspeção Veicular Ltda" },
            //    new Organismo() { CodigoOIA = 0526, Nivel_Li = "", Nome = "CIAA - CENTRO INSPEÇÃO AUTOMOTIVA AMERICANA LTDA. - EPP." },
            //    new Organismo() { CodigoOIA = 0530, Nivel_Li = "", Nome = "Costa Fernandes Inspeções Veiculares Ltda." },
            //    new Organismo() { CodigoOIA = 0531, Nivel_Li = "", Nome = "ADAR INSPEÇÃO VEICULAR E CARGAS PERIGOSAS LTDA. - ME." },
            //    new Organismo() { CodigoOIA = 0532, Nivel_Li = "", Nome = "JABOATÃO DESCONTAMINAÇÃO E INSPEÇÕES VEICULARES LTDA." },
            //    new Organismo() { CodigoOIA = 0536, Nivel_Li = "", Nome = "NIVEL - NÚCLEO DE INSPEÇÕES VEICULARES LTDA. - ME." },
            //    new Organismo() { CodigoOIA = 0540, Nivel_Li = "", Nome = "LARISSA FIRMINO DA SILVA - EPP." },
            //    new Organismo() { CodigoOIA = 0542, Nivel_Li = "", Nome = "IVECAR CENTRO INSPEÇÃO VEICULAR LTDA. - ME." },
            //    new Organismo() { CodigoOIA = 0545, Nivel_Li = "", Nome = "PAES DE OLIVEIRA & GOMES LTDA" },
            //    new Organismo() { CodigoOIA = 0549, Nivel_Li = "", Nome = "CENTRAL CHAPECÓ DE INSPEÇÕES VEICULARES LTDA." },
            //    new Organismo() { CodigoOIA = 0551, Nivel_Li = "", Nome = "Central Pelotas de Inspeções Veiculares Ltda" },
            //    new Organismo() { CodigoOIA = 0552, Nivel_Li = "", Nome = "ITAMETRO INSPEÇÕES VEICULARES - ME." },
            //    new Organismo() { CodigoOIA = 0554, Nivel_Li = "", Nome = "Atlantida Inspeção Veicular Ltda-EPP." },
            //    new Organismo() { CodigoOIA = 0556, Nivel_Li = "", Nome = "VISTEC - Vistoria Técnica Ltda" },
            //    new Organismo() { CodigoOIA = 0559, Nivel_Li = "", Nome = "Visto-Car São José Inspeção Veicular Ltda." },
            //    new Organismo() { CodigoOIA = 0562, Nivel_Li = "", Nome = "CETEM - Centro Tecnológico Mecânico Ltda." },
            //    new Organismo() { CodigoOIA = 0563, Nivel_Li = "", Nome = "ARTE REAL INSPEÇÃO VEICULAR LTDA. - ME." },
            //    new Organismo() { CodigoOIA = 0565, Nivel_Li = "", Nome = " SERITRAN - SERVIÇO DE INSPEÇÃO EM TRANSPORTES LTDA." },
            //    new Organismo() { CodigoOIA = 0568, Nivel_Li = "", Nome = "INSPETRO DE MACAÉ LTDA." },
            //    new Organismo() { CodigoOIA = 0569, Nivel_Li = "", Nome = "INSPEVEL - INSPEÇÃO VEICULAR DE CASCAVEL LTDA." },
            //    new Organismo() { CodigoOIA = 0570, Nivel_Li = "", Nome = "SETA INSTITUIÇÃO TÉCNICA DE INSPEÇÃO VEICULAR LTDA. - EPP." },
            //    new Organismo() { CodigoOIA = 0574, Nivel_Li = "", Nome = "VISTO-CAR ANCHIETA INSPEÇÃO VEICULAR LTDA. - EPP" },
            //    new Organismo() { CodigoOIA = 0575, Nivel_Li = "", Nome = "SETA REALENGO INSTITUIÇÃO TÉCNICA DE INSPEÇÃO VEICULAR LTDA." },
            //    new Organismo() { CodigoOIA = 0576, Nivel_Li = "", Nome = "Inspeguará Inspeção Veicular Guarapuava Ltda." },
            //    new Organismo() { CodigoOIA = 0579, Nivel_Li = "", Nome = "ARAUCÁRIA INSPEÇÕES LTDA." },
            //    new Organismo() { CodigoOIA = 0583, Nivel_Li = "", Nome = "SINAU - SERVIÇO DE INSPEÇÃO AUTOMOTIVA LTDA. - ME." },
            //    new Organismo() { CodigoOIA = 0584, Nivel_Li = "", Nome = "SANTO ANDRÉ INSPEÇÃO VEICULAR LTDA. - EPP." },
            //    new Organismo() { CodigoOIA = 0586, Nivel_Li = "", Nome = "LIPP - LIMEIRA INSPEÇÕES LTDA." },
            //    new Organismo() { CodigoOIA = 0587, Nivel_Li = "", Nome = "GUARU INSPEÇÕES LTDA. - EPP." },
            //    new Organismo() { CodigoOIA = 0588, Nivel_Li = "", Nome = "SEVEL - Segurança Veicular Paraná Ltda. - EPP." },
            //    new Organismo() { CodigoOIA = 0589, Nivel_Li = "", Nome = "JCP Inspeções Veiculares LTDA." },
            //    new Organismo() { CodigoOIA = 0590, Nivel_Li = "", Nome = "ARA PP INSPEÇÃO VEICULAR LTDA." },
            //    new Organismo() { CodigoOIA = 0591, Nivel_Li = "", Nome = "GMAP INSPEÇÃO VEICULAR LTDA. - EPP." },
            //    new Organismo() { CodigoOIA = 0593, Nivel_Li = "", Nome = "ECV - EMPRESA CAPIXABA DE VISTORIAS LTDA." },
            //    new Organismo() { CodigoOIA = 0594, Nivel_Li = "", Nome = "CRIVE - CENTRO REG. DE INSPEÇÃO VEICULAR LTDA." },
            //    new Organismo() { CodigoOIA = 0598, Nivel_Li = "", Nome = "ITV INSPEÇÃO TÉCNICA VEICULAR LTDA." },
            //    new Organismo() { CodigoOIA = 0601, Nivel_Li = "", Nome = "MAXY ENGENHARIA DE INSPEÇÃO LTDA." },
            //    new Organismo() { CodigoOIA = 0603, Nivel_Li = "", Nome = "IVETU INSPEÇÃO VEICULAR TUBARÃO LTDA. - ME." },
            //    new Organismo() { CodigoOIA = 0606, Nivel_Li = "", Nome = "INSPENORTE INSPEÇÃO VEICULAR LTDA." },
            //    new Organismo() { CodigoOIA = 0610, Nivel_Li = "", Nome = "CASCAVEL INSPEÇÕES LTDA. - ME." },
            //    new Organismo() { CodigoOIA = 0611, Nivel_Li = "", Nome = "ITV - Inspeção Técnica Veicular Ltda." },
            //    new Organismo() { CodigoOIA = 0612, Nivel_Li = "", Nome = "CIAUTO - CENTRO DE INSPEÇÃO AUTOMOTIVA LTDA." },
            //    new Organismo() { CodigoOIA = 0613, Nivel_Li = "", Nome = "ETESUL PLANALTO INSPEÇÃO VEICULAR LTDA ME" },
            //    new Organismo() { CodigoOIA = 0614, Nivel_Li = "", Nome = "NECAVA - Inspeção e Pesquisa em Transporte Ltda. - ME." },
            //    new Organismo() { CodigoOIA = 0616, Nivel_Li = "", Nome = "DELTA SERVIÇOS INSPEÇÃO LTDA. - EPP." },
            //    new Organismo() { CodigoOIA = 0622, Nivel_Li = "", Nome = "ECIV EMPRESA CRICIUMENSE DE INSPEÇÕES VEICULARES LTDA" },
            //    new Organismo() { CodigoOIA = 0623, Nivel_Li = "", Nome = "PANTANAL INSPEÇÕES LTDA ME" },
            //    new Organismo() { CodigoOIA = 0624, Nivel_Li = "", Nome = "CEI - CENTRO ESPECIALIZADO DE INSPEÇÕES LTDA. - EPP." },
            //    new Organismo() { CodigoOIA = 0625, Nivel_Li = "", Nome = "MINAS INSPEÇÕES EM VEÍCULOS E EQUIPAMENTOS LTDA. - ME." },
            //    new Organismo() { CodigoOIA = 0627, Nivel_Li = "", Nome = "VISAVERO- Vistorias e Inspeções de Segurança e Ambiental em Veículos Rodoviários LTDA." },
            //    new Organismo() { CodigoOIA = 0628, Nivel_Li = "", Nome = "CIC - CENTRO DE INSPEÇÕES E CONSULTORIAS LTDA. EPP." },
            //    new Organismo() { CodigoOIA = 0630, Nivel_Li = "", Nome = "VIU VEICULAR INSPEÇÕES UBERLÂNDIA LTDA. - ME." },
            //    new Organismo() { CodigoOIA = 0631, Nivel_Li = "", Nome = "SERITRAM - SERVIÇO DE INSPEÇÃO EM TRANSPORTES DE MARABÁ LTDA. - EPP." },
            //    new Organismo() { CodigoOIA = 0632, Nivel_Li = "", Nome = "PARANÁ INSPEÇÕES - EIRELLI - ME." },
            //    new Organismo() { CodigoOIA = 0633, Nivel_Li = "", Nome = "INSPESAN INSPEÇÕES MURIAÉ LTDA. - ME." },
            //    new Organismo() { CodigoOIA = 0634, Nivel_Li = "", Nome = "FÊNIX INSPEÇÕES VEICULARES LTDA." },
            //    new Organismo() { CodigoOIA = 0635, Nivel_Li = "", Nome = "Central Santa Maria de Inspeções Veiculares Ltda. - EPP." },
            //    new Organismo() { CodigoOIA = 0636, Nivel_Li = "", Nome = "POLO CAPUAVA - INSPEÇÃO VEICULAR LTDA. - EPP." },
            //    new Organismo() { CodigoOIA = 0637, Nivel_Li = "", Nome = "CIVIC 1 - Inspeção Veicular EIRELI - EPP." },
            //    new Organismo() { CodigoOIA = 0640, Nivel_Li = "", Nome = "WEST ENGENHARIA DE INSPEÇÃO LTDA." },
            //    new Organismo() { CodigoOIA = 0641, Nivel_Li = "", Nome = "VITRAN ENGENHARIA LTDA. - EPP." },
            //    new Organismo() { CodigoOIA = 0645, Nivel_Li = "", Nome = "WEST ENGENHARIA DE INSPEÇÃO LTDA. - FILIAL MARITUBA/PA" },
            //    new Organismo() { CodigoOIA = 0646, Nivel_Li = "", Nome = "WEST ENGENHARIA DE INSPEÇÃO LTDA. - GUARULHOS" },
            //    new Organismo() { CodigoOIA = 0647, Nivel_Li = "", Nome = "METROPOLITAN INSPEÇÕES LTDA - EPP" },
            //    new Organismo() { CodigoOIA = 0652, Nivel_Li = "", Nome = "MULT CEARÁ INSPEÇÃO LTDA. - ME." },
            //    new Organismo() { CodigoOIA = 0656, Nivel_Li = "", Nome = "Master Inspeções Veiculares Ltda - EPP" },
            //    new Organismo() { CodigoOIA = 0657, Nivel_Li = "", Nome = "SETA - REALENGO INSTITUIÇÃO TÉCNICA DE INSPEÇÃO VEICULAR LTDA." },
            //    new Organismo() { CodigoOIA = 0659, Nivel_Li = "", Nome = "SETA NORTE TÉCNICA INSPEÇÃO VEICULAR LTDA. - EPP." },
            //    new Organismo() { CodigoOIA = 0663, Nivel_Li = "", Nome = "AVAL LONDRINA INSPEÇÕES VEICULARES LTDA." },
            //    new Organismo() { CodigoOIA = 0665, Nivel_Li = "", Nome = "NIVEL NÚCLEO DE INSPEÇÕES VEICULARES DE ANÁPOLIS LTDA - ME" },
            //    new Organismo() { CodigoOIA = 0668, Nivel_Li = "", Nome = "BR VALE ENGENHARIA DE INSPEÇÃO LTDA." },
            //    new Organismo() { CodigoOIA = 0672, Nivel_Li = "", Nome = "BETIM INSPEÇÕES LTDA - ME" },
            //    new Organismo() { CodigoOIA = 0674, Nivel_Li = "", Nome = "LÍDER SUL INSPEÇÕES LTDA. - ME." },
            //    new Organismo() { CodigoOIA = 0676, Nivel_Li = "", Nome = "Paraiba Serviço de Inspeção Veicular Ltda - EPP" },
            //    new Organismo() { CodigoOIA = 0677, Nivel_Li = "", Nome = "CETEI - CENTRO TÉCNICO DE ENGENHARIA DE INSPEÇÃO LTDA." },
            //    new Organismo() { CodigoOIA = 0679, Nivel_Li = "", Nome = "CINSPEMAR - Centro Especializado de Inspeções do Maranhão Ltda. - ME." },
            //    new Organismo() { CodigoOIA = 0680, Nivel_Li = "", Nome = "SETA - Instituição Técnica de Inspeção Veicular Ltda. - EPP." },
            //    new Organismo() { CodigoOIA = 0684, Nivel_Li = "", Nome = "SHERLOCKAR INSPEÇÃO VEICULAR EIRELLI." },
            //    new Organismo() { CodigoOIA = 0687, Nivel_Li = "", Nome = "NORTEKAR - INSPEÇÃO VEICULAR LTDA EPP" },
            //    new Organismo() { CodigoOIA = 0689, Nivel_Li = "", Nome = "DCX Inspeçao e Engenharia Ltda" },
            //    new Organismo() { CodigoOIA = 0691, Nivel_Li = "", Nome = "CIAP CENTRO DE INSPEÇÃO AUTOMOTIVA PAULÍNIA LTDA" },
            //    new Organismo() { CodigoOIA = 0692, Nivel_Li = "", Nome = "Seta Instituição Técnica de Inspeção Veicular Ltda. - EPP." },
            //    new Organismo() { CodigoOIA = 0696, Nivel_Li = "", Nome = "ITAJAÍ INSPEÇÕES VEICULARES LTDA" },
            //    new Organismo() { CodigoOIA = 0698, Nivel_Li = "", Nome = "BUREAU ITAJAIENSE DE INSPEÇÃO VEICULAR LTDA - ME" },
            //    new Organismo() { CodigoOIA = 0699, Nivel_Li = "", Nome = "FIT INSPEÇÃO VEICULAR LTDA." },
            //    new Organismo() { CodigoOIA = 0702, Nivel_Li = "", Nome = "MAYARA LETICIA BALESTERO. - ME." },
            //    new Organismo() { CodigoOIA = 0704, Nivel_Li = "", Nome = "SIVIC SEGURANÇA E INSPEÇÃO VEICULAR LTDA." },
            //    new Organismo() { CodigoOIA = 0705, Nivel_Li = "01", Nome = "MENDONÇA, MENDONÇA & SCHUNKE LTDA. - ME." },
            //    new Organismo() { CodigoOIA = 0706, Nivel_Li = "", Nome = "SETA SÃO LUÍS INSTITUIÇÃO TÉCNICA DE INSPEÇÃO VEICULAR LTDA. - EPP." },
            //    new Organismo() { CodigoOIA = 0712, Nivel_Li = "", Nome = "ATIVE PARANAGUÁ INSPEÇÕES VEICULARES LTDA. - ME." },
            //    new Organismo() { CodigoOIA = 0714, Nivel_Li = "", Nome = "Otimiza Inspeção Veicular Ltda. - EPP." },
            //    new Organismo() { CodigoOIA = 0716, Nivel_Li = "", Nome = "ESTAÇÃO INSPEÇÃO VEICULAR LTDA." },
            //    new Organismo() { CodigoOIA = 0722, Nivel_Li = "", Nome = "Núcleo de Inspeção Veicular Ltda. - ME." },
            //    new Organismo() { CodigoOIA = 0723, Nivel_Li = "", Nome = "PANTANAL INSPEÇÃO VEICULAR LTDA .- EPP." },
            //    new Organismo() { CodigoOIA = 0724, Nivel_Li = "", Nome = "CAIBI INSPEÇÕES VEICULARES LTDA." },
            //    new Organismo() { CodigoOIA = 0727, Nivel_Li = "", Nome = "CIVI - Centro de Inspeção Veicular de Arapiraca Ltda. - ME." },
            //    new Organismo() { CodigoOIA = 0731, Nivel_Li = "", Nome = "CONRADO E SILVA LTDA. - ME." },
            //    new Organismo() { CodigoOIA = 0734, Nivel_Li = "", Nome = "INSPECAMPO INSPEÇÃO DE SEGURANÇA VEICULAR DE CAMPO LARGO LTDA." },
            //    new Organismo() { CodigoOIA = 0735, Nivel_Li = "", Nome = "EJB - SERVIÇOS DE INSPEÇÃO TÉCNICA VEICULAR LTDA." },
            //    new Organismo() { CodigoOIA = 0738, Nivel_Li = "", Nome = "NOVA INSPEÇÃO VEICULAR LTDA. - EPP." },
            //    new Organismo() { CodigoOIA = 0740, Nivel_Li = "", Nome = "ISTO MAIS VISTORIA VEICULAR LTDA." },
            //    new Organismo() { CodigoOIA = 0742, Nivel_Li = "", Nome = "BR INSPEÇÃO TÉCNICA VEICULAR - ME." },
            //    new Organismo() { CodigoOIA = 0752, Nivel_Li = "", Nome = "JULIANA DE BRITO PEREIRA - ME." },
            //    new Organismo() { CodigoOIA = 0754, Nivel_Li = "", Nome = "Maringá Inspeção Veicular Ltda. - ME." },
            //    new Organismo() { CodigoOIA = 0755, Nivel_Li = "", Nome = "AFD INSPEÇÕES VEICULARES EIRELI - ME." },
            //    new Organismo() { CodigoOIA =  7777, Nivel_Li = "", Nome = "TEST ORGANISMO - 2", EhAtivo = true },
            //    new Organismo() { CodigoOIA =  0001, Nivel_Li = "", Nome = "TESTE-ORGANISMO" });



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
