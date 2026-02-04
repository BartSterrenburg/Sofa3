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

        MovieScreening movieScreening1 =
            new MovieScreening(DateTime.Now, 12.0);

        movie1.addScreening(movieScreening1);
    }
}
