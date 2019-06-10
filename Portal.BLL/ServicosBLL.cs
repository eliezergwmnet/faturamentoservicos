using System.Collections.Generic;
using System.Linq;
using Portal.BE;
using Portal.Dao;

namespace Portal.BLL
{
    public class ServicosBLL
    {
        public List<ServicosBE> Select(int conf_id)
        {
            return new ServicosDao().Select<ServicosBE>(new { conf_id = conf_id }).ToList();
        }
        public List<ServicosBE> Select(ServicosBE obj, bool subitem = false)
        {
            if(!subitem)
                return new ServicosDao().Select<ServicosBE>(obj).ToList();
            else
            {
                var servico = CarregaSubItem(new ServicosDao().Select<ServicosBE>(obj).ToList());
                return servico;
            }
        }
        public ServicosBE SelectId(ServicosBE obj)
        {
            return new ServicosDao().SelectId<ServicosBE>(obj);
        }
        public ServicosBE Insert(ServicosBE obj)
        {
            obj.serv_id = new ServicosDao().Insert(obj);
            return obj;
        }
        public bool Update(ServicosBE obj)
        {
            return new ServicosDao().Update(obj).Value;
        }
        public bool Delete(ServicosBE obj)
        {
            return new ServicosDao().Delete(new { serv_id = obj.serv_id }).Value;
        }

        List<ServicosBE> CarregaSubItem(List<ServicosBE> obj)
        {
            if (obj.Count > 0)
            {
                ServicosBE sub = new ServicosBE { conf_id = obj[0].conf_id };
                foreach (var item in obj)
                {
                    sub.serv_idsub = item.serv_id;
                    item.SubMenu = new ServicosBLL().Select(sub);
                }
            }

            return obj;
        }

    }
}
