using System;
using System.Collections.Generic;
using System.Linq;
using Globais.BE;
using Portal.BE;
using Portal.Dao;

namespace Portal.BLL
{
    public class ClienteBLL
    {

        public List<ClienteBE> Select(int conf_id)
        {
            return this.CarregaEnderecosCliente(new ClienteDao().Select<ClienteBE>(new ClienteBE { conf_id = conf_id }).ToList());
        }
        public List<ClienteBE> SelectNotas(int conf_id)
        {
            return this.CarregaEnderecosCliente(new ClienteDao().SelectNotas<ClienteBE>(new { conf_id = conf_id }).ToList());
        }
        public List<ClienteNotasContratoBE> SelectNotasContrato(int conf_id)
        {
            return this.CarregaEnderecosCliente(new ClienteDao().SelectNotasContrato<ClienteNotasContratoBE>(new { conf_id = conf_id }).ToList());
        }

        public List<ClienteBE> Select(ClienteBE obj)
        {
            return this.CarregaEnderecosCliente(new ClienteDao().Select<ClienteBE>(obj).ToList());
        }

        public ClienteBE SelectBuscaNomeFantasia(string cli_nomeFantasia, int conf_id)
        {
            ClienteBE retorno = new ClienteDao().Select<ClienteBE>(new { cli_nomeFantasia = cli_nomeFantasia, conf_id = conf_id }).First<ClienteBE>();
            return retorno;
        }

        public List<ClienteBE> SelectBuscaServicoLista(string cli_nomeFantasia, string cli_razaoSocial, string cli_CGC, int conf_id)
        {
            List<ClienteBE> retorno;
            if (!String.IsNullOrEmpty(cli_nomeFantasia) || !String.IsNullOrEmpty(cli_razaoSocial) || !String.IsNullOrEmpty(cli_CGC))
                retorno = this.CarregaServicosCliente(new ClienteDao().Select<ClienteBE>(new { cli_nomeFantasia = cli_nomeFantasia, cli_razaoSocial = cli_razaoSocial, cli_CGC = cli_CGC, conf_id = conf_id }).ToList());
            else
                retorno = this.CarregaServicosCliente(new ClienteDao().Select<ClienteBE>(new { conf_id = conf_id }).ToList());
            return retorno;
        }

        public List<ClienteBE> SelectBuscaNotasLista(string cli_nomeFantasia, string cli_razaoSocial, string cli_CGC, int conf_id,  int cli_id = 0, bool endereco = false)
        {
            List<ClienteBE> retorno;
            if (!String.IsNullOrEmpty(cli_nomeFantasia) || !String.IsNullOrEmpty(cli_razaoSocial) || !String.IsNullOrEmpty(cli_CGC) || cli_id != 0)
                retorno = this.CarregaNotaCliente(new ClienteDao().Select<ClienteBE>(new { cli_nomeFantasia = cli_nomeFantasia, cli_razaoSocial = cli_razaoSocial, cli_CGC = cli_CGC, cli_id = cli_id, conf_id = conf_id }).ToList());
            else
                retorno = this.CarregaNotaCliente(new ClienteDao().Select<ClienteBE>(new { conf_id = conf_id }).ToList());

            if(endereco)
                retorno = this.CarregaEnderecosCliente(retorno);
            return retorno;
        }


        public List<ClienteBE> SelectBuscaNotasInNotIdLista(string not_id, int conf_id, bool endereco = false)
        {
            List<ClienteBE> retorno = new ClienteDao().SelectClienteIn<ClienteBE>(new { cli_id = not_id, conf_id = conf_id }).ToList();
            //retorno = this.CarregaNotaClientein(retorno, not_id);

            if (endereco)
                retorno = this.CarregaEnderecosCliente(retorno);
            return retorno;
        }


