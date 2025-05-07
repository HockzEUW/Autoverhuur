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
    /// Interaction logic for AutoOverzichtApp.xaml
    /// </summary>
    public partial class AutoOverzichtApp : Window
    {
        private DataManagerGebruiker _dm;
        private DateTime _startDatum;
        private DateTime _eindDatum;
        public AutoOverzichtApp(DataManagerGebruiker dm)
        {
            InitializeComponent();
            _dm = dm;
            InitialiseerData();
        }

        public void InitialiseerData() {
            VestigingenComboBox.ItemsSource = _dm.GeefVestigingen();
            ReservatieStartDatumDatePicker.DisplayDateStart = DateTime.Today;
            ReservatieStartDatumDatePicker.Text = DateTime.Today.ToShortDateString();
            ReservatieEindDatumDatePicker.DisplayDateStart = DateTime.Today;
            ReservatieEindDatumDatePicker.Text = DateTime.Today.ToShortDateString();
            VestigingenComboBox.SelectedIndex = 0;
        }

        private void ZoekButton_Click(object sender, RoutedEventArgs e) {

            _startDatum = DateTime.Parse(ReservatieStartDatumDatePicker.Text);
            _eindDatum = DateTime.Parse(ReservatieEindDatumDatePicker.Text);
            VestigingDto vestigingDto = VestigingenComboBox.SelectedItem as VestigingDto;
            AutosListView.ItemsSource = _dm.ZoekBeschikbareAutos(vestigingDto.Id.ToString(), _startDatum, _eindDatum);
        }

        private void ExporterenButton_Click(object sender, RoutedEventArgs e) {
            VestigingDto vestigingDto = VestigingenComboBox.SelectedItem as VestigingDto;
            var autos = _dm.ZoekBeschikbareAutos(vestigingDto.Id.ToString(), _startDatum, _eindDatum);
            _dm.GenereerAsciiDoc(autos, vestigingDto.Id.ToString(), _startDatum, _eindDatum);
            MessageBox.Show($"AsciiDoc document succesvol gegenereerd");
        }

        private void VorigeButton_Click(object sender, RoutedEventArgs e) {
            OverzichtApp overzichtApp = new(_dm);
            overzichtApp.Show();
            this.Close();
        }
    }
}
