using System;
using System.Collections;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Text.RegularExpressions;

namespace Globais.Helper
{
    public class EnviarEmail
    {

        public EnviarEmailEnum Enviar(string Destinatario, string Assunto, string enviaMensagem, EmailDados objEmail, ArrayList Anexos = null)
        {
            try
            {

                if (!ValidaEnderecoEmail(Destinatario))
                    return EnviarEmailEnum.Email_Invalido;

                MailAddress from = new MailAddress(objEmail.Email, objEmail.Nome);
                MailAddress to = new MailAddress(Destinatario);

                MailMessage mensagemEmail = new MailMessage(from, to);
                mensagemEmail.Subject = Assunto;
                mensagemEmail.Body = enviaMensagem;
                mensagemEmail.IsBodyHtml = true;
                mensagemEmail.Priority = MailPriority.Normal;


                if (Anexos != null && Anexos.Count > 0)
                {
                    foreach (string anexo in Anexos)
                    {
                        Attachment anexado = new Attachment(anexo, MediaTypeNames.Application.Octet);
                        mensagemEmail.Attachments.Add(anexado);
                    }
                }

                SmtpClient client = new SmtpClient(objEmail.SMTP, Convert.ToInt32(objEmail.Porta));
                client.EnableSsl = false;
                NetworkCredential cred = new NetworkCredential(objEmail.Email, objEmail.Senha);
                client.Credentials = cred;

                client.Send(mensagemEmail);

                return EnviarEmailEnum.Email_Enviado;
            }
            catch (Exception ex)
            {
                Common.TratarLogErro(ex);
                return EnviarEmailEnum.Erro_Enviar;
            }

        }

        public static bool ValidaEnderecoEmail(string enderecoEmail)
        {
            try
            {
                string texto_Validar = enderecoEmail;
                Regex expressaoRegex = new Regex(@"\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}");

                if (expressaoRegex.IsMatch(texto_Validar))
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

    public enum EnviarEmailEnum
    {
        Email_Invalido,
        Email_Enviado,
        Erro_Enviar,
        Erro_DestinatarioINvalido
    }


    public class EmailDados
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string SMTP { get; set; }
        public string Senha { get; set; }
        public string Porta { get; set; }
    }
}
