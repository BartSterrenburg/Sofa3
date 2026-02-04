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
        private DateTime dateTime;
        private double pricePerSeat;

        public MovieScreening(DateTime dateTime, double pricePerSeat)
        {
            this.dateTime = dateTime;
            this.pricePerSeat = pricePerSeat;
        }

        public Double getPricePerSeat()
        {
            return this.pricePerSeat;
        }

        override public string ToString()
        {
            // CHECK IF THIS DATEANDTIME TO STRING WORKS AS EXPECTED
            return this.pricePerSeat + ", " + this.dateTime;
        }
    }
}
