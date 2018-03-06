using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using INMETRO.CIPP.SHARED.Email;
using INMETRO.CIPP.SHARED.Helper;
using INMETRO.CIPP.SHARED.Interfaces;
using INMETRO.CIPP.SHARED.ModelShared;

namespace INMETRO.CIPP.SHARED.Servicos
{

    public class GerenciarCsv : IGerenciarCsv
    {
        public List<string> Linhas;
        private static string commaReplacement = "_;)(*_";
        private List<string> _inputColumns;

        public InspecaoCsvModel ObterDadosInspecao(string diretorio)
        {
            var inspecaoLine = LerLinhasCsv(diretorio);
            var inspecaoModel = ObterInspecao(inspecaoLine);
            return inspecaoModel ?? new InspecaoCsvModel();
        }

        public bool ExcluirArquivoCippCsv(string diretorio)
        {
            try
            {
                var files = Directory.GetFiles(diretorio, "*.csv");
                var fileNamePath = files[0];
                if (File.Exists(fileNamePath))
                {
                    File.Delete(fileNamePath);
                    return true;
                }

            }
            catch (Exception e)
            {
                throw e;
            }

            return false;

        }

        public string CriarArquivoInspecoesAnexo(IList<InspecaoCsvModel> inspecoes)
        {
           // string physicalPathToDirectory = Environment.GetEnvironmentVariable("CIPP");
            string fileName =  ".csv";
            var date = DateTime.Now.ToString("yyyy-MM-dd_HH-mm", CultureInfo.InvariantCulture);
            ExportarCSV inspecaoCsv = new ExportarCSV();
            Notificacao email = new Notificacao();
            foreach (var item in inspecoes)
            {
                inspecaoCsv.AddRow();
                inspecaoCsv["Codigo Cipp"] = item.CodigoCipp;
                inspecaoCsv["Codigo OIA-PP"] = item.CodigoOia;
                inspecaoCsv["Placa"] = item.PlacaLicenca;
                inspecaoCsv["Equipamento"] = item.NumeroEquipamento;
                inspecaoCsv["Data da Inspecao"] = item.DataInspecao.Date;
            }
            var path = "CIPP -" + date + fileName;
           

            inspecaoCsv.ExportToFile(path);
            //todo:Informar emails que irão receber emails da rotina automática
            email.EnviarEmailComAnexo("wtslima@gmail.com", path);
            email.EnviarEmailComAnexo("astrindade@colaborador.inmetro.gov.br", path);
            return path;
        }

        private string LerLinhasCsv(string diretorio)
        {
            var files = Directory.GetFiles(diretorio, "*.csv");

            if (files.Length <= 0) return string.Empty;
            var inspecaoLinha = string.Empty;

            Linhas = File.ReadAllLines(files[0]).Where(l => !string.IsNullOrWhiteSpace(l) && !l.StartsWith("//")).ToList();

            for (var i = 0; i < Linhas.Count; i++)
            {
                inspecaoLinha = Linhas[i];
            }

            return string.IsNullOrWhiteSpace(inspecaoLinha) ? string.Empty : inspecaoLinha;
        }

        private InspecaoCsvModel ObterInspecao(string inputLine)
        {
         
            string inputLineWithoutExtraCommas = ReplaceDelimitersWithinQuotes(inputLine);
            _inputColumns = inputLineWithoutExtraCommas.Split(',').ToList();

            var inspecao = new InspecaoCsvModel();

            for (var i = 0; i < _inputColumns.Count; )
            {
                inspecao.CodigoOia = _inputColumns[0];
                inspecao.CodigoCipp = _inputColumns[1];
                inspecao.PlacaLicenca = _inputColumns[2];
                inspecao.NumeroEquipamento =  Convert.ToInt32(_inputColumns[3]);

                var date = DateTime.ParseExact(_inputColumns[4], "ddMMyyyy", CultureInfo.InvariantCulture);
                inspecao.DataInspecao = date;



                break;
            }

            return inspecao;
        }

        private static string ReplaceDelimitersWithinQuotes(string inputLine)
        {
            bool inQuotes = false;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < inputLine.Length; i++)
            {
                char c = inputLine[i];
                if (c == '\"')
                {
                    inQuotes = !inQuotes;
                }
                if (c == ',')
                {
                    sb.Append(inQuotes ? commaReplacement : "" + ",");
                }
                else
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
    }
}