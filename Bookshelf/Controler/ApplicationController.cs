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
        private int SelectViewLater { get; set; }

        public ApplicationController()
        {
            SelectView = 1;
            SelectViewLater = 1;
        }

        public TypeView GetTypeView(UserControler.TypeBook type)
        {
            if (type == UserControler.TypeBook.ReadBook)
                return (TypeView)SelectView;
            else
                return (TypeView)SelectViewLater;
        }

        public void SetTypeView(TypeView type,UserControler.TypeBook typeBook)
        {
            if (typeBook == UserControler.TypeBook.ReadBook)
                SelectView = (int)type;
            else
                SelectViewLater = (int)type;
        }
    }
}