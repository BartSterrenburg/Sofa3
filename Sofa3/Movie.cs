using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3
{
    class Movie
    {
        private string title;

        public Movie(string title)
        {
            this.title = title;
        }

        public void addScreening(MovieScreening screening)
        {
            // TO BE IMPLEMENTED
        }

        override public string ToString()
        {
            return this.title;
        }
    }
}
