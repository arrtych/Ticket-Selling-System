using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketSellingSystem
{
    public interface ITicketSellingMachine
    {

        String DepartureTime
        {
            get;
            set;
        }

        String FlightCode
        {
            get;
            set;
        }

        Double Price
        {
            get;
            set;
        }

        int NumberOfSeats
        {
            get;
            set;
        }

        // method for setting the base price and number of seats
        void setTicketsData(double price, int seats);

        //method for printing out info about all the sold tickets. 
        void printInfo();

        //method for writing info about all the tickets to .txt file.
        String putToFile();

        //method for printing out the number of free seats.
        int getFreeSeats();

        //method for selling the ticket
        void sellTicket(string name);

        //method for selling the ticket
        void sellTicket(string name, string buyingTime);

    }
}
