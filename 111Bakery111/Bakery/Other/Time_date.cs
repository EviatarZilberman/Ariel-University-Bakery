using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Other
{
    class Time_date
    {
        private int day;
        private int month;
        private int year;
        private bool isOkay; // If the constructor returns true the date is fine and can be send to a product.
        
        public Time_date(int day, int month, int year) // The constructor checks if the count of days fits the month.
        {
            this.year = year;
            this.month = month;

            if (this.month == 1 || this.month == 3 || this.month == 5 || this.month == 7
                || this.month == 8 || this.month == 10 || this.month == 12)
            {
                if (this.day >= 1 && this.day <= 31)
                {
                    this.day = day;

                }
            }
            else if (this.month == 4 || this.month == 6 || this.month == 9 || this.month == 11)
            {
                if (this.day >= 1 && this.day <= 30)
                {
                    this.day = day;

                }
            }
            else if (this.month == 2 && this.year % 4 != 0)
            {
                if (this.day >= 1 && this.day <= 28)
                {
                    this.day = day;
                }
            }
            else if (this.month == 2 && this.year % 4 == 0 && this.year%100==0 && this.year%400!=0)
            {
                if (this.day >= 1 && this.day <= 29)
                {
                    this.day = day;
                }
            }
            else
            {
                Console.WriteLine("The month "+this.month+" doesn't exist!");
                this.month = -1;
                Console.WriteLine("The month " + this.day + " doesn't exist!");
                this.day = -1;
            }
            this.day = day;

            if (this.day != -1 & this.month != -1)
            {
                this.isOkay = true;
            }
            else this.isOkay =  false;

        }
        public int Day
        {
            get { return day; }
            set { day = value; }
        }

        public int Month
        {
            get { return month; }
            set { month = value; }
        }

        public int Year
        {
            get { return year; }
            set { year = value; }
        }

        public bool Isokay
        {
            get { return isOkay; }                        
        }
    }
}
