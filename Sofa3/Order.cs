using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sofa3
{
    class Order
    {
        public List<MovieTicket> movieTickets = new List<MovieTicket>();

        private int orderNr;
        public Boolean isStudentOrder;

        public Order(int orderNr, Boolean isStudentOrder)
        {
            this.orderNr = orderNr;
            this.isStudentOrder = isStudentOrder;
        }

        public int getOrderNr()
        {
            return this.orderNr;
        }

        public void addSeatReservation(MovieTicket ticket)
        {
            movieTickets.Add(ticket);
        }

        public Double calculatePrice()
        {
            return 1; // TO BE IMPLEMENTED
        }

        public void export(TicketExportFormat exportFormat)
        {
            if (exportFormat == TicketExportFormat.PLAINTEXT)
            {
                Console.WriteLine($"Ordernumber: {orderNr}");
                Console.WriteLine($"IsStudent: {isStudentOrder}");

                foreach (MovieTicket ticket in movieTickets)
                {
                    Console.WriteLine(ticket);
                }
            } else if (exportFormat == TicketExportFormat.JSON)
            {
                var exportObject = new
                {
                    OrderNumber = orderNr,
                    IsStudent = isStudentOrder,
                    Tickets = movieTickets.Select(t => new
                    {
                        RowNr = t.getRowNr(),
                        SeatNr = t.getSeatNr(),
                        IsPremium = t.isPremiumTicket(),
                        Price = t.getPrice()
                    })
                };

                string json = JsonSerializer.Serialize(
                    exportObject,
                    new JsonSerializerOptions { WriteIndented = true }
                );

                Console.WriteLine(json);
            }

        }
    }
}
