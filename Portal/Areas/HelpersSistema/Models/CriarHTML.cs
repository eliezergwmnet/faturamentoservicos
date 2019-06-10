using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelpersSistema.Models
{
    public class CriarHTML
    {
        List<Table> colunas;
        string table;
        public CriarHTML(string _table, string nome, List<Table> coln)
        {
            colunas = coln;
            table = _table;
        }
        public string CriarItem()
        {
            CriarIndex();
            CriarInsert();
            CriarUpdate();

            return "";
        }

        public string CriarIndex()
        {
            string retorno = Help.CriarTituloPage();

            retorno += "@model List<PortalSCM.BE.SCMContratoBE> \n";
            retorno += " \n";
            retorno += "<div class=\"data-table-list\"> \n";
            retorno += "    <div class=\"table-responsive\"> \n";
            retorno += "        <table id=\"data-table-basic\" class=\"table table-striped\"> \n";
            retorno += "            <thead> \n";
            retorno += "                <tr> \n";
            foreach (var item in colunas)
            {
                retorno += "                    <th>" + item.Coluna + "</th> \n";
            }
            retorno += "                    <th style=\"padding: 15px 0px !important;width: 35px !important;\"></th> \n";
            retorno += "                    <th style=\"padding: 15px 0px !important;width: 35px !important;\"></th> \n";
            retorno += "                    <th style=\"padding: 15px 0px !important;width: 35px !important;\"></th> \n";
            retorno += "                </tr> \n";
            retorno += "            </thead> \n";
            retorno += "            <tbody> \n";
            retorno += "                @foreach (var item in Model) \n";
            retorno += "                { \n";
            retorno += "                    <tr> \n";
            foreach (var item in colunas)
            {
                if (item.Tipo == "decimal")
                    retorno += "                        <td>@string.Format(\"{0:0,0.00}\", item." + item.Coluna + ")</td> \n";
                else if (item.Tipo == "date" || item.Tipo == "datetime")
                    retorno += "                        <td>@item." + item.Coluna + ".ToString(\"dd/MM/yyyy\")</td> \n";
                else
                    retorno += "                        <td>@item." + item.Coluna + "</td> \n";
            }
            retorno += "                        <td style=\"padding: 15px 0px !important;\"><a href=\"@Url.Content(\"~/SCM/ContratoServicos?contrato=\" + item." + colunas[0].Coluna + ")\" class=\"btn btn-success btn-sm\" alt=\"Alterar Informação\" title=\"Serviços\"><i class=\"fa fa-briefcase\"></i></a></td> \n";
            retorno += "                        <td style=\"padding: 15px 0px !important;\"><button class=\"btn btn-warning btn-sm\" alt=\"Alterar Informação\" title=\"Alterar Informação\" onclick=\"SCMAlterarContrato(@item." + colunas[0].Coluna + ")\"><i class=\"fa fa-arrow-circle-o-right\"></i></button></td> \n";
            retorno += "                        <td style=\"padding: 15px 0px !important;\"><button class=\"btn btn-danger btn-sm\" alt=\"Remover Item\" onclick=\"SCMRemoverContrato(@item.cont_id)\" title=\"Remover Item\"><i class=\"fa fa-trash-o\"></i></button></td> \n";
            retorno += "                    </tr> \n";
            retorno += "                } \n";
            retorno += "            </tbody> \n";
            retorno += "        </table> \n";
            retorno += "    </div> \n";
            retorno += "</div> \n";
            retorno += " \n";
            retorno += "@Html.Partial(\"~/Areas/PortalSCM/Views/Home/ModelContratoCadastro.cshtml\") \n";
            retorno += " \n";
            retorno += "<script src=\"~/Content/Template/Verde/js/data-table/jquery.dataTables.min.js\"></script> \n";
            retorno += "<script src=\"~/Content/Template/Verde/js/data-table/data-table-act.js\"></script> \n";
            retorno += "<script src=\"~/Areas/PortalSCM/js/Contrato.js\"></script> \n";

            retorno = retorno.Replace("//NOME//", table);
            retorno = retorno.Replace("//ENTIDADE//", HelpersSistema.Models.Base.Base.NomeEntidade);

            return retorno;
        }


        public string CriarInsert()
        {
            string retorno = Help.CriarTituloPage();

            retorno += " <div class=\"modal fade\" id=\"SCMCadastroContrato\" role=\"dialog\">\n";
            retorno += "     <div class=\"modal-dialog modals-default\">\n";
            retorno += "         <div class=\"modal-content\">\n";
            retorno += "             <div class=\"modal-header\" style=\"margin-bottom: 20px;background-color: #fff !important;\">\n";
            retorno += "                 <div class=\"row\">\n";
            retorno += "                     <div class=\"col-lg-12 col-md-12 col-sm-12 col-xs-12\">\n";
            retorno += "                         <div class=\"breadcomb-wp\">\n";
            retorno += "                             <div class=\"breadcomb-icon\">\n";
            retorno += "                                 <i class=\"notika-icon notika-app\"></i>\n";
            retorno += "                             </div>\n";
            retorno += "                             <div class=\"breadcomb-ctn\">\n";
            retorno += "                                 <h2>Contrato</h2>\n";
            retorno += "                                 <p>Cadastro de Contrato</p>\n";
            retorno += "                             </div>\n";
            retorno += "                         </div>\n";
            retorno += "                     </div>\n";
            retorno += "                 </div>\n";
            retorno += "                 <button type=\"button\" class=\"close\" data-dismiss=\"modal\">&times;</button>\n";
            retorno += "             </div>\n";
            retorno += "             <form id=\"SCMCadastroServico\" method=\"get\">\n";
            retorno += "                 <div class=\"modal-body\">\n";
            retorno += "                     <input id=\"cont_id\" name=\"cont_id\" type=\"hidden\" value=\"\" />\n";
            foreach (var item in colunas)
            {
                if (item.Tipo == "int")
                {
                    retorno += "                     <div class=\"col-lg-12 col-md-12 col-sm-12 col-xs-12\">\n";
                    retorno += "                         <div class=\"form-group\">\n";
                    retorno += "                             <label for=\"" + item.Coluna + "\" class=\"control-label mb-1\">" + item.Coluna + "</label>\n";
                    retorno += "                             <div class=\"nk-int-st\">\n";
                    retorno += "                                 <select id=\"" + item.Coluna + "\" name=\"" + item.Coluna + "\" class=\"form-control obrigatorio\"></select>\n";
                    retorno += "                             </div>\n";
                    retorno += "                         </div>\n";
                    retorno += "                     </div>\n";
                }
                else
                {
                    var classe = "";
                    if (item.Tipo == "date" || item.Tipo == "datetime")
                        classe = "formDate";
                    else if (item.Tipo == "decimal")
                        classe = "money";

                    retorno += "                     <div class=\"col-lg-12 col-md-12 col-sm-12 col-xs-12\">\n";
                    retorno += "                         <div class=\"form-group\">\n";
                    retorno += "                             <label for=\"" + item.Coluna + "\" class=\"control-label mb-1\">" + item.Coluna + "</label>\n";
                    retorno += "                             <div class=\"nk-int-st\">\n";
                    retorno += "                                 <input id=\"" + item.Coluna + "\" placeholder=\"" + item.Coluna + "\" name=\"" + item.Coluna + "\" type=\"text\" value=\"\" class=\"form-control " + classe + " obrigatorio\">\n";
                    retorno += "                             </div>\n";
                    retorno += "                         </div>\n";
                    retorno += "                     </div>\n";
                }

            }
            retorno += "                 </div>\n";
            retorno += "                 <div class=\"modal-footer\" style=\"margin-top: 30px\">\n";
            retorno += "                     <button type=\"button\" class=\"btn btn-default\" onclick=\"SCMCadastrarServicos()\" >Salvar</button>\n";
            retorno += "                     <button type=\"button\" class=\"btn btn-default\" data-dismiss=\"modal\">Cancelar</button>\n";
            retorno += "                 </div>\n";
            retorno += "             </form>\n";
            retorno += "         </div>\n";
            retorno += "     </div>\n";
            retorno += " </div>\n";


            retorno = retorno.Replace("//NOME//", table);
            retorno = retorno.Replace("//ENTIDADE//", HelpersSistema.Models.Base.Base.NomeEntidade);

            return retorno;
        }


        public string CriarUpdate()
        {
            string retorno = Help.CriarTituloPage();

            retorno += "/*";
            retorno += "EXEMPLO DE FUNÇÕES BASICAS DO SISTEMA\n";
            retorno += "\n";
            retorno += "CARREGAR CHECKBOX\n";
            retorno += "$('.i-checks').iCheck({\n";
            retorno += "	checkboxClass: 'icheckbox_square-green',\n";
            retorno += "	radioClass: 'iradio_square-green',\n";
            retorno += "});\n";
            retorno += "\n";
            retorno += "ATIVAR CHECKBOX\n";
            retorno += "$(\"#cont_avulso\").iCheck('check');\n";
            retorno += "DESATIVAR CHECKBOX\n";
            retorno += "$(\"#cont_avulso\").iCheck('uncheck');\n";
            retorno += "\n";
            retorno += "//CARREGAR COMBO\n";
            retorno += "CarregarSelect(\"cli_id\", \"CarregaClientes\");\n";
            retorno += "*/\n";
            retorno += "\n";
            retorno += "$(function () {\n";
            retorno += "    $(\"#buttonTituloPagesCadastro\").click(function () {\n";
            retorno += "        CadastrarConfiguracaoItem();\n";
            retorno += "    })\n";
            retorno += "});\n";
            retorno += "\n";
            retorno += "function CadastrarConfiguracaoItem() {\n";
            retorno += "    \n";
            int i = 0;
            foreach (var item in colunas)
            {
                if (item.Tipo == "int" && i > 0)
                    retorno += "        CarregarSelect(\"" + item.Coluna + "\", \"CarregaClientes\");\n";
                i++;
            }
            retorno += "    $('#SCMCadastroContrato').modal({ backdrop: 'static', keyboard: true });\n";
            retorno += "}\n";
            retorno += "\n";
            retorno += "function SCMCadastrarServicos() {\n";
            retorno += "    if (ValidaCampos(\"SCMCadastroContrato\")) {\n";
            retorno += "        var obj = {\n";
            foreach (var item in colunas)
            {
                retorno += "            " + item.Coluna + ": $(\"#" + item.Coluna + "\").val(),\n";
            }

            retorno += "        }\n";
            retorno += "\n";
            retorno += "        EnviarRequisicao(\"/SCM/Home/Cadastrar\", obj, \"Post\", function (res) {\n";
            retorno += "            window.location.href = \"/SCM/ContratoServicos?contrato=\" + res;\n";
            retorno += "        })\n";
            retorno += "    }\n";
            retorno += "}\n";
            retorno += "\n";
            retorno += "function SCMAlterarContrato(id) {\n";
            retorno += "    EnviarRequisicao(\"/SCM/Home/SelectId\", {  " + colunas[0].Coluna + ": id }, \"Post\", function (res) {\n";
            foreach (var item in colunas)
            {
                retorno += "        $(\"#" + item.Coluna + "\").val(res." + item.Coluna + ");\n";
            }

            retorno += "\n";
            retorno += "        $('#SCMCadastroContrato').modal({ backdrop: 'static', keyboard: true });\n";
            retorno += "    });\n";
            retorno += "}\n";
            retorno += "\n";
            retorno += "function SCMRemoverContrato(id) {\n";
            retorno += "    Confirmacao(\"Atenção\", \"Tem certeza que deseja remover o servço ? \", function (res) {\n";
            retorno += "        if (res) {\n";
            retorno += "            EnviarRequisicao(\"/SCM/Home/Deletar\", {  " + colunas[0].Coluna + ": id }, \"Post\", function () {\n";
            retorno += "                window.location.reload();\n";
            retorno += "            });\n";
            retorno += "        }\n";
            retorno += "    });\n";
            retorno += "}\n";

            retorno = retorno.Replace("//NOME//", table);
            retorno = retorno.Replace("//ENTIDADE//", HelpersSistema.Models.Base.Base.NomeEntidade);

            return retorno;
        }

    }
}
