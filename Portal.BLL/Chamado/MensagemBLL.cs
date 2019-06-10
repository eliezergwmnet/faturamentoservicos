using System;
using System.Collections.Generic;
using System.Linq;
using Portal.BE.Chamado;
using Portal.Dao.Chamado;

namespace Portal.BLL.Chamado
{
    public class MensagemBLL
    {
        public List<MensagemBE> Select()
        {
            return new MensagemDao().Select<MensagemBE>().ToList();
        }
        public List<MensagemBE> Select(MensagemBE obj)
        {
            return new MensagemDao().Select<MensagemBE>(obj).ToList();
        }
        public MensagemBE SelectId()
        {
            return new MensagemDao().SelectId<MensagemBE>(new { });
        }
        public MensagemBE SelectId(MensagemBE obj)
        {
            return new MensagemDao().SelectId<MensagemBE>(obj);
        }
        public int Insert(MensagemBE obj)
        {
            var result = new MensagemDao().Insert(obj);
            return result;
        }
        public bool Update(MensagemBE obj)
        {
            var result = new MensagemDao().Update(obj);
            return result.Value;
        }
        public bool Delete(MensagemBE obj)
        {
            return new MensagemDao().Delete(obj).Value;
        }
    }
}
