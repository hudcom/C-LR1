using System;
using System.Collections;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class MobileOperatorAccount
    {
        public enum TARIFF_NAME { Unlim3G, RedX, RedS, Light }
        private TARIFF_NAME tariff;

        private double costPerMinutes;
        private double fundsOnAccount;
        private String phoneNumber;

        public enum SERVICE_LIST { ExtraMinutes, ExtraInternet, UnlimitedInternet, FreeDay}
        private List<SERVICE_LIST> list = new List<SERVICE_LIST>();

        private static MobileOperatorAccount account = new MobileOperatorAccount();
        private static List<String> History = new List<string>();

        static void Main(string[] args)
        {
            bool run = true;
            Console.WriteLine("\t\t\tWelcome to 'Vodafone' mobile operator");
            while (run)
            {
                Console.WriteLine("\n\tChoose a service:\n1.Create an account\n2.Change the tariff\n3.Call\n4.Active service\n5.Disconnected service\n6.Replenishment of the account\n7.History of calls\n8.Show info");
                String i = Console.ReadLine();
                switch (Convert.ToInt32(i))
                {
                    case 0: { run = false; break; }
                    case 1: {
                            CreateAccount(); 
                            break; 
                        }
                    case 2:
                        {
                            TARIFF_NAME tempTariff  = ChooseTariff();
                            account.ChangeTariff(tempTariff);
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("Input number:");
                            string n = Console.ReadLine();
                            Console.WriteLine("Input minutes:");
                            string m = Console.ReadLine();
                            Console.WriteLine("The cost of the call was: " + account.Call(Convert.ToInt32(m), n));
                            break;
                        }
                    case 4:
                        {   
                            account.ActiveService(chooseService());
                            break;
                        }
                    case 5:
                        {
                            account.disconnectService(chooseService());
                            break;
                        }
                    case 6:
                        {
                            Console.WriteLine("Enter the recharge amount:");
                            string sum = Console.ReadLine();
                            BalanceReplenishment(Convert.ToInt32(sum));
                            break;
                        }
                    case 7:
                        {
                            ShowHistory();
                            break;
                        }
                    case 8: { ShowAccountInfo(); break; }
                }
            }
        }

        private static SERVICE_LIST chooseService()
        {
            Console.WriteLine("Choose a service:\n1.ExtraInternet\n2.ExtraMinutes\n3.FreeDay\n4.UnlimitedInternet");
            string num = Console.ReadLine();
            SERVICE_LIST tempService;
            switch (Convert.ToInt32(num))
            {
                case 1: tempService = SERVICE_LIST.ExtraInternet; break;
                case 2: tempService = SERVICE_LIST.ExtraMinutes; break;
                case 3: tempService = SERVICE_LIST.FreeDay; break;
                case 4: tempService = SERVICE_LIST.UnlimitedInternet; break;
                default: tempService = SERVICE_LIST.FreeDay; break;
            }
            return tempService;
        }

        private static void ShowAccountInfo()
        {
            Console.WriteLine("\tInfo about account:");
            Console.WriteLine("Phone number: "+account.phoneNumber);
            Console.WriteLine("Your tariff: "+account.tariff);
            Console.WriteLine("Balance: "+account.fundsOnAccount);
            Console.WriteLine("Cost per minutes: "+account.costPerMinutes);
            Console.Write("List service:");
            foreach(SERVICE_LIST s in account.list)
            {
                Console.Write(s.ToString()+" ");
            }
            Console.WriteLine("");
        }

        private static TARIFF_NAME ChooseTariff()
        {
            Console.WriteLine("Choose a tariff:\n1.Unlim3G\n2.RedX\n3.RedS\n4.Light");
            string num = Console.ReadLine();
            TARIFF_NAME tempTariff;
            switch (Convert.ToInt32(num))
            {
                case 1: tempTariff = TARIFF_NAME.Unlim3G; break;
                case 2: tempTariff = TARIFF_NAME.RedX; break;
                case 3: tempTariff = TARIFF_NAME.RedS; break;
                case 4: tempTariff = TARIFF_NAME.Light; break;
                default: tempTariff = TARIFF_NAME.Light; break;
            }
            return tempTariff;
        }

        public static void CreateAccount()
        {
            TARIFF_NAME tempTariff = ChooseTariff();
            

            string tempNumber = CreateNumber();
            Console.WriteLine("Your number is: " + tempNumber);
            Console.WriteLine("You are connected to the tariff " + tempTariff);
            Console.WriteLine("You have a bonus of 50 UAN");


            List<SERVICE_LIST> listService = new List<SERVICE_LIST>();
            account = new MobileOperatorAccount(tempTariff, tempNumber, 50, listService);
            Console.ReadLine();
        }

        private static String CreateNumber()
        {
            Random random = new Random();
            string number = "+38066";
            for(int i = 0; i < 8; i++)
            {
                number += random.Next(0, 9).ToString();
            }
            
            return number;
        }

        MobileOperatorAccount()
        {
            this.tariff = TARIFF_NAME.Light;
            this.costPerMinutes = tariffCheck(TARIFF_NAME.Light);
            this.fundsOnAccount = 10;
            this.phoneNumber = "+380000000000";
            list.Add(SERVICE_LIST.FreeDay);
        }

        MobileOperatorAccount(TARIFF_NAME _tariff, String _phoneNumber,double _fundsOnAccount, List<SERVICE_LIST> _list) 
        {
            this.tariff = _tariff;

            this.costPerMinutes = tariffCheck(_tariff);
            if (costPerMinutes == 0) Console.WriteLine("Error. Your cost per minutes 0. Please, try again later");

            this.fundsOnAccount = _fundsOnAccount;
            this.phoneNumber = _phoneNumber;
            for(int i = 0; i < _list.Count; i++)
            {
                list.Add(_list[i]);
            }
            
        }

        private double tariffCheck(TARIFF_NAME _tariff)
        {
            if (_tariff.ToString().Equals("Light")) return 0.1;
            else if (_tariff.ToString().Equals("RedS")) return 0.2;
            else if (_tariff.ToString().Equals("RedX")) return 0.3;
            else if (_tariff.ToString().Equals("Unlim3G")) return 0.25;
            else { 
                Console.WriteLine("Method tariffCheck returns 0");
                return 0; 
            }
        }

        public void ChangeTariff(TARIFF_NAME newTariff)
        {
            if (!(this.tariff.Equals(newTariff)))
            {
                this.tariff = newTariff;
                Console.WriteLine("Your new tariff is " + newTariff);
                this.costPerMinutes = tariffCheck(newTariff);
                if (costPerMinutes == 0) Console.WriteLine("Error. Your ost per minutes 0. Please, try again later");
            }
            else Console.WriteLine("It`s your tariff");
        }

        public double Call(int minutes, String number) 
        {
            Console.WriteLine("You called to " + number);
            History.Add(number + "\t" + minutes);
            Console.WriteLine("The call lasted  " + minutes);
            double cost = minutes * tariffCheck(this.tariff);
            this.fundsOnAccount -= cost;
            return cost;
        }

        public void ActiveService(SERVICE_LIST _service)
        {
            int sum = checkService(_service);
            if (this.fundsOnAccount >= sum)
            {
                Console.WriteLine("The service is connected");
                this.fundsOnAccount -= sum;
                list.Add(_service);
            }
            else Console.WriteLine("Not enough money on the account");
        }

        public int checkService(SERVICE_LIST _service)
        {
            if (_service.Equals(SERVICE_LIST.ExtraInternet))
            {
                Console.WriteLine("You want to connect the service Extra Internet it cost 20 UAN");
                return 20;
            }
            else if (_service.Equals(SERVICE_LIST.ExtraMinutes))
            {
                Console.WriteLine("You want to connect the service Extra Minutes it cost 10 UAN");
                return 10;
            }
            else if (_service.Equals(SERVICE_LIST.UnlimitedInternet))
            {
                Console.WriteLine("You want to connect the service Unlimited Internet it cost 80 UAN");
                return 80;
            }
            else if (_service.Equals(SERVICE_LIST.FreeDay))
            {
                Console.WriteLine("You want to connect the service Free Day it cost 0 UAN");
                return 0;
            }
            else return 0;

        }

        public void disconnectService(SERVICE_LIST _service)
        { 
            account.list.Remove(_service);
            Console.WriteLine("The service is disabled");
        }

       public static void BalanceReplenishment(int sum)
        {
            account.fundsOnAccount += sum;
            Console.WriteLine("Complete.");
        }

        public static void ShowHistory()
        {
            Console.WriteLine("\tHistory calls:");
            foreach (string str in History)
            {
                Console.WriteLine(str.ToString());
            }
        }
    }
}
