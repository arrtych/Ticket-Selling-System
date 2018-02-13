using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TicketSellingSystem.Test
{
    [TestClass]
    public class TicketSellingMachineTest
    {
        //DateDifference => Difference between Flight and Buying dates

        [TestMethod]
        public void CalculatePriceIfFlightDayIsFridayAndDateDifferenceIsMore_6_Months()
        {
            //arrange
            string flightDate = "2018-09-08";
            EconomyTicketMachine etm = new EconomyTicketMachine("EALOAHKG2b", flightDate);
            double expectedPrice = 115;
            etm.Price = 100;

            //actual
            double actual = etm.calculatePrice("2018-01-01");

            //assert
            Assert.AreEqual(expectedPrice, actual);
        }

        [TestMethod]
        public void CalculatePriceIfFlightDayIsSaturdayAndDateDifferenceIs_3_Months()
        {
            //arrange
            string flightDate = "2018-04-14";
            EconomyTicketMachine etm = new EconomyTicketMachine("EALOAHKG2b", flightDate);
            double expectedPrice = 149.5;
            etm.Price = 100;

            //actual
            double actual = etm.calculatePrice("2018-01-01");

            //assert
            Assert.AreEqual(expectedPrice, actual);
        }


        [TestMethod]
        public void CalculatePriceIfDateDifferenceIsLess_6_Months()
        {
            //arrange
            string flightDate = "2018-05-03";
            EconomyTicketMachine etm = new EconomyTicketMachine("EALOAHKG2b", flightDate);
            double expectedPrice = 120;
            etm.Price = 100;

            //actual
            double actual = etm.calculatePrice("2018-01-01");

            //assert
            Assert.AreEqual(expectedPrice, actual);
        }

        [TestMethod]
        public void CalculatePriceIfDateDifferenceIsMore_6_Months()
        {
            //arrange
            string flightDate = "2018-10-11";
            EconomyTicketMachine etm = new EconomyTicketMachine("EALOAHKG2b", flightDate);
            double expectedPrice = 100;
            etm.Price = 100;

            //actual
            double actual = etm.calculatePrice("2018-01-01");

            //assert
            Assert.AreEqual(expectedPrice, actual);
        }


        [TestMethod]
        public void CalculatePriceIfFlightDayIsSaturdayOnBusinessClass()
        {
            //arrange
            string flightDate = "2018-03-03";
            BusinessTicketMachine etm = new BusinessTicketMachine("EALOAHKG2b", flightDate);
            double expectedPrice = 150;
            etm.Price = 100;

            //actual
            double actual = etm.calculatePrice("2018-02-03");

            //assert
            Assert.AreEqual(expectedPrice, actual);
        }


        [TestMethod]
        public void CalculatePriceIfBuyingDateIs_3_MonthsBeforeFlight()
        {
            //arrange
            string flightDate = "2018-04-04";
            string buyingDate = "2018-01-01";
            BusinessTicketMachine btm = new BusinessTicketMachine("EALOAHKG2b", flightDate);            
            btm.Price = 100;
            double expectedPrice = btm.Price * ((6 - 3) * 0.1) + btm.Price; //130

            //actual
            double actual = btm.calculatePrice(buyingDate);

            //assert
            Assert.AreEqual(expectedPrice, actual);

        }


        [TestMethod]
        public void CalculatePriceWithOccupancyRateBetween_26_50_percent_OnEconomyClass()
        {
            //arrange
            string flightDate = "2018-04-04";
            string buyingDate = "2018-01-01";            
            EconomyTicketMachine etm = new EconomyTicketMachine("EALOAHKG2", flightDate);
            etm.Price = 100;
            int tickets = 3;
            etm.ConstnumberOfTickets = 10;
            //OccupancyRate 30%
            etm.setNumberOfSoldTickets(tickets);
            etm.checkPlaneOccupancy();
            double expected = 143;

            //actual
            double actual = etm.calculatePrice(buyingDate);

            //assert
            Assert.AreEqual(expected, actual);

        }


        [TestMethod]
        public void CalculatePriceWithOccupancyRateBetween_51_75_percent_OnEconomyClass()
        {
            //arrange
            string flightDate = "2018-03-04";
            string buyingDate = "2018-01-01";
            EconomyTicketMachine etm = new EconomyTicketMachine("EALOAHKG2", flightDate);
            etm.Price = 100;
            int tickets = 7;
            etm.ConstnumberOfTickets = 10;
            //OccupancyRate 70%
            etm.setNumberOfSoldTickets(tickets);
            //ticket price before checkPlaneOccupancy => 140
            etm.checkPlaneOccupancy();
            double expected = 158.2;

            //actual
            double actual = etm.calculatePrice(buyingDate);

            //assert
            Assert.AreEqual(expected, actual);

        }


        [TestMethod]
        public void CalculatePriceWithOccupancyRateBetween_75_100_percent_OnEconomyClass()
        {
            //arrange
            string flightDate = "2018-04-04";
            string buyingDate = "2018-01-01";
            EconomyTicketMachine etm = new EconomyTicketMachine("EALOAHKG2", flightDate);
            etm.Price = 100;
            int tickets = 9;
            etm.ConstnumberOfTickets = 10;
            //OccupancyRate 90%
            etm.setNumberOfSoldTickets(tickets);
            //ticket price before checkPlaneOccupancy => 130
            etm.checkPlaneOccupancy();
            double expected = 152.1;

            //actual
            double actual = etm.calculatePrice(buyingDate);

            //assert
            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void CalculatePriceWithOccupancyRateBetween_26_50_percent_OnBusinessClass()
        {
            //arrange
            string flightDate = "2018-04-04";
            string buyingDate = "2018-01-01";
            BusinessTicketMachine btm = new BusinessTicketMachine("EALOAHKG2", flightDate);
            btm.Price = 100;
            int tickets = 3;
            btm.ConstnumberOfTickets = 10;
            //OccupancyRate 30%            
            btm.setNumberOfSoldTickets(tickets);
            //ticket price before checkPlaneOccupancy => 130
            btm.checkPlaneOccupancy();
            double expected = 156;

            //actual
            double actual = btm.calculatePrice(buyingDate);

            //assert
            Assert.AreEqual(expected, actual);

        }


        [TestMethod]
        public void CalculatePriceWithOccupancyRateBetween_51_75_percent_OnBusinessClass()
        {
            //arrange
            string flightDate = "2018-04-04";
            string buyingDate = "2018-01-01";
            BusinessTicketMachine btm = new BusinessTicketMachine("EALOAHKG2", flightDate);
            btm.Price = 100;
            int tickets = 6;
            btm.ConstnumberOfTickets = 10;
            //OccupancyRate 60%            
            btm.setNumberOfSoldTickets(tickets);
            //ticket price before checkPlaneOccupancy => 130
            btm.checkPlaneOccupancy();
            double expected = 163.8;

            //actual
            double actual = btm.calculatePrice(buyingDate);

            //assert
            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void CalculatePriceWithOccupancyRateBetween_75_100_percent_OnBusinessClass()
        {
            //arrange
            string flightDate = "2018-02-04";
            string buyingDate = "2018-01-01";
            BusinessTicketMachine btm = new BusinessTicketMachine("EALOAHKG2", flightDate);
            btm.Price = 100;
            int tickets = 8;
            btm.ConstnumberOfTickets = 10;
            //OccupancyRate 80%            
            btm.setNumberOfSoldTickets(tickets);
            //ticket price before checkPlaneOccupancy => 150
            btm.checkPlaneOccupancy();
            double expected = 201;

            //actual
            double actual = btm.calculatePrice(buyingDate);

            //assert
            Assert.AreEqual(expected, actual);

        }


        [TestMethod]
        //Testing calculating date difference
        public void CheckDifferenceBetweenFlightAndBuyingDates()
        {
            //arrange
            string flightDate = "2018-02-25";
            string buyingTime = "2018-01-01";
            DateTime bt = Convert.ToDateTime(buyingTime);
            DateTime ft = Convert.ToDateTime(flightDate);
            int expected = 2;

            //actual
            double diff = ft.Subtract(bt).Days / (365.25 / 12);
            diff = Math.Round(diff, 0);

            //assert
            Assert.AreEqual(expected, diff);

        }
    }
}
