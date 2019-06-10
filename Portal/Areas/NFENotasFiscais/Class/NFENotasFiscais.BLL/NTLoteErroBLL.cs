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
using NFENotasFiscais.BE;
using NFENotasFiscais.Dao;

namespace NFENotasFiscais.BLL
{
    public class NTLoteErroBLL
    {
        public List<NTLoteErroBE> Select()
        {
            return new NTLoteErroDao().Select<NTLoteErroBE>().ToList();
        }
        public List<NTLoteErroBE> Select(NTLoteErroBE obj)
        {
            return new NTLoteErroDao().Select<NTLoteErroBE>(obj).ToList();
        }
        public NTLoteErroBE SelectId(NTLoteErroBE obj)
        {
            return new NTLoteErroDao().SelectId<NTLoteErroBE>(obj);
        }
        public NTLoteErroBE Insert(NTLoteErroBE obj)
        {
            obj.LoteErr_id = new NTLoteErroDao().Insert(obj);
            return obj;
        }
        public bool Update(NTLoteErroBE obj)
        {
            return new NTLoteErroDao().Update(obj).Value;
        }
        public bool Delete(NTLoteErroBE obj)
        {
            return new NTLoteErroDao().Delete(obj).Value;
        }
    }
}
