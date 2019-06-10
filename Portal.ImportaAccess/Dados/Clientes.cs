/*using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Globais.BE;
using Portal.BE;
using Portal.BLL;

namespace Portal.ImportaAccess.Dados
{
    public class Clientes
    {
        public void CarregaClientes()
        {
            var cliente = Conection.Select("select * from clientes");
            ClienteBLL _cliBLL = new ClienteBLL();
            EnderecoBLL _end = new EnderecoBLL();
            ClienteEnderecoBLL _Cli_End = new ClienteEnderecoBLL();
            ClienteContatoBLL _contato = new ClienteContatoBLL();

            List<ClienteBE> clientes = new List<ClienteBE>();
            while (cliente.Read())
            {
                ClienteBE _cli = new ClienteBE
                {
                    cli_nomeFantasia = cliente["Codcli"].ToString(),
                    cli_razaoSocial = cliente["Nome do Cliente"].ToString(),
                    cli_CodCidIBGE = cliente["CodCidIBGE"].ToString(),
                    cli_CGC = cliente["CGC"].ToString(),
                    cli_CPF = cliente["CGC"].ToString(),
                    cli_incricaoEstadual = cliente["Inscricao"].ToString(),
                    cli_tipoVencimento = cliente["TipoVencimento"].ToString(),
                    cli_parametroVencimento = cliente["ParametroVencimento"].ToString(),
                    conf_id = 2,
                    cli_tipoInscricao = cliente["TipoInscricao"].ToString(), //"01 Pessoa Fisica - 02 Pessoa Juritica

                };
                _cli = _cliBLL.Insert(_cli);
                if (_cli != null)
                {
                    _cli.Endereco = new List<GlobaisClienteEnderecoBE>();
                    _cli.Endereco.Add(new GlobaisClienteEnderecoBE
                    {
                        end_cep = cliente["Cep"].ToString(),
                        end_logradouro = cliente["Endereço"].ToString(),
                        end_numero = cliente["Numero"].ToString(),
                        end_complemento = cliente["Complemento"].ToString(),
                        end_bairro = cliente["Bairro"].ToString(),
                        end_cidade = cliente["Cidade"].ToString(),
                        end_estado = cliente["Estado"].ToString(),
                        end_latitude = "",
                        end_longetitude = "",
                    });
                    _cli.Endereco[0].end_id = _end.Insert(_cli.Endereco[0]).end_id;
                    _Cli_End.Insert(_cli.cli_id, _cli.Endereco[0].end_id, "EP");

                    if (cliente["Endereco-cob"].ToString().Trim().ToUpper() != cliente["Endereço"].ToString().Trim().ToUpper() || cliente["Numero-cob"].ToString().Trim().ToUpper() != cliente["Numero"].ToString().Trim().ToUpper() || cliente["Complemento-cob"].ToString().Trim().ToUpper() != cliente["Complemento"].ToString().Trim().ToUpper())
                    {
                        _cli.Endereco.Add(new GlobaisClienteEnderecoBE
                        {
                            end_cep = cliente["Cep-cob"].ToString(),
                            end_logradouro = cliente["Endereco-cob"].ToString(),
                            end_numero = cliente["Numero-cob"].ToString(),
                            end_complemento = cliente["Complemento-cob"].ToString(),
                            end_bairro = cliente["Bairro-cob"].ToString(),
                            end_cidade = cliente["Cidade-cob"].ToString(),
                            end_estado = cliente["Estado-cob"].ToString(),
                            end_latitude = "",
                            end_longetitude = "",
                        });
                        _cli.Endereco[1].end_id = _end.Insert(_cli.Endereco[1]).end_id;
                        _Cli_End.Insert(_cli.cli_id, _cli.Endereco[1].end_id, "EC");
                    }
                    if (cliente["Endereco-inst"].ToString().Trim().ToUpper() != "" && (cliente["Endereco-inst"].ToString().Trim().ToUpper() != cliente["Endereço"].ToString().Trim().ToUpper() || cliente["bairro-inst"].ToString().Trim().ToUpper() != cliente["bairro"].ToString().Trim().ToUpper()))
                    {
                        _cli.Endereco.Add(new GlobaisClienteEnderecoBE
                        {
                            end_cep = cliente["cep-inst"].ToString(),
                            end_logradouro = cliente["estado-inst"].ToString(),
                            end_numero = "--",
                            end_complemento = "--",
                            end_bairro = cliente["bairro-inst"].ToString(),
                            end_cidade = cliente["cidade-inst"].ToString(),
                            end_estado = cliente["estado-inst"].ToString(),
                            end_latitude = "",
                            end_longetitude = "",
                        });
                        _cli.Endereco[_cli.Endereco.Count - 1].end_id = _end.Insert(_cli.Endereco[(_cli.Endereco.Count - 1)]).end_id;
                        _Cli_End.Insert(_cli.cli_id, _cli.Endereco[_cli.Endereco.Count - 1].end_id, "EI");
                    }


                    _cli.Contato = new List<GlobaisClienteContatoBE>();
                    _cli.Contato.Add(new GlobaisClienteContatoBE
                    {
                         cli_id= _cli.cli_id,
                        cont_nome = cliente["Contato"].ToString() == "" ? cliente["Nome do Cliente"].ToString() : cliente["Contato"].ToString(),
                        cont_departamento = cliente["Contato_Cob"].ToString(),
                        cont_email = cliente["Email"].ToString(),
                        cont_ddd = cliente["DDD"].ToString(),
                        cont_fone = cliente["Telefone"].ToString()


                    });
                    _contato.Insert(_cli.Contato[0]);
                    clientes.Add(_cli);
                }
            }

            cliente.Close();
        }
    }

}
*/