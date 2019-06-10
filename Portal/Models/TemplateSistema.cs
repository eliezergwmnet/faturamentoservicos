using System;
using Globais.Helper;

namespace Portal.Models
{
    public class TemplateSistema
    {
        ModeloTemplate templateSelecionado;
        ModeloTemplate temp;
        public TemplateSistema(string template)
        {
            foreach (ModeloTemplate val in Enum.GetValues(typeof(ModeloTemplate)))
            {
                if (template == val.ToString())
                {
                    templateSelecionado = val;
                    return;
                }
            }
        }
        
        public ModeloTemplate TemplateSelecionado {
            get
            {
                return templateSelecionado;
            }
            set
            {
                templateSelecionado = temp;
            }
        }
    }
}