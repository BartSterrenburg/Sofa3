using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        }

        public Double calculatePrice()
        {
            return 1; // TO BE IMPLEMENTED
        }

        public void export(TicketExportFormat exportFormat)
        {
            //TO BE IMPLEMENTED
        }
    }
}
