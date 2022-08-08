using AuthenticationWithClie.ApplicationLogic;
using AuthenticationWithClie.Database.Repository;
using System;

namespace AuthenticationWithClie.UI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            

            Console.WriteLine("Commands :");
            Console.WriteLine("/register");
            Console.WriteLine("/login");
            Console.WriteLine("/logout");
            Console.WriteLine("/exit");

            while (true)
            {
                Console.WriteLine();
                Console.Write("Enter command : ");
                string command = Console.ReadLine();

                if (command == "/register")
                {
                    Authentication.Register();
                }
                else if (command == "/login")
                {
                    Authentication.Login();
                }
                else if (command == "/logout")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Command not found!");
                }


            }
        }
    }
}
