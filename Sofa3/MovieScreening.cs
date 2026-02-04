using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3
{
    class MovieScreening
    {
        private DateAndTime dateAndTime;
        private double pricePerSeat;

        public MovieScreening(DateAndTime dateAndTime, double pricePerSeat)
        {
            this.dateAndTime = dateAndTime;
            this.pricePerSeat = pricePerSeat;
        }

        public Double getPricePerSeat()
        {
            return this.pricePerSeat;
        }

        override public string ToString()
        {
            // CHECK IF THIS DATEANDTIME TO STRING WORKS AS EXPECTED
            return this.pricePerSeat + ", " + this.dateAndTime;
        }
    }
}
