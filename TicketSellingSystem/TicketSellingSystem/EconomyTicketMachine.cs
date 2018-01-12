using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketSellingSystem
{
    public class EconomyTicketMachine : TicketSellingMachine
    {
        public int ticketType = 2;

        //constructor
        public EconomyTicketMachine(string code, string departureTime) : base(code, departureTime)
        {
            base.ticketFlightType = this.ticketType;
        }
    }
}
