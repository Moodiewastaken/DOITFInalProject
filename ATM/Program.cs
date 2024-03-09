using System;
using System.Dynamic;
using System.IO;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text.Json;
using System.Text.Json.Serialization;
using MiniBank.Exceptions;


public class Account
{

    private string HistoryFile = "../../../user_history.txt";
    DateTime currentDate = DateTime.Now;

    private static List<Account> accounts = new List<Account>();
    public Account() { }
    private int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Pin
    {
        get { return _pin; }
        set
        {
            if (value.Length == 11)
            {
                _pin = value;
            }
        }
    }
    private string _pin;
    public string Password { get; set; }
    public int Balance { get; set; }

    private string RandomPassword()
    {
        Random random = new Random();
        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()-_=+[{]};:?";
        string password = "";

        for (int i = 0; i < 4; i++)
        {
            int index = random.Next(chars.Length);
            password += chars[index];
        }

        return password;

    }



    public Account(string firstName, string lastName, string pin)
    {
        Id = accounts.Count + 1;
        FirstName = firstName;
        LastName = lastName;
        Pin = pin;
        Password = RandomPassword();
        Balance = 0;

    }
    public void UpdateAccountsJson(int data)
    {
        foreach (Account account in AllInfo())
        {
            account.Balance = data;
        }
        AccountsToJson();
    }

    public void Fill(Account account, int balance)
    {
        if (balance < 0)
        {
            Console.WriteLine("Please enter valid amount");
        }
        else
        {
            int currentBalance = account.Balance += balance;
            string opHistory = $"ID: {account.Id} || {account.FirstName} {account.LastName} added {balance} to their balance on {currentDate}, current balance is {currentBalance}";
            File.AppendAllText(HistoryFile, opHistory);
            UpdateAccountsJson(balance);
        }

    }

    public void withdraw(Account account, int balance)
    {
        if (balance < 0)
        {
            Console.WriteLine("Please enter valid amount");
        }
        else if (balance > account.Balance)
        {
            Console.WriteLine("There is not enough balance on your account");
        }
        else
        {
            account.Balance -= balance;
            string opHistory = $"ID: {account.Id} || {account.FirstName} {account.LastName} withdrew {balance} from their account on {currentDate}, current balance is {account.Balance}";
            File.AppendAllText(HistoryFile, opHistory);
        }


    }

    public int checkBalance(Account account)
    {
        return account.Balance;

        string opHistory = $"ID: {account.Id} || {account.FirstName} {account.LastName} checked their balance on {currentDate}";
        File.AppendAllText(HistoryFile, opHistory);
    }

    public void CheckOperationHistory(Account account)
    {
        string[] content = File.ReadAllLines(HistoryFile);
        List<String> currentUser = new List<String>();
        char userId = char.Parse(account.Id.ToString());

        foreach (string line in content)
        {
            if (line.Contains($"ID: {userId}"))
            {
                Console.WriteLine(line);
                currentUser.Add(line);
            }

        }
    }

    private bool pinIsValid(string pin)
    {
        if (pin != null && pin.Length == 11 && pin.All(char.IsDigit))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void register()
    {
        try
        {
            Account user = new();


            Console.WriteLine("Enter your first name: ");
            string firstName = Console.ReadLine();
            Console.WriteLine("Enter youe last name: ");
            string lastName = Console.ReadLine();
            Console.WriteLine("Enter your pin: ");
            string pin = Console.ReadLine();

            if (firstName != "" && lastName != "" && firstName.All(char.IsLetter) && lastName.All(char.IsLetter) && pinIsValid(pin))
            {

                Account newAccount = new Account(firstName, lastName, pin);

                Console.WriteLine($"Welcome {newAccount.FirstName} {newAccount.LastName}");
                Console.WriteLine($"Your account has been created with ID: {newAccount.Id}, Password: {newAccount.Password}, and Balance: {newAccount.Balance}");

                accounts.Add(newAccount);
                AccountsToJson();

            }
            else if (firstName == "" || lastName == "" || firstName.All(char.IsLetter) == false || lastName.All(char.IsLetter) == false)
            {
                throw new InvalidName();
            }
            else if (pinIsValid(pin) == false)
            {
                throw new InvalidPin();
            }
        }
        catch (InvalidName e)
        {
            Console.WriteLine(e.Message);
        }
        catch (InvalidPin e)
        {
            Console.WriteLine(e.Message);
        }



    }

    private void AccountsToJson()
    {
        string json = JsonSerializer.Serialize(accounts);

        if (File.Exists("users_data.json"))
        {
            string existingJson = File.ReadAllText("users_data.json");
            if (existingJson.Trim() == "[]")
            {
                File.WriteAllText("users_data.json", $"{json}");
            }
            else
            {
                existingJson = existingJson.TrimEnd(']');
                json = json.TrimStart('[');
                File.WriteAllText("users_data.json", $"{existingJson},{json}");
            }
        }
        else
        {
            File.WriteAllText("users_data.json", $"{json}");
        }
    }
    public List<Account> AllInfo()
    {
        List<Account> allAccounts = new List<Account>();

        try
        {
            string jsonContent = File.ReadAllText("users_data.json");

            allAccounts = JsonSerializer.Deserialize<List<Account>>(jsonContent);

            if (allAccounts == null)
            {
                allAccounts = new List<Account>();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading user data: {ex.Message}");
        }

        return allAccounts;
    }




    public void SaveOperationHistory(Account account, string data)
    {
        File.AppendAllText(HistoryFile, data);
    }

    public void Login()
    {
        Console.WriteLine("Enter your PIN: ");
        string pin = Console.ReadLine();
        Console.WriteLine("Password:");
        string pass = Console.ReadLine();

        bool loggedIn = false;
        List<Account> allAccounts = AllInfo();
        foreach (Account account in AllInfo())
        {
            if (account.Pin == pin && account.Password == pass)
            {
                loggedIn = true;
                Console.WriteLine("Login Complete!");
                Console.WriteLine("Choose your operation");
                Console.WriteLine("[1] - Check your balance");
                Console.WriteLine("[2] - Fill your balance");
                Console.WriteLine("[3] - Withdraw money");
                Console.WriteLine("[4] - History");
                int operation = int.Parse(Console.ReadLine());


                if (operation == 1)
                {
                    Console.WriteLine(account.Balance);
                }
                else if (operation == 2)
                {
                    Console.WriteLine("Enter amount: ");
                    int amount = int.Parse(Console.ReadLine());
                    account.Fill(account, amount);
                }
                else if (operation == 3)
                {
                    Console.WriteLine("Enter amount: ");
                    int amount = int.Parse(Console.ReadLine());
                    account.withdraw(account, amount);
                }


                break;
            }
        }

        if (!loggedIn)
        {
            Console.WriteLine("User does not exist");
        }

    }
}
