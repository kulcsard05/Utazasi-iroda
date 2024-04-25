using System.IO;

struct Traveler
{
    public string Name { get; set;}
    public string Home_address { get; set;}
    public string Phone_number { get; set;}

    public Traveler (string name, string home_address, string phone_number)
    {
        Name = name;
        Home_address = home_address;
        Phone_number = phone_number;
    }

    public void ChangeName (string name)
    {
        Name = name;
    }

    public void ChangeHomeAddress (string home_address)
    {
        Home_address = home_address;
    }

    public void ChangePhoneNumber (string phone_number)
    {
        Phone_number = phone_number;
    }
}

struct Path
{
    public string destination;
    public int price;
    public int down_payment;
    public int[] travelers;
    public int[] paid_down_payment;

    public Path (string destination, int price, int down_payment, int max_travelers)
    {
        this.destination = destination;
        this.price = price;
        this.down_payment = down_payment;
        travelers = new int[max_travelers];
        paid_down_payment = new int[max_travelers];
    }

    public void ChangeDestination (string destination)
    {
        this.destination = destination;
    }

    public void ChangePrice (int price)
    {
        this.price = price;
    }

    public void ChangeDownPayment (int down_payment)
    {
        this.down_payment = down_payment;
    }

    public void AddTraveler (int index)
    {
        for (int i = 0; i < travelers.Length; i++)
        {
            if (travelers[i] == 0)
            {
                travelers[i] = index;
                break;
            }
        }

        //save it to file
        //find the line with the index of this path
        // index | destination | price | down_payment | [travelers]
        // travelers would look like this [1, 4, 5] in the file 
        string[] lines = File.ReadAllLines("paths.txt");
        for (int i = 0; i < lines.Length; i++)
        {
            string[] data = lines[i].Split(" | ");
            if (Convert.ToInt32(data[0]) == index)
            {
                // if there is a 4th element in the data array get the travelers array from it and add the index to it
                if (data.Length > 4)
                {
                    string[] travelers = data[4].Split(",");
                    for (int j = 0; j < travelers.Length; j++)
                    {
                        if (Convert.ToInt32(travelers[j]) == 0)
                        {
                            travelers[j] = index.ToString();
                            break;
                        }
                    }
                }
                else
                {
                    lines[i] += "," + index;
                }

                break;
            }
        }
    }

    public void AddPaidDownPayment (int index)
    {
        for (int i = 0; i < paid_down_payment.Length; i++)
        {
            if (paid_down_payment[i] == 0)
            {
                paid_down_payment[i] = index;
                break;
            }
        }

        //save it to file
        //find the line with the index of this path
        // index | destination | price | down_payment | [travelers] | [paid_down_payment]
        // paid_down_payment would look like this [1, 4, 5] in the file
        string[] lines = File.ReadAllLines("paths.txt");
        for (int i = 0; i < lines.Length; i++)
        {
            string[] data = lines[i].Split(" | ");
            if (Convert.ToInt32(data[0]) == index)
            {
                // if there is a 5th element in the data array get the paid_down_payment array from it and add the index to it
                if (data.Length > 5)
                {
                    string[] paid_down_payment = data[5].Split(",");
                    for (int j = 0; j < paid_down_payment.Length; j++)
                    {
                        if (Convert.ToInt32(paid_down_payment[j]) == 0)
                        {
                            paid_down_payment[j] = index.ToString();
                            break;
                        }
                    }
                }
                else
                {
                    lines[i] += "," + index;
                }

                break;
            }
        }
    }

    public void RemoveTraveler (int index)
    {
        for (int i = 0; i < travelers.Length; i++)
        {
            if (travelers[i] == index)
            {
                travelers[i] = 0;
                break;
            }
        }
    }
}
