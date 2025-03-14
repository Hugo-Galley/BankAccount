namespace BankAccount;

public class Menu
{
    public int DisplayMainMenu()
    {
        Console.WriteLine("\n==== Bienvenue sur le Bank Account ===");
        Console.WriteLine("1. Se connecter");
        Console.WriteLine("2. Créer un compte");
        Console.WriteLine("0. Quitter");
        Console.WriteLine("Votre choix : ");

        if (int.TryParse(Console.ReadLine(), out int choice))
        {
            return choice;
        }
        Console.WriteLine("Choix invalide, veuillez entrer un nombre. ");
        return -1;
    }
    public int DisplayChoice()
    {
    Console.WriteLine("==== Bienvenue sur le Bank Account ===");
    Console.WriteLine("0. Se déconnecter");
    Console.WriteLine("1. Effectuer un dépôt");
    Console.WriteLine("2. Effectuer un retrait");
    Console.WriteLine("3. Effectuer un virement");
    Console.WriteLine("4. Consulter le solde");
    Console.WriteLine("5. Voir l'historique des opérations");
    Console.Write("Votre choix : ");
        string choice = Console.ReadLine();
        return int.Parse(choice);
    }
}