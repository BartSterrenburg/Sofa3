// Main class

using Sofa3;

public class Program
{
    public static void Main()
    {
        var movie1 = new Movie("movie1");

        var movieScreening1 = new MovieScreening(DateTime.Now, 10);

        var movieTicket1 = new MovieTicket(movieScreening1, 3, 1, false);
        var movieTicket2 = new MovieTicket(movieScreening1, 3, 2, false);
        var movieTicket3 = new MovieTicket(movieScreening1, 3, 3, true);
        var movieTicket4 = new MovieTicket(movieScreening1, 3, 4, true);
        var movieTicket5 = new MovieTicket(movieScreening1, 3, 5, false);

        var order1 = new Order(1, true);
        
        order1.addSeatReservation(movieTicket1);
        order1.addSeatReservation(movieTicket2);
        order1.addSeatReservation(movieTicket3);
        order1.addSeatReservation(movieTicket4);
        order1.addSeatReservation(movieTicket5);

        order1.export(TicketExportFormat.JSON);
        order1.export(TicketExportFormat.PLAINTEXT);

        movie1.addScreening(movieScreening1);
        
        movie1.addScreening(movieScreening1);
        
        Console.WriteLine(order1.calculatePrice());
        Console.WriteLine(order1.calculatePrice2());

    }
}
