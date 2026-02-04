// Main class

using Sofa3;
using System;

using Sofa3;
using System;

public class Program
{
    public static void Main()
    {
        Movie movie1 = new Movie("movie1");
        Movie movie2 = new Movie("movie2");
        Movie movie3 = new Movie("movie3");

        MovieTicket ticket1 = new MovieTicket(1, 1, false);
        MovieTicket ticket2 = new MovieTicket(2, 2, true);
        MovieTicket ticket3 = new MovieTicket(3, 3, false);

        MovieScreening movieScreening1 =
            new MovieScreening(DateTime.Now, 12.0);

        Order order1 = new Order(1, true);
        order1.addSeatReservation(ticket1);
        order1.addSeatReservation(ticket2);
        order1.addSeatReservation(ticket3);
        order1.export(TicketExportFormat.JSON);
        order1.export(TicketExportFormat.PLAINTEXT);

        movie1.addScreening(movieScreening1);
    }
}
