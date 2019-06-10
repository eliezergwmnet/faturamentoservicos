using System.Collections.Generic;
using System.Linq;
using Portal.BE;
using Portal.Dao;

namespace Portal.BLL
{
    public class ClienteNotaBLL
    {
        public List<ClienteNotaBE> Select()
        {
            return new ClienteNotaDao().Select<ClienteNotaBE>().ToList();
        }
        /// <summary>
        /// Retorna a lista de Notas para serem gerados do cliente
        /// Caso o cliente tenha Serviços e algum produto parcela, o retorno será somente dos Serviços e a Proxima parcela pendente
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<ClienteNotaBE> SelectNota(int id)
        {
            return new ClienteNotaDao().SelectNota<ClienteNotaBE>(new { cli_id = id}).ToList();
        }
        public List<ClienteNotaBE> SelectNota(int id, int cliNot_contrato)
        {
            return new ClienteNotaDao().SelectNota<ClienteNotaBE>(new { cli_id = id, cliNot_contrato = cliNot_contrato }).ToList();
        }
        public List<ClienteNotaBE> Select(ClienteNotaBE obj)
        {
            return new ClienteNotaDao().Select<ClienteNotaBE>(obj).ToList();
        }
        public ClienteNotaBE SelectId(ClienteNotaBE obj)
        {
            return new ClienteNotaDao().SelectId<ClienteNotaBE>(obj);
        }
        public ClienteNotaBE Insert(ClienteNotaBE obj)
        {
            obj.cliNot_id = new ClienteNotaDao().Insert(obj);
            return obj;
        }
        public bool BaixaParcela(ClienteNotaBE obj)
        {
            return new ClienteNotaDao().BaixaParcela( new { cli_id = obj.cli_id, servCli_id=obj.servCli_id}).Value;
        }
        public bool Update(ClienteNotaBE obj)
        {
            return new ClienteNotaDao().Update(obj).Value;
        }
        public bool Delete(ClienteNotaBE obj)
        {
            return new ClienteNotaDao().Delete(obj).Value;
        }
    }
}
