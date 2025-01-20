//// See https://aka.ms/new-console-template for more information
using System.Linq;
using System.Numerics;

List<BasketballPlayer> players = new List<BasketballPlayer>();

bool running = true;
while (running)
{
    Console.WriteLine("Odaberite opciju:");
    Console.WriteLine("1. Vidi sve igrače");
    Console.WriteLine("2. Dodaj novog igrača");
    Console.WriteLine("3. Izlaz");
    Console.WriteLine("4. Uredi broj poena");
    Console.Write("Vaš odabir: ");

    string choice = Console.ReadLine() ;

    switch (choice)
    {
        case "1":
            // Prikaz svih igrača
            if (players.Count == 0)
            {
                Console.WriteLine("\nNema igrača u sustavu.\n");
            }
            else
            {
                Console.WriteLine("\nPopis igrača:");
                foreach (var player in players)
                {
                    Console.WriteLine($"{player.FirstName}, {player.PointsPerGame}, {player.Team}, {player.ID}");
                }
                Console.WriteLine();
            }
            break;

        case "2":
           
            Console.Write("Unesite id igrača: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Unesite ime igrača: ");
            string name = Console.ReadLine() ?? "Nepoznato";

            Console.Write("Unesite tim igrača: ");
            string team = Console.ReadLine() ?? "N/A";


            Console.Write("Unesite broj poena po utakmici: ");
            float points = float.Parse(Console.ReadLine() ?? "0");

            BasketballPlayer novi = new BasketballPlayer(id, name, points, team){PointsPerGame = points};
            players.Add(novi);
            
            Console.WriteLine("\nIgrač je uspješno dodan!");
            
            Console.WriteLine(novi.DisplayData());
            break;

        case "3":
            running = false;
            break;

        case "4":
            Console.WriteLine("Unesite ID igrača kojem želite ažurirati broj poena:");
            id = int.Parse(Console.ReadLine());

            Console.Write("Unesite broj poena po utakmici: ");
            points = float.Parse(Console.ReadLine() ?? "0"); //ovdje provjeri
            BasketballPlayer? currentPlayer = players.FirstOrDefault(p => p.ID == id);

            if (currentPlayer == null)
            {
                Console.WriteLine("Error");
                break;
            }
            //BasketballPlayer? currentPlayer = players.FirstOrDefault(p => p.ID == id);
            currentPlayer.PointsPerGame = points;


            Console.WriteLine(currentPlayer.DisplayData());



            break;



    }
}



Console.WriteLine("Hello, World!");

Console.ReadLine();