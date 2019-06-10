using System;
using System.IO;

namespace Globais.Helper
{
    public class ConverterImagem
    {
        public string Base64ToImage(string base64String, TipoImagem tipoImagem)
        {
            try
            {
                string t = System.Uri.UnescapeDataString(base64String.Replace("data:image/jpeg;base64,", "").Replace("data:image/png;base64,", ""));
                byte[] newfile = Convert.FromBase64String(t);
                string diretorio = ConverterImagem.RetornaDiretorioImagem(tipoImagem);
                var nome = DateTime.Now.ToString("yyyy-MM-dd-hrh-MM-ss") + ".jpg";
                File.WriteAllBytes(diretorio + @"\" + nome, newfile);
                //return diretorio + @"\" + nome + ".jpg";
                return String.Concat("~/Imagem/", tipoImagem.GetDescription(), "/", nome);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        public static string RetornaDiretorioImagem(TipoImagem tipoImagem)
        {
            try
            {

                string diretorio = System.Web.HttpContext.Current.Server.MapPath(String.Concat("~/Imagem/", tipoImagem.GetDescription()));
                if (!Directory.Exists(diretorio))
                    Directory.CreateDirectory(diretorio);

                return diretorio;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}