        public List<ClienteBE> SelectClienteNotasArquivos(string mes, int conf_id)
        {
            List<ClienteBE> retorno = new ClienteDao().SelectClienteNotasArquivos<ClienteBE>(new { mes = mes, conf_id = conf_id, tipo = 0 }).ToList();
            retorno = this.CarregaEnderecosCliente(retorno);
            return retorno;
        }




        public List<ClienteBE> SelectBuscaLista(string cli_nomeFantasia, string cli_razaoSocial, string cli_CGC)
        {
            List<ClienteBE> retorno;
            if (!String.IsNullOrEmpty(cli_nomeFantasia) || !String.IsNullOrEmpty(cli_razaoSocial) || !String.IsNullOrEmpty(cli_CGC))
                retorno = this.CarregaEnderecosCliente(new ClienteDao().Select<ClienteBE>(new ClienteBE { cli_nomeFantasia = cli_nomeFantasia, cli_razaoSocial = cli_razaoSocial, cli_CGC = cli_CGC, conf_id = Globais.Helper.Common.GetEmpresaSelecionada() }).ToList());
            else
                retorno = this.CarregaEnderecosCliente(new ClienteDao().Select<ClienteBE>(new ClienteBE { }).ToList());
            return retorno;
        }

        public ClienteBE SelectId(int id)
        {
            var obj = new ClienteDao().SelectId<ClienteBE>(new { cli_id = id });
            ClienteEnderecoBLL _bll = new ClienteEnderecoBLL();
            ClienteContatoBLL _con = new ClienteContatoBLL();

            obj.Endereco = _bll.Select(new GlobaisClienteEnderecoBE { cli_id = obj.cli_id });
            obj.Contato = _con.Select(new GlobaisClienteContatoBE { cli_id = obj.cli_id });
            return obj;
        }

        public ClienteBE SelectIdComServico(int id)
        {
            var obj = new ClienteDao().SelectId<ClienteBE>(new { cli_id = id });
            ClienteEnderecoBLL _bll = new ClienteEnderecoBLL();
            ClienteContatoBLL _con = new ClienteContatoBLL();

            obj.Endereco = _bll.Select(new GlobaisClienteEnderecoBE { cli_id = obj.cli_id });
            obj.Contato = _con.Select(new GlobaisClienteContatoBE { cli_id = obj.cli_id });

            obj.Servicos = new ClienteServicosBLL().Select(new ClienteServicosBE { cli_id = obj.cli_id, log_ativo = true });
            return obj;
        }

        public ClienteBE Insert(ClienteBE obj, GlobaisEnderecoBE _end, GlobaisClienteContatoBE _contato)
        {
            var clienteEnd = new GlobaisClienteEnderecoBE();

            obj.cli_SCM = false;
            obj.cli_MNT = false;
            obj.cli_CGC = obj.cli_CPF;

            _end.end_id = new EnderecoDao().Insert(_end);
            obj.cli_id = new ClienteDao().Insert(obj);
            _contato.cli_id = obj.cli_id;

            _contato.cont_id = new ClienteContatoDao().Insert(_contato);
            new ClienteEnderecoBLL().Insert(obj.cli_id, _end.end_id, "EP");

            return obj;
        }
        public ClienteBE Insert(ClienteBE obj)
        {
            var clienteEnd = new GlobaisClienteEnderecoBE();

            obj.cli_SCM = false;
            obj.cli_MNT = false;
            obj.cli_CGC = obj.cli_CPF;
            var clie = this.Select(new ClienteBE { cli_CGC = obj.cli_CGC, conf_id = obj.conf_id });
            if (clie.Count == 0)
            {
                obj.cli_id = new ClienteDao().Insert(obj);
                return obj;
            }
            else
                return null;
            
        }

        public bool Update(ClienteBE obj)
        {
            obj.cli_CGC = obj.cli_CPF;
            return new ClienteDao().Update(obj).Value;
        }

        public bool Delete(ClienteBE obj)
        {
            return new ClienteDao().Delete(obj).Value;
        }

