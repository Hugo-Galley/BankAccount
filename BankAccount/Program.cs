using BankAccount; 

class Program  
{
    public static async Task Main()
    {
        ClientGestion gestion = new ClientGestion();
        Menu menu = new Menu();
        bool exit = false;
        int clientId = -1;
        
        while (!exit)
        {
            if (clientId <= 0)
            {
                int choix = menu.DisplayMainMenu();
                switch (choix)
                {
                    case 0:
                        exit = true;
                        Console.WriteLine("Au revoir");
                        break;
                    case 1: 
                        clientId = gestion.ConnectClient();
                        if (clientId <= 0)
                        {
                            Console.WriteLine("Erreur de connexion");
                        }
                        break;
                    case 2:
                        await gestion.CreateClient();
                        break;
                    default:
                        Console.WriteLine("Choix invalide");
                        break;
                }
            }
            else
            {
                int choix = menu.DisplayChoice();
                switch (choix)
{
    case 0:
        Console.WriteLine("Au revoir");
        clientId = -1;
        break;
    case 1:
        await gestion.MoveMoney(clientId, 1);
        break;
    case 2:
        await gestion.MoveMoney(clientId, 2);
        break;
    case 3:
            Console.WriteLine("Fonctionnalité gérée par un autre développeur");

        break;
    case 4:
        gestion.ShowAccounts(clientId);
        break;
    case 5:
        gestion.ShowHistory(clientId);
        break;
    default:
        Console.WriteLine("Choix invalide");
        break;
}
                Console.WriteLine("Appuyer sur une touche pour continuer");
                Console.ReadKey();
            }
        }
    }
}