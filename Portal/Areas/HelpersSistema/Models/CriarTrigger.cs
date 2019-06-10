using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelpersSistema.Models
{
    public class CriarTrigger
    {
        public string CriarTriggerTable(string table, string campoChave)
        {
            string retorno = Help.CriarTituloPage();

            retorno += " CREATE TRIGGER [dbo].[triInsert_@nomeTabela] \n";
            retorno += " 	ON  [dbo].[@nomeTabela] \n";
            retorno += " 	AFTER INSERT \n";
            retorno += " AS  \n";
            retorno += " BEGIN \n";
            retorno += " 	DECLARE @idlog INT \n";
            retorno += " 	exec @idlog = [sistemaviaweb_helper].[dbo].[proc_Log_Insert] @tbl_nome = '@nomeTabela'\n";
            retorno += " 	UPDATE @nomeTabela SET [log_id] = @idlog WHERE @campoChave = (SELECT MAX( @campoChave) FROM @nomeTabela) \n";
            retorno += " END \n";
            retorno += " \n";
            retorno += Help.CriarTituloPage();
            retorno += " CREATE TRIGGER [dbo].[triUpdate_@nomeTabela] \n";
            retorno += " 	ON  [dbo].[@nomeTabela] \n";
            retorno += " 	AFTER Update \n";
            retorno += " AS  \n";
            retorno += " BEGIN \n";
            retorno += " 	UPDATE [sistemaviaweb_helper].dbo.tblLog SET [log_alteracao] = getdate()  \n";
            retorno += " 		WHERE log_id = (SELECT log_id FROM deleted) \n";
            retorno += " END \n";
            retorno += " \n";
            retorno += Help.CriarTituloPage();
            retorno += " CREATE TRIGGER [dbo].[triDelete_@nomeTabela] \n";
            retorno += " 	ON  [dbo].[@nomeTabela] \n";
            retorno += " 	FOR DELETE \n";
            retorno += " AS  \n";
            retorno += " BEGIN \n";
            retorno += " 	UPDATE [sistemaviaweb_helper].dbo.tblLog SET [log_exclusao] = getdate() , log_ativo = 0 \n";
            retorno += " 		WHERE log_id = (SELECT log_id FROM deleted) \n";
            retorno += " END \n";

            return retorno.Replace("@nomeTabela", table).Replace("@campoChave", campoChave);
        }
    }
}