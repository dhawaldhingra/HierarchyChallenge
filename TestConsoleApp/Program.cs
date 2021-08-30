using HierarchyChallenge.BusinessLogic;
using HierarchyChallenge.DataObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length < 3)
            {
                Console.WriteLine("Correct arguments not passed. Please re-start the app and pass the following three arguments-");
                Console.WriteLine("1. Users Json File");
                Console.WriteLine("2. Roles Json File");
                Console.WriteLine("3. Expected Output File");
                Console.WriteLine("Press Any Key To Continue..");
                Console.ReadKey(false);
            }

            try
            {
                UsersRoles usrRoles = new UsersRoles();
                usrRoles.Users = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(args[0]));
                usrRoles.Roles = JsonConvert.DeserializeObject<List<Role>>(File.ReadAllText(args[1]));
                var actualOutput = usrRoles.GetSubordinates(5);
                var expectedOutput = JsonConvert.DeserializeObject<List<Role>>(File.ReadAllText(args[2]));
                if(actualOutput.Count != expectedOutput.Count)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Test Case Failed");
                    Console.ReadKey();
                    return;
                }
                else
                {
                    actualOutput.ForEach(ao =>
                    {
                        if(expectedOutput.Where(eo => eo.Id == ao.Id).Count() != 1)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Test Case Failed");
                            Console.ReadKey();
                            return;
                        }
                    });
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Test Case Passed!\nPress any key to continue");
                Console.ReadKey();
                return;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                
                Console.WriteLine(ex.ToString());
                Console.WriteLine("Press Any Key To Continue");
                Console.ReadKey();
            }
        }
    }
}
