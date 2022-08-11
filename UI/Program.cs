using AuthenticationWithClie.ApplicationLogic;
using AuthenticationWithClie.ApplicationLogic.Validations.Services;
using AuthenticationWithClie.Database.Repository;
using System;

namespace AuthenticationWithClie.UI
{
    internal class Program
    {
        static void Main(string[] args)
        {

            
            Console.WriteLine("Commands :");
            Console.WriteLine("1./register");
            Console.WriteLine("2./login");
            Console.WriteLine("3./show-blogs-with-comments");
            Console.WriteLine("4./show-filtered-blogs-with-comments");
            Console.WriteLine("5./find-blog-by-code");
            Console.WriteLine("_________________________________________________________");
           
            BlogService blogService = new BlogService();

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
                else if (command == "/show-blogs-with-comments")
                {
                    blogService.ShowBlogWithComments();
                }
                else if (command == "/show-filtered-blogs-with-comments")
                {
                    blogService.ShowFilteredBlogs();
                   
                }
                else if (command == "/find-blog-by-code")
                {

                }
                else
                {
                    Console.WriteLine("Command not found! ");
                }


            }
        }
    }
}
