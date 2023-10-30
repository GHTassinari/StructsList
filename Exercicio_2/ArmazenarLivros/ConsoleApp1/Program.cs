using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    struct BookData
    {
        public string title;
        public string author;
        public int year;
        public int shelf;
    }

    static void AddBook(List<BookData> list)
    {
        BookData bookData = new BookData();
        Console.WriteLine("Digite o Título do Livro: ");
        bookData.title = Console.ReadLine();
        Console.WriteLine("Digite o Nome do Autor: ");
        bookData.author = Console.ReadLine();
        Console.WriteLine("Digite o Ano do Livro: ");
        bookData.year = int.Parse(Console.ReadLine());
        Console.WriteLine("Digite a Prateleira do Livro: ");
        bookData.shelf = int.Parse(Console.ReadLine());
        list.Add(bookData);
    }

    static void SearchByTitle(List<BookData> list, string title)
    {
        int count = list.Count();
        bool found = false;

        for (int i = 0; i < count; i++)
        {
            if (list[i].title.ToUpper().Contains(title.ToUpper()))
            {
                found = true;
                Console.WriteLine($"Título do Livro: {list[i].title}");
                Console.WriteLine($"Prateleira do Livro: {list[i].shelf}");
            }
        }

        if (!found)
        {
            Console.WriteLine("Livro não encontrado.");
        }
    }

    static void ListBooks(List<BookData> list)
    {
        int count = list.Count();

        for (int i = 0; i < count; i++)
        {
            Console.WriteLine($"Título do Livro: {list[i].title}");
            Console.WriteLine($"Autor do Livro: {list[i].author}");
            Console.WriteLine($"Ano do Livro: {list[i].year}");
            Console.WriteLine($"Prateleira do Livro: {list[i].shelf}");
            Console.WriteLine("\n");
        }
    }

    static void ListNewerBooks(List<BookData> list, int year)
    {
        int count = list.Count();
        Console.WriteLine($"Livros mais novos que {year}: ");

        for (int i = 0; i < count; i++)
        {
            if (list[i].year > year)
            {
                Console.WriteLine($"Título do Livro: {list[i].title}");
                Console.WriteLine($"Autor do Livro: {list[i].author}");
                Console.WriteLine($"Ano do Livro: {list[i].year}");
                Console.WriteLine($"Prateleira do Livro: {list[i].shelf}");
                Console.WriteLine("\n");
            }
        }
    }

    static int Menu()
    {
        Console.WriteLine("**Menu**");
        Console.WriteLine("1 - Adicionar Livro");
        Console.WriteLine("2 - Buscar Livro por Título");
        Console.WriteLine("3 - Listar Livros");
        Console.WriteLine("4 - Listar Livros mais Novos");
        Console.WriteLine("0 - Sair");
        int choice = int.Parse(Console.ReadLine());
        return choice;
    }

    static void SaveData(List<BookData> list, string fileName)
    {
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            foreach (BookData book in list)
            {
                writer.WriteLine($"{book.title},{book.author},{book.year},{book.shelf}");
            }
        }
        Console.WriteLine("Dados salvos com sucesso!");
    }

    static void LoadData(List<BookData> list, string fileName)
    {
        if (File.Exists(fileName))
        {
            string[] lines = File.ReadAllLines(fileName);
            foreach (string line in lines)
            {
                string[] fields = line.Split(',');
                BookData book = new BookData
                {
                    title = fields[0],
                    author = fields[1],
                    year = int.Parse(fields[2]),
                    shelf = int.Parse(fields[3])
                };
                list.Add(book);
            }
            Console.WriteLine("Dados carregados com sucesso!");
        }
        else
        {
            Console.WriteLine("Arquivo não encontrado :(");
        }
    }

    static void Main()
    {
        List<BookData> bookList = new List<BookData>();
        int choice;

        do
        {
            choice = Menu();

            switch (choice)
            {
                case 1:
                    AddBook(bookList);
                    break;
                case 2:
                    Console.WriteLine("Digite o Título do Livro que deseja buscar: ");
                    string title = Console.ReadLine();
                    SearchByTitle(bookList, title);
                    break;
                case 3:
                    ListBooks(bookList);
                    break;
                case 4:
                    Console.WriteLine("Digite o ano para filtrar: ");
                    int year = int.Parse(Console.ReadLine());
                    ListNewerBooks(bookList, year);
                    break;
                case 0:
                    SaveData(bookList, "data.txt");
                    break;
                default:
                    break;
            }
            Console.ReadKey();
            Console.Clear();
        } while (choice != 0);
    }
}
