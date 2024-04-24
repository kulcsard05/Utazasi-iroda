/*
1. Utasok kezelése.
    a. Lehessen felvenni és tárolni az utasok adatait: név, cím, telefonszám
    b. Utas adatainak módosítása utólag is megoldható legyen.
2. Utazások adatainak kezelése.
    a. Új út felvétele: úticél, ár, maximális létszám
3. Utazásokra jelentkezés.
    a. Adott utas jelentkezése adott útra.
    b. Befizetett előleg nyilvántartása.
    c. Az előleg módosítható legyen, de nem lehet több, mint az út ára.
    d. Utaslista nyomtatás állományba
*/

using System.Text.RegularExpressions;

class Office
{
    //list of travelers
    public List<Traveler> travelers = new List<Traveler>();

    //list of paths
    public List<Path> paths = new List<Path>();

    public void AddTraveler (Traveler traveler)
    {
        travelers.Add(traveler);
    }

    public void AddPath (Path path)
    {
        paths.Add(path);
    }

    public void PrintTravelers ()
    {
        foreach (Traveler traveler in travelers)
        {
            
            Console.WriteLine($"{travelers.IndexOf(traveler)} | Name: {traveler.name} Home address: {traveler.home_address} Phone number: {traveler.phone_number}");
        }
    }

    public void PrintPaths ()
    {
        foreach (Path path in paths)
        {
            Console.WriteLine();
            Console.WriteLine($"{paths.IndexOf(path)} | Destination: {path.destination} Price: {path.price} Down payment: {path.down_payment}");
        }
    }

    public void PrintTravelersToFile ()
    {
        StreamWriter sw = new StreamWriter("travelers.txt");

        foreach (Traveler traveler in travelers)
        {
            sw.WriteLine("Name: " + traveler.name);
            sw.WriteLine("Home address: " + traveler.home_address);
            sw.WriteLine("Phone number: " + traveler.phone_number);
            // melyik utra fizettek be :
            sw.WriteLine();
        }

        sw.Close();
    }

    public void PrintPathsToFile ()
    {
        StreamWriter sw = new StreamWriter("paths.txt");

        foreach (Path path in paths)
        {
            sw.WriteLine("Destination: " + path.destination);
            sw.WriteLine("Price: " + path.price);
            sw.WriteLine("Down payment: " + path.down_payment);
            sw.WriteLine();
        }

        sw.Close();
    }

    public Menu menu = new Menu();
}

class Menu 
{
    string GetInput(string request, string input_type, out string input)
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

    public void MainMenu (Office office)
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
                TravelerMenu(office);
                break;
            case 2:
                PathMenu();
                break;
            case 3:
                office.PrintTravelers();
                break;
            case 4:
                office.PrintPaths();
                break;
            case 5:
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Invalid option!");
                break;
        }
    }

    public void TravelerMenu (Office office)
    {
        Console.Clear();

        string name = "";
        string home_address = "";
        string phone_number = "";

        GetInput("Name: ", "string", out name);
        GetInput("Home address: ", "string", out home_address);
        GetInput("Phone number: ", "string", out phone_number);


        office.AddTraveler(new Traveler(name, home_address, phone_number));
    }

    public void PathMenu ()
    {
        Console.WriteLine("1. Change destination");
        Console.WriteLine("2. Change price");
        Console.WriteLine("3. Change down payment");
        Console.WriteLine("4. Add traveler");
        Console.WriteLine("5. Add paid down payment");
    }
}

struct Traveler
{
    public string name;
    public string home_address;
    public string phone_number;

    public Traveler (string name, string home_address, string phone_number)
    {
        this.name = name;
        this.home_address = home_address;
        this.phone_number = phone_number;
    }

    public void ChangeName (string name)
    {
        this.name = name;
    }

    public void ChangeHomeAddress (string home_address)
    {
        this.home_address = home_address;
    }

    public void ChangePhoneNumber (string phone_number)
    {
        this.phone_number = phone_number;
    }
}

struct Path
{
    public string destination;
    public int price;
    public int down_payment;
    public Traveler[] travelers;
    public Traveler[] paid_down_payment;

    public Path (string destination, int price, int down_payment, int max_travelers)
    {
        this.destination = destination;
        this.price = price;
        this.down_payment = down_payment;
        this.travelers = new Traveler[max_travelers];
        this.paid_down_payment = new Traveler[max_travelers];
    }

    public void AddTraveler (Traveler traveler)
    {
        for (int i = 0; i < travelers.Length; i++)
        {
            if (travelers[i].name == null)
            {
                travelers[i] = traveler;
                break;
            }
        }
    }

    public void AddPaidDownPayment (Traveler traveler)
    {
        for (int i = 0; i < paid_down_payment.Length; i++)
        {
            if (paid_down_payment[i].name == null)
            {
                paid_down_payment[i] = traveler;
                break;
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Office office = new Office();

        while (true)
        {
            office.menu.MainMenu(office);
        }
    }
}
