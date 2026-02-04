namespace Sofa3
{
    class MovieScreening
    {
        private DateTime dateTime;
        private float pricePerSeat;

        public MovieScreening(DateTime dateTime, float pricePerSeat)
        {
            this.dateTime = dateTime;
            this.pricePerSeat = pricePerSeat;
        }
        
        public float getPricePerSeat()
        {
            return pricePerSeat;
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
