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
        
        var order1 = new Order(1, true);
        
        order1.addSeatReservation(movieTicket1);
        order1.addSeatReservation(movieTicket2);
        order1.addSeatReservation(movieTicket3);
        
        order1.export(TicketExportFormat.JSON);
        order1.export(TicketExportFormat.PLAINTEXT);

        movie1.addScreening(movieScreening1);
        
        movie1.addScreening(movieScreening1);
        
        Console.Write(order1.calculatePrice());
    }
}
