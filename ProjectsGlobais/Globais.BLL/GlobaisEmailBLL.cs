using System;
using System.Collections.Generic;
using System.Linq;
using Globais.BE;
using Globais.Dao;
using Globais.Helper;

namespace Globais.BLL
{
    public class GlobaisEmailBLL
    {
        public List<GlobaisEmailBE> Select()
        {
            return new GlobaisEmailDao().Select<GlobaisEmailBE>().ToList();
        }
        public List<GlobaisEmailBE> Select(GlobaisEmailBE obj)
        {
            return new GlobaisEmailDao().Select<GlobaisEmailBE>(obj).ToList();
        }
        public GlobaisEmailBE SelectId(GlobaisEmailBE obj)
        {
            return new GlobaisEmailDao().SelectId<GlobaisEmailBE>(obj);
        }
        public GlobaisEmailBE Insert(GlobaisEmailBE obj)
        {
            obj.ema_id = new GlobaisEmailDao().Insert(obj);
            return obj;
        }
        public bool Update(GlobaisEmailBE obj)
        {
            return new GlobaisEmailDao().Update(obj).Value;
        }
        public bool Delete(GlobaisEmailBE obj)
        {
            return new GlobaisEmailDao().Delete(obj).Value;
        }

        public bool EsqueciSenha(string email)
        {
            GlobaisUsuarioBE usuario = new GlobaisUsuarioBLL().SelectEmail(email);
            if (usuario == null)
                return false;
            else
            {
                GlobaisEmailBE emailDados = this.SelectId(new GlobaisEmailBE { ema_referencia = TipoEmail.EsqueciSenha.GetDescription() });
                EmailDados dados = new EmailDados { Email = emailDados.ema_email, Nome = emailDados.ema_nome, SMTP = emailDados.ema_smtp, Porta = emailDados.ema_porta, Senha = emailDados.ema_senha };

                string Link = Common.CriptografarEmail(usuario.usu_id.ToString() + "|" + DateTime.Now.AddDays(1).ToString("dd-MM-yyyy HH:mm"));
                var empresa = new GlobaisEmpresaBLL().SelectId(new GlobaisEmpresaBE { conf_id = usuario.conf_id });


                //Replace dos campos
                emailDados.ema_html = emailDados.ema_html.Replace("{nome}", usuario.usu_nome);
                emailDados.ema_html = emailDados.ema_html.Replace("{email}", usuario.usu_email);
                emailDados.ema_html = emailDados.ema_html.Replace("{telefone}", usuario.usu_telefone);
                emailDados.ema_html = emailDados.ema_html.Replace("{celular}", usuario.usu_celular);
                emailDados.ema_html = emailDados.ema_html.Replace("{href_link}", string.Format("{0}/Login/RecuperarSenha?userautenticationvalue={1}", empresa.conf_dominio, Link));
                    
                var retorno = new EnviarEmail().Enviar(usuario.usu_email, usuario.usu_nome, emailDados.ema_html, dados);

                if (EnviarEmailEnum.Email_Enviado == retorno)
                    return true;
                else
                    return false;
            }
        }

        public bool EmailNovoCadastro(string email, string senha)
        {
            GlobaisUsuarioBE usuario = new GlobaisUsuarioBLL().SelectEmail(email);
            if (usuario == null)
                return false;
            else
            {
                GlobaisEmailBE emailDados = this.SelectId(new GlobaisEmailBE { ema_referencia = TipoEmail.NovoCadastro.GetDescription() });
                EmailDados dados = new EmailDados { Email = emailDados.ema_email, Nome = emailDados.ema_nome, SMTP = emailDados.ema_smtp, Porta = emailDados.ema_porta, Senha = emailDados.ema_senha };

                string Link = Common.CriptografarEmail(usuario.usu_id.ToString() + "|" + DateTime.Now.AddDays(1).ToString("dd-MM-yyyy HH:mm"));
                var empresa = new GlobaisEmpresaBLL().SelectId(new GlobaisEmpresaBE { conf_id = usuario.conf_id });


                //Replace dos campos
                emailDados.ema_html = emailDados.ema_html.Replace("{nome}", usuario.usu_nome);
                emailDados.ema_html = emailDados.ema_html.Replace("{email}", usuario.usu_email);
                emailDados.ema_html = emailDados.ema_html.Replace("{senha}", usuario.usu_senha);

                emailDados.ema_html = emailDados.ema_html.Replace("{href_link}", string.Format("{0}/Login", empresa.conf_dominio, Link));

                var retorno = new EnviarEmail().Enviar(usuario.usu_email, usuario.usu_nome, emailDados.ema_html, dados);

                if (EnviarEmailEnum.Email_Enviado == retorno)
                    return true;
                else
                    return false;
            }
        }

        public GlobaisUsuarioBE ValidaTrocaSenha(string idUser)
        {
            try
            {
                var dados = Common.DescriptografarEmail(idUser);
                if (dados.Contains("|"))
                {
                    var userDados = dados.Split('|');
                    if (userDados.Length == 2)
                    {
                        var id = Convert.ToInt32(userDados[0]);
                        var data = Convert.ToDateTime(userDados[1]);
                        if (data > DateTime.Now)
                        {
                            GlobaisUsuarioBE usuario = new GlobaisUsuarioBLL().SelectId(id);
                            return usuario;
                        }
                        else
                            return null;
                    }
                    else
                        return null;
                }
                else
                    return null;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

    }
}
