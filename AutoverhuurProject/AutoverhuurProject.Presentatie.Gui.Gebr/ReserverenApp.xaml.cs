using AutoverhuurProject.Domein.DTOs;
using AutoverhuurProject.Domein;
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
    /// Interaction logic for ReserverenApp.xaml
    /// </summary>
    public partial class ReserverenApp : Window
    {
        private DataManagerGebruiker _dm;
        private KlantDto _ingelogdeKlant;
        private DateTime _startDatum;
        private DateTime _eindDatum;

        public ReserverenApp(DataManagerGebruiker dm, KlantDto ingelogdeKlant) {
            InitializeComponent();
            _dm = dm;
            _ingelogdeKlant = ingelogdeKlant;
            NaamIngelogdeKlantTextBlock.Text = $"Welkom, {ingelogdeKlant.Voornaam} {ingelogdeKlant.Achternaam}. Kies een vestiging om een reservatie te maken.";
            InitialiseerData();
            InitialiseerVestigingenList();
        }

        public void InitialiseerData() {
            StartDatumDatePicker.DisplayDateStart = DateTime.Today;
            StartDatumDatePicker.Text = DateTime.Today.ToShortDateString();
            EindDatumDatePicker.DisplayDateStart = DateTime.Today;
            EindDatumDatePicker.Text = DateTime.Today.ToShortDateString();
        }
        public void InitialiseerVestigingenList() {
            VestigingenListView.ItemsSource = _dm.GeefVestigingen();
        }

        private void ZoekAutosButton_Click(object sender, RoutedEventArgs e) {
            _startDatum = (DateTime)StartDatumDatePicker.SelectedDate;
            _eindDatum = (DateTime)EindDatumDatePicker.SelectedDate;
            VestigingDto selectedVestiging = VestigingenListView.SelectedItem as VestigingDto;
            if(selectedVestiging == null) {
                MessageBox.Show("Duid een vestiging aan", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            AutosListView.ItemsSource = _dm.ZoekBeschikbareAutos(selectedVestiging.Id.ToString(), _startDatum, _eindDatum);
        }

        private void MaakReservatieButton_Click(object sender, RoutedEventArgs e) {
            VestigingDto vestiging = VestigingenListView.SelectedItem as VestigingDto;
            AutoDto auto = AutosListView.SelectedItem as AutoDto;
            KlantDto klant = _ingelogdeKlant;
           
            int? aantalPersonen = int.TryParse(AantalPersonenTextBox.Text, out int tempAantalPersonen) ? tempAantalPersonen : (int?)null;
            try {
                _dm.MaakReservatie(klant, auto, vestiging, _startDatum, _eindDatum, aantalPersonen);
                MessageBox.Show("Reservatie succesvol geplaatst. U wordt nu terug naar het overzichtsscherm gestuurd.", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                OverzichtApp app = new(_dm);
                app.Show();
                this.Close();
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void VorigeButton_Click(object sender, RoutedEventArgs e) {
            LoginApp app = new(_dm);
            app.Show();
            this.Close();
        }
    }
}

