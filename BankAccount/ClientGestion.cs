using System.Security.Cryptography;
using System.Text;
using BankAccount.Models;

namespace BankAccount;

public class ClientGestion
{
    public void CreateClient()
    {
        Console.WriteLine("Entrer votre prénom :");
        string? name = Console.ReadLine();
        Console.WriteLine("Entrer votre nom :");
        string? lastName = Console.ReadLine();
        Console.WriteLine("Entrer votre mail : ");
        string? mail = Console.ReadLine();
        Console.WriteLine("Entrer votre mot de passe : ");
        string? password = Console.ReadLine();
        Console.WriteLine("Entrer votre numéro de téléphone : ");
        string? phone = Console.ReadLine();
        Client client = new Client()
        {
            Nom = name,
            Prenom = lastName,
            Mail = mail,
            Mdp = Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(password))),
            Tel = phone,
            CreateAt = DateTime.Now
        };
        using (var context = new BankAccountContext())
        {
            context.Clients.Add(client);
            context.SaveChanges();
        }
        CreateAccount();
    }

    public void CreateAccount()
    {
        int choice;
        string type = "Unknow";
        int solde;
        decimal interet = 0.0m;
        
        Console.WriteLine("Quelle type de compte souhaiter vous ?");
        Console.WriteLine("1 - Compte Courant");
        Console.WriteLine("2 - Compte Epargne");

        while (!int.TryParse(Console.ReadLine(), out choice))
        {
            Console.WriteLine("Veuillez entrer un nombre valide");
        }
        switch (choice)
        {
            case 1:
                type = "Courant";
                interet = 0.0m;
                break;
            case 2:
                type = "Epargne";
                interet = 2.2m;
                break;
            default:
                Console.WriteLine("Choix non disponible");
                break;
                
        }
        Console.WriteLine("Entrer le montant du dépôt initial : ");
        while (!int.TryParse(Console.ReadLine(), out solde))
        {
            Console.WriteLine("Veuillez entrer un nombre valide");
        }
        using (var context = new BankAccountContext())
        {
            var clientId = context.Clients.OrderByDescending(x => x.IdClient).FirstOrDefault().IdClient;
        Compte compte = new Compte()
        {
            IdClient = clientId,
            Type = type,
            Solde = solde,
            CreateAt = DateTime.Now,
            Interet = interet,
            Rib = Guid.NewGuid()
        };
        context.Comptes.Add(compte);
        context.SaveChanges();
        }
    }
}