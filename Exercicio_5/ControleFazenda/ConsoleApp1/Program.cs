using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    struct BirthDate
    {
        public int Month;
        public int Year;
    }

    struct Cattle
    {
        public int Code;
        public double MilkProduction; // por semana
        public double FoodConsumption; // por semana
        public BirthDate Birth;
        public char Slaughter; // S/N
    }

    static void GenerateData(List<Cattle> cattleList)
    {
        Random random = new Random();
        int code = 0;

        for (int i = 0; i < 100; i++)
        {
            Cattle cattle = new Cattle();
            cattle.Code = ++code;
            cattle.MilkProduction = random.Next(30, 100);
            cattle.FoodConsumption = random.Next(15, 50);
            cattle.Birth.Month = random.Next(1, 13);
            cattle.Birth.Year = random.Next(2016, 2023);

            if (2023 - cattle.Birth.Year > 5 || cattle.MilkProduction < 40)
            {
                cattle.Slaughter = 'S';
            }
            else
            {
                cattle.Slaughter = 'N';
            }

            cattleList.Add(cattle);
        }
    }

    static void TotalMilkProduction(List<Cattle> cattleList)
    {
        int count = cattleList.Count;
        double weeklyMilkProduction = 0;

        foreach (Cattle cattle in cattleList)
        {
            weeklyMilkProduction += cattle.MilkProduction;
        }

        Console.WriteLine("Total de leite produzido por semana: " + weeklyMilkProduction.ToString("F1") + "L");
    }

    static void TotalFoodConsumption(List<Cattle> cattleList)
    {
        int count = cattleList.Count;
        double totalFoodConsumption = 0;

        foreach (Cattle cattle in cattleList)
        {
            totalFoodConsumption += cattle.FoodConsumption;
        }

        Console.WriteLine("Total de alimento consumido por semana: " + totalFoodConsumption.ToString("F1") + "KG");
    }

    static void ListSlaughterCattle(List<Cattle> cattleList)
    {
        int count = cattleList.Count;

        Console.WriteLine("Animais para abate:");
        foreach (Cattle cattle in cattleList)
        {
            if (cattle.Slaughter == 'S')
            {
                Console.WriteLine($"Código do animal: {cattle.Code}");
            }
        }
    }

    static void SaveData(List<Cattle> cattleList, string fileName)
    {
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            foreach (Cattle cattle in cattleList)
            {
                writer.WriteLine($"{cattle.Code},{cattle.MilkProduction},{cattle.FoodConsumption},{cattle.Birth.Month},{cattle.Birth.Year},{cattle.Slaughter}");
            }
        }

        Console.WriteLine("Dados salvos com sucesso!");
    }

    static void LoadData(List<Cattle> cattleList, string fileName)
    {
        if (File.Exists(fileName))
        {
            cattleList.Clear();

            string[] lines = File.ReadAllLines(fileName);
            foreach (string line in lines)
            {
                string[] fields = line.Split(',');
                Cattle cattle = new Cattle
                {
                    Code = int.Parse(fields[0]),
                    MilkProduction = double.Parse(fields[1]),
                    FoodConsumption = double.Parse(fields[2]),
                    Birth = new BirthDate { Month = int.Parse(fields[3]), Year = int.Parse(fields[4]) },
                    Slaughter = char.Parse(fields[5])
                };

                cattleList.Add(cattle);
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
        Console.WriteLine("Menu Principal");
        Console.WriteLine("1 - Total de leite produzido por semana");
        Console.WriteLine("2 - Total de alimento consumido por semana");
        Console.WriteLine("3 - Listar animais para abate");
        Console.WriteLine("4 - Salvar dados em arquivo");
        Console.WriteLine("5 - Carregar dados do arquivo");
        Console.WriteLine("0 - Sair");
        Console.Write("Digite a opção desejada: ");

        int choice = int.Parse(Console.ReadLine());
        return choice;
    }

    static void Main()
    {
        List<Cattle> cattleList = new List<Cattle>();

        GenerateData(cattleList);

        int choice;

        do
        {
            choice = Menu();

            switch (choice)
            {
                case 1:
                    TotalMilkProduction(cattleList);
                    break;
                case 2:
                    TotalFoodConsumption(cattleList);
                    break;
                case 3:
                    ListSlaughterCattle(cattleList);
                    break;
                case 4:
                    SaveData(cattleList, "data.txt");
                    break;
                case 5:
                    LoadData(cattleList, "data.txt");
                    break;
                case 0:
                    Console.WriteLine("Saindo");
                    break;
                default:
                    Console.WriteLine("ERRO: Opção Inválida");
                    break;
            }

            Console.ReadKey();
            Console.Clear();

        } while (choice != 0);
    }
}
