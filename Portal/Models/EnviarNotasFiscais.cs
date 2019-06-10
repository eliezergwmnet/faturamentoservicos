/*using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using Globais.BE;
using Portal.BE;
using Portal.BLL;

namespace Portal.Models
{
    public class EnviarNotasFiscais
    {
        EmpresaBE empresa;
        public void EnviarNotas(CancellationToken cancellationToken = default(CancellationToken))
        {
            empresa = new EmpresaBLL().SelectId(new EmpresaBE { });
            List<NotasClientesEmitidasMes> listaClientes = new NotasBLL().SelecionarNotasEmitidasMes("");
            SendEmail _send = new SendEmail();
            ClienteContatoBLL _con = new ClienteContatoBLL();
            NotasBLL _notas = new NotasBLL();

            var strSubject = "Notificação de Emissão de NFE //@NumeroNota - SCM ";
            var strBody = "Prezado Cliente, <br><br>";
            //strBody = string.Concat(strBody, "Neste email, você está recebendo a nota fiscal referente ao mês de setembro.<br><br>");
            //strBody = string.Concat(strBody, "Favor desconsiderar o email anterior, com a nota fiscal do mês de agosto, reenviada indevidamente.<br><br>");

            strBody = string.Concat(strBody, "Em anexo, segue NF Modelo 21 referente ao Serviço de Comunicação Multimídia e o respectivo boleto bancário para pagamento.<br><br>");
            strBody = string.Concat(strBody, "Agradecemos a atenção. <br><br>");
            strBody = string.Concat(strBody, "Atenciosamente,<br><br>");
            strBody = string.Concat(strBody, "GWMNET - Global Web Master Ltda.");

            var email = new EmailBLL().SelectReferencia("NFE");

            foreach (var item in listaClientes)
            {
                if (item.not_preenchida == 0)
                {
                    ArrayList anexos = CarregaAnexos(item);
                    item.Contato = _con.Select(new GlobaisClienteContatoBE { cli_id = item.cli_id });

                    EnviarEmailEnum res = _send.EnviaMensagem(item.Contato, strSubject.Replace("//@NumeroNota", item.not_numero.ToString().PadLeft(6, '0')), strBody.Replace("//@NumeroNota", item.not_numero.ToString().PadLeft(6, '0')), email, anexos);
                    if (res == EnviarEmailEnum.Email_Enviado)
                    {
                        _notas.BaixaBoletoEnviado(item.not_id);
                    }
                }
            }
        }
        ArrayList CarregaAnexos(NotasClientesEmitidasMes nota)
        {
            ArrayList anexos = new ArrayList();
            string _nota = "";
            string _bole = "";

            if(empresa.confCom_nota)
                _nota = nota.not_linkNota != "" ? this.CarregaEndereco(nota.not_id, nota.not_dtEmissao, true, nota.not_numero) : "";
            if(empresa.confCom_boleto)
                _bole = nota.not_linkBoleto != "" ? CarregaEndereco(nota.not_id, nota.not_dtEmissao, false, nota.not_numero) : "";
            
            if (_nota != "") anexos.Add(_nota);
            if (_bole != "") anexos.Add(_bole);
            return anexos;
        }

        string CarregaEndereco(int numeroNota, DateTime dtEmissao, bool nota, int not_numero)
        {
            var nomeFile = numeroNota.ToString();
            var pasta = nota ? "Notas" : "Boletos";
            var result = String.Format(System.Web.Hosting.HostingEnvironment.MapPath("~\\{0}"), "Notas_Fiscais");
            result += "\\" + dtEmissao.Year.ToString() + "\\" + dtEmissao.Month.ToString() + "\\" + pasta + "\\";
            return validaArquivo(result, nomeFile, ".pdf", nota, not_numero);
        }

        string validaArquivo(string urlFile, string nome, string extensao, bool nota, int not_numero)
        {
            string fileName = string.Concat(urlFile, nome, extensao);
            if (File.Exists(fileName))
            {
                string result = "";
                if (nota)
                    result = string.Concat(urlFile, "Nota Fiscal Numero ", not_numero, extensao);
                else
                    result = string.Concat(urlFile, "Boleto bancário ", not_numero, extensao);
                if (!File.Exists(result))
                    File.Copy(fileName, result);
                return result;
            }
            else
                return "";
        }
    }
}*/