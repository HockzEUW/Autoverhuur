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

namespace AutoverhuurProject.Presentatie.Gui.Gebr
{
    /// <summary>
    /// Interaction logic for ZoekApp.xaml
    /// </summary>
    public partial class ZoekApp : Window
    {
        private DataManagerGebruiker _dm;
        public ZoekApp(DataManagerGebruiker dm) {
            InitializeComponent();
            _dm = dm;
            InitialiseerData();

        }
        public void InitialiseerData() {
            ReservatieDatumDatePicker.DisplayDateStart = DateTime.Today;
            ReservatieDatumDatePicker.Text = DateTime.Today.ToShortDateString();
            VestigingenComboBox.ItemsSource = _dm.GeefVestigingen();
            VestigingenComboBox.SelectedIndex = 0;
        }

        private void ZoekenButton_Click(object sender, RoutedEventArgs e) {
            VestigingDto vestiging = VestigingenComboBox.SelectedItem as VestigingDto;
            ReservatiesListView.ItemsSource = _dm.GeefReservatiesDetails(KlantnaamTextBox.Text, vestiging.Id.ToString(), ReservatieDatumDatePicker.Text);
        }

        private void AnnuleerButton_Click(object sender, RoutedEventArgs e) {
            ReservatieDetailsDto reservatie = ReservatiesListView.SelectedItem as ReservatieDetailsDto;
            if(reservatie == null) {
                MessageBox.Show("Gelieve een reservatie te selecteren.");
                return;
            }
            _dm.DeleteReservatie(reservatie.Id);
            MessageBox.Show("Reservatie succesvol geannuleerd.");
        }

        private void VorigeButton_Click(object sender, RoutedEventArgs e) {
            OverzichtApp app = new(_dm);
            app.Show();
            this.Close();
        }
    }
}
