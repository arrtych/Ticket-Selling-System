using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketSellingSystem
{
    public class TicketSellingMachine : ITicketSellingMachine
    {

        protected string departureTime;
        protected string flightCode;
        protected double price;
        protected int numberOfSeats;
        protected int ticketFlightType;

        public  int ConstnumberOfTickets = 0;

        //list of all tickets before sale
        private List<Ticket> soldTickets = new List<Ticket>();
        //list of sold tickets
        private List<Ticket> allTickets = new List<Ticket>();
        //list of cities codes
        public Dictionary<string, string> citiesCodes = new Dictionary<string, string>()
            {
                {"TYO","Tokyo"},
                {"TLL","Tallinn"},
                {"MSC","Moscow" },
                {"RGA","Riga"},
                {"NRV","Narva"},
                {"HKG","Hong Kong"},
                {"LOA","Los Angeles"},
                {"BER","Berlin"}
            };


        // constructor
        public TicketSellingMachine(string code, string departureTime)
        {
            this.departureTime = departureTime;
            this.flightCode = code;
        }

        public String DepartureTime
        {
            get { return departureTime; }
            set {departureTime = value; }
        }

        public String FlightCode
        {
            get { return flightCode; }
            set{ flightCode = value; }
        }

        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        public int NumberOfSeats
        {
            get { return numberOfSeats; }
            set { numberOfSeats = value; }
        }

        public int FlightTicketType
        {
            get { return ticketFlightType; }
            set { ticketFlightType = value; }
        }


        //method for checking flighcode
        //return pair of cities From and To
        public KeyValuePair<string, string> ParseFlightCode(string code)
        {
            
            string from = code.Substring(2, 3);
            string to = code.Substring(5, 3);
            //if the number is 5 then the flight is from B->A
            if (code[8].Equals('5'))
            {
                string temp = from;
                from = to;
                to = temp;
            }
            //if (test.Last().Equals('b'))
            //{
            //    Console.WriteLine("Business class");
            //}
            //(Business class tickets can only be sold when the flight code ends with „b“.) Test it!
            string fromCity = "", toCity = "";
            foreach (KeyValuePair<string, string> pair in citiesCodes)
            {
                if (pair.Key.ToString() == from)
                {
                    fromCity = pair.Value.ToString();
                }
                if (pair.Key.ToString() == to)
                {
                    toCity = pair.Value.ToString();
                }

               
            }
            return new KeyValuePair<string, string>(fromCity, toCity);
        }

        //method for setting the base price and number of seats
        // Also generates number of tickets by seats parameter
        public void setTicketsData(double price, int seats)
        {
            this.price = price;
            this.numberOfSeats = seats;
            ConstnumberOfTickets = numberOfSeats;
            for (int i = 1; i<= numberOfSeats; i++){
                Ticket t = new Ticket();
                t.seatNumber = i;
                t.price = price;
                allTickets.Add(t);
            }
        }


        //put info about sold tickets to .txt file
        public string putToFile()
        {
            string result = "";
            foreach (Ticket t in soldTickets)
            {
                result += t.printInfo();
            }
            System.IO.File.AppendAllText("output.txt", result);
            return result;
        }

        public void printInfo()
        {
            string result = "";
            foreach (Ticket t in soldTickets)
            {
                result += t.printInfo();
            }
            Console.WriteLine(result);
        }

        public void sellTicket(string name)
        {
            try
            {
                string now = DateTime.Now.ToString("yyyy-MM-dd");
                //create ticket objects for selling
                Ticket t = new Ticket();

                //get Cities form flight code
                var pair = ParseFlightCode(this.flightCode);
                string from = pair.Key;
                string to = pair.Value;
                //set tickets data
                t.passengerName = name;
                t.fromPoint = from;
                t.toPoint = to;
                t.seatNumber = this.numberOfSeats;
                t.buyingTime = now;

                double newPrice = calculatePrice(t.buyingTime);
                t.price = newPrice;
                t.type = this.ticketFlightType;
                t.flightDate = this.departureTime;

                //remove sold ticket from list of available tickets
                allTickets.RemoveAt(t.seatNumber - 1);
                //decrease number of available seats
                numberOfSeats -= 1;
                //add to list of sold tickets
                soldTickets.Add(t);
            }
            catch(ArgumentOutOfRangeException )
            {               
                Console.WriteLine("Tickets are not available anymore. Was sold "+this.soldTickets.Count() + " tickets");
            }
 

        }

        public void sellTicket(string name, string buyingTime)
        {
            try
            {
                //create ticket objects for selling
                Ticket t = new Ticket();

                //get Cities form flight code
                var pair = ParseFlightCode(this.flightCode);
                string from = pair.Key;
                string to = pair.Value;
                //set tickets data
                t.passengerName = name;
                t.fromPoint = from;
                t.toPoint = to;
                t.seatNumber = this.numberOfSeats;
                t.buyingTime = buyingTime;

                double newPrice = calculatePrice(t.buyingTime);
                t.price = newPrice;
                t.type = this.ticketFlightType;
                t.flightDate = this.departureTime;

                //remove sold ticket from list of available tickets
                allTickets.RemoveAt(t.seatNumber - 1);
                //decrease number of available seats
                numberOfSeats -= 1;
                //add to list of sold tickets
                soldTickets.Add(t);
            }
            catch (ArgumentOutOfRangeException )
            {
                Console.WriteLine("Tickets are not available anymore. Was sold " + this.soldTickets.Count() + " tickets");
            }
           
        }

        // Method for calculating price with multiple factors
        public double calculatePrice(string date)
        {
            double result = this.price;
            DateTime buyingTime = Convert.ToDateTime(date);
            DateTime flightDate = Convert.ToDateTime(this.departureTime);
            DayOfWeek fDay = flightDate.DayOfWeek;

            //difference between buyingDate and flightDate
            double diff = flightDate.Subtract(buyingTime).Days / (365.25 / 12);
            diff = Math.Round(diff, 0);

            //only for Economy class ticket
            if (this.ticketFlightType == 2)
            {
                //If the flight is on Friday or Saturday, ticket is 15% more expensive.
                if (fDay == DayOfWeek.Friday || fDay == DayOfWeek.Saturday)
                {
                    result += this.price * 0.15;
                }
            }
            // if ticket is sold 6 or more months before flight departure date.
            if (flightDate.AddMonths(-6) > buyingTime)
            {
                result +=0;
            }

            // if difference between Flight and Buying dates less than 6 months
            //every next month the price increases (6-n)*0.1 times.
            if (diff < 6)
            {
                result = result * ((6 - diff) * 0.1) + result;
            }

            double priceByOccupancy = checkPlaneOccupancy();
            result += result * priceByOccupancy;
           // Console.WriteLine("r -" + getRate() +" occupancy-"+ priceByOccupancy + " curr soldtick-"+soldTickets.Count() + " curr res: "+ result);

            return result;
        }

        //sub method for checking how many of the available seats are sold out
        //return rate in % 
        public double getRate()
        {
            double st = soldTickets.Count();
            double alt = ConstnumberOfTickets;
            double rate = (st / alt) * 100;
            rate = Math.Round(rate, 2);
            return rate;
        }

        //method for check  how increases ticket price by occupancy rate
        public double checkPlaneOccupancy()
        {
           double result = 0;
           double rate = getRate();
            // for business classs
            if (this.ticketFlightType == 1)
            {
                if (rate >= 0 && rate <= 25)
                {
                    result = 0;
                }
                if (rate >= 26 && rate <= 50)
                {
                    result =  0.2;
                }
                if (rate >= 51 && rate <= 75)
                {
                    result =  0.26;
                }
                if (rate >= 76 && rate <= 100)
                {
                    result =  0.34;
                }
            }
            else if (this.ticketFlightType == 2)
            {
                if (rate >= 0 && rate <= 25)
                {
                    result = 0;
                }
                if (rate >= 26 && rate <= 50)
                {
                    result =  0.1;
                }
                if (rate >= 51 && rate <= 75)
                {
                    result =  0.13;
                }
                if (rate >= 76 && rate <= 100)
                {
                    result =  0.17;
                }
            }
            
            return result;
        }

        //get of available tickets
        public int getFreeSeats()
        {
            return allTickets.Count();
        }


        //additional method for testing plane occupancy rate
        public int setNumberOfSoldTickets(int count)
        {
            for (int i = 1; i <= count; i++)
            {
                Ticket t = new Ticket();
                soldTickets.Add(t);
            }
            count = soldTickets.Count();
            return count;
        }



    }
}
