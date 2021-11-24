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
            Global globalVars = new Global();
            globalVars.usernamesPath = @"G:\.shortcut-targets-by-id\1L9FXfZ6Gk14A_KccWsyhIpjoyBOXHHtA\BankApp\userData\Usernames.txt";
            globalVars.passwordsPath = @"G:\.shortcut-targets-by-id\1L9FXfZ6Gk14A_KccWsyhIpjoyBOXHHtA\BankApp\userData\Passwords.txt";
            globalVars.balancesPath = @"G:\.shortcut-targets-by-id\1L9FXfZ6Gk14A_KccWsyhIpjoyBOXHHtA\BankApp\userData\Balances.txt";
            globalVars.changeLogPath = @"G:\.shortcut-targets-by-id\1L9FXfZ6Gk14A_KccWsyhIpjoyBOXHHtA\BankApp\userData\ChangeLog.txt";
            globalVars.usernames = System.IO.File.ReadAllLines(globalVars.usernamesPath);
            globalVars.passwords = System.IO.File.ReadAllLines(globalVars.passwordsPath);
            globalVars.balances = System.IO.File.ReadAllLines(globalVars.balancesPath);
            globalVars.screenPosition = 0;

            Screen screenOne = new Screen();
            
            screenOne.buttonOne =   "  Withdrawl  ";
            screenOne.buttonTwo =   "   Deposit   ";
            screenOne.buttonThree = "   Transfer  ";
            screenOne.screenNumber = 1;

            //=======================================================================================
            

            bool satisfied = false;
            while (satisfied == false)
            {
                if(globalVars.screenPosition == 0)
                {
                    Console.Clear();
                    startUp();
                    Console.Clear();
                    globalVars.screenPosition++;
                }
                if (globalVars.screenPosition == 1)
                {
                    globalVars.position = (LoginCheck(Login(globalVars, true), globalVars));
                    if(globalVars.position == -2)//To go back
                    {
                        globalVars.screenPosition--;
                    }
                    if (globalVars.position == 3)//To new account
                    {
                        globalVars.screenPosition = 3;
                    }
                    if (globalVars.position == 1)//If it needs to redo
                    {
                        while (globalVars.position == 1)
                        {
                            globalVars.screenPosition = 1;
                            globalVars.position = (LoginCheck(Login(globalVars, false), globalVars));
                        }
                    }
                    if(globalVars.position > 1 && globalVars.screenPosition == 1)//To go forward
                    {
                        globalVars.screenPosition++;
                    }
                }
                if (globalVars.screenPosition == 2)
                {
                    string checkThis = PasswordCheck(globalVars);
                    if (checkThis == "false")
                    {
                        exitApp();
                    }
                    else if(checkThis == "-2")
                    {
                        globalVars.screenPosition--;
                    }
                    else
                    {
                        globalVars.screenPosition = 4;
                    }
                }
                if(globalVars.screenPosition == 3)
                {
                    globalVars.position = NewAccount(globalVars);
                    if(globalVars.position == -2)
                    {
                        globalVars.screenPosition = 1;
                    }
                    if(globalVars.position == 0)
                    {
                        Console.WriteLine("problem");
                        Console.ReadLine();//TODO i dont know what this is meant to do
                    }
                    if (globalVars.screenPosition == 3)
                    {
                        globalVars.screenPosition++;
                        globalVars.usernames = System.IO.File.ReadAllLines(globalVars.usernamesPath);
                        globalVars.passwords = System.IO.File.ReadAllLines(globalVars.passwordsPath);
                        globalVars.balances = System.IO.File.ReadAllLines(globalVars.balancesPath);
                    }
                }
                if (globalVars.screenPosition == 4)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    clearLogoNInfo(globalVars);
                    int checkThis = readButtons(globalVars, screenOne);
                    if(checkThis == -2)
                    {
                        globalVars.screenPosition = 1;
                    }
                    if (globalVars.screenPosition == 4)
                    {
                        globalVars.screenPosition++;
                    }
                    if (checkThis == 0)
                    {
                        globalVars.screenPosition = 5;
                    }
                }
                if(globalVars.screenPosition == 5)
                {
                    satisfied = keepGoing(globalVars, satisfied);
                    if (satisfied == false)
                    {
                        globalVars.screenPosition = 4;
                    }
                }
                globalVars.usernames = System.IO.File.ReadAllLines(globalVars.usernamesPath);
                globalVars.passwords = System.IO.File.ReadAllLines(globalVars.passwordsPath);
                globalVars.balances = System.IO.File.ReadAllLines(globalVars.balancesPath);
            }
        }

        //THE LOGIN
        public static string Login(Global globalVars, bool error)
        {
            Console.Clear();
            Logo();
            Console.WriteLine("Enter \"NEW\" to create an account");
            Console.WriteLine("Enter your username:");
            if(error == false)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Username not found");
                Console.WriteLine("Please try agian");
                Console.ForegroundColor = ConsoleColor.White;
            }
            string loginUsername = Console.ReadLine();
            
            return (loginUsername);
        }
        public static int LoginCheck(string checkUsername, Global globalVars)
        {
            if(checkUsername.ToUpper() == "NEW")
            {
                return (3);
            }
            if (checkUsername == "-1")
            {
                exitApp();
            }
            if(checkUsername == "-2")
            {
                return (-2);
            }
            for(int i = 2; i < globalVars.usernames.Length; i++)
            {
                if (globalVars.usernames[i] == checkUsername)
                {
                    return(i);
                }
            }
            return (1);
        }
        public static int NewAccount(Global globalVars)
        {
            int spot = 0;
            bool satisfied = false;
            string newUsername = "error";
            string newPassword = "error";
            double deposit = 0;
            while (satisfied == false)
            {
                if (spot == 0)
                {
                    Console.Clear();
                    Logo();

                    Console.WriteLine("Enter your prefered username: ");
                    newUsername = Console.ReadLine();
                    if (newUsername == "-1")
                    {
                        exitApp();
                    }
                    if(newUsername == "-2")
                    {
                        return (-2);
                    }

                    bool inUse = true;
                    while (inUse == true)
                    {
                        inUse = false;

                        for (int i = 0; i < globalVars.usernames.Length; i++)
                        {
                            if (newUsername == globalVars.usernames[i])
                            {
                                inUse = true;
                                break;
                            }
                        }
                        if (inUse == true)
                        {
                            Console.Clear();
                            Logo();
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("That username is already in use, please enter a new one.");
                            Console.ForegroundColor = ConsoleColor.White;
                            newUsername = Console.ReadLine();
                            if (newUsername == "-1")
                            {
                                exitApp();
                            }
                            else if(newUsername == "-2")
                            {
                                return (-2);
                            }

                        }
                        spot++;
                    }

                }
                if (spot == 1)
                {
                    Console.Clear();
                    Logo();
                    Console.WriteLine("Enter your prefered password:");
                    newPassword = Console.ReadLine();
                    if (newPassword == "-1")
                    {
                        exitApp();
                    }
                    else if (newPassword == "-2")
                    {
                        spot--;
                    }
                    if (spot == 1)
                    {

                        Console.Clear();
                        Logo();

                        Console.WriteLine("Please re-enter password");
                        string passwordRedo = Console.ReadLine();
                        if (passwordRedo == "-1")
                        {
                            exitApp();
                        }
                        else if (passwordRedo == "-2")
                        {
                            spot--;
                        }
                        while (passwordRedo != "-1" && passwordRedo != "-2" && passwordRedo != newPassword && spot == 1)
                        {
                            Console.Clear();
                            Logo();
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Password Didn't match.");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("Please re-enter password");
                            passwordRedo = Console.ReadLine();
                            if (passwordRedo == "-1")
                            {
                                exitApp();
                            }
                            else if (passwordRedo == "-2")
                            {
                                spot--;
                            }
                        }
                        if (spot == 1)
                        {
                            spot++;
                        }
                    }
                }
                if (spot == 2)
                {
                    Console.Clear();
                    Logo();
                    Console.WriteLine("Please enter how much are you depositing initially.");
                    deposit = Convert.ToDouble(Console.ReadLine());
                    while (deposit < -2)
                    {
                        Console.Clear();
                        Logo();
                        Console.WriteLine("Please enter how much are you depositing initially.");
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Please enter a positive number");
                        deposit = Convert.ToDouble(Console.ReadLine());
                    }
                    if (deposit == -1)
                    {
                        exitApp();
                    }
                    else if (deposit == -2)
                    {
                        spot--;
                    }
                    if (spot == 2)
                    {
                        spot++;
                    }
                }
                if (spot == 3)
                { 
                    Console.Clear();
                    Logo();
                    Console.WriteLine("Username: " + newUsername);
                    Console.WriteLine("Password: " + newPassword);
                    Console.WriteLine("Balance: " + deposit);
                    Console.WriteLine("");
                    Console.WriteLine("Is this correct? (Y/N)");
                    string YN = (Console.ReadLine()).ToUpper();
                    if (YN == "Y")
                    {
                        satisfied = true;
                        AddText(globalVars.usernamesPath, newUsername);
                        AddText(globalVars.passwordsPath, newPassword);
                        AddText(globalVars.balancesPath, Convert.ToString(deposit));
                        AddChangeLogText(globalVars, "New account: " + newUsername + " Password: " + "Balance: " + Convert.ToString(deposit), 0);

                        Console.Clear();
                        Logo();
                        Console.WriteLine("You have been registered");
                        return globalVars.usernames.Length;
                    }
                    else if (YN == "N" || YN == "-2")
                    {
                        satisfied = false;
                        spot = 0;

                    }
                    else if(YN == "-1")
                    {
                        exitApp();
                    }
                }
                
            }
            return (-10);
        }
        //The Password
        public static string Password(Global globalVars, bool error, int numberOfTries)
        {
            Logo();
            Console.WriteLine("Enter Password for " + globalVars.usernames[globalVars.position] + ": ");
            if(error == true)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Password doesn't match");
                Console.WriteLine("You have " + (numberOfTries + 1) + " tries Left");
                Console.ForegroundColor = ConsoleColor.White;
            }
            string loginPassword = Console.ReadLine();
            if(loginPassword == "-1")
            {
                exitApp();
            }
            Console.Clear();
            return (loginPassword);
        }
        public static string PasswordCheck(Global globalVars)
        {
            int numberOfTries = 9;
            Console.Clear();
            string tryThis = Password(globalVars, false, numberOfTries);
            if(tryThis == "-1")
            {
                exitApp();
            }
            if(tryThis == "-2")
            {
                return ("-2");
            }
            while (globalVars.passwords[globalVars.position] != tryThis)
            {
                numberOfTries--;
                tryThis = Password(globalVars, true, numberOfTries);
                if (numberOfTries <= 0)
                {
                    Console.Clear();
                    Logo();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Failed too many passwords");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.ReadLine();
                    AddChangeLogText(globalVars, "User " + globalVars.usernames[globalVars.position] + " had a failed login attempt", 4);
                    exitApp();
                }
                
                
            }
            return("true");
        }
        //Layout materials
        private static void Logo()
        {
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("|                                           |");
            Console.WriteLine("|             Max EcConomy Bank             |");
            Console.WriteLine("|           MaxMcConomy@gmail.com           |");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine(" Enter \"-1\" to exit and \"-2\" to go back. ");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        }
        public static void showButtons(Screen screen, Global globalVars)
        {
            clearLogoNInfo(globalVars);
            Console.WriteLine(" |~~~~~~~~~~~~~|~~~~~~~~~~~~~|~~~~~~~~~~~~~|");
            Console.WriteLine(" |1            |2            |3            |");
            Console.WriteLine(" |" + screen.buttonOne + "|" + screen.buttonTwo + "|" + screen.buttonThree + "|");
            Console.WriteLine(" |             |             |             |");
            Console.WriteLine(" |~~~~~~~~~~~~~|~~~~~~~~~~~~~|~~~~~~~~~~~~~|");

            Console.Write("What button do you want to press? (use the number in the corner)   ");
            
        }
        public static int readButtons(Global globalVars, Screen screen)
        {
            bool satisfied = false;
            int spot = 0;
            string buttonPress = "";
            while (satisfied == false)
            {
                if (spot == 0)
                {
                    showButtons(screen, globalVars);
                    buttonPress = Console.ReadLine();
                    bool goodValue = false;
                    if (buttonPress == "1" || buttonPress == "2" || buttonPress == "3")
                    {
                        goodValue = true;
                        spot++;
                    }
                    else if (buttonPress == "-1")
                    {
                        exitApp();
                    }
                    else if (buttonPress == "-2")
                    {
                        return (-2);
                    }
                    while (goodValue == false)
                    {
                        showButtons(screen, globalVars);
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine(buttonPress + "isnt correct");
                        Console.WriteLine("ERROR. please insert a number above");
                        Console.ForegroundColor = ConsoleColor.White;
                        buttonPress = Console.ReadLine();
                        if (buttonPress == "1" || buttonPress == "2" || buttonPress == "3" || buttonPress == "-1")
                        {
                            goodValue = true;
                            spot++;
                        }
                        else if (buttonPress == "-1")
                        {
                            exitApp();
                        }
                        else if (buttonPress == "-2")
                        {
                            return (-2);
                        }
                        spot++;
                    }
                }
                if (buttonPress == "1" && screen.screenNumber == 1 && spot == 1)//withdrawl
                {
                    int checkThis = withdrawl(globalVars);
                    if (checkThis == -1)
                    {
                        exitApp();
                    }
                    if (checkThis == -2)
                    {
                        spot = 0;
                    }
                    if (checkThis == 0)
                    {
                        return (0);
                    }
                }
                else if (buttonPress == "2" && screen.screenNumber == 1 && spot == 1)//deposit
                {
                    int checkThis = deposit(globalVars);
                    if (checkThis == -1)
                    {
                        exitApp();
                    }
                    if (checkThis == -2)
                    {
                        spot = 0;
                    }
                    if (checkThis == 0)
                    {
                        return (0);
                    }
                }
                else if (buttonPress == "3" && screen.screenNumber == 1 && spot == 1)//transfer
                {
                    int checkThis = transfer(globalVars);
                    if (checkThis == -1)
                    {
                        exitApp();
                    }
                    if (checkThis == -2)
                    {
                        spot = 0;
                    }
                    if (checkThis == 0)
                    {
                        return (0);
                    }
                }
            }
            return (0);
        }
        private static void accountInfo(Global globalVars)
        {
            Console.WriteLine("Account Holder: " + globalVars.usernames[globalVars.position]);
            Console.WriteLine("Account Balance: $" + globalVars.balances[globalVars.position]);
            Console.WriteLine("_________________________________________");
        }
        private static void clearLogoNInfo(Global globalVars)
        {
            Console.Clear();
            Logo();
            accountInfo(globalVars);
        }
        private static void exitApp()
        {
            Console.Clear();
            Logo();
            Console.WriteLine("Thank you for banking with us");
            System.Threading.Thread.Sleep(1000);
            Environment.Exit(0);
        }
        private static void startUp()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Logo();
            Console.WriteLine("Welcome to Max EcConomy bank!");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Please note that you need to type and press enter with this program.");
            Console.WriteLine("");
            Console.WriteLine("Before you use this program please make sure you have");
            Console.WriteLine("read the README file and followed its instructions.");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("");
            Console.Write("Press enter to continue.");
            Console.ReadLine();
        }
        private static bool keepGoing(Global globalVars, bool satisfied)
        {
            clearLogoNInfo(globalVars);
            Console.WriteLine("Would you like to continue? (Y/N)");
            string YN = (Console.ReadLine()).ToUpper();
            while (YN != "Y" && YN != "N")
            {
                clearLogoNInfo(globalVars);
                Console.WriteLine("Y/N");
                YN = Console.ReadLine();
            }
            if (YN == "Y")
            {
                satisfied = false;
            }
            else if (YN == "N")
            {
                exitApp();
            }
            return (satisfied);
        }
        //Functions
        public static double IsNumeric(string checkIfNumber, Global globalVars)
        {
            int num = -1;
            while (int.TryParse(checkIfNumber, out num) == false)
            {
                clearLogoNInfo(globalVars);
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Please enter a number");
                Console.ForegroundColor = ConsoleColor.White;
                checkIfNumber = Console.ReadLine();
            }
            return Math.Round(Convert.ToDouble(checkIfNumber), 2);
        }
        public static int withdrawl(Global globalVars)
        {
            clearLogoNInfo(globalVars);
            Console.WriteLine("Please enter how much you would like to withdrawl from your account. ");
            double WDAmount = IsNumeric(Console.ReadLine(), globalVars);
            while(WDAmount <= -3)
            {
                clearLogoNInfo(globalVars);
                Console.WriteLine("Please enter how much you would like to withdrawl from your account. ");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Please enter a positive number.");
                Console.ForegroundColor = ConsoleColor.White;
                WDAmount = IsNumeric(Console.ReadLine(), globalVars);
            }
            if(WDAmount == -1)
            {
                exitApp();
            }
            if (WDAmount == -2)
            {
                return (-2);
            }
            while (Math.Round(Convert.ToDouble(globalVars.balances[globalVars.position]) - WDAmount, 2) < 0)
            {
                Console.Clear();
                Logo();
                accountInfo(globalVars);
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("You cannot withdrawl money you don't have.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Please enter how much you would like to withdrawl from your account. ");
                Console.ForegroundColor = ConsoleColor.White;
                WDAmount = Convert.ToDouble(Console.ReadLine());
                if(WDAmount == -1)
                {
                    exitApp();
                }
                if(WDAmount == -2)
                {
                    return (-2);
                }
            }
            double amountLeft = Math.Round(Convert.ToDouble(globalVars.balances[globalVars.position]) - WDAmount, 2);
            string text = File.ReadAllText(globalVars.balancesPath);
            text = text.Replace(globalVars.balances[globalVars.position], Convert.ToString(amountLeft));
            File.WriteAllText(globalVars.balancesPath, text);
            globalVars.balances = System.IO.File.ReadAllLines(globalVars.balancesPath);
            AddChangeLogText(globalVars, "User " + globalVars.usernames[globalVars.position] + " withdrew $" + WDAmount, 1);

            Console.Clear();
            Logo();
            accountInfo(globalVars);
            Console.WriteLine("You know have $" + amountLeft + " in your account.");
            Console.ReadLine();
            return (0);
        }
        public static int deposit(Global globalVars)
        {
            clearLogoNInfo(globalVars);

            Console.WriteLine("Please enter how much you would like to deposit. ");
            double DPAmount = IsNumeric(Console.ReadLine(), globalVars);
            while (DPAmount <= -3)
            {

                clearLogoNInfo(globalVars);
                Console.WriteLine("Please enter how much you would like to deposit. ");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Please enter a positive number.");
                Console.ForegroundColor = ConsoleColor.White;
                DPAmount = IsNumeric(Console.ReadLine(), globalVars);
            }
            if (DPAmount == -1)
            {
                exitApp();
            }
            else if(DPAmount == -2)
            {
                return (-2);
            }

            double newBalance = Math.Round(Convert.ToDouble(globalVars.balances[globalVars.position]) + DPAmount,2);

            string text = File.ReadAllText(globalVars.balancesPath);
            text = text.Replace(globalVars.balances[globalVars.position], Convert.ToString(newBalance));
            File.WriteAllText(globalVars.balancesPath, text);
            globalVars.balances = System.IO.File.ReadAllLines(globalVars.balancesPath);
            AddChangeLogText(globalVars, "User " + globalVars.usernames[globalVars.position] + " deposited $" + DPAmount, 2);

            clearLogoNInfo(globalVars);
            Console.WriteLine("You know have $" + newBalance + " in your account.");
            Console.ReadLine();
            return (0);
        }
        public static int transfer(Global globalVars)
        {
            int spot = 0;
            bool satisfied = false;
            int otherPosition = 0;
            double TNAmount = 0;
            while (satisfied == false)
            {
                if(spot == 0)
                {
                    clearLogoNInfo(globalVars);
                    Console.WriteLine("Please enter who you are trying to transfer to today. ");
                    string TNUser = Console.ReadLine();
                    if (TNUser == "-1")
                    {
                        exitApp();
                    }
                    else if (TNUser == "-2")
                    {
                        return (-2);
                    }
                    else
                    {
                        bool goodName = false;
                        while (goodName == false)
                        {
                            if (TNUser != globalVars.usernames[globalVars.position])
                            {
                                for (int i = 2; i < globalVars.usernames.Length; i++)
                                {
                                    if (globalVars.usernames[i] == TNUser)
                                    {
                                        goodName = true;
                                        otherPosition = i;
                                        spot++;
                                    }
                                }
                            }
                            if(spot == 0)
                            {
                                clearLogoNInfo(globalVars);
                                Console.WriteLine("Please enter who you are trying to transfer to today. ");
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("That account does not exist, please try again.");
                                Console.ForegroundColor = ConsoleColor.White;
                                TNUser = Console.ReadLine();
                            }
                        }
                    }
                }

                if(spot == 1)
                {
                    clearLogoNInfo(globalVars);
                    Console.WriteLine(globalVars.usernames[globalVars.position] + " is transfering to " + globalVars.usernames[otherPosition]);
                    Console.WriteLine();
                    Console.WriteLine("Please enter how much you would like to transfer. ");
                    TNAmount = IsNumeric(Console.ReadLine(), globalVars);
                    if(TNAmount == -1)
                    {
                        exitApp();
                    }
                    else if (TNAmount == -2)
                    {
                        spot--;
                    }
                    else if(TNAmount < -2)
                    {
                        clearLogoNInfo(globalVars);
                        Console.WriteLine(globalVars.usernames[globalVars.position] + " is transfering to " + globalVars.usernames[otherPosition]);
                        Console.WriteLine();
                        Console.WriteLine("Please enter how much you would like to transfer. ");
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("please enter a positive number");
                        Console.ForegroundColor = ConsoleColor.White;
                        TNAmount = IsNumeric(Console.ReadLine(), globalVars);
                    }
                    if(spot == 1)
                    {
                        spot++;
                    }
                }

                if(spot == 2)
                {
                    clearLogoNInfo(globalVars);
                    double TNUserNewAmount = Math.Round(Convert.ToDouble(globalVars.balances[otherPosition]) + TNAmount);
                    double UserNewAmount = Math.Round(Convert.ToDouble(globalVars.balances[globalVars.position]) - TNAmount);
                    Console.WriteLine(globalVars.usernames[globalVars.position] + " is sending $" + TNAmount + " to " + globalVars.usernames[otherPosition] +".");
                    Console.WriteLine("Is this correct? (Y/N)");
                    string good = Console.ReadLine().ToUpper();
                    if (good == "Y")
                    {
                        string text = File.ReadAllText(globalVars.balancesPath);
                        text = text.Replace(globalVars.balances[otherPosition], Convert.ToString(TNUserNewAmount));
                        File.WriteAllText(globalVars.balancesPath, text);
                        globalVars.balances = System.IO.File.ReadAllLines(globalVars.balancesPath);
                        text = text.Replace(globalVars.balances[globalVars.position], Convert.ToString(UserNewAmount));
                        File.WriteAllText(globalVars.balancesPath, text);
                        globalVars.balances = System.IO.File.ReadAllLines(globalVars.balancesPath);
                        AddChangeLogText(globalVars, "User " + globalVars.usernames[globalVars.position] + " transfered  $" + TNAmount + " to user " + globalVars.usernames[otherPosition], 3);

                        clearLogoNInfo(globalVars);
                        Console.WriteLine("Succesfully Sent $" + TNAmount + " to " + globalVars.usernames[otherPosition]);
                        Console.WriteLine("You now have $" + UserNewAmount + " in your account.");
                        Console.Write("Press enter to continue. ");
                        Console.ReadLine();
                        break;
                    }
                    if (good == "N" || good == "-2")
                    {
                        spot = 0;
                    }
                    else if (good == "-1")
                    {
                        exitApp();
                    }
                }
            }
            return (0);
        }
        //Filestream managment
        private static string ShowText(string path, int number)
        {
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
        private static void AddChangeLogText(Global globalVars, string message, int value)
        {
            string[] changeLog = System.IO.File.ReadAllLines(globalVars.changeLogPath);
            AddText(globalVars.changeLogPath, "Change #: " + (changeLog.Length)/5);
            AddText(globalVars.changeLogPath, "ChangeValue: " + value);
            AddText(globalVars.changeLogPath, "Date and time: " + DateTime.Now.ToString());
            AddText(globalVars.changeLogPath, message);
            AddText(globalVars.changeLogPath, "-------------------------------------------------");
        }
    }
}