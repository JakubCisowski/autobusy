using Autobusy.Logic.Contexts;
using Autobusy.Logic.Operations;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Autobusy.Logic.Helpers;

public class ReportHelper
{
	private static bool _includeKierowcyInfo, _includeLinieInfo, _includeEntityStatistics;
	
	public static void CreateReport(bool includeKierowcyInfo, bool includeLinieInfo, bool includeEntityStatistics)
	{
		_includeKierowcyInfo = includeKierowcyInfo;
		_includeLinieInfo = includeLinieInfo;
		_includeEntityStatistics = includeEntityStatistics;

		var date = DateTime.Now.ToString("dd/MM/yyyy hh/mm");
		
		Document.Create(container =>
			{
				container.Page(page =>
				{
					page.Size(PageSizes.A4);
					page.Margin(2, Unit.Centimetre);
					page.PageColor(Colors.White);
					page.DefaultTextStyle(x => x.FontSize(12));

					page.Header()
						.Text(text =>
						{
							text.AlignCenter();

							text.Line($"Autobusy - Raport, {date}").SemiBold().FontSize(24).FontColor(Colors.Blue.Medium);

							string coZawieraRaport = $"{(includeKierowcyInfo ? "Informacje o kierowcach " : "")} {(includeLinieInfo ? "Informacje o liniach " : "")} {(includeEntityStatistics ? "Statystyki bazy danych " : "")}";

							text.Line($"Raport zawiera: {coZawieraRaport}").FontSize(18).FontColor(Colors.Blue.Darken1);
						});
					
					// OMEGA TODO:
					// Wygenerować wiele page'ów w takiej formie:
					// I. Informacje o kierowcach (tabelki z przejazdami spóźnieniami itd):
					// 1. Jan Kowalski
					// 2. Jakub Nowak
					
					// II. Informacje o liniach (tabelki z kursami, przejazdami, biletami, paliwem itd):
					// 1. Linia 1:
					//	a. Poniedziałek, 8:00
					//	b. Poniedziałek, 12:00
					//	c. Wtorek, 15:00
					// 2. Linia 2:
					//	a. Poniedziałek, 20:00
					//	b. Piątek, 22:00
					
					// III. Informacje o obiektach w bazie danych (skrótowo): DONE
					// 1. Kierowcy: 2
					// 2. Linie: 5
					// 3. Pojazdy: 3
					// 4. Przystanki: 52

					page.Content().Element(CreatePdfContent);

					page.Footer()
						.AlignCenter()
						.Text(x =>
						{
							x.Span("Strona ");
							x.CurrentPageNumber();
						});
				});
			})
			.GeneratePdf($"Autobusy raport - {date}.pdf");
	}

	private static void CreatePdfContent(IContainer container)
	{
		container.PaddingVertical(20).Column(column =>
		{
			column.Spacing(20);

			if (_includeKierowcyInfo)
			{
				column.Item().Element(CreateKierowcyInfo);
				column.Item().PageBreak();
			}

			if (_includeLinieInfo)
			{
				column.Item().Element(CreateLinieInfo);
				column.Item().PageBreak();
			}

			if (_includeEntityStatistics)
			{
				column.Item().Element(CreateEntityInfo);
				column.Item().PageBreak();
			}
		});
	}
	
	private static void CreateKierowcyInfo(IContainer container)
	{
		var kierowcyFromDb = DatabaseOperations.GetKierowcy();
		
		container.Table(table =>
		{
			table.ColumnsDefinition(columns =>
			{
				columns.ConstantColumn(25);	// Id
				columns.RelativeColumn(2);		// Imię i nazwisko
				columns.RelativeColumn(1);		// Ilość przejazdów
				columns.RelativeColumn(1);		// Średnie spalanie
				columns.RelativeColumn(1);		// Ilość spóźnień
				columns.RelativeColumn(1);		// Średnie spóźnienie
			});
			
			table.Header(header =>
			{
				header.Cell().Element(CellStyle).Text("Id");
				header.Cell().Element(CellStyle).Text("Imię i nazwisko");
				header.Cell().Element(CellStyle).AlignRight().Text("Ilość przejazdów");
				header.Cell().Element(CellStyle).AlignRight().Text("Średnie spalanie / km");
				header.Cell().Element(CellStyle).AlignRight().Text("Ilość spóźnień");
				header.Cell().Element(CellStyle).AlignRight().Text("Średnie spóźnienie");
			});

			foreach (var kierowca in kierowcyFromDb)
			{
				table.Cell().Element(CellStyle).Text(kierowca.KierowcaId);
				table.Cell().Element(CellStyle).Text(kierowca.Imie + " " + kierowca.Nazwisko);
				table.Cell().Element(CellStyle).AlignRight().Text(kierowca.Przejazdy.Count);

				double spalonePaliwo = 0;
				double iloscKilometrow = 0;
				
				foreach (var przejazd in kierowca.Przejazdy)
				{
					spalonePaliwo += przejazd.IloscSpalonegoPaliwa;
					iloscKilometrow += przejazd.Kurs.Linia.DlugoscWKm;
				}

				double srednieSpalonePaliwoNa1Km = iloscKilometrow == 0 ? 0 : spalonePaliwo / iloscKilometrow;
				
				table.Cell().Element(CellStyle).AlignRight().Text(srednieSpalonePaliwoNa1Km.ToString("0.00"));

				double iloscMinutSpoznienia = 0;
				int iloscSpoznien = 0;

				foreach (var przejazd in kierowca.Przejazdy)
				{
					if (przejazd.RealizacjePrzejazdu is not null && przejazd.RealizacjePrzejazdu.Count > 0)
					{
						iloscSpoznien++;
						iloscMinutSpoznienia+= przejazd.RealizacjePrzejazdu.Sum(x => (x.FaktycznaGodzina - x.PlanKursu.PlanowaGodzina).TotalMinutes);
					}
				}
				
				table.Cell().Element(CellStyle).AlignRight().Text(iloscSpoznien);
				
				double srednieSpoznienie = iloscSpoznien == 0 ? 0 : iloscMinutSpoznienia / iloscSpoznien;
				
				table.Cell().Element(CellStyle).AlignRight().Text(srednieSpoznienie);
			}
		});
	}

