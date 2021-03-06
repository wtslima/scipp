﻿using System;
using System.Collections.Generic;
using INMETRO.CIPP.DOMINIO.Modelos;

namespace INMETRO.CIPP.DOMINIO.Interfaces.Repositorios
{
    public interface IInspecaoRepositorio
    {
        bool AdicionarDadosInspecao(Inspecao inspecao);
        Inspecao ObterDadosInspecao(string cipp);
        bool BuscarInspecaoPorCodigoCipp(string cipp);
        IList<Inspecao> ObterInspecaosPorCodigoOia(string codigoOia);
        IEnumerable<Inspecao> ObterInspecaosPorPlacaLicenca(string placaLicenca);

        IEnumerable<Inspecao> ObterInspecoesPorDataInspecao(DateTime dataInspecao);

        IEnumerable<Inspecao> ObterTodasInspecoes();
    }
}