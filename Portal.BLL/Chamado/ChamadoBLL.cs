using System;
using System.Collections.Generic;
using System.Linq;
using Portal.BE.Chamado;
using Portal.Dao.Chamado;

namespace Portal.BLL.Chamado
{
    public class ChamadoBLL
    {
        public List<ChamadoBE> Select()
        {
            return new ChamadoDao().Select<ChamadoBE>().ToList();
        }
        public List<ChamadoBE> Select(ChamadoBE obj, bool completo)
        {
            if (completo)
                return this.CarregaDadosChamado(new ChamadoDao().Select<ChamadoBE>(obj).ToList());
            else
                return new ChamadoDao().Select<ChamadoBE>(obj).ToList();
        }
        public ChamadoBE SelectId()
        {
            return new ChamadoDao().SelectId<ChamadoBE>(new { });
        }
        public ChamadoBE SelectId(ChamadoBE obj)
        {
            return this.CarregaDadosChamado(new ChamadoDao().SelectId<ChamadoBE>(obj));
        }
        public ChamadoBE Insert(ChamadoBE obj, string mensagem, List<ArquivosBE> objArquivos)
        {
            obj.cOs_id = new ChamadoDao().Insert(obj);
            var objMensagem = new MensagemBE { cOs_id = obj.cOs_id, cOsMes_mensagem = mensagem, cOsMes_data = DateTime.Now };
            objMensagem.cOsMes_id = new MensagemBLL().Insert(objMensagem);
            obj.Mensagens = new List<MensagemBE>();
            obj.Mensagens.Add(objMensagem);
            ArquivosBLL _arq = new ArquivosBLL();
            foreach (var item in objArquivos) {
                item.cOs_id = obj.cOs_id;
                item.cOsMes_id = objMensagem.cOsMes_id;
                _arq.Insert(item);
            }

            return obj;
        }
        public bool Update(ChamadoBE obj)
        {
            var result = new ChamadoDao().Update(obj);
            return result.Value;
        }
        public bool Delete(ChamadoBE obj)
        {
            return new ChamadoDao().Delete(obj).Value;
        }

        ChamadoBE CarregaDadosChamado(ChamadoBE obj)
        {
            ClienteBLL _cliente = new ClienteBLL();
            UsuarioBLL _usuario = new UsuarioBLL();
            StatusBLL _status = new StatusBLL();
            PrioridadeBLL _prioridade = new PrioridadeBLL();
            CategoriaBLL _categoria = new CategoriaBLL();
            MensagemBLL _mensagem = new MensagemBLL();

            obj.Cliente = _cliente.SelectId(obj.cli_id);
            //obj.Usuario = _usuario.SelectId(new BE.UsuarioBE { usu_id = obj.usu_id });
            obj.Status = _status.SelectId(new StatusBE { cSt_id = obj.cSt_id });
            obj.Prioridade = _prioridade.SelectId(new PrioridadeBE { cPr_id = obj.cPr_id });
            obj.Categoria = _categoria.SelectId(new CategoriaBE { cCa_id = obj.cCa_id });
            obj.Mensagens = _mensagem.Select(new MensagemBE { cOs_id = obj.cOs_id });

            return obj;
        }
        List<ChamadoBE> CarregaDadosChamado(List<ChamadoBE> obj)
        {
            ClienteBLL _cliente = new ClienteBLL();
            UsuarioBLL _usuario = new UsuarioBLL();
            StatusBLL _status = new StatusBLL();
            PrioridadeBLL _prioridade = new PrioridadeBLL();
            CategoriaBLL _categoria = new CategoriaBLL();
            MensagemBLL _mensagem = new MensagemBLL();

            foreach (var item in obj)
            {
                item.Cliente = _cliente.SelectId(item.cli_id);
                //item.Usuario = _usuario.SelectId(new BE.UsuarioBE { usu_id = item.usu_id });
                item.Status = _status.SelectId(new StatusBE { cSt_id = item.cSt_id });
                item.Prioridade = _prioridade.SelectId(new PrioridadeBE { cPr_id = item.cPr_id });
                item.Categoria = _categoria.SelectId(new CategoriaBE { cCa_id = item.cCa_id });
                item.Mensagens = _mensagem.Select(new MensagemBE { cOs_id = item.cOs_id });
            }

            return obj;
        }
    }
}
