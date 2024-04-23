using RestoranMarioMario.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RestoranMarioMario
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static restaurantEntities db { get; } = new restaurantEntities();
        public static Users CurrentUser = null;
        public static Table CurrentTable = null;
        public static Order CurrentOrder = null;
        public static List<OrderMenuBarCard> CurrentOrderProducts = null;
    }
}
