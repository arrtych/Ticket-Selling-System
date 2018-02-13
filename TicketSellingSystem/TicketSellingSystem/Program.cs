using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketSellingSystem
{
    class Program
    {

        static void Main(string[] args)
        {

            int ticketsNumber = 60;
            //object with random data for creating ticket machines
            RandomData rd = new RandomData();
            Random rand = new Random();

//----------First ticket machine
            int indexCodes = rand.Next(rd.dates.Count());
            string code = rd.codes[indexCodes];
            Console.WriteLine("\n\tTicket machine 1");
            BusinessTicketMachine btm = new BusinessTicketMachine(code, "2018-06-07");
            btm.setTicketsData(100, 100);
            for (int i = 1; i <= ticketsNumber; i++)
            {
                //get random buying date
                int indexDate = rand.Next(rd.dates.Count());
                string date = rd.dates[indexDate];
                //get random buyers name
                int indexName = rand.Next(rd.names.Count());
                string name = rd.names[indexName];

                btm.sellTicket(name, date);
            }
            btm.printInfo();
            btm.putToFile();
            Console.WriteLine("\nFree seets count  btm: " + btm.getFreeSeats());

            ////----------Second ticket machine
            int indexCodes1 = rand.Next(rd.dates.Count());
            string code1 = rd.codes[indexCodes1];
            Console.WriteLine("\n\tTicket machine 2");
            EconomyTicketMachine etm = new EconomyTicketMachine(code1, "2018-06-07");
            etm.setTicketsData(100, 100);
            for (int i = 1; i <= ticketsNumber; i++)
            {
                //get random buying date
                int indexDate = rand.Next(rd.dates.Count());
                string date = rd.dates[indexDate];
                //get random buyers name
                int indexName = rand.Next(rd.names.Count());
                string name = rd.names[indexName];

                etm.sellTicket(name, date);
            }
            etm.printInfo();
            etm.putToFile();
            Console.WriteLine("\nFree seets count  etm: " + etm.getFreeSeats());

            ////----------Third ticket machine. If soldTickets are more than available tickets
           
            Console.WriteLine("\n\tTicket machine 3");
            //get random flight date
            int indexDate1 = rand.Next(rd.dates.Count());
            string date1 = rd.dates[indexDate1];
            int indexCodes2 = rand.Next(rd.dates.Count());
            string code2 = rd.codes[indexCodes2];
            EconomyTicketMachine etm1 = new EconomyTicketMachine(code2, date1);
            //available tickets 30
            etm1.setTicketsData(100, 30);
            for (int i = 1; i <= ticketsNumber; i++)
            {
                //get random buying date
                int indexDate = rand.Next(rd.dates.Count());
                string date = rd.dates[indexDate];
                //get random buyers name
                int indexName = rand.Next(rd.names.Count());
                string name = rd.names[indexName];

                //buying time is now
                etm1.sellTicket(name);
            }
            etm1.printInfo();
            etm1.putToFile();
            Console.WriteLine("\nFree seets count  etm1: " + etm1.getFreeSeats());

            Console.ReadKey();
        }
    }
}
