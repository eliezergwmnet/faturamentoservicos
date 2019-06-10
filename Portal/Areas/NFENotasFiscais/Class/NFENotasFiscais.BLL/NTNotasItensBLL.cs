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
    public class NTNotasItensBLL
    {
        public List<NTNotasItensBE> Select()
        {
            return new NTNotasItensDao().Select<NTNotasItensBE>().ToList();
        }
        public List<NTNotasItensBE> Select(NTNotasItensBE obj)
        {
            return new NTNotasItensDao().Select<NTNotasItensBE>(obj).ToList();
        }
        public NTNotasItensBE SelectId(NTNotasItensBE obj)
        {
            return new NTNotasItensDao().SelectId<NTNotasItensBE>(obj);
        }
        public NTNotasItensBE Insert(NTNotasItensBE obj)
        {
            obj.notI_ind = new NTNotasItensDao().Insert(obj);
            return obj;
        }
        public bool Update(NTNotasItensBE obj)
        {
            return new NTNotasItensDao().Update(obj).Value;
        }
        public bool Delete(NTNotasItensBE obj)
        {
            return new NTNotasItensDao().Delete(obj).Value;
        }
    }
}
