using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    struct Emprestimo
    {
        public DateTime Data;
        public string NomePessoa;
        public char Emprestado;
    }

    struct Jogo
    {
        public string Titulo;
        public string Console;
        public int Ano;
        public int Ranking;
        public Emprestimo InfoEmprestimo;
    }

    static void AdicionarJogo(List<Jogo> lista)
    {
        Jogo novoJogo = new Jogo();
        Console.WriteLine("Digite o Título do Jogo: ");
        novoJogo.Titulo = Console.ReadLine();
        Console.WriteLine("Digite o Modelo de Console: ");
        novoJogo.Console = Console.ReadLine();
        Console.WriteLine("Digite o Ano do Jogo: ");
        novoJogo.Ano = int.Parse(Console.ReadLine());
        Console.WriteLine("Digite o Ranking do Jogo: ");
        novoJogo.Ranking = int.Parse(Console.ReadLine());
        novoJogo.InfoEmprestimo.Emprestado = 'N';
        lista.Add(novoJogo);
    }

    static void ListarJogos(List<Jogo> lista)
    {
        Console.WriteLine("Lista de Jogos: ");
        foreach (var jogo in lista)
        {
            Console.WriteLine($"Título do Jogo: {jogo.Titulo}");
            Console.WriteLine($"Modelo de Console: {jogo.Console}");
            Console.WriteLine($"Ano do Jogo: {jogo.Ano}");
            Console.WriteLine($"Ranking do Jogo: {jogo.Ranking}");
            Console.WriteLine($"Emprestado: {jogo.InfoEmprestimo.Emprestado}");
            if (jogo.InfoEmprestimo.Emprestado == 'S')
            {
                Console.WriteLine($"Data de Empréstimo: {jogo.InfoEmprestimo.Data}");
                Console.WriteLine($"Nome da Pessoa: {jogo.InfoEmprestimo.NomePessoa}");
            }
        }
    }

    static void BuscarPorTitulo(List<Jogo> lista, string titulo)
    {
        var jogosEncontrados = lista.Where(j => j.Titulo.ToUpper().Equals(titulo.ToUpper())).ToList();
        if (jogosEncontrados.Count == 0)
        {
            Console.WriteLine("Nenhum jogo encontrado com o título especificado.");
        }
        else
        {
            Console.WriteLine("Jogo(s) encontrado(s): ");
            foreach (var jogo in jogosEncontrados)
            {
                Console.WriteLine($"Título do Jogo: {jogo.Titulo}");
                Console.WriteLine($"Modelo de Console: {jogo.Console}");
                Console.WriteLine($"Ano do Jogo: {jogo.Ano}");
                Console.WriteLine($"Ranking do Jogo: {jogo.Ranking}");
                Console.WriteLine($"Emprestado: {jogo.InfoEmprestimo.Emprestado}");
                if (jogo.InfoEmprestimo.Emprestado == 'S')
                {
                    Console.WriteLine($"Data de Empréstimo: {jogo.InfoEmprestimo.Data}");
                    Console.WriteLine($"Nome da Pessoa: {jogo.InfoEmprestimo.NomePessoa}");
                }
            }
        }
    }
    static void RealizarEmprestimo(List<Jogo> lista, string titulo)
    {
        var jogo = lista.FirstOrDefault(j => j.Titulo.ToUpper().Equals(titulo.ToUpper()));
        if (jogo.Equals(default(Jogo))) // Verifica se o jogo é o valor padrão (nulo)
        {
            Console.WriteLine("Jogo não encontrado.");
        }
        else
        {
            if (jogo.InfoEmprestimo.Emprestado == 'S')
            {
                Console.WriteLine("Jogo já está emprestado!");
            }
            else
            {
                jogo.InfoEmprestimo.Data = DateTime.Now;
                Console.WriteLine("Digite o nome da pessoa que está pegando o jogo:");
                jogo.InfoEmprestimo.NomePessoa = Console.ReadLine();
                jogo.InfoEmprestimo.Emprestado = 'S';
                Console.WriteLine("Empréstimo realizado com sucesso!");
            }
        }
    }

    static void DevolverJogo(List<Jogo> lista, string titulo)
    {
        var jogo = lista.FirstOrDefault(j => j.Titulo.ToUpper().Equals(titulo.ToUpper()));
        if (jogo.Equals(default(Jogo))) // Verifica se o jogo é o valor padrão (nulo)
        {
            Console.WriteLine("Jogo não encontrado.");
        }
        else
        {
            if (jogo.InfoEmprestimo.Emprestado == 'N')
            {
                Console.WriteLine("Jogo não está emprestado!");
            }
            else
            {
                jogo.InfoEmprestimo.Data = DateTime.MinValue;
                jogo.InfoEmprestimo.NomePessoa = "";
                jogo.InfoEmprestimo.Emprestado = 'N';
                Console.WriteLine("Devolução realizada com sucesso!");
            }
        }
    }


    static void ListarJogosEmprestados(List<Jogo> lista)
    {
        var jogosEmprestados = lista.Where(j => j.InfoEmprestimo.Emprestado == 'S').ToList();
        if (jogosEmprestados.Count == 0)
        {
            Console.WriteLine("Nenhum jogo emprestado no momento.");
        }
        else
        {
            Console.WriteLine("Jogos emprestados: ");
            foreach (var jogo in jogosEmprestados)
            {
                Console.WriteLine($"Título do Jogo: {jogo.Titulo}");
                Console.WriteLine($"Modelo de Console: {jogo.Console}");
                Console.WriteLine($"Ano do Jogo: {jogo.Ano}");
                Console.WriteLine($"Ranking do Jogo: {jogo.Ranking}");
                Console.WriteLine($"Data de Empréstimo: {jogo.InfoEmprestimo.Data}");
                Console.WriteLine($"Nome da Pessoa: {jogo.InfoEmprestimo.NomePessoa}");
            }
        }
    }

    static void SalvarDados(List<Jogo> lista, string nomeArquivo)
    {
        using (StreamWriter writer = new StreamWriter(nomeArquivo))
        {
            foreach (var jogo in lista)
            {
                writer.WriteLine($"{jogo.Titulo},{jogo.Console},{jogo.Ano},{jogo.Ranking},{jogo.InfoEmprestimo.Data},{jogo.InfoEmprestimo.NomePessoa},{jogo.InfoEmprestimo.Emprestado}");
            }
        }
        Console.WriteLine("Dados salvos com sucesso!");
    }

    static int Menu()
    {
        Console.WriteLine("Menu Principal");
        Console.WriteLine("1 - Adicionar Jogo");
        Console.WriteLine("2 - Listar Jogos");
        Console.WriteLine("3 - Buscar por Título");
        Console.WriteLine("4 - Realizar Empréstimo");
        Console.WriteLine("5 - Devolver Jogo Emprestado");
        Console.WriteLine("6 - Listar Jogos Emprestados");
        Console.WriteLine("0 - Sair");
        int opcao = int.Parse(Console.ReadLine());
        return opcao;
    }

    static void Main()
    {
        List<Jogo> listaJogos = new List<Jogo>();
        int opcao;

        do
        {
            opcao = Menu();

            switch (opcao)
            {
                case 1:
                    AdicionarJogo(listaJogos);
                    break;
                case 2:
                    ListarJogos(listaJogos);
                    break;
                case 3:
                    Console.WriteLine("Digite o Título do jogo que deseja procurar: ");
                    string tituloBusca = Console.ReadLine();
                    BuscarPorTitulo(listaJogos, tituloBusca);
                    break;
                case 4:
                    Console.WriteLine("Digite o Título do Jogo: ");
                    string titulo = Console.ReadLine();
                    RealizarEmprestimo(listaJogos, titulo);
                    break;
                case 5:
                    Console.WriteLine("Digite o Título do Jogo: ");
                    titulo = Console.ReadLine();
                    DevolverJogo(listaJogos, titulo);
                    break;
                case 6:
                    ListarJogosEmprestados(listaJogos);
                    break;
                case 0:
                    Console.WriteLine("Saindo");
                    SalvarDados(listaJogos, "dados.txt");
                    break;
                default:
                    Console.WriteLine("ERRO: Opção Inválida.");
                    break;
            }

            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        } while (opcao != 0);
    }
}
