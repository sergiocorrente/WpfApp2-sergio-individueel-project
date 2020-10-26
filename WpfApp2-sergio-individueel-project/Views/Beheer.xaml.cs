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
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Security.Cryptography;


namespace WpfApp2_sergio_individueel_project.Views
{
    /// <summary>
    /// Interaction logic for Beheer.xaml
    /// </summary>
    public partial class Beheer : UserControl
    {
        public Beheer()
        {
            InitializeComponent();
            CategorieList();

        }
         public PersoneelsID Selected { get; set; }
        
        public Beheer(PersoneelsID selected)
        {
            selected = Selected;
            InitializeComponent();
            CategorieList();

        }
        
        private void PersoneelList()
        {
            cbRoleID.Items.Clear();
            cbRoleID.Items.Add("Beheerder");
            cbRoleID.Items.Add("Verkoper");
            cbRoleID.Items.Add("Magazinier");

            using (MagazijnEntities ctx = new MagazijnEntities())
            {
                cbPersoneelID.ItemsSource = null;
                var personeelList = ctx.PersoneelsIDs.Select(x => new { Username = x.Voornaam + " " + x.Achternaam + " " + x.Username, id = x.PersoneelID }).ToList();
                cbPersoneelID.DisplayMemberPath = "Username";
                cbPersoneelID.SelectedValuePath = "id";
                cbPersoneelID.ItemsSource = personeelList;
                cbPersoneelID.SelectedIndex = 0;
                cbRoleID.ItemsSource = null;
                var listRoleID = ctx.PersoneelsIDs.Select(x => x.RoleID).ToList();
       
            }
        }


