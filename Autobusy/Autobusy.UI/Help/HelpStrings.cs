using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;

namespace Autobusy.UI.Help;

public static class HelpStrings
{
	public const string Introduction = "Tworzenie Aplikacji Bazodanowych.\nProjekt, Informatyka sem. 6, temat: Autobusy\nProwadzący: dr inż. Łukasz Wyciślik.";

	public const string Authors = "Autorzy:\nRafał Klinowski\nJakub Cisowski\nKacper Nitkiewicz\nPaweł Kabza\nMichał Chyla";

	public const string Dyspozytor = "Dyzpozytor\nOdpowiedzialność dyzpozytora jest podzielona na dwie części:\n\n1. Kierowcy - na tym ekranie jest możliwe dodawanie nowych kierowców (przycisk Dodaj kierowcę) oraz uzupełniania danych osobowych zatrudnionych kierowców (imię, nazwisko, doświadczenie). Jest również możliwość sprawdzenia średniego spalania dla każdego z kierowców. Jeżeli osoba ta nie brała jeszcze udziału w żadnych przejazdach, stosowna informacja zostanie wyświetlona na ekranie.\n\n2. Przejazdy - na tym ekranie jest możliwe tworzenie nowych przejazdów na podstawie kursów. Po wybraniu kursu (reprezentującego teoretyczny rozkład jazdy - układaniem zajmuje się Planista) jest możliwe sprecyzowanie dokładnej daty odbycia, kierowcy, autobusu, a po przejeździe możliwe jest uzupełnienie dodatkowych informacji takich jak: ilość skasowanych biletów, ilość spalonego paliwa. Do stworzenia przejazdu konieczne jest wybranie kursu, w ramach którego dany przejazd się odbywa. Możliwe jest również podejrzenie linii tego kursu, wraz z kolejnością odwiedzenia przystanków.";

	public const string Planista = "Planista\nOdpowiedzialność planisty jest podzielona na trzy części:\n\n1. Przystanki - na tym ekranie jest możliwe dodawanie nowych przystanków, edycja istniejących i ich usuwanie. Za pomocą przycisku Dodaj przystanek, do listy dodawany jest nowy, następnie jest możliwość edycji jego parametrów bezpośrednio na liście. Przycisk Wycofanie usuwa dany przystanek.\n\n2. Linie - na tym ekranie definiowana jest linia - podstawowa jednostka podziału. Pierwszym krokiem jest stworzenie nowej linii za pomocą przycisku Dodaj linię - w osobnym oknie nastąpi wpisanie parametrów takich jak numer linii czy jej typ. Jest możliwość również edycji i usuwania istniejących linii. Kolejnym krokiem jest wybór z listy rozwijalnej przystanku (po nazwie). Następnie za pomocą przycisku Dodaj przystanek następuje dodanie wybranego z listy przystanku do rozkładu jazdy. Przystanki na liście można usuwać, jak również zmieniać im kolejność za pomocą kolumny Liczba porządkowa. Ostatnią funkcją na tym ekranie jest sprawdzenie rentowności linii - zwraca ona statystyki takie jak: ilość skasowanych biletów, ilość spalonego paliwa, najbardziej popularne i najmniej popularne dni i godziny dla danej linii. Jeżeli w ramach danej linii nie było żadnych przejazdów, stosowna informacja jest wyświetlona na ekranie.\n\n3. Kursy - na tym ekranie definiowane są kursy na podstawie linii, reprezentujące rozkład jazdy autobusu. Na początku należy z listy rozwijalnej wybrać linię (po numerze linii). Po wciśnięciu przycisku Dodaj kurs do listy zostanie dodany nowy kurs - należy wybrać dzień tygodnia (NIE konkretną datę) i godzinę rozpoczęcia. Po wciśnięciu przycisku Edycja, wyświetli się nowe okno, w którym widnieje lista zawierająca przystanki w takiej kolejności, jaka była ustalona w panelu Linie. Dla każdego przystanku z listy można ustawić godzinę, o której autobus ma pojawić się na danym przystanku. Jest tu również możliwość usuwania przystanków (w ten sposób niektóre kursy mają inną trasę niż wynikałoby z definicji linii) lub dodawania nowych. Przycisk Usunięcie na głównym ekranie usuwa dany kurs wraz z całym rozkładem jazdy dla niego.";

	public const string ZarzadcaFlota = "Zarządca Flotą\n\nOdpowiedzialnością Zarządcy Flotą jest kontrola stanu autobusów, przyjmowanie nowych do taboru oraz oddawanie ich do serwisu. Za pomocą przycisku Dodaj autobus jest możliwe dodanie do listy nowego pojazdu - konieczne jest następnie wypełnienie wszystkich kolumn reprezentujących parametry autobusu. WAŻNE: spalanie pojazdu na 100km, będące liczbą zmiennoprzecinkową, należy podać używając kropki a nie przecinka! (na przykład: 9.7). Stan autobusu można w każdym momencie zmienić korzystając z listy rozwijalnej. Za pomocą przycisku Wycofanie możliwe jest usunięcie wybranego pojazdu. Przycisk Serwis służy do oddania autobusu do serwisu - w ten sposób, stan pojazdu automatycznie zmieni się na 'W serwisie', a w bazie danych zostanie utworzony nowy Serwis z danym pojazdem.";

	public const string Database = "Baza danych\n\nAplikacja korzysta z lokalnej bazy danych. Przy pierwszym uruchomieniu aplikacji, jeżeli baza danych nie istnieje, zostanie automatycznie stworzona. Domyślna ścieżka: C:\\Użytkownicy\\(Aktualnie zalogowany użytkownik)\\AutobusyDB.MDF.mdf. Jest możliwość podejrzenia zawartości bazy danych za pomocą programu SSMS (SQL Server Management Studio) od firmy Microsoft. Po połączeniu z lokalną bazą danych i wybraniu z listy pliku AutobusyDB.MDF.mdf, możliwe jest zobaczenie schematu, poszczególnych tabeli oraz ich zawartości.\n\nAplikacja korzysta z bazy danych SQL Server.";

	public static List<Inline> StringToInlineCollection(string str)
	{
		IEnumerable<Inline> stringInlines = str.Split('\n').Select(x => new Run(x));
		
		return stringInlines.SelectMany(x => new []
		{
			x,
			new LineBreak()
		}).ToList();
	}
} 