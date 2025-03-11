using BankAccount;
class Program
{
    public static async Task Main()
    {
        Menu menu = new Menu();
        ClientGestion gestionClient = new ClientGestion();
        int choice = menu.DisplayChoice();
        switch (choice)
        {
            case 1:
                await gestionClient.MoveMoney(4,1);
                break;
            case 2:
                await gestionClient.MoveMoney(4,1);
                break;
            default:
                Console.WriteLine("y'a rien");
                break;
                
        }
        
    }
}

