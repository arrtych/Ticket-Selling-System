# Ticket-Selling-System
Ticket selling system using OOP principles.
The system that sells tickets for a flight. Each flight has a fixed number of seats, base fare for the ticket and a
list of already sold tickets. There are 2 types of tickets (economy and business) which have slight
differences in how the price is calculated. Ticket machines sell (create) ticket objects.

Class <b>TicketSellingMachine:</b> has the general data about the flight and also is responsible for creating Ticket
objects and storing them. 

Class <b>Ticket:</b> represents the ticket object


<b>FlightCodes</b><br>
FlightCodes have a fixed structure:
1) The code for the operating company (length:2)
2) CityCode A (length:3)
3) CityCode B (length:3)
4) A number to show the direction: if the number is 2 it means the flight is from A->B, if the number
is 5 then the flight is from B->A

Destination and starting point for the flight based on the flight code
Some city codes:
TYO – Tokyo
TLL – Tallinn
BER - Berlin


<b>Ticket price</b><br>
There are multiple factors involved for setting the price. All of them make the base price x times more
expensive and should be checked every time a ticket is sold. The factors are checked and applied (if
necessary) to the base price in the same order as listed here.
1) Day of the flight:
If the flight is on Friday or Saturday, ticket is 15% more expensive.
2) How early is the ticket bought:
Price does not change when the ticket is sold 6 or more months before flight departure date.
With every next month the price increases (6-n)*0.1 times. If there is less than a month
remaining until the departure then the price increases 0.6 times.
For example: 2 months before the flight date the price increases (6-2)*0.1=0.4 times.
3) Occupancy rate (täituvus); how many of the available seats are sold out (if plane has 60 seats and
30 are sold out then occupancy rate is 30/60*100%=50%).
If 0-25% of the seats are sold out: it does not affect the price
If 26-50% of the seats are sold out: the price increases by 10%.
If 51-75% of the seats are sold out: the price increases by 13%.
If 76%-100% of the seats are sold out: the price increases by 17%.
 For business class ticket the weekday does not affect the price.
 Also the occupancy rates are double for business class (20% for 26-50%; 26% for 51%-75%
and 34% for 76%-100%)
Example: economy ticket with base price 100; bought 3 months in advance for Sunday when the plane is
70% full.
Friday price: 100*0.15 + 100=115
3 months price: 115*((6-3)*0.1) + 115 = 149.5
Occupancy rate price: 149.5 * 0.17 + 149.5 = 168.94
Final price is: 168.94

<b>Ticket type</b><br>
1- Business class ticket
2- Economy class ticket
