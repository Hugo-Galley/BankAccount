namespace BankAccount;

public class Menu
{
    public int DisplayChoice()
    {
        Console.WriteLine("==== Bienvenue sur le Bank Account ===");
        Console.WriteLine("1. Effectuer un dépôt");
        Console.WriteLine("2. Effectuer un retrait");
        Console.WriteLine("3. Effectuer un virement");
        Console.WriteLine("4. Consulter le solde");
        string choice = Console.ReadLine();
        return int.Parse(choice);
    }
}