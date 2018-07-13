namespace INMETRO.CIPP.SHARED
{
    public class MensagemSistema
    {
        public const string ErroObterDiretorios = "Erro ao obter novas inspeções. Mensagem: {0}";
        public const string SucessoDownloadCodigoOiaeCipp = "O download para CÓDIGO-OIA-PP {0} e o CIPP {1} foi feito com sucesso!";
        public const string SucessoDownloadCodigoOia = "O download para CÓDIGO-OIA-PP {0} foi feito com sucesso!";
        public const string CampoObrigatorio = "Campo obrigatório.";
        public const string FtpInvalido = "Não existe um site FTP válido para o Código OIA-PP {0}.";
        public const string SemNovosDiretorios = "Não há nenhuma inspeção disponível para o Código OIA-PP {0}";
        public const string FalhaDownloadFtp = "Houve uma falha ao fazer download do ftp para servidor Inmetro para CÓDIGO-OIA-PP {0} e o CIPP {1}.";
        public const string NaoExisteCodigoOia = "Não existe CÓDIGO-OIA-PP {0} informado.";
        public const string NenhumInspecaoEncontradoParaCodigoOia = "Nenhum resultado encontrado para Código OIA-PP {0}";
        public const string NenhumInspecaoEncontradoParaCodigoCipp = "Nenhum resultado encontrado para Nº Cipp {0}";
        public const string NenhumInspecaoEncontradoParaCodigoOiAeCipp = "Nenhum resultado encontrado para Código OIA-PP {0} e Nº Cipp {1}";

        public const string ErroDownloadRepositorioRemoto = "Não foi possível fazer download do Repositorio Remoto do Codigo-Oia {0}";

        public const string NenhumArquivoEncontrado = "Nenhum Inspeção foi encontrado ao realizar a Rotina Automática";

        public const string ErroIntegracaoObterArquivoc = "Nenhum Inspeção foi encontrado ao realizar a Rotina Automática";

        public const string DiretoriosInvalidos = "Não existem diretórios válidos disponíveis para o CÓDIGO-OIA-PP{0}.";
        public const string ErroDescompactarTipoRar = "Erro ao descompactar arquivo INSPEÇÃO {0} para CÓDIGO-OIA-PP{1}. Falha {2}.";
    }
}