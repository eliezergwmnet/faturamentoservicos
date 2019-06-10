using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Globais.BE;
using Globais.Dao.Base;

namespace Globais.Dao
{
    public class ClienteContatoDao : Functions
    {
        public ClienteContatoDao()
        {
            this.procedure = "proc_GlobaisClienteContato";
        }

       

    }
}
