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
using System.Windows.Shapes;

namespace WpfApp2_sergio_individueel_project
{
    /// <summary>
    /// Interaction logic for HoofdMenu.xaml
    /// </summary>
    public partial class HoofdMenu : Window
    {
        string value;
        public HoofdMenu(string _value)
        {
            InitializeComponent();
            value = _value;
            txtKeuze.Text = value;
            string gebruiker = txtKeuze.Text;

            using (MagazijnEntities ctx = new MagazijnEntities())
            {
                var role = ctx.PersoneelsIDs.Where(x => x.Username == value).Select(y => y.RoleID).FirstOrDefault().ToString();
                var naam = ctx.PersoneelsIDs.Where(n => n.Username == value).Select(n => n.Voornaam).FirstOrDefault().ToString();
                //MessageBox.Show(Convert.ToString (role));
                txtKeuze.Text = Convert.ToString(naam);
                switch (role)
                {
                    case "1": break;
                    case "2": btnDatabeheer.Visibility = Visibility.Hidden; break;
                    case "3": btnDatabeheer.Visibility = Visibility.Hidden; break;
                    default:
                        break;
                }
                //MessageBox.Show(txtKeuze.Text);
            }
        }
            private void btnDatabeheer_Click(object sender, RoutedEventArgs e)
            {
                using (MagazijnEntities ctx = new MagazijnEntities()) {
                var role = ctx.PersoneelsIDs.Where(x => x.Username == value).Select(y => y.RoleID).FirstOrDefault().ToString();
                var naam = ctx.PersoneelsIDs.Where(n => n.Username == value).Select(n => n.Voornaam).FirstOrDefault().ToString();
                MainWindow dataBeheer = new MainWindow(value);
                dataBeheer.Show();
                this.Close();

                }
            }

        private void btnOverzicht_Click(object sender, RoutedEventArgs e)
        {
            using (MagazijnEntities ctx = new MagazijnEntities()) {
                var role = ctx.PersoneelsIDs.Where(x => x.Username == value).Select(y => y.RoleID).FirstOrDefault().ToString();
                MainWindow dataBeheer = new MainWindow(value);
                dataBeheer.Show();
                this.Close();
            }             
        }

        private void btnBestelling_Click(object sender, RoutedEventArgs e)
        {
            using (MagazijnEntities ctx = new MagazijnEntities())
            {
                var role = ctx.PersoneelsIDs.Where(x => x.Username == value).Select(y => y.RoleID).FirstOrDefault().ToString();
                MessageBox.Show(role);
                MainWindow dataBeheer = new MainWindow(value);
                dataBeheer.Show();
                this.Close();

            }
        }
    }
    }
