using MobileAT1.Views;

namespace MobileAT1
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("gamepage", typeof(GamePage));
            Routing.RegisterRoute("searchpage", typeof(SearchPage));
        }
    }
}
