using System.Text.RegularExpressions;

public partial class Program {
    static string GetInput(string request, string input_type, out string input)
    {
        Console.Write(request);
        string userInput = Console.ReadLine();

        // check for the input types: string, int
        if (input_type == "string")
        {
            //check if input only contains letters
            if (Regex.IsMatch(userInput, @"^[a-zA-Z]+$"))
            {
                input = userInput;
            }
            else
            {
                input = "0";
            }
        }
        else if (input_type == "int")
        {
            //check if input only contains numbers
            if (Regex.IsMatch(userInput, @"^[0-9]+$"))
            {
                input = userInput;
            }
            else
            {
                input = "0";
            }
        }
        else // input type is invalid
        {
            input = "0";
        }

        return input;
    }

    static string GetOption (string request, string[] options)
    {
        Console.WriteLine(request);

        for (int i = 0; i < options.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {options[i]}");
        }

        Console.WriteLine();

        string option = "";
        while(true) 
        {
            string input;
            string _input = GetInput("Choose an option: ", "int", out input);

            if (input != "0")
            {
                int _option = Convert.ToInt32(input);

                if (_option > 0 && _option <= options.Length)
                {
                    option = options[_option - 1];
                    break;
                }
            }

            Console.WriteLine("Invalid option!");
        }

        return option;
    }

    static void MainMenu ()
    {
        Console.Clear();
        Console.WriteLine("1. Add new traveler");
        Console.WriteLine("2. Add new path");
        Console.WriteLine("3. Travelers");
        Console.WriteLine("4. Paths");
        Console.WriteLine("5. Exit");

        Console.WriteLine();
        
        int option = 0;
        while(true) 
        {
            string input;
            string _option = GetInput("Choose an option: ", "int", out input);

            if (input != "0")
            {
                option = Convert.ToInt32(input);
                break;
            }
        }

        switch (option)
        {
            case 1:
                AddTravelerMenu();
                break;
            case 2:
                AddPathMenu();
                break;
            case 3:
                TravelerMenu();
                break;
            case 4:
                PathMenu();
                break;
            case 5:
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Invalid option!");
                break;
        }
    }

    static void AddTravelerMenu ()
    {
        Console.Clear();

        Console.WriteLine("Name: ");
        string name = Console.ReadLine();

        Console.WriteLine("Home address: ");
        string home_address = Console.ReadLine();

        Console.WriteLine("Phone number: ");
        string phone_number = Console.ReadLine();

        Console.Clear();

        foreach (Path path in paths)
        {
            Console.WriteLine();
            Console.WriteLine("Paths:");
            Console.WriteLine($"{paths.IndexOf(path)} | Destination: {path.destination} Price: {path.price} Down payment: {path.down_payment}");
        }

        int id = 0;

        while(true)
        {
            string input;

            Console.WriteLine("Which path do you want to add the traveler to? (q:exit): ");
            string option = Console.ReadLine();

            if (option == "q")
            {
                break;
            }
            else if (Regex.IsMatch(option, @"^[0-9]+$"))
            {
                id = Convert.ToInt32(option);
                break;
            }
        }

        paths[id].AddTraveler(travelers.Count);

        string paid_down_payment = GetOption("Did they pay down payment?", new string[] { "Yes", "No" });

        if (paid_down_payment == "Yes")
        {
            paths[id].AddPaidDownPayment(travelers.Count);
        }

        var traveler = new Traveler(name, home_address, phone_number);
        AddTraveler(traveler);
    }

    static void AddPathMenu()
    {
        Console.Clear();

        string destination = "";
        string price = "";
        string down_payment = "";
        string max_travelers = "";

        GetInput("Destination: ", "string", out destination);

        int _price = 0;
        while(true) 
        {
            string input;
            string _price_input = GetInput("Price: ", "int", out input);

            if (input != "0")
            {
                _price = Convert.ToInt32(input);
                break;
            }
        }

        int _down_payment = 0;
        while(true) 
        {
            string input;
            string _down_payment_input = GetInput("Down payment: ", "int", out input);

            if (input != "0")
            {
                _down_payment = Convert.ToInt32(input);
                break;
            }
        }

        int _max_travelers = 0;
        while(true) 
        {
            string input;
            string _max_travelers_input = GetInput("Max travelers: ", "int", out input);

            if (input != "0")
            {
                _max_travelers = Convert.ToInt32(input);
                break;
            }
        }

        AddPath(new Path(destination, _price, _down_payment, _max_travelers));
    }

