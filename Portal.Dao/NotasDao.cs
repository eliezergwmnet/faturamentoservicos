using System.Collections.Generic;
using Portal.Dao.Base;

namespace Portal.Dao
{
    public class NotasDao : Functions
    {
        public override IList<T> Select<T>()
        {
            this.procedure = "proc_Notas_Select";
            return base.Select<T>();
        }

        public int SelecionarUltimaNova(object obj)
        {
            this.procedure = "proc_Notas_SelectUltimaNota";
            return base.Insert(obj);
        }

        public IList<T> SelecionarNotasEmitidasMes<T>(object obj)
        {
            this.procedure = "proc_ClienteNotas_MesSelecionado";
            return base.Select<T>(obj);
        }

        public IList<T> SelecionarNotasEmitidasCliente<T>(object obj)
        {
            this.procedure = "proc_ClienteNotas_ClienteSelecionado";
            return base.Select<T>(obj);
        }

        public IList<T> SelecionarNotasGerarXMl_Remessa<T>()
        {
            this.procedure = "proc_ClienteNotas_GerarXML_Remessa";
            return base.Select<T>();
        }

        public IList<T> SelecionarNotasGerarXMl_Remessa_Selecionadas<T>(object obj)
        {
            this.procedure = "proc_ClienteNotas_GerarXML_Remessa_In_CliId";
            return base.Select<T>(obj);
        }
        

        public IList<T> SelecionarNotasNaoEmitidas<T>(object obj)
        {
            this.procedure = "proc_Notas_SelectNaoEmitidas";
            return base.Select<T>(obj);
        }

        public bool? BaixaEmailEnviado(object obj)
        {
            this.procedure = "proc_Notas_BaixaEnviado";
            return base.Update(obj).Value;
        }


        public override IList<T> Select<T>(object obj)
        {
            this.procedure = "proc_Notas_Select";
            return base.Select<T>(obj);
        }
        public override T SelectId<T>(object obj)
        {
            this.procedure = "proc_Notas_Select";
            return base.SelectId<T>(obj);
        }

        public IList<T> SelectIN<T>(object obj)
        {
            this.procedure = "proc_Notas_IN";
            return base.Select<T>(obj);
        }

        public override int Insert(object obj)
        {
            this.procedure = "proc_Notas_Insert";
            return base.Insert(obj);
        }
        public override bool? Update(object obj)
        {
            this.procedure = "proc_Notas_Update";
            return base.Update(obj).Value;
        }

        public override bool? Delete(object obj)
        {
            this.procedure = "proc_Notas_Delete";
            return base.Delete(obj).Value;
        }

        public bool? Cancelar(object obj)
        {
            this.procedure = "proc_Notas_Cancelar";
            return base.Delete(obj).Value;
        }
        public bool? CancelaNotasMes(object obj)
        {
            this.procedure = "proc_Notas_CancelarNotaMes";
            return base.Delete(obj).Value;
        }
    }
}
