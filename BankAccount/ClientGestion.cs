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

    public int DepositMoney(int clientId)
    {
        int choice;
        int deposit;
        
        using (var context = new BankAccountContext())
        {
            List<Compte> listComptes = context.Comptes
                .Where(x => x.IdClient == clientId)
                .ToList();
            if (! listComptes.Any())
            {
                Console.WriteLine("Aucun compte trouvé pour se client");
                return 1;
            }
            Console.WriteLine("Sur quelle compte voulez vous effectuer le dépôt ?");
            for (int i = 0; i < listComptes.Count; i++)
            {
                Console.WriteLine($"{i}. Compte {listComptes[i].Type} au solde de {listComptes[i].Solde}");
            }
            while (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Veuillez entrer un nombre valide");
            }
            Console.WriteLine("Combien voulez vous deposer ?");
            while (!int.TryParse(Console.ReadLine(), out deposit))
            {
                Console.WriteLine("Veuillez entrer un nombre valide");
            }
            context.Comptes.Where(x => x.IdCompte == listComptes[choice].IdCompte)
                .FirstOrDefault().Solde += deposit;
            context.SaveChanges();
            Console.WriteLine($"Un depot de {deposit} à bien été effectué sur le compte {listComptes[choice].Rib}");
            return 1;
        }
    }
}