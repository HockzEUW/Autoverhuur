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
    /// Interaction logic for OverzichtApp.xaml
    /// </summary>
    public partial class OverzichtApp : Window {
        private DataManagerGebruiker _dm;
        public OverzichtApp(DataManagerGebruiker dm) {
            _dm = dm;
            InitializeComponent();
        }

        private void ReservatieMakenButton_Click(object sender, RoutedEventArgs e) {
            LoginApp app = new(_dm);
            app.Show();
            this.Close();
        }

        private void ReservatieOpzoekenButton_Click(object sender, RoutedEventArgs e) {
            ZoekApp app = new(_dm);
            app.Show();
            this.Close();
        }

        private void AutoOverzichtButton_Click(object sender, RoutedEventArgs e) {
            AutoOverzichtApp app = new(_dm);
            app.Show();
            this.Close();
        }
    }
}
