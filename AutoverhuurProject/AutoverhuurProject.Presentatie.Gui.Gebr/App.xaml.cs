using AutoverhuurProject.Domein;
using AutoverhuurProject.Domein.Interfaces;
using AutoverhuurProject.Persistentie.Db;
using System.Configuration;
using System.Data;
using System.Windows;

namespace AutoverhuurProject.Presentatie.Gui.Gebr;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private void Application_Startup(object sender, StartupEventArgs e) {
        const string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=AutoverhuurDB;Integrated Security=True;Encrypt=False;Trust Server Certificate=True";

        IAutoRepositoryFull autoRepoDb = new AutoRepositoryDb(connectionString);
        IKlantRepositoryFull klantRepoDb = new KlantRepositoryDb(connectionString);
        IVestigingRepositoryFull vestigingRepoDb = new VestigingRepositoryDb(connectionString);
        IReservatieRepositoryFull reservatieRepoDb = new ReservatieRepositoryDb(connectionString);

        DataManagerGebruiker dm = new(klantRepoDb, autoRepoDb, vestigingRepoDb, reservatieRepoDb);

        OverzichtApp app = new(dm);
        app.Show();
    }
}

