using AutoverhuurProject.Domein;
using AutoverhuurProject.Domein.Exceptions;
using AutoverhuurProject.Domein.Interfaces;
using AutoverhuurProject.Persistentie.Bestand.Repository;
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

namespace AutoverhuurProject.Presentatie.Gui.Geg
{
    /// <summary>
    /// Interaction logic for GegevensApp.xaml
    /// </summary>
    public partial class GegevensApp : Window
    {
        private DataManager _dm;

        private IKlantRepositoryFull _klantRepoDb;
        private IVestigingRepositoryFull _vestigingRepoDb;
        private IAutoRepositoryFull _autoRepoDb;
        private IReservatieRepositoryFull _reservatieRepoDb;

        private IKlantRepositoryRead _klantenRepoBestand;
        private IAutoRepositoryRead _autosRepoBestand;
        private IVestigingRepositoryRead _vestigingenRepoBestand;

        public GegevensApp(IAutoRepositoryFull autoRepoDb, IKlantRepositoryFull klantRepoDb, IVestigingRepositoryFull vestigingRepoDb, IReservatieRepositoryFull reservatieRepoDb) {
            _autoRepoDb = autoRepoDb;
            _klantRepoDb = klantRepoDb;
            _vestigingRepoDb = vestigingRepoDb;
            _reservatieRepoDb = reservatieRepoDb;
            InitializeComponent();
        }

        private void ToevoegenAutosButton_Click(object sender, RoutedEventArgs e) {
            // Configure open file dialog box
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Filter = "Autos|*.csv"; // Filter files by extension
            // Show open file dialog box
            dialog.ShowDialog();

            try {
                _autosRepoBestand = new AutoRepositoryBestand(dialog.FileName);
                AutosTextBlock.Background = Brushes.Green;
            } catch (Exception de) {
                MessageBox.Show(de.Message, "Fout bij het toevoegen", MessageBoxButton.OK, MessageBoxImage.Error);
                AutosTextBlock.Background = Brushes.Red;
                return;
            }
        }

        private void ToevoegenKlantenButton_Click(object sender, RoutedEventArgs e) {
            // Configure open file dialog box
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Filter = "Klanten|*.csv"; // Filter files by extension
            dialog.ShowDialog();

            try {
                _klantenRepoBestand = new KlantRepositoryBestand(dialog.FileName);
                KlantenTextBlock.Background = Brushes.Green;
            } catch (DomeinException de) {
                MessageBox.Show(de.Message, "Fout bij het toevoegen", MessageBoxButton.OK, MessageBoxImage.Error);
                KlantenTextBlock.Background = Brushes.Red;
                return;
            }

        }

        private void ToevoegenVestigingenButton_Click(object sender, RoutedEventArgs e) {
            // Configure open file dialog box
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Filter = "Vestigingen|*.csv"; // Filter files by extension
            dialog.ShowDialog();

            try {
                _vestigingenRepoBestand = new VestigingRepositoryBestand(dialog.FileName);
                VestigingenTextBlock.Background = Brushes.Green;
            } catch (DomeinException de) {
                MessageBox.Show(de.Message, "Fout bij het toevoegen", MessageBoxButton.OK, MessageBoxImage.Error);
                VestigingenTextBlock.Background = Brushes.Red;
                return;
            }
        }

        private void ToevoegenBestandenButton_Click(object sender, RoutedEventArgs e) {

            if (_klantenRepoBestand == null || _autosRepoBestand == null || _vestigingenRepoBestand == null) {
                MessageBox.Show("Zorg ervoor dat alle bestanden zijn toegevoegd voordat u doorgaat.", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            _dm = new DataManager(_klantenRepoBestand, _autosRepoBestand, _vestigingenRepoBestand, _klantRepoDb, _autoRepoDb, _vestigingRepoDb, _reservatieRepoDb);
            
            _dm.MaakDatabaseTablesLeeg();

            int vestigingenCount = _dm.ImporteerVestigingenUitBestandNaarDb();
            int autosCount = _dm.ImporteerAutosUitBestandNaarDb();
            int klantenCount = _dm.ImporteerKlantenUitBestandNaarDb();

            MessageBox.Show($"Er werden {vestigingenCount} vestigingen, {autosCount} autos, en {klantenCount} klanten succesvol in de database geïmporteerd.", "Importeren voltooid", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
            
        }
    }
}