        List<ClienteBE> CarregaEnderecosCliente(List<ClienteBE> obj)
        {
            ClienteEnderecoBLL _bll = new ClienteEnderecoBLL();
            ClienteContatoBLL _con = new ClienteContatoBLL();

            foreach (var item in obj)
            {
                item.Endereco = _bll.Select(new GlobaisClienteEnderecoBE { cli_id = item.cli_id });
                item.Contato = _con.Select(new GlobaisClienteContatoBE { cli_id = item.cli_id });
            }
            return obj;
        }
        List<ClienteNotasContratoBE> CarregaEnderecosCliente(List<ClienteNotasContratoBE> obj)
        {
            ClienteEnderecoBLL _bll = new ClienteEnderecoBLL();
            ClienteContatoBLL _con = new ClienteContatoBLL();

            foreach (var item in obj)
            {
                item.Endereco = _bll.Select(new GlobaisClienteEnderecoBE { cli_id = item.cli_id });
                item.Contato = _con.Select(new GlobaisClienteContatoBE { cli_id = item.cli_id });
            }
            return obj;
        }

        List<ClienteBE> CarregaServicosCliente(List<ClienteBE> obj)
        {
            ClienteServicosBLL _serv = new ClienteServicosBLL();
            ClienteNotaBLL _notas = new ClienteNotaBLL();

            foreach (var item in obj)
            {
                item.Servicos = _serv.Select(new ClienteServicosBE{ cli_id = item.cli_id, log_ativo = true });
                item.ServicosNotas = _notas.Select(new ClienteNotaBE { cli_id = item.cli_id });
            }
            return obj;
        }

        List<ClienteBE> CarregaNotaCliente(List<ClienteBE> obj)
        {
            NotasBLL _notas = new NotasBLL();

            foreach (var item in obj)
            {
                item.Nota = _notas.SelectId(new NotasBE { cli_id = item.cli_id, conf_id = item.conf_id });
            }
            return obj;
        }

        /// <summary>
        /// Carrega a lista de notas utilizando IN
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        List<ClienteBE> CarregaNotaClientein(List<ClienteBE> obj, string notas)
        {
            var _notas = new NotasBLL().SelectIN(notas);
            var _notasItens = new NotasItensBLL().SelectIn(notas);


            List<ClienteBE> retorno = new List<ClienteBE>();

            foreach (var item in _notas)
            {
                item.ItensNota = (from x in _notasItens where x.not_id.Equals(item.not_id) select x).ToList();
                var cliente = (from x in obj where x.cli_id.Equals(item.cli_id) select x).FirstOrDefault<ClienteBE>();
                cliente.Nota.ItensNota = (from x in _notasItens where x.not_id.Equals(item.not_id) select x).ToList();
                retorno.Add(cliente);
            }

            return obj;
        }
        public List<NotasBE> CarregaNotaClientein(string notas)
        {
            var _notas = new NotasBLL().SelectIN(notas);
            var _notasItens = new NotasItensBLL().SelectIn(notas);

            foreach (var item in _notas)
                item.ItensNota = (from x in _notasItens where x.not_id.Equals(item.not_id) select x).ToList();

            return _notas;
        }

        public List<NotasBE> CarregaNotaClienteinMes(string mes)
        {
            var _notas = new ClienteDao().SelectClienteNotasArquivos<NotasBE>(new { mes = mes, conf_id = Globais.Helper.Common.GetEmpresaSelecionada(), tipo = 1 }).ToList();
            var _notasItens = new ClienteDao().SelectClienteNotasArquivos<NotasItensBE>(new { mes = mes, conf_id = Globais.Helper.Common.GetEmpresaSelecionada(), tipo = 2 }).ToList();
            foreach (var item in _notas)
                item.ItensNota = (from x in _notasItens where x.not_id.Equals(item.not_id) select x).ToList();

            return _notas;
        }
    }
}
