
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
        public List<NTNotasEmitidasBE> Select()
        {
            return new NTNotasDao().Select<NTNotasEmitidasBE>().ToList();
        }
        public List<NTNotasBE> Select(NTNotasBE obj)
        {
            return new NTNotasDao().Select<NTNotasBE>(obj).ToList();
        }
        public NTNotasEmitidasBE SelectId(NTNotasBE obj)
        {
            return new NTNotasDao().Select<NTNotasEmitidasBE>(obj).FirstOrDefault<NTNotasEmitidasBE>();
        }

        /// <summary>
        /// Carrega a lista de nota que anida não foram geradas o boleto
        /// </summary>
        /// <returns></returns>
        public List<NTNotasEmitidasBE> SelectPendentesBoleto()
        {
            return new NTNotasDao().SelectPendentesBoleto();
        }

        public bool AtualizaBoletoNota(int not_id, string urlBoleto)
        {
            var res = new NTNotasDao().UpdateBoleto(new { not_id = not_id, not_chave = urlBoleto });
            return res.Value;
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
