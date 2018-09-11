# Carubbi.DiffAnalyzer
Difference Analyzer between two instances

> It's a component able to compare two instances of the same type and by reflection show the differences. Has a Asp.net webforms colorful panel to show these differences and highly configurable. You can set the depth of the object or to return specifc status of comparison like added, deleted, modified or unmodified.

> Example:

With this two instances: 

```csharp
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
```

Just call:

```csharp
    var analyzer = new DiffAnalyzer();
    var results = analyzer.Compare(before, after);
```
