using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using INMETRO.CIPP.DOMINIO.Mensagens;
using INMETRO.CIPP.DOMINIO.Modelos;
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

        public InspecaoCsvModel ObterDadosInspecao(string diretorio, IntegracaoInfos ftpInfo)
        {
            if (ExisteArquivoCsv(diretorio))
            {
                var inspecaoLine = LerLinhasCsv(diretorio);
                var inspecaoModel = ObterInspecao(inspecaoLine, ftpInfo);
                return inspecaoModel ?? new InspecaoCsvModel();
            }
            return new InspecaoCsvModel
            {
                Excecao = new ExcecaoCsv
                {
                    ExisteExcecao = true,
                    Mensagem = string.Format(MensagemNegocio.ArquivoCSVNaoEncontrado, " ", ftpInfo.Organismo.CodigoOIA)
                }
            };
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
            string filePhisical = Configurations.DiretorioRaiz();
            string fileName = ".csv";
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
            var path = filePhisical + "CIPP -" + date + fileName;


            inspecaoCsv.ExportToFile(path);

            email.EnviarEmailComAnexo("wslima@colaborador.inmetro.gov.br", path);
            email.EnviarEmailComAnexo("scipp-recebe@inmetro.gov.br", path);
            return path;
        }

        private string LerLinhasCsv(string diretorio)
        {

            var files = Directory.GetFiles(diretorio, "*.csv");

            if (files.Length <= 0)
            {
                DeletarDiretorioLocalInspecao(diretorio);
                return string.Empty;
            }
            var inspecaoLinha = string.Empty;

            Linhas = File.ReadAllLines(files[0]).Where(l => !string.IsNullOrWhiteSpace(l) && !l.StartsWith("//")).ToList();

            for (var i = 0; i < Linhas.Count; i++)
            {
                inspecaoLinha = Linhas[i];
            }

            return string.IsNullOrWhiteSpace(inspecaoLinha) ? string.Empty : inspecaoLinha;
        }

        private InspecaoCsvModel ObterInspecao(string inputLine, IntegracaoInfos ftpInfo)
        {
            try
            {
                DateTime dDate;
                string format;
                CultureInfo provider = CultureInfo.InvariantCulture;
                format = "yyyyMMdd";
                string inputLineWithoutExtraCommas = ReplaceDelimitersWithinQuotes(inputLine);
                _inputColumns = inputLineWithoutExtraCommas.Split(',').ToList();

                var inspecao = new InspecaoCsvModel();
                if (_inputColumns.Count == 5)
                {
                    for (var i = 0; i < _inputColumns.Count;)
                    {
                        inspecao.CodigoOia = ftpInfo.DiretorioInspecaoLocal;
                        if (!string.IsNullOrEmpty(_inputColumns[1]))
                        {
                            inspecao.CodigoCipp = _inputColumns[1];
                        }
                        else
                        {
                            return new InspecaoCsvModel
                            {

                                Excecao = new ExcecaoCsv
                                {
                                    ExisteExcecao = true,
                                    Mensagem = string.Format(MensagemNegocio.CodidoCippNaoInformado)
                                }
                            };
                        }

                        if (!string.IsNullOrEmpty(_inputColumns[2]))
                        {
                            if (_inputColumns[2].Length > 6)
                            {
                                inspecao.PlacaLicenca = _inputColumns[2];
                            }
                            else
                            {
                                inspecao.PlacaLicenca = _inputColumns[2].ToString();
                                return new InspecaoCsvModel
                                {
                                    Excecao = new ExcecaoCsv
                                    {
                                        ExisteExcecao = true,
                                        Mensagem = string.Format(MensagemNegocio.PlacaNaoExiste, inspecao.PlacaLicenca, inspecao.CodigoCipp, inspecao.CodigoOia)
                                    }
                                };
                            }

                        }
                        else
                        {
                            inspecao.PlacaLicenca = "ADG0000";
                        }

                        if (!string.IsNullOrEmpty(_inputColumns[3]))
                        {
                            inspecao.NumeroEquipamento = _inputColumns[3];
                        }
                        else
                        {

                            return new InspecaoCsvModel
                            {
                                Excecao = new ExcecaoCsv
                                {
                                    ExisteExcecao = true,
                                    Mensagem = string.Format(MensagemNegocio.NumeroDoEquipamentoNaoInformado, inspecao.CodigoCipp, ftpInfo.Organismo.CodigoOIA)
                                }
                            };
                        }

                        if (!string.IsNullOrEmpty(_inputColumns[4]))
                        {
                            try
                            {
                               
                                if (DateTime.TryParseExact(_inputColumns[4], "ddMMyyyy", provider, DateTimeStyles.None,
                                    out dDate) || DateTime.TryParseExact(_inputColumns[4], format, provider, DateTimeStyles.None, out dDate))
                                {
                                    inspecao.DataInspecao = dDate;
                                }
                                else
                                {
                                    DateTime dt = DateTime.Parse(_inputColumns[4]);
                                }
                                
                            }
                            catch 
                            {

                                return new InspecaoCsvModel
                                {
                                    Excecao = new ExcecaoCsv
                                    {
                                        ExisteExcecao = true,
                                        Mensagem = string.Format(MensagemNegocio.DataComFormatoInvalido, inspecao.CodigoCipp, inspecao.CodigoOia)
                                    }
                                };
                            }

                        }
                        else
                        {
                            return new InspecaoCsvModel
                            {
                                Excecao = new ExcecaoCsv
                                {
                                    ExisteExcecao = true,
                                    Mensagem = string.Format(MensagemNegocio.DataNaoInformada, inspecao.CodigoCipp)
                                }
                            };
                        }

                        break;
                    }

                    return inspecao;
                }

                return new InspecaoCsvModel
                {
                    Excecao = new ExcecaoCsv
                    {
                        ExisteExcecao = true,
                        Mensagem = string.Format(MensagemNegocio.ArquivoCSVForaDeFormatacao, ftpInfo.Organismo.CodigoOIA)
                    }
                };

            }
            catch (ExcecaoCsv e)
            {
                throw new Exception($"Erro ao obter dados da Inspeção. Exceção {e.Mensagem}");
            }

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
                if (c == ',' || c == ';')
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

        private void DeletarDiretorioLocalInspecao(string diretorioLocal)
        {
            var di = new DirectoryInfo(diretorioLocal);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            DirectoryInfo parentDir = Directory.GetParent(diretorioLocal.EndsWith("\\") ? diretorioLocal : string.Concat(diretorioLocal, "\\"));

            foreach (DirectoryInfo dir in parentDir.Parent.GetDirectories())
            {
                dir.Delete(true);
            }

        }

        public bool ExisteArquivoCsv(string diretorio)
        {
            var files = Directory.GetFiles(diretorio, "*.csv");
            var fileNamePath = files[0];
            if (File.Exists(fileNamePath))
            {
                return true;
            }
            return false;

        }

    }
}