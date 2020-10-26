using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp2_sergio_individueel_project.ViewModels;
using WpfApp2_sergio_individueel_project.Views;

namespace WpfApp2_sergio_individueel_project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string value;
        public MainWindow(string _value)
        {
            InitializeComponent();

            DataContext = new Beheer();
        }


     

        private void Overzicht_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new OverzichtModel();
        }

        private void Bestelling_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new BestellingModel();
        }

        private void Rapportering_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new RapporteringModel();
        }


        private void DataBeheer_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new OverzichtModel();
        }

        private void Beheer_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new Beheer();
        }
    }
}
