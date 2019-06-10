using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portal.BE;
using Portal.Dao;

namespace Portal.BLL
{
    public class EmailBLL
    {
        public List<EmailBE> Select()
        {
            return new EmailDao().Select<EmailBE>().ToList();
        }
        public List<EmailBE> Select(EmailBE obj)
        {
            return new EmailDao().Select<EmailBE>(obj).ToList();
        }
        public EmailBE SelectReferencia(string referencia)
        {
            return new EmailDao().SelectId<EmailBE>(new EmailBE { ema_referencia = referencia});
        }
        public EmailBE SelectId()
        {
            return new EmailDao().SelectId<EmailBE>(new { });
        }
        public EmailBE SelectId(EmailBE obj)
        {
            return new EmailDao().SelectId<EmailBE>(obj);
        }
        public bool Insert(EmailBE obj)
        {
            var result = new EmailDao().Insert(obj);
            return Convert.ToBoolean(result);
        }
        public bool Update(EmailBE obj)
        {
            var result = new EmailDao().Update(obj);
            return result.Value;
        }
        public bool Delete(EmailBE obj)
        {
            return new EmailDao().Delete(obj).Value;
        }
    }
}
