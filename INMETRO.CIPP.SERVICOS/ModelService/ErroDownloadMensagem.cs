namespace INMETRO.CIPP.SERVICOS.ModelService
{
    public class ErroDownloadMensagem
    {
        public string CodigoOIA { get; set; }
        public string CodigoCipp { get; set; }
        public string Mensagem { get; set; }

        public ErroDownloadMensagem(string codigo, string cipp, string mensagem)
        {
            CodigoOIA = codigo;
            CodigoCipp = cipp;
            Mensagem = mensagem;
        }
    }
}