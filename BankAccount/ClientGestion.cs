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
    }
}