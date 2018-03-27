using System.Collections.Generic;
using System.IO;
using System.Linq;
using INMETRO.CIPP.DOMINIO.Mensagens;
using INMETRO.CIPP.SERVICOS.ModelService;

namespace INMETRO.CIPP.SERVICOS.ExcecaoServico
{
    public class DownloadExcecaoService
    {
        public static InspecoesGravadasModelServico ObterInspecaoValidaParaCodigoOiaInformado(IEnumerable<string> inspecoesValidas, string codigoOia)
        {
            if (inspecoesValidas.ToList().Count <= 0)
            {
                return new InspecoesGravadasModelServico
                {
                    InspecoesGravadas = new List<InspecaoModelServico>(),
                    Excecao = new ExcecaoService
                    {
                        ExisteExcecao = true,
                        Mensagem = string.Format(MensagemNegocio.NenhumInspecaoEncontradoParaCodigoOia, codigoOia)
                    },
                    DiretoriosValidos = new List<string>()
                };
            }
            return new InspecoesGravadasModelServico
            {
                InspecoesGravadas = new List<InspecaoModelServico>(),
                Excecao = new ExcecaoService
                {
                    ExisteExcecao = false,
                    Mensagem = string.Empty
                },
                DiretoriosValidos = new List<string>(inspecoesValidas)
            };
        }

        public static InspecoesGravadasModelServico ObterInspecaoValidaParaCippInformado(IEnumerable<string> inspecoesValidas, string codigoCipp, string codigoOia)
        {
            if (inspecoesValidas.ToList().Count <= 0)
            {
                return new InspecoesGravadasModelServico
                {
                    InspecoesGravadas = new List<InspecaoModelServico>(),
                    Excecao = new ExcecaoService
                    {
                        ExisteExcecao = true,
                        Mensagem = string.Format(MensagemNegocio.NenhumInspecaoEncontradoParaCodigoOia, codigoOia)
                    },
                    DiretoriosValidos = new List<string>()
                };
            }

            foreach (var item in inspecoesValidas)
            {
                var inspecaoValor = Path.GetFileNameWithoutExtension(item);
                if (inspecaoValor.Equals(codigoCipp))
                {
                    return new InspecoesGravadasModelServico
                    {
                        InspecoesGravadas = new List<InspecaoModelServico>(),
                        Excecao = new ExcecaoService
                        {
                            ExisteExcecao = false,
                            Mensagem = string.Empty
                        },
                        DiretoriosValidos = new List<string>(inspecoesValidas)
                    };
                }
            }

            return new InspecoesGravadasModelServico
            {
                InspecoesGravadas = new List<InspecaoModelServico>(),
                Excecao = new ExcecaoService
                {
                    ExisteExcecao = true,
                    Mensagem = string.Format(MensagemNegocio.NenhumInspecaoEncontradoParaCodigoCipp, codigoCipp)
                },
                DiretoriosValidos = new List<string>()
            };
        }
    }
}