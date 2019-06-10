/*using System;
using System.Drawing;
using System.IO;
using System.Net;
using Globais.Helper;
using MessagingToolkit.QRCode.Codec;
using Portal.BE.Chamado;
using Portal.BLL;

namespace Portal.Models
{
    public class ChamadoEnviarEmail
    {
        string html;
        int idTicket;
        ChamadoBE obj;
        public ChamadoEnviarEmail(int id, ChamadoBE _obj)
        {
            this.idTicket = id;
            this.obj = _obj;
        }
        public int EnviarTicketEmail()
        {
            var email = new EmailBLL().SelectReferencia("SEN");
            SendEmail _send = new SendEmail();

            this.CarregarHtml(string.Concat(Common.UrlDados, "Login/HTML"));
            this.ReplaceHtml();
            CriarHRCode(string.Concat(Common.UrlDados, "Chamado/Ticket/Update/", idTicket));
            EnviarEmailEnum res = _send.EnviaMensagem("leandro.martins@gwmnet.net", "Ticket GWMNet", html, email);

            return 1;
        }

        void ReplaceHtml()
        {

            html = html.Replace("//NumeroChamado//", obj.cOs_numero);
            html = html.Replace("//DataChamado//", obj.Mensagens[obj.Mensagens.Count - 1].cOsMes_data.ToString("dd/MM/yyyy HH:mm"));
            html = html.Replace("//TituloEmailChamado//", "Ticket GWM Net");
            html = html.Replace("//Titulochamado//", obj.cOs_titulo);
            html = html.Replace("//StatusChamado//", obj.Status.cSt_nome);
            html = html.Replace("//CategoriaChamado//", obj.Categoria.cCa_nome);
            html = html.Replace("//DescricaoChamado//", obj.Mensagens[obj.Mensagens.Count - 1].cOsMes_mensagem);
            html = html.Replace("//LinkChamado//", string.Concat(Common.UrlDados, "Chamado/Ticket/", this.idTicket.ToString()));
            html = html.Replace("//QRCode//", this.CriarHRCode(string.Concat(Common.UrlDados, "/Chamado/Ticket/", this.idTicket.ToString())));
            html = html.Replace("//Logo//",this.Logo());
        }
        void CarregarHtml(string url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = WebRequestMethods.Http.Get;
                request.Accept = "application/json";

                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                this.html = reader.ReadToEnd();
            }
            catch(Exception ex)
            {
                Common.TratarLogErro(ex);
                html = "";
            }
        }


        string CriarHRCode(string url)
        {
            QRCodeEncoder qrCodecEncoder = new QRCodeEncoder();
            qrCodecEncoder.QRCodeBackgroundColor = System.Drawing.Color.White;
            qrCodecEncoder.QRCodeForegroundColor = System.Drawing.Color.Black;
            qrCodecEncoder.CharacterSet = "UTF-8";
            qrCodecEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            qrCodecEncoder.QRCodeScale = 6;
            qrCodecEncoder.QRCodeVersion = 0;
            qrCodecEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.Q;
            Image imageQRCode;
            //string a ser gerada
            String dados = url;
            imageQRCode = qrCodecEncoder.Encode(dados);
            imageQRCode.Save(@"d:\.qr.jpg");

            var bytes = new ConverterImagem().ImageToByteArray(imageQRCode);
            string base64 = Convert.ToBase64String(bytes);
            string fnLogo = string.Format("data:image/jpeg;base64,{0}", base64);

            return fnLogo;
        }

        string Logo()
        {

            string urllogo = System.Web.HttpContext.Current.Server.MapPath("~/images/logo-header.jpg");
            Bitmap base64Logo = new Bitmap(urllogo);
            var bytes = new ConverterImagem().ImageToByteArray(base64Logo);
            string base64 = Convert.ToBase64String(bytes);
            string fnLogo = string.Format("data:image/jpeg;base64,{0}", base64);

            return fnLogo;
        }
    }
}*/