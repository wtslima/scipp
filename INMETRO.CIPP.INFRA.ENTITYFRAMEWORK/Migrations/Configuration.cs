
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
            //    new Organismo() { CodigoOIA = 0016, Nivel_Li = "", Nome = "QUALITY INSPE��O VEICULAR LTDA. - EPP." },
            //    new Organismo() { CodigoOIA = 0082, Nivel_Li = "", Nome = "TRANSTECH IVESUR BRASIL LTDA." },
            //    new Organismo() { CodigoOIA = 0112, Nivel_Li = "01", Nome = "Tiririca Inspe��o e Seguran�a Veicular Ltda" },
            //    new Organismo() { CodigoOIA = 0142, Nivel_Li = "", Nome = "Inscape Inspe��es Ltda" },
            //    new Organismo() { CodigoOIA = 0224, Nivel_Li = "", Nome = "Tecsul Inspe��o Veicular Ltda." },
            //    new Organismo() { CodigoOIA = 0257, Nivel_Li = "", Nome = "ITTRAN - Instituto Tecnol�gico de Transporte Ltda." },
            //    new Organismo() { CodigoOIA = 0269, Nivel_Li = "", Nome = "CENTRAL PORTO ALEGRE DE INSPE��ES VEICULARES LTDA." },
            //    new Organismo() { CodigoOIA = 0279, Nivel_Li = "", Nome = "NACIONAL INSPE��ES LTDA." },
            //    new Organismo() { CodigoOIA = 0289, Nivel_Li = "", Nome = "EMBRAVEC - EMPRESA BRASILEIRA DE INSPE��O VEICULAR LTDA. - EPP." },
            //    new Organismo() { CodigoOIA = 0294, Nivel_Li = "", Nome = "WEST ENGENHARIA DE INSPE��O LTDA." },
            //    new Organismo() { CodigoOIA = 0307, Nivel_Li = "", Nome = "SBI-RS - INSPE��ES DE TANQUES E RESERVAT�RIOS LTDA." },
            //    new Organismo() { CodigoOIA = 0319, Nivel_Li = "", Nome = "SBI - SERRANA INSPE��ES DE PRODUTOS PERIGOSOS LTDA." },
            //    new Organismo() { CodigoOIA = 0322, Nivel_Li = "", Nome = "BBI SERVI�OS DE INSPE��O VEICULAR LTDA." },
            //    new Organismo() { CodigoOIA = 0328, Nivel_Li = "", Nome = "INSPECENTRO INSPE��O VEICULAR EIRELI - ME." },
            //    new Organismo() { CodigoOIA = 0351, Nivel_Li = "", Nome = "ITESV - Instituto Tecnologia de Engenharia e Seguran�a Veicular Ltda." },
            //    new Organismo() { CodigoOIA = 0368, Nivel_Li = "", Nome = "BUREAU BRASILEIRO DE INSPE��O LTDA" },
            //    new Organismo() { CodigoOIA = 0420, Nivel_Li = "", Nome = "ATIVE OURINHOS INSPE��ES VEICULARES LTDA." },
            //    new Organismo() { CodigoOIA = 0424, Nivel_Li = "", Nome = "IVECAR CENTRO INSPE��O VEICULAR LTDA.	" },
            //    new Organismo() { CodigoOIA = 0426, Nivel_Li = "", Nome = "SINTEV - Sistema de Inspe��o T�cnica Veicular Ltda" },
            //    new Organismo() { CodigoOIA = 0427, Nivel_Li = "", Nome = "GCL - INSPE��ES VEICULAR LTDA" },
            //    new Organismo() { CodigoOIA = 0430, Nivel_Li = "", Nome = "CTV - Centro de Tecnologia Veicular Ltda." },
            //    new Organismo() { CodigoOIA = 0431, Nivel_Li = "", Nome = "Monitor Engenharia - Inspe��o T�cnica de Ve�culos Ltda" },
            //    new Organismo() { CodigoOIA = 0434, Nivel_Li = "", Nome = "TRIVELATO & QUEIROZ INSPE��ES VEICULARES LTDA." },
            //    new Organismo() { CodigoOIA = 0439, Nivel_Li = "", Nome = "N�vel Inspe��es Ltda." },
            //    new Organismo() { CodigoOIA = 0440, Nivel_Li = "", Nome = "FIT INSPE��O VEICULAR LTDA." },
            //    new Organismo() { CodigoOIA = 0443, Nivel_Li = "", Nome = "AVAL SOROCABA INSPE��ES VEICULARES LTDA." },
            //    new Organismo() { CodigoOIA = 0450, Nivel_Li = "", Nome = "CPI Veicular Ltda" },
            //    new Organismo() { CodigoOIA = 0455, Nivel_Li = "", Nome = "�POTIGUAR INSPE��ES DE SEGURAN�A VEICULAR LTDA. - ME." },
            //    new Organismo() { CodigoOIA = 0463, Nivel_Li = "", Nome = "CBI - Centro Brasileiro de Inspe��o Veicular Ltda. - EPP." },
            //    new Organismo() { CodigoOIA = 0465, Nivel_Li = "", Nome = "LAUDOCENTER INSPE��O VEICULAR LTDA. - ME." },
            //    new Organismo() { CodigoOIA = 0467, Nivel_Li = "", Nome = "FIT INSPE��O VEICULAR LTDA." },
            //    new Organismo() { CodigoOIA = 0469, Nivel_Li = "", Nome = "CARDOSO DE PAULA VISTORIA DE SEGURAN�A AUTOMOBILISTICA LTDA" },
            //    new Organismo() { CodigoOIA = 0471, Nivel_Li = "", Nome = "MARTINS & CAETANO INSPE��ES VEICULARES LTDA. - ME." },
            //    new Organismo() { CodigoOIA = 0474, Nivel_Li = "", Nome = "GOI�NIA INSPE��ES LTDA" },
            //    new Organismo() { CodigoOIA = 0476, Nivel_Li = "", Nome = "NATAN - CAPACITA��O PARA TRANSPORTE DE PRODUTOS PERIGOSOS LTDA - ME." },
            //    new Organismo() { CodigoOIA = 0479, Nivel_Li = "", Nome = "INSPECIONAR - INSPE��O VEICULAR LTDA." },
            //    new Organismo() { CodigoOIA = 0481, Nivel_Li = "", Nome = "Ita Center Inspe��o Veicular Ltda." },
            //    new Organismo() { CodigoOIA = 0482, Nivel_Li = "", Nome = "VITRAN ENGENHARIA LTDA - ME" },
            //    new Organismo() { CodigoOIA = 0485, Nivel_Li = "", Nome = "EIVA INSPE��O VEICULAR LTDA" },
            //    new Organismo() { CodigoOIA = 0493, Nivel_Li = "", Nome = "INSPECENTRO INSPE��O VEICULAR EIRELI - ME." },
            //    new Organismo() { CodigoOIA = 0498, Nivel_Li = "", Nome = "SIMON INSPE��O VEICULAR LTDA." },
            //    new Organismo() { CodigoOIA = 0501, Nivel_Li = "", Nome = "Tecmetro Inspe��es Juiz de Fora Ltda" },
            //    new Organismo() { CodigoOIA = 0503, Nivel_Li = "", Nome = "VISTO-CAR PAUL�NIA INSPE��O VEICULAR LTDA." },
            //    new Organismo() { CodigoOIA = 0504, Nivel_Li = "", Nome = "BRASIL MG INSPE��ES VEICULARES JF LTDA." },
            //    new Organismo() { CodigoOIA = 0506, Nivel_Li = "", Nome = "VISTO-CAR Piracicaba Centro de Inspe��o Veicular Ltda" },
            //    new Organismo() { CodigoOIA = 0512, Nivel_Li = "", Nome = "ATIVE MARING� INSPE��ES VEICULARES LTDA. - ME" },
            //    new Organismo() { CodigoOIA = 0516, Nivel_Li = "", Nome = "SETA INSTITUI��O TECNICA DE INSPE��O VEICULAR LTDA" },
            //    new Organismo() { CodigoOIA = 0518, Nivel_Li = "", Nome = "RAMAF INSPE��ES LTDA. - ME." },
            //    new Organismo() { CodigoOIA = 0523, Nivel_Li = "", Nome = "DE PAULA VILAS BOAS - VISTORIA DE SEGURAN�A AUTOMOBILISTICA LTDA" },
            //    new Organismo() { CodigoOIA = 0524, Nivel_Li = "", Nome = "ENTRO DE INSPE��O VEICULAR DE ITABAIANA LTDA. - ME." },
            //    new Organismo() { CodigoOIA = 0525, Nivel_Li = "", Nome = "Nasa Inspe��o Veicular Ltda" },
            //    new Organismo() { CodigoOIA = 0526, Nivel_Li = "", Nome = "CIAA - CENTRO INSPE��O AUTOMOTIVA AMERICANA LTDA. - EPP." },
            //    new Organismo() { CodigoOIA = 0530, Nivel_Li = "", Nome = "Costa Fernandes Inspe��es Veiculares Ltda." },
            //    new Organismo() { CodigoOIA = 0531, Nivel_Li = "", Nome = "ADAR INSPE��O VEICULAR E CARGAS PERIGOSAS LTDA. - ME." },
            //    new Organismo() { CodigoOIA = 0532, Nivel_Li = "", Nome = "JABOAT�O DESCONTAMINA��O E INSPE��ES VEICULARES LTDA." },
            //    new Organismo() { CodigoOIA = 0536, Nivel_Li = "", Nome = "NIVEL - N�CLEO DE INSPE��ES VEICULARES LTDA. - ME." },
            //    new Organismo() { CodigoOIA = 0540, Nivel_Li = "", Nome = "LARISSA FIRMINO DA SILVA - EPP." },
            //    new Organismo() { CodigoOIA = 0542, Nivel_Li = "", Nome = "IVECAR CENTRO INSPE��O VEICULAR LTDA. - ME." },
            //    new Organismo() { CodigoOIA = 0545, Nivel_Li = "", Nome = "PAES DE OLIVEIRA & GOMES LTDA" },
            //    new Organismo() { CodigoOIA = 0549, Nivel_Li = "", Nome = "CENTRAL CHAPEC� DE INSPE��ES VEICULARES LTDA." },
            //    new Organismo() { CodigoOIA = 0551, Nivel_Li = "", Nome = "Central Pelotas de Inspe��es Veiculares Ltda" },
            //    new Organismo() { CodigoOIA = 0552, Nivel_Li = "", Nome = "ITAMETRO INSPE��ES VEICULARES - ME." },
            //    new Organismo() { CodigoOIA = 0554, Nivel_Li = "", Nome = "Atlantida Inspe��o Veicular Ltda-EPP." },
            //    new Organismo() { CodigoOIA = 0556, Nivel_Li = "", Nome = "VISTEC - Vistoria T�cnica Ltda" },
            //    new Organismo() { CodigoOIA = 0559, Nivel_Li = "", Nome = "Visto-Car S�o Jos� Inspe��o Veicular Ltda." },
            //    new Organismo() { CodigoOIA = 0562, Nivel_Li = "", Nome = "CETEM - Centro Tecnol�gico Mec�nico Ltda." },
            //    new Organismo() { CodigoOIA = 0563, Nivel_Li = "", Nome = "ARTE REAL INSPE��O VEICULAR LTDA. - ME." },
            //    new Organismo() { CodigoOIA = 0565, Nivel_Li = "", Nome = "�SERITRAN - SERVI�O DE INSPE��O EM TRANSPORTES LTDA." },
            //    new Organismo() { CodigoOIA = 0568, Nivel_Li = "", Nome = "INSPETRO DE MACA� LTDA." },
            //    new Organismo() { CodigoOIA = 0569, Nivel_Li = "", Nome = "INSPEVEL - INSPE��O VEICULAR DE CASCAVEL LTDA." },
            //    new Organismo() { CodigoOIA = 0570, Nivel_Li = "", Nome = "SETA INSTITUI��O T�CNICA DE INSPE��O VEICULAR LTDA. - EPP." },
            //    new Organismo() { CodigoOIA = 0574, Nivel_Li = "", Nome = "VISTO-CAR ANCHIETA INSPE��O VEICULAR LTDA. - EPP" },
            //    new Organismo() { CodigoOIA = 0575, Nivel_Li = "", Nome = "SETA REALENGO INSTITUI��O T�CNICA DE INSPE��O VEICULAR LTDA." },
            //    new Organismo() { CodigoOIA = 0576, Nivel_Li = "", Nome = "Inspeguar� Inspe��o Veicular Guarapuava Ltda." },
            //    new Organismo() { CodigoOIA = 0579, Nivel_Li = "", Nome = "ARAUC�RIA INSPE��ES LTDA." },
            //    new Organismo() { CodigoOIA = 0583, Nivel_Li = "", Nome = "SINAU - SERVI�O DE INSPE��O AUTOMOTIVA LTDA. - ME." },
            //    new Organismo() { CodigoOIA = 0584, Nivel_Li = "", Nome = "SANTO ANDR� INSPE��O VEICULAR LTDA. - EPP." },
            //    new Organismo() { CodigoOIA = 0586, Nivel_Li = "", Nome = "LIPP - LIMEIRA INSPE��ES LTDA." },
            //    new Organismo() { CodigoOIA = 0587, Nivel_Li = "", Nome = "GUARU INSPE��ES LTDA. - EPP." },
            //    new Organismo() { CodigoOIA = 0588, Nivel_Li = "", Nome = "SEVEL - Seguran�a Veicular Paran� Ltda. - EPP." },
            //    new Organismo() { CodigoOIA = 0589, Nivel_Li = "", Nome = "JCP Inspe��es Veiculares LTDA." },
            //    new Organismo() { CodigoOIA = 0590, Nivel_Li = "", Nome = "ARA PP INSPE��O VEICULAR LTDA." },
            //    new Organismo() { CodigoOIA = 0591, Nivel_Li = "", Nome = "GMAP INSPE��O VEICULAR LTDA. - EPP." },
            //    new Organismo() { CodigoOIA = 0593, Nivel_Li = "", Nome = "ECV - EMPRESA CAPIXABA DE VISTORIAS LTDA." },
            //    new Organismo() { CodigoOIA = 0594, Nivel_Li = "", Nome = "CRIVE - CENTRO REG. DE INSPE��O VEICULAR LTDA." },
            //    new Organismo() { CodigoOIA = 0598, Nivel_Li = "", Nome = "ITV INSPE��O T�CNICA VEICULAR LTDA." },
            //    new Organismo() { CodigoOIA = 0601, Nivel_Li = "", Nome = "MAXY ENGENHARIA DE INSPE��O LTDA." },
            //    new Organismo() { CodigoOIA = 0603, Nivel_Li = "", Nome = "IVETU INSPE��O VEICULAR TUBAR�O LTDA. - ME." },
            //    new Organismo() { CodigoOIA = 0606, Nivel_Li = "", Nome = "INSPENORTE INSPE��O VEICULAR LTDA." },
            //    new Organismo() { CodigoOIA = 0610, Nivel_Li = "", Nome = "CASCAVEL INSPE��ES LTDA. - ME." },
            //    new Organismo() { CodigoOIA = 0611, Nivel_Li = "", Nome = "ITV - Inspe��o T�cnica Veicular Ltda." },
            //    new Organismo() { CodigoOIA = 0612, Nivel_Li = "", Nome = "CIAUTO - CENTRO DE INSPE��O AUTOMOTIVA LTDA." },
            //    new Organismo() { CodigoOIA = 0613, Nivel_Li = "", Nome = "ETESUL PLANALTO INSPE��O VEICULAR LTDA ME" },
            //    new Organismo() { CodigoOIA = 0614, Nivel_Li = "", Nome = "NECAVA - Inspe��o e Pesquisa em Transporte Ltda. - ME." },
            //    new Organismo() { CodigoOIA = 0616, Nivel_Li = "", Nome = "DELTA SERVI�OS INSPE��O LTDA. - EPP." },
            //    new Organismo() { CodigoOIA = 0622, Nivel_Li = "", Nome = "ECIV EMPRESA CRICIUMENSE DE INSPE��ES VEICULARES LTDA" },
            //    new Organismo() { CodigoOIA = 0623, Nivel_Li = "", Nome = "PANTANAL INSPE��ES LTDA ME" },
            //    new Organismo() { CodigoOIA = 0624, Nivel_Li = "", Nome = "CEI - CENTRO ESPECIALIZADO DE INSPE��ES LTDA. - EPP." },
            //    new Organismo() { CodigoOIA = 0625, Nivel_Li = "", Nome = "MINAS INSPE��ES EM VE�CULOS E EQUIPAMENTOS LTDA. - ME." },
            //    new Organismo() { CodigoOIA = 0627, Nivel_Li = "", Nome = "VISAVERO- Vistorias e Inspe��es de Seguran�a e Ambiental em Ve�culos Rodovi�rios LTDA." },
            //    new Organismo() { CodigoOIA = 0628, Nivel_Li = "", Nome = "CIC - CENTRO DE INSPE��ES E CONSULTORIAS LTDA. EPP." },
            //    new Organismo() { CodigoOIA = 0630, Nivel_Li = "", Nome = "VIU VEICULAR INSPE��ES UBERL�NDIA LTDA. - ME." },
            //    new Organismo() { CodigoOIA = 0631, Nivel_Li = "", Nome = "SERITRAM - SERVI�O DE INSPE��O EM TRANSPORTES DE MARAB� LTDA. - EPP." },
            //    new Organismo() { CodigoOIA = 0632, Nivel_Li = "", Nome = "PARAN� INSPE��ES - EIRELLI - ME." },
            //    new Organismo() { CodigoOIA = 0633, Nivel_Li = "", Nome = "INSPESAN INSPE��ES MURIA� LTDA. - ME." },
            //    new Organismo() { CodigoOIA = 0634, Nivel_Li = "", Nome = "F�NIX INSPE��ES VEICULARES LTDA." },
            //    new Organismo() { CodigoOIA = 0635, Nivel_Li = "", Nome = "Central Santa Maria de Inspe��es Veiculares Ltda. - EPP." },
            //    new Organismo() { CodigoOIA = 0636, Nivel_Li = "", Nome = "POLO CAPUAVA - INSPE��O VEICULAR LTDA. - EPP." },
            //    new Organismo() { CodigoOIA = 0637, Nivel_Li = "", Nome = "CIVIC 1 - Inspe��o Veicular EIRELI - EPP." },
            //    new Organismo() { CodigoOIA = 0640, Nivel_Li = "", Nome = "WEST ENGENHARIA DE INSPE��O LTDA." },
            //    new Organismo() { CodigoOIA = 0641, Nivel_Li = "", Nome = "VITRAN ENGENHARIA LTDA. - EPP." },
            //    new Organismo() { CodigoOIA = 0645, Nivel_Li = "", Nome = "WEST ENGENHARIA DE INSPE��O LTDA. - FILIAL MARITUBA/PA" },
            //    new Organismo() { CodigoOIA = 0646, Nivel_Li = "", Nome = "WEST ENGENHARIA DE INSPE��O LTDA. - GUARULHOS" },
            //    new Organismo() { CodigoOIA = 0647, Nivel_Li = "", Nome = "METROPOLITAN INSPE��ES LTDA - EPP" },
            //    new Organismo() { CodigoOIA = 0652, Nivel_Li = "", Nome = "MULT CEAR� INSPE��O LTDA. - ME." },
            //    new Organismo() { CodigoOIA = 0656, Nivel_Li = "", Nome = "Master Inspe��es Veiculares Ltda - EPP" },
            //    new Organismo() { CodigoOIA = 0657, Nivel_Li = "", Nome = "SETA - REALENGO INSTITUI��O T�CNICA DE INSPE��O VEICULAR LTDA." },
            //    new Organismo() { CodigoOIA = 0659, Nivel_Li = "", Nome = "SETA NORTE T�CNICA INSPE��O VEICULAR LTDA. - EPP." },
            //    new Organismo() { CodigoOIA = 0663, Nivel_Li = "", Nome = "AVAL LONDRINA INSPE��ES VEICULARES LTDA." },
            //    new Organismo() { CodigoOIA = 0665, Nivel_Li = "", Nome = "NIVEL N�CLEO DE INSPE��ES VEICULARES DE AN�POLIS LTDA - ME" },
            //    new Organismo() { CodigoOIA = 0668, Nivel_Li = "", Nome = "BR VALE ENGENHARIA DE INSPE��O LTDA." },
            //    new Organismo() { CodigoOIA = 0672, Nivel_Li = "", Nome = "BETIM INSPE��ES LTDA - ME" },
            //    new Organismo() { CodigoOIA = 0674, Nivel_Li = "", Nome = "L�DER SUL INSPE��ES LTDA. - ME." },
            //    new Organismo() { CodigoOIA = 0676, Nivel_Li = "", Nome = "Paraiba Servi�o de Inspe��o Veicular Ltda - EPP" },
            //    new Organismo() { CodigoOIA = 0677, Nivel_Li = "", Nome = "CETEI - CENTRO T�CNICO DE ENGENHARIA DE INSPE��O LTDA." },
            //    new Organismo() { CodigoOIA = 0679, Nivel_Li = "", Nome = "CINSPEMAR - Centro Especializado de Inspe��es do Maranh�o Ltda. - ME." },
            //    new Organismo() { CodigoOIA = 0680, Nivel_Li = "", Nome = "SETA - Institui��o T�cnica de Inspe��o Veicular Ltda. - EPP." },
            //    new Organismo() { CodigoOIA = 0684, Nivel_Li = "", Nome = "SHERLOCKAR INSPE��O VEICULAR EIRELLI." },
            //    new Organismo() { CodigoOIA = 0687, Nivel_Li = "", Nome = "NORTEKAR - INSPE��O VEICULAR LTDA EPP" },
            //    new Organismo() { CodigoOIA = 0689, Nivel_Li = "", Nome = "DCX Inspe�ao e Engenharia Ltda" },
            //    new Organismo() { CodigoOIA = 0691, Nivel_Li = "", Nome = "CIAP CENTRO DE INSPE��O AUTOMOTIVA PAUL�NIA LTDA" },
            //    new Organismo() { CodigoOIA = 0692, Nivel_Li = "", Nome = "Seta Institui��o T�cnica de Inspe��o Veicular Ltda. - EPP." },
            //    new Organismo() { CodigoOIA = 0696, Nivel_Li = "", Nome = "ITAJA� INSPE��ES VEICULARES LTDA" },
            //    new Organismo() { CodigoOIA = 0698, Nivel_Li = "", Nome = "BUREAU ITAJAIENSE DE INSPE��O VEICULAR LTDA - ME" },
            //    new Organismo() { CodigoOIA = 0699, Nivel_Li = "", Nome = "FIT INSPE��O VEICULAR LTDA." },
            //    new Organismo() { CodigoOIA = 0702, Nivel_Li = "", Nome = "MAYARA LETICIA BALESTERO. - ME." },
            //    new Organismo() { CodigoOIA = 0704, Nivel_Li = "", Nome = "SIVIC SEGURAN�A E INSPE��O VEICULAR LTDA." },
            //    new Organismo() { CodigoOIA = 0705, Nivel_Li = "01", Nome = "MENDON�A, MENDON�A & SCHUNKE LTDA. - ME." },
            //    new Organismo() { CodigoOIA = 0706, Nivel_Li = "", Nome = "SETA S�O LU�S INSTITUI��O T�CNICA DE INSPE��O VEICULAR LTDA. - EPP." },
            //    new Organismo() { CodigoOIA = 0712, Nivel_Li = "", Nome = "ATIVE PARANAGU� INSPE��ES VEICULARES LTDA. - ME." },
            //    new Organismo() { CodigoOIA = 0714, Nivel_Li = "", Nome = "Otimiza Inspe��o Veicular Ltda. - EPP." },
            //    new Organismo() { CodigoOIA = 0716, Nivel_Li = "", Nome = "ESTA��O INSPE��O VEICULAR LTDA." },
            //    new Organismo() { CodigoOIA = 0722, Nivel_Li = "", Nome = "N�cleo de Inspe��o Veicular Ltda. - ME." },
            //    new Organismo() { CodigoOIA = 0723, Nivel_Li = "", Nome = "PANTANAL INSPE��O VEICULAR LTDA .- EPP." },
            //    new Organismo() { CodigoOIA = 0724, Nivel_Li = "", Nome = "CAIBI INSPE��ES VEICULARES LTDA." },
            //    new Organismo() { CodigoOIA = 0727, Nivel_Li = "", Nome = "CIVI - Centro de Inspe��o Veicular de Arapiraca Ltda. - ME." },
            //    new Organismo() { CodigoOIA = 0731, Nivel_Li = "", Nome = "CONRADO E SILVA LTDA. - ME." },
            //    new Organismo() { CodigoOIA = 0734, Nivel_Li = "", Nome = "INSPECAMPO INSPE��O DE SEGURAN�A VEICULAR DE CAMPO LARGO LTDA." },
            //    new Organismo() { CodigoOIA = 0735, Nivel_Li = "", Nome = "EJB - SERVI�OS DE INSPE��O T�CNICA VEICULAR LTDA." },
            //    new Organismo() { CodigoOIA = 0738, Nivel_Li = "", Nome = "NOVA INSPE��O VEICULAR LTDA. - EPP." },
            //    new Organismo() { CodigoOIA = 0740, Nivel_Li = "", Nome = "ISTO MAIS VISTORIA VEICULAR LTDA." },
            //    new Organismo() { CodigoOIA = 0742, Nivel_Li = "", Nome = "BR INSPE��O T�CNICA VEICULAR - ME." },
            //    new Organismo() { CodigoOIA = 0752, Nivel_Li = "", Nome = "JULIANA DE BRITO PEREIRA - ME." },
            //    new Organismo() { CodigoOIA = 0754, Nivel_Li = "", Nome = "Maring� Inspe��o Veicular Ltda. - ME." },
            //    new Organismo() { CodigoOIA = 0755, Nivel_Li = "", Nome = "AFD INSPE��ES VEICULARES EIRELI - ME." },
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
