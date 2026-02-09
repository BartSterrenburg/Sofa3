using System.Text.Json;

namespace Sofa3
{
    public class Order
    {
        private List<MovieTicket> movieTickets = new();

        private int orderNr;
        public bool IsStudentOrder;

        public Order(int orderNr, bool isStudentOrder)
        {
            this.orderNr = orderNr;
            this.IsStudentOrder = isStudentOrder;
        }

        public int getOrderNr()
        {
            return this.orderNr;
        }
        
        public void addSeatReservation(MovieTicket ticket)
        {
            movieTickets.Add(ticket);
        }

        public double calculatePrice()
        {
            double price = 0;
        
            for (var i = 0; i < movieTickets.Count; i++)
            {
                var ticket = movieTickets[i];

                if (IsStudentOrder && i % 2 == 1)
                    continue;
                
                if (ticket.getWeekday() && i % 2 == 1)
                    continue;

                var ticketPrice = ticket.getPrice();

                if (ticket.isPremiumTicket())
                    ticketPrice += IsStudentOrder ? 2f : 3f;

                price += ticketPrice;
            }

            if (!IsStudentOrder && movieTickets.Count >= 6)
                price *= 0.9;
            
            return price;
        }

        public double calculatePrice2()
        {
            int movieTicketCount = movieTickets.Count;
            double price = 0;

            for (var i = 0; i < movieTickets.Count; i++)
            {
                var ticket = movieTickets[i];

                if ((IsStudentOrder || ticket.getWeekday()) && i % 2 == 1)
                {
                    continue;
                } else if (ticket.isPremiumTicket())
                {
                    if (IsStudentOrder)
                    {
                        price += ticket.getPrice() + 2;
                    } else
                    {
                        price += ticket.getPrice() + 3;
                    }
                } else
                {
                    price += ticket.getPrice();
                }

                if (ticket.getWeekday() == false && IsStudentOrder)
                {
                    price = price * 0.9;
                }

            }


            return price;
        }

        public void export(TicketExportFormat exportFormat)
        {
            if (exportFormat == TicketExportFormat.PLAINTEXT)
            {
                Console.WriteLine($"Ordernumber: {orderNr}");
                Console.WriteLine($"IsStudent: {IsStudentOrder}");

                foreach (MovieTicket ticket in movieTickets)
                {
                    Console.WriteLine(ticket);
                }
            } else if (exportFormat == TicketExportFormat.JSON)
            {
                var exportObject = new
                {
                    OrderNumber = orderNr,
                    IsStudent = IsStudentOrder,
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

        public void addSeatReservation(object value)
        {
            throw new NotImplementedException();
        }
    }
}
