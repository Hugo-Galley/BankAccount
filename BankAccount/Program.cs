using BankAccount;

class Program
{
    public static void Main()
    {
        Menu menu = new Menu();
        ClientGestion gestionClient = new ClientGestion();
        int choice = menu.DisplayChoice();
        switch (choice)
        {
            case 1:
                gestionClient.CreateClient();
                break;
            case 2:
                Console.WriteLine("Ya R");
                break;
            default:
                Console.WriteLine("y'a rien");
                break;
                
        }
        
    }
}