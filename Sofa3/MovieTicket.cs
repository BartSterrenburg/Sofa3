using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3
{
    class MovieTicket
    {
        private MovieScreening movieScreening;

        private int rowNr;
        private int seatNr;
        private Boolean isPremium;

        public MovieTicket(int rowNr, int seatNr, Boolean isPremium)
        {
            this.rowNr = rowNr;
            this.seatNr = seatNr;
            this.isPremium = isPremium;
        }

        public Boolean isPremiumTicket()
        {
            return this.isPremium;
        }

        public int getRowNr()
        {
            return this.rowNr;
        }

        public int getSeatNr()
        {
            return this.seatNr;
        }


        public Double getPrice()
        {
            return 1; // TO BE IMPLEMENTED
        }

        public override string ToString()
        {
            return "The reservation for row " + this.rowNr + " and seatnumber " + this.seatNr + " was succesfully. Premium: " + this.isPremium + "!";
        }
    }
}
