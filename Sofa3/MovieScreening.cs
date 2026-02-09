namespace Sofa3
{
    public class MovieScreening
    {
        private DateTime dateTime;
        public double pricePerSeat;

        public MovieScreening(DateTime dateTime, float pricePerSeat)
        {
            this.dateTime = dateTime;
            this.pricePerSeat = pricePerSeat;
        }
        
        public double getPricePerSeat()
        {
            return pricePerSeat;
        }

        public void setPrice(double pricePerSeat)
        {
            this.pricePerSeat = pricePerSeat;
        }


        public bool isWeekday()
        {
            var dow = dateTime.DayOfWeek;
            return dow >= DayOfWeek.Monday && dow <= DayOfWeek.Thursday;
        }


        override public string ToString()
        {
            // CHECK IF THIS DATEANDTIME TO STRING WORKS AS EXPECTED
            return pricePerSeat + ", " + dateTime;
        }
    }
}
