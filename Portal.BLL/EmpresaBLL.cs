using System.Collections.Generic;
using System.Linq;
using Globais.BE;
using Portal.BE;
using Portal.Dao;

namespace Portal.BLL
{
    public class EmpresaBLL
    {
        public List<EmpresaBE> Select()
        {
            return new EmpresaDao().Select<EmpresaBE>().ToList();
        }
        public List<EmpresaBE> Select(EmpresaBE obj)
        {
            return new EmpresaDao().Select<EmpresaBE>(obj).ToList();
        }
        public EmpresaBE SelectId()
        {
            return new EmpresaDao().SelectId<EmpresaBE>(new { });
        }
        public EmpresaBE SelectId(EmpresaBE obj)
        {
            return new EmpresaDao().SelectId<EmpresaBE>(obj);
        }
        public EmpresaBE Insert(EmpresaBE obj, GlobaisEnderecoBE _endereco)
        {
            _endereco = new EnderecoBLL().Insert(_endereco);
            obj.end_id = _endereco.end_id;
            obj.conf_id = new EmpresaDao().Insert(obj);
            
            return obj;
        }
        public bool Update(EmpresaBE obj)
        {
            new EmpresaDao().Update(obj);
            var endereco = new GlobaisEnderecoBE
            {
                end_id = obj.end_id,
                end_bairro = obj.end_bairro,
                end_cep = obj.end_cep,
                end_cidade = obj.end_cidade,
                end_complemento = obj.end_complemento,
                end_estado = obj.end_estado,
                end_latitude = obj.end_latitude,
                end_logradouro = obj.end_logradouro,
                end_longetitude = obj.end_longetitude,
                end_numero = obj.end_numero
            };
            new EnderecoDao().Update(endereco);

            return true;
        }
        public bool Delete(EmpresaBE obj)
        {
            return new EmpresaDao().Delete(obj).Value;
        }
    }
}
