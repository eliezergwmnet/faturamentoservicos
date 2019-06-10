using System.Configuration;

namespace HelpersSistema.Models.Base
{
    public static class Base
    {
        private static string _ConnectionString;
        private static string _Owner;

        private static string _Select;
        private static string _SelectId;
        private static string _SelectName;
        private static string _Insert;
        private static string _Update;
        private static string _Delete;
        private static string _NomeEntidade;
        private static string _NomeDAO;
        private static string _NomeBO;
        private static string _NomeController;
        private static string _Table;
        private static bool _ExecutaProc;
        private static string _EnderecoArquivos;


        public static string ConnectionString
        {
            get
            {
                return _ConnectionString;
            }
            set
            {
                _ConnectionString = value;
            }
        }
        public static string Owner
        {
            get
            {
                return _Owner;
            }
            set
            {
                _Owner = value;
            }
        }

        public static string Select
        {
            get
            {
                return _Select;
            }
            set
            {
                _Select = value;
            }
        }
        public static string SelectId
        {
            get
            {
                return _SelectId;
            }
            set
            {
                _SelectId = value;
            }
        }
        public static string SelectName
        {
            get
            {
                return _SelectName;
            }
            set
            {
                _SelectName = value;
            }
        }
        public static string Insert
        {
            get
            {
                return _Insert;
            }
            set
            {
                _Insert = value;
            }
        }
        public static string Update
        {
            get
            {
                return _Update;
            }
            set
            {
                _Update = value;
            }
        }
        public static string Delete
        {
            get
            {
                return _Delete;
            }
            set
            {
                _Delete = value;
            }
        }
        public static string NomeEntidade
        {
            get
            {
                return _NomeEntidade;
            }
            set
            {
                _NomeEntidade = value;
            }
        }
        public static string NomeDAO
        {
            get
            {
                return _NomeDAO;
            }
            set
            {
                _NomeDAO = value;
            }
        }
        public static string NomeBO
        {
            get
            {
                return _NomeBO;
            }
            set
            {
                _NomeBO = value;
            }
        }
        public static string NomeController
        {
            get
            {
                return _NomeController;
            }
            set
            {
                _NomeController = value;
            }
        }
        public static string Table
        {
            get
            {
                return _Table;
            }
            set
            {
                _Table = value;
            }
        }
        public static bool ExecutaProc
        {
            get
            {
                return _ExecutaProc;
            }
            set
            {
                _ExecutaProc = value;
            }
        }
        public static string EnderecoArquivos
        {
            get
            {
                return _EnderecoArquivos;
            }
            set
            {
                _EnderecoArquivos = value;
            }
        }
    }
}
