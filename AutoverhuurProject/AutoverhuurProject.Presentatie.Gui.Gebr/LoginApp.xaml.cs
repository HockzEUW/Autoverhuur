using AutoverhuurProject.Domein;
using AutoverhuurProject.Domein.DTOs;
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

namespace AutoverhuurProject.Presentatie.Gui.Gebr {
    /// <summary>
    /// Interaction logic for LoginApp.xaml
    /// </summary>
    public partial class LoginApp : Window {
        private DataManagerGebruiker _dm;

        public LoginApp(DataManagerGebruiker dm) {

            _dm = dm;

            InitializeComponent();

            InitialiseerKlantenList();
        }

        private void InitialiseerKlantenList() {
            KlantenListView.ItemsSource = _dm.GeefKlanten();
        }

        private void ZoekButton_Click(object sender, RoutedEventArgs e) {
            KlantenListView.ItemsSource = _dm.ZoekKlanten(ZoektermTextBox.Text);
            KlantenListView.SelectedItem = null;
        }


        private void InlogButton_Click(object sender, RoutedEventArgs e) {
            if(!(KlantenListView.SelectedItem == null)) {
                ReserverenApp app = new(_dm, KlantenListView.SelectedValue as KlantDto);
                app.Show();
                this.Close();
            } else {
                MessageBox.Show("Gelieve een klant te selecteren.");
                return;
            }
        }

        private void VorigeButton_Click(object sender, RoutedEventArgs e) {
            OverzichtApp app = new(_dm);
            app.Show();
            this.Close();
        }
    }
}
