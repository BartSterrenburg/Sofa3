namespace Sofa3
{
    class Order
    {
        private List<MovieTicket> movieTickets = new List<MovieTicket>();

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

        public double calculatePrice()
        {
            var price = 0f;

            for (var i = 0; i < movieTickets.Count; i++)
            {
                var ticket = movieTickets[i];

                if (isStudentOrder && i % 2 == 1)
                    continue;
                
                if (ticket.getWeekday() && i % 2 == 1)
                    continue;

                var ticketPrice = ticket.getPrice();

                if (ticket.isPremiumTicket())
                    ticketPrice += isStudentOrder ? 2f : 3f;

                price += ticketPrice;
            }

            if (!isStudentOrder && movieTickets.Count >= 6)
                price *= 0.9f;
            
            return price;
        }


        public void export(TicketExportFormat exportFormat)
        {
            //TO BE IMPLEMENTED
        }
    }
}