	private static void CreateLinieInfo(IContainer container)
	{
		
	}

	private static void CreateEntityInfo(IContainer container)
	{
		container.Table(table =>
		{
			table.ColumnsDefinition(columns =>
			{
				columns.ConstantColumn(25);	// Lp
				columns.RelativeColumn(2);		// Typ obiektu
				columns.RelativeColumn(1);		// Ilość obiektów w bazie danych
				columns.RelativeColumn(2);		// Puste miejsce
			});
			
			table.Header(header =>
			{
				header.Cell().Element(CellStyle).Text("#");
				header.Cell().Element(CellStyle).Text("Typ obiektu");
				header.Cell().Element(CellStyle).AlignRight().Text("Ilość obiektów");
				header.Cell().Element(CellStyle).AlignRight().Text("");
			});
			
			// TODO: zmienić tu po zrobieniu repository

			using (var db = new AutobusyContext())
			{
				var kierowcy = db.Kierowcy.Count();
				table.Cell().Element(CellStyle).Text("1");
				table.Cell().Element(CellStyle).Text("Kierowcy");
				table.Cell().Element(CellStyle).AlignRight().Text(kierowcy);
				table.Cell().Element(CellStyle).AlignRight().Text("");

				var kursy = db.Kursy.Count();
				table.Cell().Element(CellStyle).Text("2");
				table.Cell().Element(CellStyle).Text("Kursy");
				table.Cell().Element(CellStyle).AlignRight().Text(kursy);
				table.Cell().Element(CellStyle).AlignRight().Text("");

				var linie = db.Linie.Count();
				table.Cell().Element(CellStyle).Text("3");
				table.Cell().Element(CellStyle).Text("Linie");
				table.Cell().Element(CellStyle).AlignRight().Text(linie);
				table.Cell().Element(CellStyle).AlignRight().Text("");

				var przejazdy = db.Przejazdy.Count();
				table.Cell().Element(CellStyle).Text("4");
				table.Cell().Element(CellStyle).Text("Przejazdy");
				table.Cell().Element(CellStyle).AlignRight().Text(przejazdy);
				table.Cell().Element(CellStyle).AlignRight().Text("");

				var przejazdyRealizowane = db.RealizacjePrzejazdu.Count();
				table.Cell().Element(CellStyle).Text("5");
				table.Cell().Element(CellStyle).Text("Przejazdy realizowane");
				table.Cell().Element(CellStyle).AlignRight().Text(przejazdyRealizowane);
				table.Cell().Element(CellStyle).AlignRight().Text("");

				var autobusy = db.Autobusy.Count();
				table.Cell().Element(CellStyle).Text("6");
				table.Cell().Element(CellStyle).Text("Pojazdy");
				table.Cell().Element(CellStyle).AlignRight().Text(autobusy);
				table.Cell().Element(CellStyle).AlignRight().Text("");

				var planyKursu = db.PlanyKursu.Count();
				table.Cell().Element(CellStyle).Text("7");
				table.Cell().Element(CellStyle).Text("Plany kursów");
				table.Cell().Element(CellStyle).AlignRight().Text(planyKursu);
				table.Cell().Element(CellStyle).AlignRight().Text("");

				var przystanki = db.Przystanki.Count();
				table.Cell().Element(CellStyle).Text("8");
				table.Cell().Element(CellStyle).Text("Przystanki");
				table.Cell().Element(CellStyle).AlignRight().Text(przystanki);
				table.Cell().Element(CellStyle).AlignRight().Text("");

				var przystankiWLinii = db.PrzystankiWLinii.Count();
				table.Cell().Element(CellStyle).Text("9");
				table.Cell().Element(CellStyle).Text("Przystanki w linii");
				table.Cell().Element(CellStyle).AlignRight().Text(przystankiWLinii);
				table.Cell().Element(CellStyle).AlignRight().Text("");

				var serwisy = db.Serwisy.Count();
				table.Cell().Element(CellStyle).Text("10");
				table.Cell().Element(CellStyle).Text("Serwisy");
				table.Cell().Element(CellStyle).AlignRight().Text(serwisy);
				table.Cell().Element(CellStyle).AlignRight().Text("");
			}
		});
	}
	
	private static IContainer CellStyle(IContainer container)
	{
		return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
	}
}