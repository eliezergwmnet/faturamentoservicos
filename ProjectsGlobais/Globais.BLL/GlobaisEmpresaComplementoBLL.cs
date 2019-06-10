/*
********************************************
********************************************
********************************************
*** DESENVOLVIDO POR LEANDRO MARTINS *******
*** PLATAFORMA DE CRIAÇÃO DE TELAS**********
********************************************
********************************************
********************************************
*/


using System.Collections.Generic;
using System.Linq;
using Globais.BE;
using Globais.Dao;

namespace GlobaisEmpresaDadosComplem.BLL
{
    public class GlobaisEmpresaComplementoBLL
    {
        public List<GlobaisEmpresaTaxasNotasBE> Select()
        {
            return new GlobaisEmpresaDadosComplemDao().Select<GlobaisEmpresaTaxasNotasBE>().ToList();
        }
        public List<GlobaisEmpresaTaxasNotasBE> Select(GlobaisEmpresaTaxasNotasBE obj)
        {
            return new GlobaisEmpresaDadosComplemDao().Select<GlobaisEmpresaTaxasNotasBE>(obj).ToList();
        }
        public GlobaisEmpresaTaxasNotasBE SelectId(GlobaisEmpresaTaxasNotasBE obj)
        {
            return new GlobaisEmpresaDadosComplemDao().SelectId<GlobaisEmpresaTaxasNotasBE>(obj);
        }
        public GlobaisEmpresaTaxasNotasBE Insert(GlobaisEmpresaTaxasNotasBE obj)
        {
            obj.confCom_id = new GlobaisEmpresaDadosComplemDao().Insert(obj);
            return obj;
        }
        public bool Update(GlobaisEmpresaTaxasNotasBE obj)
        {
            return new GlobaisEmpresaDadosComplemDao().Update(obj).Value;
        }
        public bool Delete(GlobaisEmpresaTaxasNotasBE obj)
        {
            return new GlobaisEmpresaDadosComplemDao().Delete(obj).Value;
        }
    }
}
