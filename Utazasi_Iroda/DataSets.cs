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
    public int[] travelers;
    public int[] paid_down_payment;

    public Path (string destination, int price, int down_payment, int max_travelers)
    {
        this.destination = destination;
        this.price = price;
        this.down_payment = down_payment;
        this.travelers = new int[max_travelers];
        this.paid_down_payment = new int[max_travelers];
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
        for (int i = 0; i < this.travelers.Length; i++)
        {
            if (this.travelers[i] == 0)
            {
                this.travelers[i] = index;
                break;
            }
        }
    }

    public void AddPaidDownPayment (int index)
    {
        for (int i = 0; i < this.paid_down_payment.Length; i++)
        {
            if (this.paid_down_payment[i] == 0)
            {
                this.paid_down_payment[i] = index;
                break;
            }
        }
    }

    public void RemoveTraveler (int index)
    {
        for (int i = 0; i < this.travelers.Length; i++)
        {
            if (this.travelers[i] == index)
            {
                this.travelers[i] = 0;
                break;
            }
        }
    }
}
