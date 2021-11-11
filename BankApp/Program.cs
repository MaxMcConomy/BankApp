/*
Bank App
Author@ Max McConomy
Version@ V3
~~~
This Programs purpose is to be a make belive banking system
*/
using System;
using System.IO;
using System.Text;

namespace BankApp
{
    class Program
    {
        public static void Main(string[] args)
        {
            string UsernamesPath = @"c:\Users\Sonic\source\repos\BankApp\BankApp\Usernames.txt";
            string PasswordsPath = @"c:\Users\Sonic\source\repos\BankApp\BankApp\Passwords.txt";
            string BalancesPath = @"c:\Users\Sonic\source\repos\BankApp\BankApp\Balances.txt";
            string[] Usernames = System.IO.File.ReadAllLines(UsernamesPath);
            string[] Passwords = System.IO.File.ReadAllLines(PasswordsPath);
            string[] Balances = System.IO.File.ReadAllLines(BalancesPath);

            int position = (LoginCheck(Login(), Usernames, UsernamesPath, PasswordsPath, BalancesPath));
            if(PasswordCheck(position, Passwords, Usernames) == false)
            {
                Environment.Exit(0);
            }

            bool satisfied = false;
            while (satisfied == false)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Logo();
                accountInfo(Usernames, Balances, position);
                buttons("  withdrawl  ", "   deposit   ", "  transfer   ", "1", position, Usernames, Balances, BalancesPath);
                Usernames = System.IO.File.ReadAllLines(UsernamesPath);
                Passwords = System.IO.File.ReadAllLines(PasswordsPath);
                Balances = System.IO.File.ReadAllLines(BalancesPath);

                Console.Clear();
                Logo();
                accountInfo(Usernames, Balances, position);
                Console.WriteLine("Would you like to continue? (Y/N)");
                string YN = Console.ReadLine();
                while (YN != "Y" && YN != "N")
                {
                    Console.Clear();
                    Logo();
                    accountInfo(Usernames, Balances, position);
                    Console.WriteLine("Y/N");
                    YN = Console.ReadLine();
                }
                if (YN == "Y")
                {
                    satisfied = false;
                }
                else if (YN == "N")
                {
                    satisfied = true;
                }
                
            }
            Console.Clear();
            Logo();
            accountInfo(Usernames, Balances, position);
            Console.WriteLine("Thank you for banking with us");
            Console.ReadLine();
            Environment.Exit(0);
        }

