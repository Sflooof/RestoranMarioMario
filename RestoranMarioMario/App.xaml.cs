using RestoranMarioMario.Entities;
using System.Collections.Generic;
using System.Windows;

namespace RestoranMarioMario
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static restaurantEntities db { get; } = new restaurantEntities();
        public static Users CurrentUser = null;
        public static Users CurrentUserPassword = null;
        public static Table CurrentTable = null;
        public static Order CurrentOrder = null;
        public static OrderMenu CurrentOrderMenuDate = null;
        public static List<OrderMenu> CurrentOrderMenu = null;
    }
}
