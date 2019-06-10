using System.Collections.Generic;
using System.Linq;
using Portal.BE;
using Portal.Dao;

namespace Portal.BLL
{
    public class ConfiguracaoBLL
    {
        public List<ConfiguracaoBE> Select()
        {
            return new ConfiguracaoDao().Select<ConfiguracaoBE>().ToList();
        }

        /// <summary>
        /// Carrega Tipo de Vencimento
        /// </summary>
        /// <param name="descricao"></param>
        /// <returns></returns>
        public List<ConfiguracaoBE> SelectTipoVencimento(string descricao = "")
        {
            return new ConfiguracaoDao().Select<ConfiguracaoBE>(new {Conf_Tipo = "TVC", Conf_Descricao= descricao }).ToList();
        }

        /// <summary>
        /// Carrega Lista do IBGE
        /// </summary>
        /// <param name="descricao"></param>
        /// <returns></returns>
        public List<ConfiguracaoBE> SelectCodigoIBGE(string descricao = "")
        {
            return new ConfiguracaoDao().Select<ConfiguracaoBE>(new { Conf_Tipo = "IBG", Conf_Descricao = descricao }).ToList();
        }

        /// <summary>
        /// Carrega CFO do Sistema
        /// </summary>
        /// <param name="descricao"></param>
        /// <returns></returns>
        public List<ConfiguracaoBE> SelectCFOP(string descricao = "")
        {
            return new ConfiguracaoDao().Select<ConfiguracaoBE>(new { Conf_Tipo = "CFO", Conf_Descricao = descricao }).ToList();
        }

        /// <summary>
        /// Carrega Paginas do Sistema
        /// </summary>
        /// <param name="descricao"></param>
        /// <returns></returns>
        public List<ConfiguracaoBE> SelectPage(string descricao = "")
        {
            return new ConfiguracaoDao().Select<ConfiguracaoBE>(new { Conf_Tipo = "PAG", Conf_Descricao = descricao }).ToList();
        }

        /// <summary>
        /// Carrega Tipo de Endereço
        /// </summary>
        /// <param name="descricao"></param>
        /// <returns></returns>
        public List<ConfiguracaoBE> SelectTipoEndereco(string descricao = "")
        {
            return new ConfiguracaoDao().Select<ConfiguracaoBE>(new { Conf_Tipo = "TEN", Conf_Descricao = descricao }).ToList();
        }

        /// <summary>
        /// Carrega Instruções do Boleto
        /// </summary>
        /// <param name="descricao"></param>
        /// <returns></returns>
        public List<ConfiguracaoBE> SelectInstrucaoBoleto(string descricao = "")
        {
            return new ConfiguracaoDao().Select<ConfiguracaoBE>(new { Conf_Tipo = "IBO", Conf_Descricao = descricao }).ToList();
        }

        public List<ConfiguracaoBE> Select(ConfiguracaoBE obj)
        {
            return new ConfiguracaoDao().Select<ConfiguracaoBE>(obj).ToList();
        }
        public ConfiguracaoBE SelectId(ConfiguracaoBE obj)
        {
            return new ConfiguracaoDao().SelectId<ConfiguracaoBE>(obj);
        }
        public ConfiguracaoBE Insert(ConfiguracaoBE obj)
        {
            switch (obj.Conf_Tipo)
            {
                case "TPVenc":
                    obj.Conf_Tipo = "TVC";
                    break;
                case "IBGE":
                    obj.Conf_Tipo = "IBG";
                    break;
                case "CFOP":
                    obj.Conf_Tipo = "CFO";
                    break;
                case "PAGE":
                    obj.Conf_Tipo = "PAG";
                    break;
                case "TIPEND":
                    obj.Conf_Tipo = "TEN";
                    break;
                case "INSTBOL":
                    obj.Conf_Tipo = "IBO";
                    break;
            }
            obj.Conf_id = new ConfiguracaoDao().Insert(obj);
            return obj;
        }
        public bool Update(ConfiguracaoBE obj)
        {
            switch (obj.Conf_Tipo)
            {
                case "TPVenc":
                    obj.Conf_Tipo = "TVC";
                    break;
                case "IBGE":
                    obj.Conf_Tipo = "IBG";
                    break;
                case "CFOP":
                    obj.Conf_Tipo = "CFO";
                    break;
                case "PAGE":
                    obj.Conf_Tipo = "PAG";
                    break;
                case "TIPEND":
                    obj.Conf_Tipo = "TEN";
                    break;
                case "INSTBOL":
                    obj.Conf_Tipo = "IBO";
                    break;
            }
            return new ConfiguracaoDao().Update(obj).Value;
        }
        public bool Delete(ConfiguracaoBE obj)
        {
            return new ConfiguracaoDao().Delete(obj).Value;
        }
    }
}