        //THE LOGIN
        public static string Login()
        {
            Logo();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Press \"NEW\" to create a new account");
            Console.WriteLine("\t\t Username:");
            string loginUsername = Console.ReadLine();
            return (loginUsername);
        }
        public static int LoginCheck(string CheckThis, string[] lines, string usernamePath, string passwordsPaths, string balancePaths)
        {
            if(CheckThis == "NEW")
            {
                //NewAccount
                NewAccount(usernamePath, passwordsPaths, balancePaths, lines);
            }
            for(int i = 0; i < lines.Length; i++)
            {
                if (lines[i] == CheckThis)
                {
                    return(i);
                }
            }
            return(1);
        }
        public static void NewAccount(string usernamePath, string passwordsPaths, string balancePaths, string[] usernames)
        {
            bool satisfied = false;
            while(satisfied == false)
            {

                Console.Clear();
                Logo();

                Console.WriteLine("Enter your prefered username: ");
                string username = Console.ReadLine();
                for (int i = 0; i < usernames.Length; i++)
                {
                    if (username == usernames[i])
                    {
                        Console.WriteLine("That username is already in use, please pick a new one.");
                        Console.ReadLine();
                    }
                }
                Console.WriteLine("Enter your prefered password:");
                string password = Console.ReadLine();

                Console.Clear();
                Logo();

                Console.WriteLine("Please re-enter password");
                string passwordRedo = Console.ReadLine();
                while (passwordRedo != password)
                {
                    Console.Clear();
                    Logo();
                    Console.WriteLine("Please re-enter password");
                    passwordRedo = Console.ReadLine();
                }

                Console.Clear();
                Logo();
                Console.WriteLine("How Much are you depositing initially?");
                double deposit = Convert.ToDouble(Console.ReadLine());

                Console.Clear();
                Logo();
                Console.WriteLine("Username: " + username);
                Console.WriteLine("Password: " + password);
                Console.WriteLine("Balance: " + deposit);
                Console.WriteLine("");
                Console.WriteLine("Is this correct? (Y/N)");
                string YN = Console.ReadLine();
                if (YN == "Y")
                {
                    satisfied = true;
                }
                else if(YN == "N")
                {
                    satisfied = false;
                }

                AddText(usernamePath, username);
                AddText(passwordsPaths, password);
                AddText(balancePaths, Convert.ToString(deposit));

                Console.Clear();
                Logo();
                Console.WriteLine("You have been registered");
                Console.WriteLine("Thank you for banking with us.");
                Console.WriteLine("Now closing");
                Console.ReadLine();
                Environment.Exit(0);
            }

        }
        //The Password
        public static string Password(int position, string[] Usernames)
        {
            Logo();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("\t\t Password for " + Usernames[position] + ": ");
            string loginPassword = Console.ReadLine();
            return (loginPassword);
        }
        public static bool PasswordCheck(int position, string[] path, string[] path2)
        {
            int numberOfTries = 10;
            Console.Clear();
            while (path[position] != Password(position, path2))
            {
                if (numberOfTries <= 0)
                {
                    Console.Clear();
                    Logo();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Failed too many passwords, now leaving...");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.ReadLine();
                    break;
                }
                numberOfTries--;
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Password doesn't match");
                Console.WriteLine("You have " + numberOfTries + " tries Left");
                Console.ForegroundColor = ConsoleColor.White;
                
            }
            return(true);
        }
        //Layout materials
        private static void Logo()
        {
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("|                                           |");
            Console.WriteLine("|             Max EcConomy Bank             |");
            Console.WriteLine("|           MaxMcConomy@gmail.com           |");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        }
        public static void buttons(string b1F, string b2F, string b3F, string screen, int position, string[] Usernames, string[] Balances, string path)
        {
            //b1F stands for Button 1 Function
            Console.WriteLine(" |~~~~~~~~~~~~~|~~~~~~~~~~~~~|~~~~~~~~~~~~~|");
            Console.WriteLine(" |1            |2            |3            |");
            Console.WriteLine(" |" + b1F + "|" + b2F + "|" + b3F + "|");
            Console.WriteLine(" |             |             |             |");
            Console.WriteLine(" |~~~~~~~~~~~~~|~~~~~~~~~~~~~|~~~~~~~~~~~~~|");

            Console.Write("What button do you want to press? (use the number in the corner)   ");
            string buttonPress = Console.ReadLine();

            if (buttonPress == "1" && screen == "1" )
            {
                //withdrawl
                withdrawl(position, Balances, Usernames, path);
            }
            else if(buttonPress == "2" && screen == "1")
            {
                //deposit
                deposit(position, Balances, Usernames, path);
            }
            else if(buttonPress == "3" && screen == "1")
            {
                //transfer
                transfer(position, Balances, Usernames, path);
            }
        }
        private static void accountInfo(string[] usernames, string[] balances, int position)
        {
            Console.WriteLine("Account Holder: " + usernames[position]);
            Console.WriteLine("Account Balance: $" + balances[position]);
            Console.WriteLine("_________________________________________");
        }
        //Functions
        public static void withdrawl(int position, string[] Balances, string[] Usernames, string path)
        {
            Console.Clear();
            Logo();
            accountInfo(Usernames, Balances, position);

            Console.WriteLine("How much would you like to withdrawl from your account? ");
            double WDAmount = Convert.ToDouble(Console.ReadLine());
            while(Math.Round(Convert.ToDouble(Balances[position]) - WDAmount, 2) < 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("You cannot withdrawl money you don't have.");
                Console.WriteLine("Press \"00.00\" to leave");
                Console.ForegroundColor = ConsoleColor.White;
                WDAmount = Convert.ToDouble(Console.ReadLine());
                if(WDAmount == 00.00)
                {
                    Console.WriteLine("Thank you for banking with us.");
                    Environment.Exit(0);
                }
            }
            double amountLeft = Math.Round(Convert.ToDouble(Balances[position]) - WDAmount, 2);
            string text = File.ReadAllText(path);
            text = text.Replace(Balances[position], Convert.ToString(amountLeft));
            File.WriteAllText(path, text);
            Balances = System.IO.File.ReadAllLines(path);

            Console.Clear();
            Logo();
            accountInfo(Usernames, Balances, position);
            Console.WriteLine("You know have $" + amountLeft + " in yout account.");
            Console.ReadLine();
        }
        public static void deposit(int position, string[] Balances, string[] Usernames, string path)
        {
            Console.Clear();
            Logo();
            accountInfo(Usernames, Balances, position);

            Console.WriteLine("How much are you depositing today?");
            double DPAmount = Convert.ToDouble(Console.ReadLine());
            double newBalance = Math.Round(Convert.ToDouble(Balances[position]) + DPAmount,2);

            string text = File.ReadAllText(path);
            text = text.Replace(Balances[position], Convert.ToString(newBalance));
            File.WriteAllText(path, text);
            Balances = System.IO.File.ReadAllLines(path);

            Console.Clear();
            Logo();
            accountInfo(Usernames, Balances, position);
            Console.WriteLine("You know have $" + newBalance + " in yout account.");
            Console.ReadLine();
        }
        public static void transfer(int position, string[] Balances, string [] Usernames, string path)
        {
            Console.Clear();
            Logo();
            accountInfo(Usernames, Balances, position);
            Console.WriteLine("Who are you trying to transfer to today? ");
            string TNUser = Console.ReadLine();
            if (TNUser != Usernames[position] && TNUser != Usernames[0] && TNUser != Usernames[1])
            {
                for (int i = 0; i < Usernames.Length; i++)
                {
                    if (Usernames[i] == TNUser)
                    {
                        Console.Clear();
                        Logo();
                        accountInfo(Usernames, Balances, position);
                        Console.WriteLine(Usernames[position] + " is transfering to " + Usernames[i]);
                        Console.WriteLine("How Much would you like to transfer? ");
                        double TNAmount = Math.Round(Convert.ToDouble(Console.ReadLine()),2);
                        double TNUserNewAmount = Math.Round(Convert.ToDouble(Balances[i]) + TNAmount);
                        double UserNewAmount = Math.Round(Convert.ToDouble(Balances[position]) - TNAmount);

                        string text = File.ReadAllText(path);
                        text = text.Replace(Balances[i], Convert.ToString(TNUserNewAmount));
                        File.WriteAllText(path, text);
                        Balances = System.IO.File.ReadAllLines(path);
                        text = text.Replace(Balances[position], Convert.ToString(UserNewAmount));
                        File.WriteAllText(path, text);
                        Balances = System.IO.File.ReadAllLines(path);

                        Console.Clear();
                        Logo();
                        accountInfo(Usernames, Balances, position);
                        Console.WriteLine("Succesfully Sent $" + TNAmount + "to " + TNUser);
                        Console.WriteLine("You now have $" + UserNewAmount + " in your account.");
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.Clear();
                        Logo();
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("That account is not real");
                        Console.WriteLine("Please try again");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.ReadLine();
                    }
                }
            }
            else
            {
                Console.Clear();
                Logo();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("That account is either unable to be transfered to or is your own account");
                Console.WriteLine("Please try again");
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadLine();


            }
        }
        //Filestream managment
        private static string ShowText(string path, int number)
        {
            //this also starts at zero, keep that in mind
            string[] line = System.IO.File.ReadAllLines(path);
            Console.WriteLine("the entry on line " + number + " is " + line[number - 1]);
            return (line[number - 1]);
        }
        private static void AddText(string path, string Addthis)
        {
            TextWriter tsw = new StreamWriter(path, true);
            tsw.WriteLine(Addthis);
            tsw.Close();
        }
    }
}