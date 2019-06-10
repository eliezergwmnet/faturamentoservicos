using System;
using System.Collections.Generic;
using System.Linq;
using Portal.BE;
using Portal.Dao;

namespace Portal.BLL
{
    public class NotasBLL
    {
        public List<NotasBE> Select()
        {
            return new NotasDao().Select<NotasBE>().ToList();
        }
        public List<NotasBE> Select(NotasBE obj)
        {
            return new NotasDao().Select<NotasBE>(obj).ToList();
        }
        public List<NotasBE> SelecionarNotasNaoEmitidas()
        {
            return new NotasDao().SelecionarNotasNaoEmitidas<NotasBE>(new { conf_id = Globais.Helper.Common.GetEmpresaSelecionada() }).ToList();
        }

        public List<NotasClientesEmitidasMes> SelecionarNotasEmitidasMes(string mes = "")
        {
            if (mes == "" || mes == null)
                mes = DateTime.Now.ToString("MM/yyyy");
            return new NotasDao().SelecionarNotasEmitidasMes<NotasClientesEmitidasMes>(new {mes = mes, conf_id = Globais.Helper.Common.GetEmpresaSelecionada() }).ToList();
        }
        public List<NotasClientesEmitidasMes> SelecionarNotasEmitidasCliente(int cliente)
        {
            return new NotasDao().SelecionarNotasEmitidasCliente<NotasClientesEmitidasMes>(new { cliente = cliente }).ToList();
        }

        public List<NotasClientesEmitidasMes> SelecionarNotasGerarXMl_Remessa()
        {
            return new NotasDao().SelecionarNotasGerarXMl_Remessa<NotasClientesEmitidasMes>().ToList();
        }

        public bool BaixaBoletoEnviado(int not_id)
        {
            return new NotasDao().BaixaEmailEnviado(new { not_id = not_id}).Value;
        }

        /// <summary>
        /// Carrega a lista de Notas a serem geradas usando o In com os itens selecionados
        /// </summary>
        /// <returns></returns>
        public List<NotasClientesEmitidasMes> SelecionarNotasGerarXMl_Remessa_Selecionadas(string notas)
        {
            return new NotasDao().SelecionarNotasGerarXMl_Remessa_Selecionadas<NotasClientesEmitidasMes>(new { @cli_id = notas }).ToList();
        }

        public int SelecionarUltimaNova()
        {
            return new NotasDao().SelecionarUltimaNova(new { });
        }
        public NotasBE SelectId(NotasBE obj)
        {
            var result = new NotasDao().SelectId<NotasBE>(obj);
            if (result != null)
                result.ItensNota = new NotasItensBLL().Select(new NotasItensBE { not_id = result.not_id });
            return result;
        }

        public List<NotasBE> SelectIN(string notas)
        {
            var result = new NotasDao().SelectIN<NotasBE>(new { cli_id = notas }).ToList();
            return result;
        }



        public NotasBE Insert(NotasBE obj)
        {
            obj.not_id = new NotasDao().Insert(obj);
            return obj;
        }
        public bool Update(NotasBE obj)
        {
            return new NotasDao().Update(obj).Value;
        }
        public bool Delete(NotasBE obj)
        {
            return new NotasDao().Delete(obj).Value;
        }
        public bool Cancelar(int not_id, int conf_id)
        {
            return new NotasDao().Cancelar(new { not_id  = not_id, conf_id = conf_id}).Value;
        }
        public bool CancelaNotasMes(int conf_id)
        {
            return new NotasDao().CancelaNotasMes(new { conf_id = conf_id }).Value;
        }
    }
}
