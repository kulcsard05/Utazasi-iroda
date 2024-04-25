public partial class Program {
    static List<Traveler> travelers = new List<Traveler>();
    static List<Path> paths = new List<Path>();

    static void AddTraveler (Traveler traveler)
    {
        travelers.Add(traveler);

        //index | name | home_address | phone_number
        string line = $"{travelers.IndexOf(traveler)} | {traveler.name} | {traveler.home_address} | {traveler.phone_number}";
        File.AppendAllText("travelers.txt", line + Environment.NewLine);
    }

    static void AddPath (Path path)
    {
        paths.Add(path);

        //index | destination | price | down_payment
        string line = $"{paths.IndexOf(path)} | {path.destination} | {path.price} | {path.down_payment}";
        File.AppendAllText("paths.txt", line + Environment.NewLine);
    }

    static void LoadData ()
    {
        //if travelers txt exists, load it if not create it
        if (File.Exists("travelers.txt"))
        {
            string[] lines = File.ReadAllLines("travelers.txt");

            foreach (string line in lines)
            {
                string[] data = line.Split(" | ");

                string name = data[1];
                string home_address = data[2];
                string phone_number = data[3];

                var traveler = new Traveler(name, home_address, phone_number);
                travelers.Insert(Convert.ToInt32(data[0]), traveler);
            }
        }
        else
        {
            File.Create("travelers.txt");
        }

        //if paths txt exists, load it if not create it
        if (File.Exists("paths.txt"))
        {
            string[] lines = File.ReadAllLines("paths.txt");

            foreach (string line in lines)
            {
                string[] data = line.Split(" | ");

                string destination = data[1];
                int price = Convert.ToInt32(data[2]);
                int down_payment = Convert.ToInt32(data[3]);

                var path = new Path(destination, price, down_payment, 0);
                
                if (data.Length > 4)
                {
                    string[] travelers = data[4].Split(",");
                    foreach (string index in travelers)
                    {
                        path.AddTraveler(Convert.ToInt32(index));
                    }
                }

                if (data.Length > 5)
                {
                    string[] paid_down_payment = data[5].Split(",");
                    foreach (string index in paid_down_payment)
                    {
                        path.AddPaidDownPayment(Convert.ToInt32(index));
                    }
                }

                paths.Insert(Convert.ToInt32(data[0]), path);
            }
        }
        else
        {
            File.Create("paths.txt");
        }
    }
}