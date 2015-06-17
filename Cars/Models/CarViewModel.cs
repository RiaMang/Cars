using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cars.Models
{
    public class CarViewModel
    {
        public Car Car { get; set; }
        public string Recalls { get; set; }
        public string Image { get; set; }
    }
}