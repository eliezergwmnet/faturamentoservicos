using System.Web.Mvc;
using System.Xml;
using Globais.Helper;
using PortalSCM.BE;
using PortalSCM.BLL;

namespace PortalSCM.Controllers
{
    public class HomeController : _SCMBaseController
    {
        public ActionResult Index()
        {
            ViewData[TituloPageEnum.TituloPagina.ToString()] = "Contrato";
            ViewData[TituloPageEnum.TituloModuloPagina.ToString()] = "Lista Contratos Clientes";



            XmlDocument doc = new XmlDocument();
            doc.Load(@"E:\MeusDocumentos\Pessoal\__Dados\GwmNet_Faturamento\Portal\NotaFiscalNFe\28-Retorno.xml");


            


            var lista = new SCMContratoBLL().Select();
            return View(lista);
        }

        public JsonResult SelectId(SCMContratoBE obj)
        {
            return Json(new SCMContratoBLL().SelectID(obj), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Cadastrar(SCMContratoBE obj)
        {
            var serv = new SCMContratoBLL();

            if (obj.cont_id == 0)
            {
                obj.conf_id = (int)Session[Globais.Helper.Globais.SessionSistemaConfId];
                obj.cont_id = serv.Insert(obj);
            }
            else
                serv.Update(obj);
            return Json(obj.cont_id, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Deletar(SCMContratoBE obj)
        {
            var serv = new SCMContratoBLL();
            serv.Delete(obj);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public void LerCSV()
        {
            /* StreamReader rd = new StreamReader(@"d:\listaservicos.csv");
            string linha = null;
            string[] linhaseparada = null;
            while ((linha = rd.ReadLine()) != null)
            {
                linhaseparada = linha.Split(';');
                var serv = new SCMServicosBE
                {
                    cat_id = "SCM",
                    log_id = 1,
                    ser_codigoServico = linhaseparada[0],
                    ser_codigoAtividade = linhaseparada[1],
                    ser_descricaoServico = linhaseparada[2],
                    ser_aliquota = Convert.ToInt32(linhaseparada[3])
                };

                new SCMServicosBLL().Insert(serv);

            }
            rd.Close();*/
        }

    }
}