        private void CategorieList()
        {
            using (MagazijnEntities ctx = new MagazijnEntities())
            {
                cbCategorie.ItemsSource = null;
                var listCategorie = ctx.Categories.Select(x => x).ToList();
                cbCategorie.DisplayMemberPath = "CategorieNaam";
                cbCategorie.SelectedValuePath = "CategorieID";
                cbCategorie.ItemsSource = listCategorie;
                cbCategorie.SelectedIndex = 0;
            }
        }

       
        public string  WachwoordEncryptie(string inWachtwoord)
        {
            string hash = "kcuf@BV";
            //byte[] data = Convert.FromBase64String(inWachtwoord);
            byte[] data = UTF8Encoding.UTF8.GetBytes(inWachtwoord);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateEncryptor();
                    Byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    inWachtwoord = Convert.ToBase64String(results, 0, results.Length);
                }
                
            }
            return inWachtwoord;
        }

        public string WachwoordDecryptie(string inWachtwoord)
        {
            string hash = "kcuf@BV";
            byte[] data = Convert.FromBase64String(inWachtwoord);

            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateDecryptor();
                    Byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    inWachtwoord = UTF32Encoding.UTF8.GetString(results);            
                }
            }
            return inWachtwoord;
        }

        private void tbPersoneel_Checked(object sender, RoutedEventArgs e)
        {
            if (tbPersoneel.IsChecked == true)
            {
                PersoneelList();
                cbPersoneelID.IsEnabled = true;
               txtAchternaamWoord.Visibility = Visibility.Hidden;
               txtvoornaamWoord.Visibility= Visibility.Hidden;  
               txtGebruikernaamWoord.Visibility= Visibility.Hidden;
               txtWachtwoordWoord.Visibility = Visibility.Hidden;
            }
        }

        private void TabDatabeheer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            cbPersoneelID.IsEnabled = true;
        }

        private void cbPersoneelID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //cbRoleID.Items.Clear();
            using (MagazijnEntities ctx= new MagazijnEntities())
            {
                if (cbPersoneelID.SelectedValue != null)
                   {  // String test= Convert.ToString(cbPersoneelID.SelectedValue);
                    //    MessageBox.Show(test);

                    if (ctx.PersoneelsIDs.Single(x => x.PersoneelID == (int)cbPersoneelID.SelectedValue) != null)
                    {
                        var geslectioneerdePersoneel = ctx.PersoneelsIDs.Single(p => p.PersoneelID == (int)cbPersoneelID.SelectedValue);
                    }
                if (ctx.PersoneelsIDs.Single(p => p.PersoneelID == (int)cbPersoneelID.SelectedValue) != null)
                {
                        string hash = "kcuf@BV";
                        
                        var geslectioneerdePersoneel = ctx.PersoneelsIDs.Single(p => p.PersoneelID == (int)cbPersoneelID.SelectedValue);
                        txtvoornaam.Text = geslectioneerdePersoneel.Voornaam;
                        txtAchternaam.Text = geslectioneerdePersoneel.Achternaam;
                        txtGebruikernaam.Text = geslectioneerdePersoneel.Username;
                        string gebruikerNaam = geslectioneerdePersoneel.Username;
                        String pass = Convert.ToString(geslectioneerdePersoneel.Wachtwoord);                                            
                        var wachtwoord = ctx.PersoneelsIDs.Where(x => x.Username == gebruikerNaam).Select(x => x.Wachtwoord).FirstOrDefault().ToString();                  
                        txtPasword.Text = WachwoordDecryptie(wachtwoord);
                        cbRoleID.SelectedIndex  = geslectioneerdePersoneel.RoleID -1;
                }
            }
        }

    }

        private void btnBewerken_Click(object sender, RoutedEventArgs e)
        {
            using (MagazijnEntities ctx = new MagazijnEntities())
            {
                var gesectioneerdePersoneel = ctx.PersoneelsIDs.Single(g => g.PersoneelID == (int)cbPersoneelID.SelectedValue);

                gesectioneerdePersoneel.Voornaam = txtvoornaam.Text;
                gesectioneerdePersoneel.Achternaam = txtAchternaam.Text;
                int roleID = (int)cbRoleID.SelectedIndex + 1;
                //var listRoleID = ctx.PersoneelsIDs.Select(x => x.RoleID).ToList();
                gesectioneerdePersoneel.RoleID = cbRoleID.SelectedIndex + 1;
                if (txtPasword.Text != "")
                {
                    var passwoord = WachwoordEncryptie(txtPasword.Text);
                    gesectioneerdePersoneel.Wachtwoord = passwoord;
                }
                gesectioneerdePersoneel.Username = txtvoornaam.Text;
                gesectioneerdePersoneel.Achternaam = txtvoornaam.Text;
                gesectioneerdePersoneel.Username = txtGebruikernaam.Text;
                gesectioneerdePersoneel.RoleID = roleID;
                ctx.SaveChanges();
                MessageBox.Show("Is bewerkt");
                PersoneelList();
            }

        }

        private void cbRoleID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnToevoegen_Click(object sender, RoutedEventArgs e)
        {
            var wachtwoord = txtPasword.Text;   
            string voornaam = txtvoornaam.Text;
            string achternaam = txtAchternaam.Text;
            //string wachtwwoord = WachwoordEncryptie(wachtwoord);
            int roleID = (int)cbRoleID.SelectedIndex + 1;
            string gebruikerNaam= txtGebruikernaam.Text;
         
            using (MagazijnEntities ctx= new MagazijnEntities())
            {
                ctx.PersoneelsIDs.Add(new PersoneelsID() {
                Voornaam = voornaam,
                Achternaam = achternaam,
                Wachtwoord = WachwoordEncryptie(wachtwoord),
                RoleID = roleID,
                Username = gebruikerNaam,

            });
                ctx.SaveChanges();
                MessageBox.Show("Is toegevoegt");
                PersoneelList();
            }
        }

        private void btnVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            using (MagazijnEntities ctx=new MagazijnEntities())
            {
                ctx.PersoneelsIDs.Remove(ctx.PersoneelsIDs.Single(p => p.PersoneelID == (int)cbPersoneelID.SelectedValue));
                ctx.SaveChanges();
            }
            MessageBox.Show("Is verwijderd");
            PersoneelList();
        }

      

        private void btnAddCategorie_Click(object sender, RoutedEventArgs e)
        {
            using (MagazijnEntities ctx = new MagazijnEntities())
            {
                ctx.Categories.Add(new Categorie { CategorieNaam = txtCategorieToevoegen.Text });
                //ctx.Categories.Add(new Categorie { CategorieNaam = txtCategorieToevoegen.Text });
                ctx.SaveChanges();
            }
            CategorieList();
        }

        private void btnEditCategorie_Click(object sender, RoutedEventArgs e)
        {
            using (MagazijnEntities ctx= new MagazijnEntities())
            {
                ctx.Categories.Single(x => x.CategorieID == (int)cbCategorie.SelectedValue).CategorieNaam = txtCategorieToevoegen.Text;
                ctx.SaveChanges();
            }
            CategorieList();
        }

        private void btnDeleteCategorie_Click(object sender, RoutedEventArgs e)
        {
            using (MagazijnEntities ctx=new MagazijnEntities())
            {
                ctx.Categories.Remove(ctx.Categories.Single(x => x.CategorieID == (int)cbCategorie.SelectedValue));
                ctx.SaveChanges();
            }
            CategorieList();
            
        }

        private void tbEditProducten_Checked(object sender, RoutedEventArgs e)
        {
            if (tbEditProducten.IsChecked == true)
            {
                //EditProducten();
                btnEditProducten.IsEnabled = true;
                btnDeletedProducten.IsEnabled = true;
                cbEditProducten.IsEnabled = true;
                btnToevoegenProducten.IsEnabled = false;
            }
            else if (tbEditProducten.IsChecked == false)
            {
                btnEditProducten.IsEnabled = false;
                btnDeletedProducten.IsEnabled = false;
                cbEditProducten.IsEnabled = false;
                btnToevoegenProducten.IsEnabled = true;
                txtNaamEditProducten.Text = "Naam";
                txtMargeEditProducten.Text = "Marge";
                txtEenheidEditProducten.Text = "Eenheid";
                txtBTWEditProducten.Text = "BTW";
                nudAantalOpVooraadProducten.Text = "0";
                //EditProductenfillCombobox();
            }

        }

        private void btnToevoegenProducten_Click(object sender, RoutedEventArgs e)
        {
            using (MagazijnEntities ctx = new MagazijnEntities())
            {
                ctx.Products.Add(new Product
                {
                    Naam = txtNaamEditProducten.Text,
                    Marge = Convert.ToDecimal(txtMargeEditProducten.Text),
                    Eenheid = Convert.ToInt32(txtEenheidEditProducten.Text),
                    BTW = Convert.ToDecimal(txtBTWEditProducten.Text),
                    LeverancierID = (int)cbLeverancierEditProducten.SelectedValue,
                    CategorieID = (int)cbCategorieEditProducten.SelectedValue,         
                }); 
                ctx.SaveChanges();
                MessageBox.Show("Toevoegen");
            }
        }
    }
}


