using System.Collections.Generic;

namespace Carubbi.DiffAnalyzer.MockClasses
{
    /// <summary>
    /// Classe de negócio fictícia para testar o componente
    /// </summary>
    public class UF
    {
        public string Nome
        {
            get;
            set;
        }

        private Cidade[] _cidades;
        public Cidade[] Cidades
        {
            get => _cidades ?? (_cidades = new Cidade[5]);
            set => _cidades = value;
        }
    }

    /// <summary>
    /// Classe de negócio fictícia para testar o componente
    /// </summary>
    public class Cidade
    {
        public string Nome
        {
            get;
            set;
        }
    }

    /// <summary>
    /// Classe de negócio fictícia para testar o componente
    /// </summary>
    public class Endereco
    {
        public string Logradouro
        {
            get;
            set;
        }

        public UF UF
        {
            get;
            set;
        }
    }

    /// <summary>
    /// Classe de negócio fictícia para testar o componente
    /// </summary>
    public class Telefone
    {
        public string Numero
        {
            get;
            set;
        }

        public TipoTelefone Tipo
        {
            get;
            set;
        }

        public string DDD
        {
            get;
            set;
        }
    }

    /// <summary>
    /// Classe de negócio fictícia para testar o componente
    /// </summary>
    public enum TipoTelefone : int
    {
        Residencia,
        Comercial,
        Celular,
        Recado
    }

    /// <summary>
    /// Classe de negócio fictícia para testar o componente
    /// </summary>
    public class Usuario
    {
        // Ignora tanto com comportamento CompareAll como CompareMark
        [DiffAnalyzable(DiffAnalyzableUsage.Ignore)]
        public int Id
        {
            get;
            set;
        }

        // Compara em ambos os comportamentos
        [DiffAnalyzable]
        public string Nome
        {
            get;
            set;
        }

        private List<string> _apelidos;

        // propriedade sem atributo compara apenas no comportamento CompareAll
        public List<string> Apelidos
        {
            get => _apelidos ?? (_apelidos = new List<string>());
            set => _apelidos = value;
        }

        private List<Telefone> _telefones;

        [DiffAnalyzable]
        public List<Telefone> Telefones
        {
            get => _telefones ?? (_telefones = new List<Telefone>());
            set => _telefones = value;
        }

        private List<Endereco> _enderecos;

        public List<Endereco> Enderecos
        {
            get => _enderecos ?? (_enderecos = new List<Endereco>());
            set => _enderecos = value;
        }
    }
}
