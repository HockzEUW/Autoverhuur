# Autoverhuur
Dit project is opgemaakt in het kader van een examen waarvan de opgave terug te vinden is onder de map `Data`.

Autoverhuur is een WPF-applicatie geschreven in .NET 9 voor het beheren en verhuren van auto's via een lokale database. 
Het project ondersteunt het importeren van gegevens uit CSV-bestanden, het beheren van klanten, auto's en vestigingen, en het maken en beheren van reservaties.

## Functionaliteiten

### 1. Gegevensbeheer (GegevensApp)
- **Importeren van gegevens**: importeren van klanten, auto's en vestigingen vanuit CSV-bestanden naar de database
- **Validatie**: de structuur van de CSV-bestanden controleren en fouten loggen in `errors.csv`
- **Databasebeheer**: leegmaken van de tabellen in de database vóór het importeren
- **Feedback**: visuele feedback weergeven bij succesvolle of mislukte import

### 2. Gebruikersinterface (OverzichtApp)
- **Zoeken**: klanten en beschikbare auto's opzoeken op basis van vestiging en periode
- **Reservaties**: nieuwe reservaties aanmaken en bestaande beheren
- **Overzichten**: details van klanten, auto's, vestigingen en reservaties weergeven
- **Export**: AsciiDoc-overzicht genereren van beschikbare auto's

## Data import

CSV-bestanden moeten de volgende headers bevatten:

- **Klanten**: `Voornaam;Achternaam;Email;Straat;Postcode;Woonplaats;Land`
- **Auto's**: `Nummerplaat;Model;Zitplaatsen;Motortype`
- **Vestigingen**: `Luchthaven;Straat;Postcode;Plaats;Land`

Fouten tijdens het importeren worden gelogd in `errors.csv`.

## Technologieën

- **.NET 9**
- **WPF**
- **SQL Server (SQLExpress)**
- **3-tier model**
- **Repository Design Pattern**
- **xUnit testing**

## Setup

1. **Database**: zorg dat SQL Server draait en de database `AutoverhuurDB` bestaat, eventueel importeren van deze database aan de hand van het `autoverhuur.sql` script dat terug te vinden is onder de `Data` folder
2. **CSV-bestanden**: gebruik de geldige CSV bestanden onder de `Data` folder of volg dezelfde opmaak qua headers
3. **Start GegevensApp**: kies als start up project `AutoverhuurProject.Presentatie.Gui.Geg`, voer de applicatie uit en laad de gevraagde bestanden op
4. **Start OverzichtApp**: kies nu `AutoverhuurProject.Presentatie.Gui.Gebr` als start up project en gebruik de gebruikersinterface om te zoeken, reserveren en beheren

## Projectstructuur

- `Domein`: businesslogica, modellen en services
- `Persistentie`: data access en repositories voor bestanden en database
- `Presentatie.Gui.Geg`: gegevensbeheer via WPF-app
- `Presentatie.Gui.Gebr`: gebruikersinterface via WPF-app

## Foutafhandeling

- Fouten bij het importeren worden gelogd in `errors.csv` met bestandsnaam, regelnummer en foutmelding
- Onjuiste bestandsstructuren worden geweigerd
