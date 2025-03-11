using System.Security.Cryptography;
using System.Text;
using BankAccount.Models;
using Microsoft.EntityFrameworkCore;

namespace BankAccount;

public class ClientGestion
{
    public async Task CreateClient()
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
            await context.Clients.AddAsync(client);
            await context.SaveChangesAsync();
        }

        await CreateAccount();
    }

    public async Task CreateAccount()
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
            var clientId = context.Clients.OrderByDescending(x => x.IdClient).FirstOrDefaultAsync().Result.IdClient;
            Compte compte = new Compte()
            {
                IdClient = clientId,
                Type = type,
                Solde = solde,
                CreateAt = DateTime.Now,
                Interet = interet,
                Rib = Guid.NewGuid()
            };
            await context.Comptes.AddAsync(compte);
            await context.SaveChangesAsync();
        }
    }

    public async Task<int> MoveMoney(int clientId, int action)
    {
        int choice;
        int deposit;

        using (var context = new BankAccountContext())
        {
            List<Compte> listComptes = context.Comptes
                .Where(x => x.IdClient == clientId)
                .ToList();
            if (!listComptes.Any())
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

            switch (action)
            {
                case 1:
                    context.Comptes.Where(x => x.IdCompte == listComptes[choice].IdCompte)
                        .FirstOrDefault().Solde += deposit;
                    Console.WriteLine(
                        $"Un depot de {deposit} à bien été effectué sur le compte {listComptes[choice].Rib}");
                    break;
                case 2:
                    context.Comptes.Where(x => x.IdCompte == listComptes[choice].IdCompte)
                        .FirstOrDefault().Solde -= deposit;
                    Console.WriteLine(
                        $"Un retrait de {deposit} à bien été effectué sur le compte {listComptes[choice].Rib}");
                    break;
                default:
                    Console.WriteLine("Operation impossible");
                    break;

            }

            context.SaveChanges();
            return 1;
        }
    }

    public int ConnectClient()
    {
        Console.WriteLine("=== Connexion ===");
        Console.WriteLine("Entrer votre mail : ");
        string? mail = Console.ReadLine();
        Console.WriteLine("Entrer votre mot de passe : ");
        string? password = Console.ReadLine();

        string hashedPassword = Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(password)));

        using (var context = new BankAccountContext())
        {
            var client = context.Clients.Where(x => x.Mail == mail && x.Mdp == hashedPassword).FirstOrDefault();
            if (client == null)
            {
                Console.WriteLine("Identifiant ou mot de passe incorrect");
                return 1;
            }

            Console.WriteLine($"Bienvenue {client.Prenom} {client.Nom}");
            return client.IdClient;
        }
        
    }

    public void ShowAccounts(int clientId)
    {
        using (var context = new BankAccountContext())
        {
            var comptes = context.Comptes.Where(x => x.IdClient == clientId).ToList();

            if (comptes.Count == 0)
            {
                Console.WriteLine("Aucun compte trouvé pour ce client");
                return;
            }

            Console.WriteLine("Vos comptes :");
            foreach (var compte in comptes)
            {
                 Console.WriteLine($"- {compte.Type} : {compte.Solde} EUR (RIB: {compte.Rib})");
            }
        }
    }



//     public async Task<int> GetTransfert(int Idreceveur, int idDonneur)
//     {
//         using (var context = new BankAccountContext())
//         {
//             var donneurId = context.Comptes.Where(x => x.IdClient == idDonneur);
//
}