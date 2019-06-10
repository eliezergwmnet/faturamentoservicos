/*using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BoletoNet;
using Globais.Helper;
using Portal.BE;
using Portal.BLL;
using Portal.Models;

namespace Portal.Controllers
{
    public class BoletoController : _BaseController
    {
        public ActionResult Index()
        {            
            return View();
        }

        [HttpPost]
       public ActionResult Index(HttpPostedFileBase upload)
        {
            if(upload == null)
                ModelState.AddModelError("File", "Formato do arquivo invalido");
            else if (upload.FileName.EndsWith(".csv"))
            {
                try
                {
                    var end = this.CriarArquivoCSV(upload);
                    StreamReader rd = new StreamReader(end);
                    string linha = null;
                    string[] linhaseparada = null;
                    int usu = GlobalVariables.User.usu_id;


                    List<RetornoBancoBoletoBE> listDados = new List<RetornoBancoBoletoBE>();
                    bool contenValidacao = false;
                    while ((linha = rd.ReadLine()) != null)
                    {
                        if (!contenValidacao)
                        {
                            if (linha.Contains("Vencimento") && linha.Contains("Pagador"))
                                contenValidacao = true;
                        }
                        else
                        {
                            linhaseparada = linha.Split(';');
                            if(linhaseparada.Count() > 8)
                            {
                                listDados.Add(new RetornoBancoBoletoBE(linhaseparada[0], linhaseparada[1], linhaseparada[2], linhaseparada[3], linhaseparada[4], linhaseparada[5], linhaseparada[6], linhaseparada[7], linhaseparada[8], "BOL", usu));
                            }
                        }
                       // ModelState.AddModelError("Leitura", linha);
                    }

                    if(listDados.Count > 0)
                    {
                        RetornoBancoBoletoBLL ret = new RetornoBancoBoletoBLL();
                        foreach (var item in listDados)
                        {
                            ret.Insert(item);
                        }
                    }

                    rd.Close();

                    ViewData["listaboletos"] = listDados;
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Erro ao executar Leitura do Arquivo");
                }
               // ModelState.AddModelError("File", "Arquivo carregando com sucesso");
            }
            else
            {
                ModelState.AddModelError("File", "Formato do arquivo invalido");
            }
            return View();
        }


        public string CriarArquivoCSV(HttpPostedFileBase upload)
        {
            try
            {
                var end = String.Format(System.Web.Hosting.HostingEnvironment.MapPath("~\\{0}"), "ArquivoRemessaBanco");
                if (!Directory.Exists(end))
                    Directory.CreateDirectory(end);

                end = end + "\\" + DateTime.Now.Year.ToString();
                if (!Directory.Exists(end))
                    Directory.CreateDirectory(end);

                end = end + "\\" + DateTime.Now.Month;
                if (!Directory.Exists(end))
                    Directory.CreateDirectory(end);

                var nomeArquivo = Path.Combine(end, DateTime.Now.Day + "-" + DateTime.Now.ToString("HH-mm-ss") + ".csv");
                upload.SaveAs(nomeArquivo);
                
                return nomeArquivo;
            }
            catch (Exception ex)
            {
                Common.TratarLogErro(ex);
                return "";
            }
        }
    }
}*/