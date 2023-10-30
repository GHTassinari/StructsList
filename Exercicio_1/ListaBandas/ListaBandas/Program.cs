using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    struct Banda
    {
        public string nome;
        public string genero;
        public int integrantes;
        public int ranking;
    }

    static void AdicionarBanda(List<Banda> lista)
    {
        Banda banda = new Banda();
        Console.WriteLine("Digite o nome da Banda: ");
        banda.nome = Console.ReadLine();
        Console.WriteLine("Digite o gênero musical: ");
        banda.genero = Console.ReadLine();
        Console.WriteLine("Digite a quantidade de integrantes: ");
        banda.integrantes = int.Parse(Console.ReadLine());
        Console.WriteLine("Digite a posição no Ranking: ");
        banda.ranking = int.Parse(Console.ReadLine());
        lista.Add(banda);
    }

    static void ListarBandas(List<Banda> lista)
    {
        int qtd = lista.Count();

        Console.WriteLine("Listagem de Bandas:");

        for (int i = 0; i < qtd; i++)
        {
            Console.WriteLine("Nome da Banda: " + lista[i].nome);
            Console.WriteLine("Gênero Musical: " + lista[i].genero);
            Console.WriteLine("Quantidade de Integrantes: " + lista[i].integrantes);
            Console.WriteLine("Posição no Ranking: " + lista[i].ranking);
        }
    }

    static void BuscarPorRanking(List<Banda> lista, int posicao)
    {
        int qtd = lista.Count();
        bool flag = false;
        for (int i = 0; i < qtd; i++)
        {
            if (lista[i].ranking == posicao)
            {
                flag = true;
                Console.WriteLine("Nome da Banda: " + lista[i].nome);
                Console.WriteLine("Gênero Musical: " + lista[i].genero);
                Console.WriteLine("Quantidade de Integrantes: " + lista[i].integrantes);
                Console.WriteLine("Posição no Ranking: " + lista[i].ranking);
            }
        }
        if (!flag)
        {
            Console.WriteLine($"Banda na posição {posicao} não encontrada.");
        }
    }

    static void BuscarPorGenero(List<Banda> lista, string genero)
    {
        int qtd = lista.Count();
        bool flag = false;
        for (int i = 0; i < qtd; i++)
        {
            if (lista[i].genero.ToUpper().Equals(genero.ToUpper()))
            {
                flag = true;
                Console.WriteLine("Nome da Banda: " + lista[i].nome);
                Console.WriteLine("Gênero Musical: " + lista[i].genero);
                Console.WriteLine("Quantidade de Integrantes: " + lista[i].integrantes);
                Console.WriteLine("Posição no Ranking: " + lista[i].ranking);
            }
        }
        if (!flag)
        {
            Console.WriteLine($"Nenhuma banda do gênero {genero} encontrada.");
        }
    }

    static void BuscarPorNome(List<Banda> lista, string nome)
    {
        int qtd = lista.Count();
        bool flag = false;
        for (int i = 0; i < qtd; i++)
        {
            if (lista[i].nome.ToUpper().Equals(nome.ToUpper()))
            {
                flag = true;
                Console.WriteLine("Nome da Banda: " + lista[i].nome);
                Console.WriteLine("Gênero Musical: " + lista[i].genero);
                Console.WriteLine("Quantidade de Integrantes: " + lista[i].integrantes);
                Console.WriteLine("Posição no Ranking: " + lista[i].ranking);
            }
        }
        if (!flag)
        {
            Console.WriteLine($"Banda com nome {nome} não encontrada.");
        }
    }

    static void ExcluirBanda(List<Banda> lista, string nome)
    {
        int qtd = lista.Count();
        for (int i = 0; i < qtd; i++)
        {
            if (lista[i].nome.ToUpper().Equals(nome.ToUpper()))
            {
                Console.WriteLine("Tem certeza que deseja excluir a banda? [S/N]");
                char resp = char.Parse(Console.ReadLine());

                if (resp == 'S' || resp == 's')
                {
                    lista.RemoveAt(i);
                    Console.WriteLine("Banda removida com sucesso.");
                }
                else if (resp == 'N' || resp == 'n')
                {
                    Console.WriteLine("Operação cancelada.");
                }
                else
                {
                    Console.WriteLine("Entrada inválida.");
                }
            }
        }
    }

    static void AlterarBanda(List<Banda> lista, string nome)
    {
        int qtd = lista.Count();
        bool flag = false;
        for (int i = 0; i < qtd; i++)
        {
            if (lista[i].nome.ToUpper().Equals(nome.ToUpper()))
            {
                flag = true;
                Banda banda = new Banda();
                Console.WriteLine("Digite o novo nome da Banda: ");
                banda.nome = Console.ReadLine();
                Console.WriteLine("Digite o gênero musical: ");
                banda.genero = Console.ReadLine();
                Console.WriteLine("Digite a quantidade de integrantes: ");
                banda.integrantes = int.Parse(Console.ReadLine());
                Console.WriteLine("Digite a posição no Ranking: ");
                banda.ranking = int.Parse(Console.ReadLine());
                lista[i] = banda;
            }
        }
        if (!flag)
        {
            Console.WriteLine($"Banda com nome {nome} não encontrada.");
        }
    }

    static void SalvarDados(List<Banda> bandas, string nomeArquivo)
    {
        using (StreamWriter writer = new StreamWriter(nomeArquivo))
        {
            foreach (Banda banda in bandas)
            {
                writer.WriteLine($"{banda.nome},{banda.genero},{banda.integrantes},{banda.ranking}");
            }
        }
        Console.WriteLine("Dados salvos com sucesso!");
    }

    static void CarregarDados(List<Banda> bandas, string nomeArquivo)
    {
        if (File.Exists(nomeArquivo))
        {
            string[] linhas = File.ReadAllLines(nomeArquivo);
            foreach (string linha in linhas)
            {
                string[] campos = linha.Split(',');
                Banda banda = new Banda
                {
                    nome = campos[0],
                    genero = campos[1],
                    integrantes = int.Parse(campos[2]),
                    ranking = int.Parse(campos[3])
                };
                bandas.Add(banda);
            }
            Console.WriteLine("Dados carregados com sucesso!");
        }
        else
        {
            Console.WriteLine("Arquivo não encontrado :(");
        }
    }

    static int Menu()
    {
        Console.WriteLine("Menu Principal:");
        Console.WriteLine("1 - Adicionar Banda");
        Console.WriteLine("2 - Listar Bandas");
        Console.WriteLine("3 - Buscar por Posição no Ranking");
        Console.WriteLine("4 - Buscar por Gênero");
        Console.WriteLine("5 - Buscar por Nome da Banda");
        Console.WriteLine("6 - Excluir Banda");
        Console.WriteLine("7 - Alterar Banda");
        Console.WriteLine("0 - Sair");
        Console.WriteLine();
        Console.WriteLine("Digite a opção desejada: ");
        int op = int.Parse(Console.ReadLine());
        return op;
    }

    static void Main()
    {
        List<Banda> listaBandas = new List<Banda>();

        int escolha = 0;

        do
        {
            escolha = Menu();
            switch (escolha)
            {
                case 1:
                    AdicionarBanda(listaBandas);
                    break;
                case 2:
                    ListarBandas(listaBandas);
                    break;
                case 3:
                    Console.WriteLine("Digite a posição no Ranking: ");
                    int posicao = int.Parse(Console.ReadLine());
                    BuscarPorRanking(listaBandas, posicao);
                    break;
                case 4:
                    Console.WriteLine("Digite o Gênero: ");
                    string genero = Console.ReadLine();
                    BuscarPorGenero(listaBandas, genero);
                    break;
                case 5:
                    Console.WriteLine("Digite o Nome da Banda: ");
                    string nome = Console.ReadLine();
                    BuscarPorNome(listaBandas, nome);
                    break;
                case 6:
                    Console.WriteLine("Digite o Nome da Banda: ");
                    nome = Console.ReadLine();
                    ExcluirBanda(listaBandas, nome);
                    break;
                case 7:
                    Console.WriteLine("Digite o Nome da Banda: ");
                    nome = Console.ReadLine();
                    AlterarBanda(listaBandas, nome);
                    break;
                case 0:
                    SalvarDados(listaBandas, "dados.txt");
                    break;
                default:
                    Console.WriteLine("Opção Inválida.");
                    break;
            }
            Console.ReadKey();
            Console.Clear();
        } while (escolha != 0);
    }
}
