using Carubbi.DiffAnalyzer.MockClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Carubbi.DiffAnalyzer.Tests
{
    [TestClass]
    public class UnitTest1
    {
       
        [TestMethod]
        public void TestMethod1()
        {

            var before = new Usuario
            {
                Id = 1,
                Nome = "Michael",
                Apelidos = new List<string>
                {
                    "Mike",
                    "MK"
                },
                Enderecos = new List<Endereco>
                {
                    new Endereco()
                    {
                        Logradouro = "1st street",
                        UF = new UF()
                        {
                            Nome = "SP",
                            Cidades = new[]
                            {
                                new Cidade
                                {
                                    Nome = "Sao Paulo"
                                },
                                new Cidade
                                {
                                    Nome =  "Santos"
                                }
                            },
                        },
                    },
                },
                Telefones =  new List<Telefone>
                {
                    new Telefone()
                    {
                        DDD = "11",
                        Numero = "91234-5678",
                        Tipo = TipoTelefone.Celular
                    }
                }
      
            };

            var after = new Usuario
            {
                Id = 1,
                Nome = "Michael",
                Apelidos = new List<string>
                {
                    "None",
                },
                Telefones = new List<Telefone>
                {
                    new Telefone()
                    {
                        DDD = "11",
                        Numero = "1234-5678",
                        Tipo = TipoTelefone.Comercial
                    }
                }

            };


            var analyzer = new DiffAnalyzer();
            var results = analyzer.Compare(before, after);
        }
    }
}
