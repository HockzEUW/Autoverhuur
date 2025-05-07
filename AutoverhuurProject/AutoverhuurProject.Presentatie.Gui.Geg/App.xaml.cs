using AutoverhuurProject.Domein.Interfaces;
using AutoverhuurProject.Domein;
using System.Configuration;
using System.Data;
using System.Windows;
using AutoverhuurProject.Persistentie.Db;

namespace AutoverhuurProject.Presentatie.Gui.Geg;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application {
    private void Application_Startup(object sender, StartupEventArgs e) {
        const string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=AutoverhuurDB;Integrated Security=True;Encrypt=False;Trust Server Certificate=True";

        IAutoRepositoryFull autoRepoDb = new AutoRepositoryDb(connectionString);
        IKlantRepositoryFull klantRepoDb = new KlantRepositoryDb(connectionString);
        IVestigingRepositoryFull vestigingRepoDb = new VestigingRepositoryDb(connectionString);
        IReservatieRepositoryFull reservatieRepoDb = new ReservatieRepositoryDb(connectionString);

        GegevensApp app = new(autoRepoDb, klantRepoDb, vestigingRepoDb, reservatieRepoDb);
        app.Show();
    }
}

