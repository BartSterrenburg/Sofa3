// csharp
namespace Sofa3
{
    class MovieTicket(MovieScreening movieScreening, int rowNr, int seatNr, bool isPremium)
    {
        private readonly MovieScreening _movieScreening = movieScreening;
        private readonly int _rowNr = rowNr;
        private readonly int _seatNr = seatNr;
        private readonly bool _isPremium = isPremium;

        public bool isPremiumTicket()
        {
            return _isPremium;
        }

        public int getRowNr()
        {
            return _rowNr;
        }

        public int getSeatNr()
        {
            return _seatNr;
        }

        public float getPrice()
        {
            return _movieScreening.getPricePerSeat();
        }

        public bool getWeekday()
        {
            return _movieScreening.isWeekday();
        }

        public override string ToString()
        {
            return "The reservation for row " + _rowNr + " and seatnumber " + _seatNr + " was succesfully. Premium: " + _isPremium + "!";
        }
    }
}