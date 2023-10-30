using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    struct Appliance
    {
        public string Name;
        public double Power;
        public double AverageActiveTime;
    }

    static void AddAppliance(List<Appliance> applianceList)
    {
        Appliance appliance = new Appliance();
        Console.WriteLine("Digite o nome do eletrodoméstico: ");
        appliance.Name = Console.ReadLine();
        Console.WriteLine("Digite a potência (em watts): ");
        appliance.Power = double.Parse(Console.ReadLine());
        Console.WriteLine("Digite o tempo médio de uso por dia (em horas): ");
        appliance.AverageActiveTime = double.Parse(Console.ReadLine());
        applianceList.Add(appliance);
    }

    static void ListAppliances(List<Appliance> applianceList)
    {
        int count = applianceList.Count;
        Console.WriteLine("Lista de Eletrodomésticos: ");
        for (int i = 0; i < count; i++)
        {
            Console.WriteLine($"Nome do Eletrodoméstico: {applianceList[i].Name}");
            Console.WriteLine($"Potência (Watts): {applianceList[i].Power}");
            Console.WriteLine($"Tempo Médio de Uso por Dia (horas): {applianceList[i].AverageActiveTime}\n");
        }
    }

    static void SearchByName(List<Appliance> applianceList, string name)
    {
        int count = applianceList.Count;

        for (int i = 0; i < count; i++)
        {
            if (applianceList[i].Name.ToUpper().Equals(name.ToUpper()))
            {
                Console.WriteLine("Detalhes do Eletrodoméstico: ");
                Console.WriteLine($"Nome do Eletrodoméstico: {applianceList[i].Name}");
                Console.WriteLine($"Potência (Watts): {applianceList[i].Power}");
                Console.WriteLine($"Tempo Médio de Uso por Dia (horas): {applianceList[i].AverageActiveTime}\n");
            }
        }
    }

    static void FindPowerConsumptionGreaterThan(List<Appliance> applianceList, double value)
    {
        int count = applianceList.Count;
        for (int i = 0; i < count; i++)
        {
            if (applianceList[i].Power > value)
            {
                Console.WriteLine("Detalhes do Eletrodoméstico: ");
                Console.WriteLine("Nome: " + applianceList[i].Name);
                Console.WriteLine("Potência (Watts): " + applianceList[i].Power);
                Console.WriteLine("Tempo Médio de Uso por Dia (horas): " + applianceList[i].AverageActiveTime);
            }
        }
    }

    static void CalculatePowerConsumption(List<Appliance> applianceList, string nameToCalculate, double costPerKwH)
    {
        int count = applianceList.Count;
        for (int i = 0; i < count; i++)
        {
            if (applianceList[i].Name.ToUpper().Equals(nameToCalculate.ToUpper()))
            {
                double dailyConsumption = applianceList[i].AverageActiveTime * applianceList[i].Power / 1000; // Converting Watts to Kilowatts
                double monthlyConsumption = dailyConsumption * 30;
                double dailyCost = dailyConsumption * costPerKwH;
                double monthlyCost = monthlyConsumption * costPerKwH;

                Console.WriteLine($"Consumo Diário: {Math.Round(dailyConsumption, 2)} kWh, Mensal: {Math.Round(monthlyConsumption, 2)} kWh");
                Console.WriteLine($"Custo Diário: R${Math.Round(dailyCost, 2)}, Mensal: R${Math.Round(monthlyCost, 2)}");
            }
        }
    }

    static void SaveData(List<Appliance> applianceList, string fileName)
    {
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            foreach (Appliance appliance in applianceList)
            {
                writer.WriteLine($"{appliance.Name},{appliance.Power},{appliance.AverageActiveTime}");
            }
        }
        Console.WriteLine("Dados salvos com sucesso!");
    }

    static void LoadData(List<Appliance> applianceList, string fileName)
    {
        if (File.Exists(fileName))
        {
            string[] lines = File.ReadAllLines(fileName);
            foreach (string line in lines)
            {
                string[] fields = line.Split(',');
                Appliance appliance = new Appliance
                {
                    Name = fields[0],
                    Power = double.Parse(fields[1]),
                    AverageActiveTime = double.Parse(fields[2]),
                };
                applianceList.Add(appliance);
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
        Console.WriteLine("1 - Adicionar Eletrodoméstico");
        Console.WriteLine("2 - Listar Eletrodomésticos");
        Console.WriteLine("3 - Buscar pelo Nome");
        Console.WriteLine("4 - Buscar por gasto maior que determinado valor");
        Console.WriteLine("5 - Calcular Consumo Diário e Mensal");
        Console.WriteLine("0 - Sair");
        int choice = int.Parse(Console.ReadLine());
        return choice;
    }

    static void Main()
    {
        List<Appliance> applianceList = new List<Appliance>();

        int choice;

        do
        {
            choice = Menu();

            switch (choice)
            {
                case 1:
                    AddAppliance(applianceList);
                    break;
                case 2:
                    ListAppliances(applianceList);
                    break;
                case 3:
                    Console.WriteLine("Digite o nome que deseja procurar: ");
                    string nameToSearch = Console.ReadLine();
                    SearchByName(applianceList, nameToSearch);
                    break;
                case 4:
                    Console.WriteLine("Digite o valor da potência: ");
                    double powerValue = double.Parse(Console.ReadLine());
                    FindPowerConsumptionGreaterThan(applianceList, powerValue);
                    break;
                case 5:
                    Console.WriteLine("Digite o nome do eletrodoméstico que deseja calcular: ");
                    string nameToCalculate = Console.ReadLine();
                    Console.WriteLine("Digite o valor do KW/h: ");
                    double costPerKwH = double.Parse(Console.ReadLine());
                    CalculatePowerConsumption(applianceList, nameToCalculate, costPerKwH);
                    break;
                case 0:
                    Console.WriteLine("Saindo");
                    SaveData(applianceList, "dados.txt");
                    break;
                default:
                    Console.WriteLine("ERRO: Opção Inválida.");
                    break;
            }
            Console.ReadKey();
            Console.Clear();
        } while (choice != 0);
    }
}
