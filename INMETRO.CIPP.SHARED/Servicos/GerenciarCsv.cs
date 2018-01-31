using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using INMETRO.CIPP.DOMINIO.Modelos;
using INMETRO.CIPP.SHARED.Interfaces;

namespace INMETRO.CIPP.SHARED.Servicos
{

    public class GerenciarCsv : IGerenciarCsv
    {
        public List<string> linhas;
        private static string commaReplacement = "_;)(*_";
        private List<string> inputColumns;

        public Inspecao ObterDadosInspecao(string diretorio)
        {
            var insp = LerLinhasCSV(diretorio);
            return insp;
        }

        public bool ExcluirArquivoCippCsv(string diretorio)
        {
            try
            {
                string[] files = Directory.GetFiles(diretorio, "*.csv");
                string fileNamePath = files[0];
                if (File.Exists(fileNamePath))
                {
                    File.Delete(fileNamePath);
                    return true;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return false;

        }

        private Inspecao LerLinhasCSV(string diretorio)
        {
            string[] files = Directory.GetFiles(diretorio, "*.csv");

            if (files.Length > 0)
            {
                var inspecao = new Inspecao();

                linhas = File.ReadAllLines(files[0]).Where(l => !string.IsNullOrWhiteSpace(l) && !l.StartsWith("//")).ToList();

                for (int i = 1; i < linhas.Count; i++)
                {
                    inspecao = ObterInspecao(linhas[i]);
                }

                if (inspecao != null)
                    return inspecao;
            }

            return new Inspecao();
        }

        private Inspecao ObterInspecao(string inputLine)
        {
            string inputLineWithoutExtraCommas = replaceDelimitersWithinQuotes(inputLine);
            inputColumns = inputLineWithoutExtraCommas.Split(',').ToList();

            Inspecao cipp = new Inspecao();

            for (int i = 0; i < inputColumns.Count; i++)
            {
                cipp.CodigoOIA = inputColumns[0];
                cipp.CodigoCIPP = inputColumns[1];
                cipp.PlacaLicenca = inputColumns[2];
                cipp.NumeroEquipamento = Convert.ToInt32(inputColumns[3]);
                cipp.DataInspecao = Convert.ToDateTime(inputColumns[4]);
                cipp.ResponsavelTecnico = inputColumns[5];

                break;
            }

            return cipp;
        }

        private static string replaceDelimitersWithinQuotes(string inputLine)
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