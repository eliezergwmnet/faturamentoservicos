using System;
using System.Collections.Generic;
using System.Linq;
using Portal.BE;
using Portal.Dao;

namespace Portal.BLL
{
    public class RetornoBancoBoletoBLL
    {
        public List<RetornoBancoBoletoBE> Select()
        {
            return new RetornoBancoBoletoDao().Select<RetornoBancoBoletoBE>().ToList();
        }
        public List<RetornoBancoBoletoBE> Select(RetornoBancoBoletoBE obj)
        {
            return new RetornoBancoBoletoDao().Select<RetornoBancoBoletoBE>(obj).ToList();
        }
        public RetornoBancoBoletoBE SelectId(RetornoBancoBoletoBE obj)
        {
            var result = new RetornoBancoBoletoDao().SelectId<RetornoBancoBoletoBE>(obj);
            return result;
        }

        public RetornoBancoBoletoBE BaixaManual(int id, string comentario, string formapagamento, string jurosparamento, int userId, int conf_id)
        {
            if (formapagamento == "Deposito")
                formapagamento = "DEP";
            if (formapagamento == "Transferencia")
                formapagamento = "TFB";

            var nota = new NotasBLL().SelectId(new NotasBE { not_numero = id, conf_id = conf_id });
            var cliente = new ClienteBLL().SelectId(nota.cli_id);
            var total = Convert.ToDecimal(jurosparamento) + nota.not_totalliquido;
            var obj = new RetornoBancoBoletoBE(id, "", nota.not_dtVencimento, DateTime.Now, nota.not_totalliquido, total, Convert.ToDecimal(jurosparamento), cliente.cli_razaoSocial, "--", formapagamento, userId);
            obj.retBol_comentarios = comentario;
                //) { retBol_comentarios = comentario, retBol_seuBanco = id, retBol_nossoNumero = "", retBol_formapagamento = formapagamento, retBol_ociliacao = jurosparamento }
            obj.retBol_id = new RetornoBancoBoletoDao().Insert(obj);
            return obj;
        }
        public RetornoBancoBoletoBE Insert(RetornoBancoBoletoBE obj)
        {
            obj.retBol_id = new RetornoBancoBoletoDao().Insert(obj);
            return obj;
        }
        public bool Update(RetornoBancoBoletoBE obj)
        {
            return new RetornoBancoBoletoDao().Update(obj).Value;
        }
        public bool Delete(RetornoBancoBoletoBE obj)
        {
            return new RetornoBancoBoletoDao().Delete(obj).Value;
        }
    }
}
