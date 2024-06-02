using System;

namespace AddressBook
{
  class Program
  {
    private static List<Contact> contacts = new();
    private static string filePath = "contacts.txt";

    static void Main(string[] args)
    {
      LoadContacts();
      ShowMenu();
    }

    static void ShowMenu()
    {
      while (true)
      {
        Console.Clear();
        Console.WriteLine("|------------------------------------|");
        Console.WriteLine("| Address Book v1");
        Console.WriteLine("|------------------------------------|");
        Console.WriteLine("| Select a option: ");
        Console.WriteLine("| 1. Add contact");
        Console.WriteLine("| 2. Edit contact");
        Console.WriteLine("| 3. Delete contact");
        Console.WriteLine("| 4. Search contact");
        Console.WriteLine("| 5. View contacts");
        Console.WriteLine("| 6. Save contacts and exit");
        Console.WriteLine("|------------------------------------|");
        Console.Write("| Your option: ");

        string? userOption = Console.ReadLine();

        switch (userOption)
        {
          case "1":
            AddContact();
            Pause();
            break;
          case "2":
            EditContact();
            Pause();
            break;
          case "3":
            DeleteContact();
            Pause();
            break;
          case "4":
            SearchContact();
            Pause();
            break;
          case "5":
            ViewContacts();
            Pause();
            break;
          case "6":
            SaveContacts();
            Pause();
            return;
          default:
            Console.WriteLine("|------------------------------------|");
            Console.WriteLine("| Invalid option. Press a key to return to the menu.");
            Console.WriteLine("|------------------------------------|");
            Pause();
            break;
        }
      }
    }

    static void LoadContacts()
    {
      if (File.Exists(filePath))
      {
        var lines = File.ReadAllLines(filePath);

        foreach (var line in lines)
        {
          var data = line.Split(";");
          if (data.Length == 4)
          {
            Contact newContact = new()
            {
              Name = data[0],
              LastName = data[1],
              Phone = data[2],
              Email = data[3]
            };

            contacts.Add(newContact);
          }
        }
      }
    }

    static void SaveContacts()
    {
      // Dispose
      using var writer = new StreamWriter(filePath);

      foreach (var contact in contacts)
      {
        writer.WriteLine($"{contact.Name};{contact.LastName};{contact.Phone};{contact.Email}");
      }
    }

    static void Pause()
    {
      Console.ReadKey();
    }

    static void AddContact()
    {
      Contact newContact = new();

      Console.WriteLine("|------------------------------------|");
      Console.WriteLine("| 1. Add contact");
      Console.WriteLine("|------------------------------------|");

      Console.Write("| Name: ");
      newContact.Name = Console.ReadLine();
      Console.Write("| Lastname: ");
      newContact.LastName = Console.ReadLine();
      Console.Write("| Phone: ");
      newContact.Phone = Console.ReadLine();
      Console.Write("| Email: ");
      newContact.Email = Console.ReadLine();

      contacts.Add(newContact);

      Console.WriteLine("|------------------------------------|");
      Console.WriteLine("| Added contact. Press a key to return to the menu.");
      Console.WriteLine("|------------------------------------|");
    }

    static void EditContact()
    {
      Console.WriteLine("|------------------------------------|");
      Console.WriteLine("| 2. Edit contact");
      Console.WriteLine("|------------------------------------|");
      Console.Write("Enter name or lastname to edit the contact: ");

      string? InputUser = Console.ReadLine();

      Contact? FindContact = contacts.Where(cont => cont.Name == InputUser || cont.LastName == InputUser).FirstOrDefault();

      if (FindContact != null)
      {
        Console.WriteLine("|------------------------------------|");
        Console.WriteLine($"| Editing {FindContact.Name}");
        Console.WriteLine("| Leave the field blank so as not to change.");
        Console.WriteLine("| Current data: ");
        Console.WriteLine($"| {FindContact}");
        Console.WriteLine("|-----------------------------------|");

        Console.Write("New name: ");
        string? NewContactName = Console.ReadLine();
        FindContact.Name = NewContactName ?? FindContact.Name;

        Console.Write("New lastname: ");
        string? NewContactLastName = Console.ReadLine();
        FindContact.LastName = NewContactLastName ?? FindContact.LastName;

        Console.Write("New phone: ");
        string? NewContactPhone = Console.ReadLine();
        FindContact.Phone = NewContactPhone ?? FindContact.Phone;

        Console.Write("New email: ");
        string? NewContactEmail = Console.ReadLine();
        FindContact.Email = NewContactEmail ?? FindContact.Email;
      }
      else
      {
        Console.WriteLine($"| '{InputUser}' not found. Press a key to return to the menu.");
      }

      Console.WriteLine("|-----------------------------------|");
    }

    static void DeleteContact()
    {
      Console.WriteLine("|------------------------------------|");
      Console.WriteLine("| 3. Delete contact");
      Console.WriteLine("|------------------------------------|");
      Console.Write("Enter name or lastname to remove the contact: ");

      string? InputUser = Console.ReadLine();

      Contact? FindContact = contacts.Where(cont => cont.Name == InputUser || cont.LastName == InputUser).FirstOrDefault();

      if (FindContact != null)
      {
        contacts.Remove(FindContact);
        Console.WriteLine($"| Removed contact '{FindContact.Name}'. Press a key to return to the menu.");
      }
      else
      {
        Console.WriteLine($"| '{InputUser}'. Press a key to return to the menu.");
      }

      Console.WriteLine("|------------------------------------|");
    }

    static void SearchContact()
    {
      Console.WriteLine("|------------------------------------|");
      Console.WriteLine("| 4. Search contact");
      Console.WriteLine("|------------------------------------|");
      Console.Write("Enter name or lastname to find the contact: ");

      string? InputUser = Console.ReadLine();

      Contact? FindContact = contacts.Where(cont => cont.Name == InputUser || cont.LastName == InputUser).FirstOrDefault();

      if (FindContact != null)
      {
        Console.WriteLine("|------------------------------------|");
        Console.WriteLine("| Contact data:");
        Console.WriteLine($"| {FindContact}");
        Console.WriteLine("| Press a key for continue.");
      }
      else
      {
        Console.WriteLine("| Not found contact. Press a key to return to the menu.");
      }

      Console.WriteLine("|------------------------------------|");
    }

    static void ViewContacts()
    {
      Console.WriteLine("|------------------------------------|");
      Console.WriteLine("| 5. View contacts");
      Console.WriteLine("|------------------------------------|");

      if (contacts.Count > 0)
      {
        for (int i = 0; i < contacts.Count; i++)
        {
          Console.WriteLine($"| {i + 1}. {contacts[i]}");
        }

        Console.WriteLine("|------------------------------------|");
      }
      else
      {
        Console.WriteLine("| Without contacts, create one.");
      }

      Console.WriteLine("| Press a key to return to the menu.");
      Console.WriteLine("|------------------------------------|");
    }
  }
}