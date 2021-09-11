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

namespace Bookshelf.Controler
{
    public class ApplicationController
    {
        public enum TypeView
        {
            MinInfo = 1,
            MaxInfo = 2
        }

        private int SelectView { get; set; }

        public ApplicationController()
        {
            SelectView = 1;
        }

        public TypeView GetTypeView()
        {
            return (TypeView)SelectView;
        }

        public void SetTypeView(TypeView type)
        {
            SelectView = (int)type;
        }
    }
}