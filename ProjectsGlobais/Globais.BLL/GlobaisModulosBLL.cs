using System.Collections.Generic;
using System.Linq;
using Globais.BE;
using Globais.Dao;

namespace Globais.BLL
{

    #region Modulos
    public class GlobaisModulosPagesBLL
    {
        public List<GlobaisModulosPagesBE> Select()
        {
            return new GlobaisModulosPagesDao().Select<GlobaisModulosPagesBE>().ToList();
        }
        public List<GlobaisModulosPagesBE> Select(GlobaisModulosPagesBE obj)
        {
            return new GlobaisModulosPagesDao().Select<GlobaisModulosPagesBE>(obj).ToList();
        }
        public GlobaisModulosPagesBE SelectId(GlobaisModulosPagesBE obj)
        {
            return new GlobaisModulosPagesDao().SelectId<GlobaisModulosPagesBE>(obj);
        }
        public GlobaisModulosPagesBE Insert(GlobaisModulosPagesBE obj)
        {
            obj.mod_id = new GlobaisModulosPagesDao().Insert(obj);
            return obj;
        }
        public bool Update(GlobaisModulosPagesBE obj)
        {
            return new GlobaisModulosPagesDao().Update(obj).Value;
        }
        public bool Delete(GlobaisModulosPagesBE obj)
        {
            return new GlobaisModulosPagesDao().Delete(obj).Value;
        }

    }

    #endregion

    #region Paginas do Modulo

    public class GlobaisModulosBLL
    {
        public List<GlobaisModulosBE> Select()
        {
            return new GlobaisModulosDao().CarregaLista(new GlobaisModulosBE()).ToList();
        }

        public List<GlobaisModulosBE> SelectModEmpresa(int conf_id)
        {
            return new GlobaisModulosDao().CarregaLista(new { conf_id = conf_id }).ToList();
        }

        public List<GlobaisModulosBE> Select(GlobaisModulosBE obj)
        {
            return new GlobaisModulosDao().CarregaLista(obj).ToList();
        }
        public GlobaisModulosBE SelectId(GlobaisModulosBE obj)
        {
            return new GlobaisModulosDao().SelectId<GlobaisModulosBE>(obj);
        }
        public GlobaisModulosBE Insert(GlobaisModulosBE obj)
        {
            obj.mod_id = new GlobaisModulosDao().Insert(obj);
            return obj;
        }
        public bool Update(GlobaisModulosBE obj)
        {
            return new GlobaisModulosDao().Update(obj).Value;
        }
        public bool Delete(GlobaisModulosBE obj)
        {
            return new GlobaisModulosDao().Delete(obj).Value;
        }

    }

    #endregion

    #region Modulos por Empresa

    /// <summary>
    /// Cadastro de modulos para empresa, classe simples com os dois unicos metodos
    /// Demais funções do modulo estão adicionado na class Modulo
    /// </summary>
    public class GlobaisModulosEmpresaBLL
    {
        public bool Insert(int mod_id, int conf_id)
        {
            new GlobaisModulosEmpresaDao().Insert(new { mod_id = mod_id, conf_id = conf_id });
            return true;
        }

        public bool Delete(int modConf_id)
        {
            return new GlobaisModulosEmpresaDao().Delete(new { modConf_id = modConf_id }).Value;
        }
    }

    #endregion
}
