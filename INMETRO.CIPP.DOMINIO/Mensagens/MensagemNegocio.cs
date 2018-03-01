namespace INMETRO.CIPP.DOMINIO.Mensagens
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
        public const string NenhumInspecaoEncontradoParaCodigoOia = "Nenhum resultado encontrado para Código OIA-PP {0}";
        public const string NenhumInspecaoEncontradoParaCodigoCipp = "Nenhum resultado encontrado para Nº Cipp {0} informado";
        public const string NenhumInspecaoEncontradoParaCodigoOiAeCipp = "Nenhum resultado encontrado para Código OIA-PP {0} e Nº Cipp {1}";
        public const string NenhumaInspecaoExcluidaEncontrada = "Nenhuma Inspeção Excluída foi encontrada";
        public const string NenhumaInspecaoEncontrada = "Nenhum resultado encontrado";
        public const string InspecaoJaGravadaParaCippEOia = "Já há gravados CÓDIGO-OIA-PP {0} e o CIPP {1}";

        #region Mensagens Data
        public const string NenhumaInspecaoParaPeriodoInformado = "Nenhuma inspeção foi encontrada para o período informado.";
        #endregion

        #region Mensagens Placa
        public const string NenhumaInspecaoPlcaInformada = "Nenhuma inspeção foi encontrada para placa de licença {0} informado.";
        #endregion

    }
}