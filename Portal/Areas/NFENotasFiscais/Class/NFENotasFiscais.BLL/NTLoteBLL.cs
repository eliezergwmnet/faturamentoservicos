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
    public class NTLoteBLL
    {
        public List<NTLoteBE> Select()
        {
            return new NTLoteDao().Select<NTLoteBE>().ToList();
        }
        public List<NTLoteBE> Select(NTLoteBE obj)
        {
            return new NTLoteDao().Select<NTLoteBE>(obj).ToList();
        }
        public NTLoteBE SelectId(NTLoteBE obj)
        {
            return new NTLoteDao().SelectId<NTLoteBE>(obj);
        }
        public NTLoteBE Insert(NTLoteBE obj)
        {
            obj.lote_id = new NTLoteDao().Insert(obj);
            return obj;
        }
        public bool Update(NTLoteBE obj)
        {
            return new NTLoteDao().Update(obj).Value;
        }

        public bool UpdateStatus(NTLoteBE obj)
        {
            return new NTLoteDao().UpdateStatus(obj);
        }

        public bool UpdateProtocolo(NTLoteBE obj)
        {
            return new NTLoteDao().UpdateProtocolo(obj);
        }

        public bool UpdateXmlEnvio(NTLoteBE obj)
        {
            return new NTLoteDao().UpdateXmlEnvio(obj);
        }

        public bool UpdateXmlConsulta(NTLoteBE obj)
        {
            return new NTLoteDao().UpdateXmlConsulta(obj);
        }

        public bool Delete(NTLoteBE obj)
        {
            return new NTLoteDao().Delete(obj).Value;
        }
    }
}
