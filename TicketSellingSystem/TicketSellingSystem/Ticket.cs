using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketSellingSystem
{
    class Ticket
    {
        public string passengerName;
        public string flightDate;
        public string fromPoint;
        public string toPoint;
        public double price;
        public int type;//1 -business,2 -economy
        public int seatNumber;
        public string buyingTime;

        //constructor
        public Ticket(){ }

        public string printInfo()
        {
            string result = "";
            string nl = "\n";

            result += "\n-------------------------------" + "\n\tSold ticket" +
                nl + "Name: " + this.passengerName +
                nl + "Flight date: " + this.flightDate +
                nl + "Price: " + this.price + " euros" +
                nl + "From: " + this.fromPoint + " to " + this.toPoint +
                nl + "Ticket type: " + this.type +
                nl + "Seat number: " + this.seatNumber +
                //nl + "Buying time: " + this.buyingTime +
                nl + "Have a nice flight!";
            return result;
        }



    }
}
