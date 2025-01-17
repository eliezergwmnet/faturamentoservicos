﻿using System.Collections.Generic;
using System.Linq;
using Globais.BE;
using Globais.Dao;

namespace Globais.BLL
{
    public class GlobaisClienteEnderecoBLL
    {
        public List<GlobaisClienteEnderecoBE> Select(GlobaisClienteEnderecoBE obj)
        {
            return new GlobaisClienteEnderecoDao().Select<GlobaisClienteEnderecoBE>(obj).ToList();
        }
        public GlobaisClienteEnderecoBE SelectId(GlobaisClienteEnderecoBE obj)
        {
            return new GlobaisClienteEnderecoDao().SelectId<GlobaisClienteEnderecoBE>(obj);
        }
        public int Insert(GlobaisClienteEnderecoBE obj)
        {
            var end = new GlobaisEnderecoBLL().Insert(new GlobaisEnderecoBE { end_cep = obj.end_cep, end_logradouro = obj.end_logradouro, end_numero = obj.end_numero, end_complemento = obj.end_complemento, end_bairro = obj.end_bairro, end_cidade = obj.end_cidade, end_estado = obj.end_estado });
            var cli_id = new GlobaisClienteEnderecoDao().Insert(new { cli_id = obj.cli_id, cliEnd_Tipo = obj.cliEnd_Tipo, end_id = end.end_id });
            return cli_id;
        }

        public int Insert(int _cliente, int _endereco, string _tipoEnd)
        {
            var cli_id = new GlobaisClienteEnderecoDao().Insert(new { cli_id = _cliente, cliEnd_Tipo = _tipoEnd, end_id = _endereco });
            return cli_id;
        }
        public bool Update(GlobaisClienteEnderecoBE obj)
        {
            var end = new GlobaisEnderecoBLL().Update(new GlobaisEnderecoBE { end_id = obj.end_id, end_cep = obj.end_cep, end_logradouro = obj.end_logradouro, end_numero = obj.end_numero, end_complemento = obj.end_complemento, end_bairro = obj.end_bairro, end_cidade = obj.end_cidade, end_estado = obj.end_estado });
            var cli_id = new GlobaisClienteEnderecoDao().Update(new { cli_id = obj.cli_id, cliEnd_Tipo = obj.cliEnd_Tipo, end_id = obj.end_id, cliEnd_id = obj.cliEnd_id });
            return true;
        }
        public bool Delete(GlobaisClienteEnderecoBE obj)
        {
            return new GlobaisClienteEnderecoDao().Delete(obj).Value;
        }
    }
}
