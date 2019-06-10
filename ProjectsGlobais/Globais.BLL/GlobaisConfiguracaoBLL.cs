using System.Collections.Generic;
using System.Linq;
using Globais.BE;
using Globais.Dao;

namespace Globais.BLL
{
    public class GlobaisConfiguracaoBLL
    {
        public List<GlobaisConfiguracaoBE> Select()
        {
            return new GlobaisConfiguracaoDao().Select<GlobaisConfiguracaoBE>().ToList();
        }

        /// <summary>
        /// Carrega Tipo de Vencimento
        /// </summary>
        /// <param name="descricao"></param>
        /// <returns></returns>
        public List<GlobaisConfiguracaoBE> SelectTipoVencimento(string descricao = "")
        {
            return new GlobaisConfiguracaoDao().Select<GlobaisConfiguracaoBE>(new { Con_Tipo = "TVC", Conf_Descricao = descricao }).ToList();
        }

        /// <summary>
        /// Carrega Lista do IBGE
        /// </summary>
        /// <param name="descricao"></param>
        /// <returns></returns>
        public List<GlobaisConfiguracaoBE> SelectCodigoIBGE(string descricao = "")
        {
            return new GlobaisConfiguracaoDao().Select<GlobaisConfiguracaoBE>(new { Con_Tipo = "IBG", Conf_Descricao = descricao }).ToList();
        }

        /// <summary>
        /// Carrega CFO do Sistema
        /// </summary>
        /// <param name="descricao"></param>
        /// <returns></returns>
        public List<GlobaisConfiguracaoBE> SelectCFOP(string descricao = "")
        {
            return new GlobaisConfiguracaoDao().Select<GlobaisConfiguracaoBE>(new { Con_Tipo = "CFO", Conf_Descricao = descricao }).ToList();
        }

        /// <summary>
        /// Carrega Paginas do Sistema
        /// </summary>
        /// <param name="descricao"></param>
        /// <returns></returns>
        public List<GlobaisConfiguracaoBE> SelectPage(string descricao = "")
        {
            return new GlobaisConfiguracaoDao().Select<GlobaisConfiguracaoBE>(new { Con_Tipo = "PAG", Conf_Descricao = descricao }).ToList();
        }

        /// <summary>
        /// Carrega Tipo de Endereço
        /// </summary>
        /// <param name="descricao"></param>
        /// <returns></returns>
        public List<GlobaisConfiguracaoBE> SelectTipoEndereco(string descricao = "")
        {
            return new GlobaisConfiguracaoDao().Select<GlobaisConfiguracaoBE>(new { Con_Tipo = "TEN", Conf_Descricao = descricao }).ToList();
        }

        /// <summary>
        /// Carrega Instruções do Boleto
        /// </summary>
        /// <param name="descricao"></param>
        /// <returns></returns>
        public List<GlobaisConfiguracaoBE> SelectInstrucaoBoleto(string descricao = "")
        {
            return new GlobaisConfiguracaoDao().Select<GlobaisConfiguracaoBE>(new { Con_Tipo = "IBO", Conf_Descricao = descricao }).ToList();
        }

        public List<GlobaisConfiguracaoBE> Select(GlobaisConfiguracaoBE obj)
        {
            return new GlobaisConfiguracaoDao().Select<GlobaisConfiguracaoBE>(obj).ToList();
        }
        public GlobaisConfiguracaoBE SelectId(GlobaisConfiguracaoBE obj)
        {
            return new GlobaisConfiguracaoDao().SelectId<GlobaisConfiguracaoBE>(obj);
        }
        public GlobaisConfiguracaoBE Insert(GlobaisConfiguracaoBE obj)
        {
            switch (obj.Con_Tipo)
            {
                case "TPVenc":
                    obj.Con_Tipo = "TVC";
                    break;
                case "IBGE":
                    obj.Con_Tipo = "IBG";
                    break;
                case "CFOP":
                    obj.Con_Tipo = "CFO";
                    break;
                case "PAGE":
                    obj.Con_Tipo = "PAG";
                    break;
                case "TIPEND":
                    obj.Con_Tipo = "TEN";
                    break;
                case "INSTBOL":
                    obj.Con_Tipo = "IBO";
                    break;
            }
            obj.Con_id = new GlobaisConfiguracaoDao().Insert(obj);
            return obj;
        }
        public bool Update(GlobaisConfiguracaoBE obj)
        {
            switch (obj.Con_Tipo)
            {
                case "TPVenc":
                    obj.Con_Tipo = "TVC";
                    break;
                case "IBGE":
                    obj.Con_Tipo = "IBG";
                    break;
                case "CFOP":
                    obj.Con_Tipo = "CFO";
                    break;
                case "PAGE":
                    obj.Con_Tipo = "PAG";
                    break;
                case "TIPEND":
                    obj.Con_Tipo = "TEN";
                    break;
                case "INSTBOL":
                    obj.Con_Tipo = "IBO";
                    break;
            }
            return new GlobaisConfiguracaoDao().Update(obj).Value;
        }
        public bool Delete(GlobaisConfiguracaoBE obj)
        {
            return new GlobaisConfiguracaoDao().Delete(obj).Value;
        }
    }
}
