using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microcharts;

namespace Bookshelf.Model
{
    public class Statistic
    {
        public String Name { get; set; }
        public String Value { get; set; }
        public Chart ChartStat { get; set; }

    }
}