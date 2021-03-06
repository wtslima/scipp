﻿namespace INMETRO.CIPP.DOMINIO.Mensagens
{
    public class MensagemNegocio
    {
        public const string SucessoDownloadCodigoOiaeCipp = "O download para CÓDIGO-OIA-PP {0} e o CIPP {1} foi feito com sucesso!";
        public const string SucessoDownloadCodigoOia = "O download para CÓDIGO-OIA-PP {0} foi feito com sucesso!";
        public const string CampoObrigatorio = "Campo obrigatório.";
        public const string FtpInvalido = "Não existe um site FTP válido para o Código OIA-PP {0}.";
        public const string SemNovosDiretorios = "Não há nenhuma inspeção disponível para o Código OIA-PP {0}";
        public const string FalhaDownloadFtp = "Houve uma falha ao fazer download do ftp para servidor Inmetro para CÓDIGO-OIA-PP {0} e o CIPP {1}.";
        public const string NaoExisteCodigoOia = "Não existe CÓDIGO-OIA-PP {0} informado.";
        public const string NenhumInspecaoEncontradoParaCodigoOia = "O organismo de Código OIA-PP {0} não possui novos arquivo(s) de inspeção para download.";
        public const string NenhumInspecaoEncontradoParaCodigoCipp = "Nenhum resultado encontrado para o Nº CIPP {0} informado";
        public const string NenhumInspecaoEncontradoParaCodigoOiAeCipp = "Nenhum resultado encontrado para Código OIA-PP {0} e Nº CIPP {1}";
        public const string NenhumaInspecaoExcluidaEncontrada = "Não foi encontrada registro de inspeção excluída";
        public const string NenhumaInspecaoEncontrada = "Nenhum resultado encontrado";
        public const string InspecaoJaGravadaParaCippEOia = "A inspeção já está gravada na base de dados do Inmetro.";
        public const string FalhaAoInserirInspecao = "Falha ao gravar Inspeção {0}. Código-OIA-PP {1}.";
       
        #region Mensagens Data
        public const string NenhumaInspecaoParaPeriodoInformado = "Nenhuma inspeção foi encontrada para o período informado.";
        #endregion

        #region Mensagens Placa
        public const string NenhumaInspecaoPlcaInformada = "Nenhuma inspeção foi encontrada para placa {0} informado.";
        #endregion
        #region Mensagens ArquivoCSV
        public const string CodigoOiaNaoInformado = "Campo Codigo OIA-PP não preenchido no arquivo CSV.";
        public const string CodidoCippNaoInformado = "Campo CIPP não preenchido no arquivo CSV.";
        public const string PlacaNaoInformada = "Campo PLACA não foi preenchido no arquivo CSV da Inspecao {0} e Código-OIA-PP {1}.";
        public const string PlacaNaoExiste = "O valor {0} para campo PLACA na INSPEÇÃO {1} não é válido. Código-OIA-PP{2}.";
        public const string NumeroDoEquipamentoNaoInformado = "Número do equipamento da inspeção nº{0} não preenchido no arquivo CSV. Codigo-OIA-PP{1}.";
        public const string DataNaoInformada = "Data da inspecao nº{0} não preenchida no arquivo CSV.";
        public const string DataComFormatoInvalido = "Data da inspecao nº{0} não está no formato correto. Código-OIA-PP{1}";
        public const string ArquivoCSVNaoEncontrado = "Não há arquivo CSV para Inspeção {0} e CÓDIGO-OIA-PP{1}.";
        public const string ArquivoCSVForaDeFormatacao = "O arquivo CSV de CODIGO-OIA-PP {0} está com a formatação errada.";
        #endregion

    }
}