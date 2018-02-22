using System;
using System.Collections.Generic;
using INMETRO.CIPP.DOMINIO.Interfaces;
using INMETRO.CIPP.DOMINIO.Servicos;
using INMETRO.CIPP.SERVICOS.Interfaces;
using INMETRO.CIPP.SERVICOS.ModelService;

namespace INMETRO.CIPP.SERVICOS.Servicos
{
    public class InspecaoServico : IInspecaoServico
    {
        private readonly IInspecaoDominioService _inspecaoDominio;
        public InspecaoServico(IInspecaoDominioService inspecaoDominio)
        {
            _inspecaoDominio = inspecaoDominio;
        }

        public IEnumerable<InspecaoModelServico> ObterInspecoes(string codigoOia, string cipp)
        {
           if(string.IsNullOrEmpty(codigoOia)) return new List<InspecaoModelServico>();
           var listaInspecao =  new List<InspecaoModelServico>();

            if (string.IsNullOrEmpty(cipp))
            {
                
                var lista = _inspecaoDominio.ObterInspecaoPorCodigoOia(codigoOia);
                

                foreach (var item in lista)
                {
                    var inspecaoModelServico = new InspecaoModelServico
                    {
                        CodigoCipp = item.CodigoCIPP,
                        CodigoOia = item.CodigoOIA,
                        Equipamento = item.NumeroEquipamento.ToString(),
                        Placa = item.PlacaLicenca,
                        DataInspecao = item.DataInspecao,
                        Responsavel = item.ResponsavelTecnico
                    };


                    listaInspecao.Add(inspecaoModelServico);


                }
            }
            else
            {
                var inspecao = _inspecaoDominio.ObterDadosInspecao(cipp);
                var inspecaoServico = new InspecaoModelServico
                {
                    CodigoCipp = inspecao.CodigoCIPP,
                    CodigoOia = inspecao.CodigoOIA,
                    Equipamento = inspecao.NumeroEquipamento.ToString(),
                    Placa = inspecao.PlacaLicenca,
                    DataInspecao = inspecao.DataInspecao,
                    Responsavel = inspecao.ResponsavelTecnico
                };
                 listaInspecao.Add(inspecaoServico);

                return listaInspecao;
            }
            return listaInspecao;
        }

        public IEnumerable<InspecaoModelServico> ObterTodasInspecoes()
        {
            var listaInspecao = new List<InspecaoModelServico>();
            var lista = _inspecaoDominio.ObterTodasInspecoes();

            if (lista == null) return listaInspecao;

            foreach (var item in lista)
            {
                var inspecaoModelServico = new InspecaoModelServico
                {
                    CodigoCipp = item.CodigoCIPP,
                    CodigoOia = item.CodigoOIA,
                    Equipamento = item.NumeroEquipamento.ToString(),
                    Placa = item.PlacaLicenca,
                    DataInspecao = item.DataInspecao,
                    Responsavel = item.ResponsavelTecnico
                };


                listaInspecao.Add(inspecaoModelServico);


            }

            return listaInspecao;

        }


        //private string  VerificarDiretorios(string[] diretorios, string codigo)
        //{
        //    var retornoDownload = new RetornoDownloadModel();

        //    if (diretorios.Length != 0 && !ExisteInspecoesGravadas(diretorios)) return new RetornoDownloadModel();
        //    retornoDownload.ExisteExcecao = false;
        //    retornoDownload.Mensagem = string.Format(MensagemSistema.SemNovosDiretorios, codigo);
        //    return retornoDownload;
        //}


    }
}