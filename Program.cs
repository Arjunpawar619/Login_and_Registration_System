using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;


namespace Security_code
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("\n");
            Console.WriteLine("           Welcome to Data Protech                ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n");
            Console.WriteLine("\t");
            Console.WriteLine("User_Name:-");
            Console.ResetColor();
            var fname = Console.ReadLine();

            if (checker(fname) == true)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("There's already an account");
                Console.WriteLine("\n");
                Console.WriteLine("Login To the Account");
                Console.ResetColor();
                account();
            }

            else
            {
                Console.WriteLine("\n");
                Console.WriteLine(" We didn't recognize that User name");
                Console.WriteLine("\n");
                Console.WriteLine("Please Register to login");
                Console.ReadLine();
                if (RegisterEntry() == true)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("\n");
                    Console.WriteLine("\t");
                    Console.WriteLine("Login To the Account");
                    Console.ResetColor();
                    account();
                }
                
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("\n");
                    //Console.WriteLine("\t");
                    Console.WriteLine("Access denied on this sytem");
                    Console.ReadLine();
                    Console.ResetColor();
                    return;
                }


            }
            

        }

        public static bool RegisterEntry()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n");
            Console.WriteLine("\t");
            Console.WriteLine("Registration");
            Console.WriteLine("\n");
            Console.ResetColor();
            Console.WriteLine("User_Name:-");
            var fname = Console.ReadLine();
            Console.WriteLine("email:-");
            string email = Console.ReadLine();
            Console.WriteLine("Set Password");
            string user_password = Hidepassword();
            Console.WriteLine("\n");
            Console.WriteLine("Authorized person ?  ");
            string permission = Console.ReadLine();
            Console.WriteLine("Verfying the User");
            if (permission == "y" || permission == "Y")
            {
                int reglen = (registerLength() + 1);
                File.AppendAllText(@"C:\Users\arjun\family.csv", Environment.NewLine);
                File.AppendAllText(@"C:\Users\arjun\family.csv", reglen.ToString() + ',' + fname + ',' + user_password);
                mailsendto(email);
                return true;
            }

            else
            {
                Console.WriteLine("Please Login with Authorized credentials ");
                Console.ReadLine();
                return false;
            } 
        }

        public static int registerLength()
        {
            string[] pdata = File.ReadAllLines(@"C:\Users\arjun\family.csv");

            List<string> newdata = new List<string>();

            foreach (string i in pdata)
            {
                newdata.Add(i);
            }
            newdata.Remove(string.Empty);
            return newdata.Count;
        }

        public static void mailsendto(string destination)
        {
            string[] rawcsv = File.ReadAllLines(@"C:\Users\arjun\Book1.csv");
            string[] Data = rawcsv[0].Split(',');
            string username = Data[0];
            string password = Data[1];
            try
            {
                using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
                {
                    client.EnableSsl = true;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(username, password);
                    MailMessage msgObj = new MailMessage();
                    msgObj.To.Add(destination);
                    msgObj.From = new MailAddress(username);
                    msgObj.Subject = "This is your Login Credientials";
                    msgObj.Body = "User_ID :- " + (registerLength() - 1).ToString();
                    client.Send(msgObj);

                }

            }
            catch
            {
                Console.WriteLine("Error while sending a mail");
                Console.ReadLine();
            }

        }


        public static string Hidepassword()
        {
            string Hiddenpassword = String.Empty;
            ConsoleKey a;
            do
            {
                var KeyInfo = Console.ReadKey(intercept: true);
                a = KeyInfo.Key;
                if (a == ConsoleKey.Backspace)
                {
                    Console.Write("\b \b");
                }

                else if (!char.IsControl(KeyInfo.KeyChar))
                {
                    Console.Write("*");
                    Hiddenpassword += KeyInfo.KeyChar;
                }
            } while (a != ConsoleKey.Enter);

            
            return Hiddenpassword;
        }

        public static bool checker(string a)
        {
            string[] rawcsv = File.ReadAllLines(@"C:\Users\arjun\family.csv");
            var ID = new List<String>();

            for (int i = 0; i < rawcsv.Length; i++)
            {
                string[] Data = rawcsv[i].Split(',');
                for (int j = 0; j < Data.Length; j++)
                {
                    ID.Add(Data[j]);
                }

            }

            if (ID.Contains(a))
            {
                return true;
                
            }

            else
            {
                return false;
            }

        } 

        public static void account()
        {
            string[] rawcsv = File.ReadAllLines(@"C:\Users\arjun\family.csv");
            List<string> ID = new List<String>();
            List<string> fname = new List<string>();
            List<string> pass = new List<string>();
            for (int i = 0; i < rawcsv.Length; i++)
            {
                string[] Data = rawcsv[i].Split(',');
                for (int j = 0; j < Data.Length; j += 3)
                {
                    ID.Add(Data[j]);
                }

                for (int k = 1; k < Data.Length; k += 3)
                {
                    fname.Add(Data[k]);
                }

                for (int m = 2; m < Data.Length; m += 3)
                {
                    pass.Add(Data[m]);
                }
            }

            Console.WriteLine("User_id :- ");
            string user_id = Console.ReadLine();
            int id = (int)long.Parse(user_id);
            Console.WriteLine("User_name :- ");
            string user_name = Console.ReadLine();
            Console.WriteLine("Password: ");
            string password = Hidepassword();

            if (id >= fname.Count())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid User_ID");
                Console.ResetColor();
            }
            else
            {
                if (user_name != fname[id])
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid User_Name");
                    Console.ResetColor();
                }

                else if (password != pass[id])
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid Password");
                    Console.ResetColor();
                }

                else if (user_name == fname[id] && password == pass[id])
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n");
                    Console.WriteLine("\t");
                    Console.WriteLine("You have logged in successfully");
                    Console.ResetColor();
                    Console.WriteLine("\n");
                    readwritepermission();

                }
            }
            
        }

        public static void readwritepermission()
        {
            Console.WriteLine("Read/Write/Exit");
            string read = Console.ReadLine();
            if (read == "r" || read == "R")
            {
                ReadingData();
                readwritepermission();
            }
            else if (read == "W" || read == "w")
            {
                AddingData();
                readwritepermission();
            }
            else if (read == "e" || read == "E")
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\n");
                Console.WriteLine("Logging out of the system");
                Console.ResetColor();
                return;
            }
        }

        public static void ReadingData()
        {
            string[] odata = File.ReadAllLines(@"C:\Users\arjun\testing.csv");
            List<string> newadd = new List<string>();
            for (int i = 0; i < odata.Length; i++)
            {
                newadd.Add(Decryption(odata[i], 1));

            }

            for (int j = 0; j < newadd.Count; j++)
            {

                string result = newadd[j].Replace('+', '*');
                Console.WriteLine(result);
                Console.ReadLine();
            }
        }

        public static void AddingData()
        {
            Console.WriteLine("Enter the Address");
            string address = Console.ReadLine();
            Console.WriteLine("Enter the Type of House");
            string type = Console.ReadLine();
            Console.WriteLine("Enter the Price");
            string price = Console.ReadLine();
            Console.WriteLine("Enter the seller");
            string seller = Console.ReadLine();
            File.AppendAllText(@"C:\Users\arjun\testing.csv", Environment.NewLine + Encryption(address, 1) + ',' + Encryption(type, 1) + ',' + Encryption(price, 1) + ',' + Encryption(seller, 1));


        }
        private static string Decryption(String Input, int otp)
        {
            string finalstring = "";
            char nextChar = ' ';
            foreach (char letter in Input)
            {
                if (letter == 'a')
                    nextChar = 'z';
                else if (letter == 'A')
                    nextChar = 'Z';
                else if (letter == ' ')
                    nextChar = ' ';
                else
                    nextChar = (char)(((int)(letter)) - otp);
                finalstring += nextChar;

            }

            return finalstring;
        }

        private static String Encryption(String Input, int otp)
        {
            string finalstring = "";
            char nextChar = ' ';
            foreach (char letter in Input)
            {
                if (letter == 'z')
                    nextChar = 'a';
                else if (letter == 'Z')
                    nextChar = 'A';
                else if (letter == ' ')
                    nextChar = ' ';
                else
                    nextChar = (char)(((int)(letter)) + otp);
                finalstring += nextChar;

            }

            return finalstring;

        }
        /*
        public static void ReadingData()
        {
            string[] odata = File.ReadAllLines(@"C:\Users\arjun\testing.csv");
            List<string> newadd = new List<string>();
            for (int i = 0; i < odata.Length; i++)
            {
                newadd.Add(Decryption(odata[i], 1));

            }

            for (int j = 0; j < newadd.Count; j++)
            {

                string result = newadd[j].Replace('+', '*');
                Console.WriteLine(result);
                Console.ReadLine();
            }
        }

        public static void AddingData()
        {
            Console.WriteLine("Enter the Address");
            string address = Console.ReadLine();
            Console.WriteLine("Enter the Type of House");
            string type = Console.ReadLine();
            Console.WriteLine("Enter the Price");
            string price = Console.ReadLine();
            Console.WriteLine("Enter the seller");
            string seller = Console.ReadLine();
            File.AppendAllText(@"C:\Users\arjun\testing.csv", Environment.NewLine + Encryption(address, 1) + ',' + Encryption(type, 1) + ',' + Encryption(price, 1) + ',' + Encryption(seller, 1));


        }*/
        /*
        public static int registerLength()
        {
            string[] pdata = File.ReadAllLines(@"C:\Users\arjun\family.csv");

            List<string> newdata = new List<string>();

            foreach (string i in pdata)
            {
                newdata.Add(i);
            }
            newdata.Remove(string.Empty);
            return newdata.Count;
        }

        public static void mailsendto(string destination)
        {
            string[] rawcsv = File.ReadAllLines(@"C:\Users\arjun\Book1.csv");
            string[] Data = rawcsv[0].Split(',');
            string username = Data[0];
            string password = Data[1];
            try
            {
                using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
                {
                    client.EnableSsl = true;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(username, password);
                    MailMessage msgObj = new MailMessage();
                    msgObj.To.Add(destination);
                    msgObj.From = new MailAddress(username);
                    msgObj.Subject = "This is your Login Credientials";
                    msgObj.Body = "User_ID :- " + (registerLength() - 1).ToString();
                    client.Send(msgObj);
                   
                }

            }
            catch
            {
                Console.WriteLine("Error while sending a mail");
                Console.ReadLine();
            }

        } */
    }   
}
