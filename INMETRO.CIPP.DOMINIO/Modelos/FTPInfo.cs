using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INMETRO.CIPP.DOMINIO.Modelos
{
    [Table("TB_FTP_INFO")]
    public class FTPInfo
    {
       
        [Required]
        [Column("DES_DIRETORIO_CIPP")]  //Inspecao
        public string DiretorioInspecao { get; set; }

        [Required]
        [Column("DES_DIRETORIO_REMOTO_CIPP")] //123456.zip
        public string DiretorioInspecaoRemoto  { get; set; }

        [Required]
        [Column("DES_DIRETORIO_LOCAL_CIPP")]  //c:\Inspecao\NomeOraganismo
        public string DiretorioInspecaoLocal { get; set; }

        [Required]
        [Column("DES_HOST_URI")]  //ftp:///siteftp
        public string HostURI { get; set; }

        [Required]
        [Column("CDA_LOGIN_FTP")]
        public string Usuario { get; set; }

        [Required]
        [Column("DES_SENHA_FTP")]
        public string Senha { get; set; }

        [Required]
        [Column("IDT_ORGANISMO")]
        public int OrganismoId { get; set; }

        public virtual Organismo Organismo { get; set; }

        public FTPInfo(int organismoId, string diretorioInspecao, string diretorioInspecaoRemoto, string diretorioInspecaoLocal, string host, string usuario, string senha)
        {
            DiretorioInspecao = diretorioInspecao;
            DiretorioInspecaoRemoto = diretorioInspecaoRemoto;
            DiretorioInspecaoLocal = diretorioInspecaoLocal;
            HostURI = host;
            Usuario = usuario;
            Senha = senha;
            OrganismoId = organismoId;
        }

        public FTPInfo()
        {
          
        }


        public bool ExisteArquivoCsv(string extensao)
        {
            if (extensao.ToUpper().Equals("CSV"))
            {
                return true;
            }
            return false;
        }
    }
}