    static void TravelerMenu ()
    {
        Console.Clear();

        if (travelers.Count == 0)
        {
            Console.WriteLine("No travelers found!");
            Console.ReadLine();
            return;
        }

        foreach (Traveler traveler in travelers)
        {
            Console.WriteLine();
            Console.WriteLine("Travelers:");
            Console.WriteLine($"{travelers.IndexOf(traveler)} | Name: {traveler.Name} | Home address: {traveler.Home_address} | Phone number: {traveler.Phone_number}");
        }

        int id = 0;
        bool exit = false;
        while(true) 
        {
            string input;

            Console.WriteLine("Which traveler do you want to change? (q:exit): ");
            string option = Console.ReadLine();

            if (option == "q")
            {
                exit = true;
                break;
            }
            else if (Regex.IsMatch(option, @"^[0-9]+$"))
            {
                id = Convert.ToInt32(option);
                break;
            }
        }
        
        if (!exit)
        {
            Console.WriteLine();
            Console.WriteLine("1. Change name");
            Console.WriteLine("2. Change home address");
            Console.WriteLine("3. Change phone number");
            Console.WriteLine("4. Add to path");
            Console.WriteLine("5. Pay down payment");

            int option2 = 0;
            while(true) 
            {
                string input;
                string _option = GetInput("Choose an option: ", "int", out input);

                if (input != "0")
                {
                    option2 = Convert.ToInt32(input);
                    break;
                }
            }

            var traveler = travelers[id];
            System.Console.WriteLine(traveler.Name);
            Console.ReadLine();

            switch (option2)
            {
                case 1:
                    string name = "";
                    string _name = GetInput("Name: ", "string", out name);
                    
                    traveler.Name = name;
                    break;
                case 2:
                    string home_address = "";
                    string _home_address = GetInput("Home address: ", "string", out home_address);

                    traveler.Home_address = home_address;
                    break;
                case 3:
                    string phone_number = "";
                    string _phone_number = GetInput("Phone number: ", "string", out phone_number);

                    traveler.Phone_number = phone_number;
                    break;
                case 4:
                    foreach (Path path in paths)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Paths:");
                        Console.WriteLine($"{paths.IndexOf(path)} | Destination: {path.destination} Price: {path.price} Down payment: {path.down_payment}");
                    }

                    int id2 = 0;

                    while(true)
                    {
                        string input;

                        Console.WriteLine("Which path do you want to add the traveler to? (q:exit): ");
                        string option = Console.ReadLine();

                        if (option == "q")
                        {
                            break;
                        }
                        else if (Regex.IsMatch(option, @"^[0-9]+$"))
                        {
                            id2 = Convert.ToInt32(option);
                            break;
                        }
                    }

                    paths[id2].AddTraveler(id);

                    string paid_down_payment = GetOption("Did they pay down payment?", new string[] { "Yes", "No" });

                    if (paid_down_payment == "Yes")
                    {
                        paths[id2].AddPaidDownPayment(id);
                    }

                    break;
                case 5:
                    foreach (Path path in paths)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Paths:");
                        Console.WriteLine($"{paths.IndexOf(path)} | Destination: {path.destination} Price: {path.price} Down payment: {path.down_payment}");
                    }

                    int id3 = 0;

                    while(true)
                    {
                        string input;

                        Console.WriteLine("Which path do you want to pay down payment for? (q:exit): ");
                        string option = Console.ReadLine();

                        if (option == "q")
                        {
                            break;
                        }
                        else if (Regex.IsMatch(option, @"^[0-9]+$"))
                        {
                            id3 = Convert.ToInt32(option);
                            break;
                        }
                    }

                    paths[id3].AddPaidDownPayment(id);
                    break;
                default:
                    Console.WriteLine("Invalid option!");
                    break;
            }
        }
    }

    static void PathMenu ()
    {
        Console.Clear();
        //if paths list is empty
        if (paths.Count == 0)
        {
            Console.WriteLine("No paths found!");
            Console.ReadLine();
            return;
        }
        
        foreach (Path path in paths)
        {
            Console.WriteLine();
            Console.WriteLine("Travelers:");
            Console.WriteLine($"{paths.IndexOf(path)} | Destination: {path.destination} Price: {path.price} Down payment: {path.down_payment}");
        }
        
        int id = 0;
        bool exit = false;
        while(true) 
        {
            string input;

            Console.WriteLine("Which path do you want to change? (q:exit): ");
            string option = Console.ReadLine();

            if (option == "q")
            {
                exit = true;
                break;
            }
            else if (Regex.IsMatch(option, @"^[0-9]+$"))
            {
                id = Convert.ToInt32(option);
                break;
            }
        }

        if (!exit)
        {
            Console.WriteLine();
            Console.WriteLine("1. Change destination");
            Console.WriteLine("2. Change price");
            Console.WriteLine("3. Change down payment");

            int option2 = 0;
            while(true) 
            {
                string input;
                string _option = GetInput("Choose an option: ", "int", out input);

                if (input != "0")
                {
                    option2 = Convert.ToInt32(input);
                    break;
                }
            }

            var path = paths[id];

            switch (option2)
            {
                case 1:
                    string destination = "";
                    string _destination = GetInput("Destination: ", "string", out destination);

                    path.ChangeDestination(destination);
                    
                    break;
                case 2:
                    int price = 0;

                    while(true) 
                    {
                        string input;
                        string _price_input = GetInput("Price: ", "int", out input);

                        path.ChangePrice(price);
                        
                        if (input != "0")
                        {
                            price = Convert.ToInt32(input);
                            break;
                        }
                    }

                    break;
                case 3:
                    int down_payment = 0;

                    while(true) 
                    {
                        string input;
                        string _down_payment_input = GetInput("Down payment: ", "int", out input);

                        path.ChangeDownPayment(down_payment);

                        if (input != "0")
                        {
                            down_payment = Convert.ToInt32(input);
                            break;
                        }
                    }


                    break;
                default:
                    Console.WriteLine("Invalid option!");
                    break;
            }
        }
    }
}