﻿using System.ComponentModel.DataAnnotations;
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
        [Column("CDN_TIPO_INTEGRACAO")] //1 ou 2.zip
        public int TipoIntegracao  { get; set; }

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

        [Column("DES_CHAVE_PRIVADA")]
        public string PrivateKey { get; set; }



        public virtual Organismo Organismo { get; set; }

        public FTPInfo(int organismoId, string diretorioInspecao, int tipoIntegracao, string diretorioInspecaoLocal, string host, string usuario, string senha, string chave)
        {
            DiretorioInspecao = diretorioInspecao;
            TipoIntegracao = tipoIntegracao;
            DiretorioInspecaoLocal = diretorioInspecaoLocal;
            HostURI = host;
            Usuario = usuario;
            Senha = senha;
            OrganismoId = organismoId;
            PrivateKey = chave;
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