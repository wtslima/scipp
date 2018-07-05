namespace INMETRO.CIPP.SHARED.Interfaces
{
    public interface IGerenciarArquivoCompactado
    {
        bool DescompactarArquivo(string diretorio, string file);

        bool ExcluirArquivoLocal(string diretorio, string file);

        void ExcluirArquivoCasoExista(string diretorio, string file);
    }
}