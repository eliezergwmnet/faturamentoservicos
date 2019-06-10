/*using System;
using System.Collections;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Text.RegularExpressions;
using Portal.BE;
using System.Collections.Generic;
using Globais.BE;
using Globais.Helper;

namespace Portal.Models
{
    public class SendEmail
    {
        public EnviarEmailEnum EnviaMensagem(string Destinatario, string Assunto, string enviaMensagem, EmailBE objEmail)
        {
            try
            {
                return this.EnviarEmail(Destinatario, Assunto, enviaMensagem, objEmail, null);
            }
            catch (Exception ex)
            {
                Common.TratarLogErro(ex);
                return EnviarEmailEnum.Erro_Enviar;
            }
        }
        
        public EnviarEmailEnum EnviaMensagem(List<GlobaisClienteContatoBE> Destinatario, string Assunto, string enviaMensagem, EmailBE objEmail, ArrayList anexos)
        {
            try
            {
                return this.EnviarEmail(Destinatario, Assunto, enviaMensagem, objEmail, anexos);
            }
            catch (Exception ex)
            {
                Common.TratarLogErro(ex);
                return EnviarEmailEnum.Erro_Enviar;
            }
        }

        public EnviarEmailEnum EnviaMensagem(string Destinatario, string Assunto, string enviaMensagem, EmailBE objEmail, ArrayList anexos)
        {
            try
            {
                return this.EnviarEmail(Destinatario, Assunto, enviaMensagem, objEmail, anexos);
            }
            catch (Exception ex)
            {
                Common.TratarLogErro(ex);
                return EnviarEmailEnum.Erro_Enviar;
            }
        }

        EnviarEmailEnum EnviarEmail(List<GlobaisClienteContatoBE> Destinatario, string Assunto, string enviaMensagem, EmailBE objEmail, ArrayList anexos)
        {
            try
            {
                var itenEmviar = CarregaDestinatarios(Destinatario);
                if (itenEmviar.Count > 0)
                {
                    MailAddress from = new MailAddress(objEmail.ema_email, objEmail.ema_nome);
                    MailAddress to;

                    if (Common.AmbienteProducao)
                        to = itenEmviar[0];
                    else
                        to = new MailAddress("leandro.martins@gwmnet.net");

                    MailMessage mensagemEmail = new MailMessage(from, to);
                    mensagemEmail.Subject = Assunto;
                    mensagemEmail.Body = enviaMensagem;
                    mensagemEmail.IsBodyHtml = true;
                    mensagemEmail.Priority = MailPriority.Normal;

                    if (Common.AmbienteProducao)
                    {
                        if (itenEmviar.Count > 1)
                        {
                            for (int i = 1; i < itenEmviar.Count; i++)
                                mensagemEmail.CC.Add(itenEmviar[i]);
                        }
                    }

                    if (anexos != null && anexos.Count > 0)
                    {
                        foreach (string anexo in anexos)
                        {
                            Attachment anexado = new Attachment(anexo, MediaTypeNames.Application.Pdf);
                            mensagemEmail.Attachments.Add(anexado);
                        }
                    }

                    SmtpClient client = new SmtpClient(objEmail.ema_smtp, Convert.ToInt32(objEmail.ema_porta));
                    client.EnableSsl = false;
                    NetworkCredential cred = new NetworkCredential(objEmail.ema_email, objEmail.ema_senha);
                    client.Credentials = cred;

                    //client.UseDefaultCredentials = true;

                    client.Send(mensagemEmail);

                    return EnviarEmailEnum.Email_Enviado;

                }
                else
                {
                    return EnviarEmailEnum.Erro_DestinatarioINvalido;
                }
            }
            catch (Exception ex)
            {
                Common.TratarLogErro(ex);
                return EnviarEmailEnum.Erro_Enviar;
            }

        }

        EnviarEmailEnum EnviarEmail(string Destinatario, string Assunto, string enviaMensagem, EmailBE objEmail, ArrayList anexos)
        {
            try
            {

                if (!ValidaEnderecoEmail(Destinatario))
                    return EnviarEmailEnum.Email_Invalido;

                MailAddress from = new MailAddress(objEmail.ema_email, objEmail.ema_nome);
                MailAddress to = new MailAddress(Destinatario);

                MailMessage mensagemEmail = new MailMessage(from, to);
                mensagemEmail.Subject = Assunto;
                mensagemEmail.Body = enviaMensagem;
                mensagemEmail.IsBodyHtml = true;
                mensagemEmail.Priority = MailPriority.Normal;


                if (anexos != null && anexos.Count > 0)
                {
                    foreach (string anexo in anexos)
                    {
                        Attachment anexado = new Attachment(anexo, MediaTypeNames.Application.Octet);
                        mensagemEmail.Attachments.Add(anexado);
                    }
                }

                SmtpClient client = new SmtpClient(objEmail.ema_smtp, Convert.ToInt32(objEmail.ema_porta));
                client.EnableSsl = false;
                NetworkCredential cred = new NetworkCredential(objEmail.ema_email, objEmail.ema_senha);
                client.Credentials = cred;
                
                //client.UseDefaultCredentials = true;

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

        List<MailAddress> CarregaDestinatarios(List<GlobaisClienteContatoBE> lista)
        {
            List<MailAddress> result = new List<MailAddress>();

            if (lista.Count > 0) {
                string nome = "";

                foreach (var item in lista)
                {
                    if (!string.IsNullOrEmpty(item.cont_nome))
                        nome = item.cont_nome;

                    if (item.cont_email.Contains(";"))
                    {
                        var array = item.cont_email.Split(';');
                        foreach (var itens in array)
                        {
                            if (itens.Contains("@"))
                            {
                                if (nome != "")
                                    result.Add(new MailAddress(itens, nome));
                                else
                                    result.Add(new MailAddress(itens));
                            }
                        }
                    }
                    else
                    {
                        if (item.cont_email.Contains("@"))
                        {
                            if (nome != "")
                                result.Add(new MailAddress(item.cont_email, nome));
                            else
                                result.Add(new MailAddress(item.cont_email));
                        }
                    }
                }
                return result;
            }
            else
                return result;
        }
    }
    public enum EnviarEmailEnum
    {
        Email_Invalido,
        Email_Enviado,
        Erro_Enviar,
        Erro_DestinatarioINvalido
    }
}*/