using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;



Console.WriteLine("[1] - Register");
Console.WriteLine("[2] - Log In");
int choice = int.Parse(Console.ReadLine());
Account client = new Account();

if (choice == 1)
{
    client.register();

}
else if (choice == 2)
{
    client.Login();

}




