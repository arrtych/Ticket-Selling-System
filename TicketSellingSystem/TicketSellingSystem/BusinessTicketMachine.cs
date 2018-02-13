using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketSellingSystem
{
    public class BusinessTicketMachine : TicketSellingMachine
    {
        public int ticketType = 1;
        //constructor
        public BusinessTicketMachine(string code, string departureTime) : base(code, departureTime)
        {
            base.ticketFlightType = this.ticketType;
        }

        
    }
}
