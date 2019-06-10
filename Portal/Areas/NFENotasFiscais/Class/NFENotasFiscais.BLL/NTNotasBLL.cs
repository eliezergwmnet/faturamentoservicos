
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
    public class NTNotasBLL
    {
        public List<NTNotasBE> Select()
        {
            return new NTNotasDao().Select<NTNotasBE>().ToList();
        }
        public List<NTNotasBE> Select(NTNotasBE obj)
        {
            return new NTNotasDao().Select<NTNotasBE>(obj).ToList();
        }
        public NTNotasBE SelectId(NTNotasBE obj)
        {
            return new NTNotasDao().SelectId<NTNotasBE>(obj);
        }

        public int NumeroNota(int conf_id)
        {
            return new NTNotasDao().NumeroNota(conf_id);
        }

        public NTNotasBE Insert(NTNotasBE obj)
        {
            obj.not_id = new NTNotasDao().Insert(obj);
            return obj;
        }
        public bool Update(NTNotasBE obj)
        {
            return new NTNotasDao().Update(obj).Value;
        }
        public bool Delete(NTNotasBE obj)
        {
            return new NTNotasDao().Delete(obj).Value;
        }
    }
}
