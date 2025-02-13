using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace essaimyimage
{
    


    public class Pixel
    {
           	  		 	   			    
        private byte red;
        private byte green;
        private byte blue;

        public Pixel(byte Red, byte Green, byte Blue)
        {
            this.red = Red;
            this.green = Green;
            this.blue = Blue;
        }
        public byte R
        {
            get { return red; }
            set { red = value; }
        }
        public byte G
        {
            get { return green; }
            set { green = value; }
        }
        public byte B
        {
            get { return blue; }
            set
            {
                blue = value;
            }
        }
        


    }
}
