using Autobusy.Logic.Contexts;
using Autobusy.Logic.Models;
using Autobusy.Logic.Repositories;
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

		var date = DateTime.Now.ToString("dd/MM/yyyy HH/mm");

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

							var coZawieraRaport = $"{(includeKierowcyInfo ? "Informacje o kierowcach " : "")} {(includeLinieInfo ? "Informacje o liniach " : "")} {(includeEntityStatistics ? "Statystyki bazy danych " : "")}";

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
		List<Kierowca> kierowcyFromDb;

		using (var repo = new DatabaseRepository<Kierowca>(new AutobusyContext()))
		{
			kierowcyFromDb = repo.List(x => x.Przejazdy);
		}

		using (var repo = new DatabaseRepository<Przejazd>(new AutobusyContext()))
		{
			foreach (var kierowca in kierowcyFromDb)
			{
				var przejazdy = new List<Przejazd>();
				
				foreach (var przejazd in kierowca.Przejazdy)
				{
					var przejazdFromDb = repo.GetById(przejazd.Id, x=>x.Kurs, y=>y.Kurs.Linia);
					
					przejazdy.Add(przejazdFromDb);
				}

				kierowca.Przejazdy = przejazdy;
			}
		}

		container.Table(table =>
		{
			table.ColumnsDefinition(columns =>
			{
				columns.ConstantColumn(25); // Id
				columns.RelativeColumn(2); // Imię i nazwisko
				columns.RelativeColumn(); // Ilość przejazdów
				columns.RelativeColumn(); // Średnie spalanie
				columns.RelativeColumn(); // Ilość spóźnień
				columns.RelativeColumn(); // Średnie spóźnienie
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
				table.Cell().Element(CellStyle).Text(kierowca.Id);
				table.Cell().Element(CellStyle).Text(kierowca.Imie + " " + kierowca.Nazwisko);
				table.Cell().Element(CellStyle).AlignRight().Text(kierowca.Przejazdy.Count);

				double spalonePaliwo = 0;
				double iloscKilometrow = 0;

				foreach (var przejazd in kierowca.Przejazdy)
				{
					spalonePaliwo += przejazd.IloscSpalonegoPaliwa;
					iloscKilometrow += przejazd.Kurs.Linia.DlugoscWKm;
				}

				var srednieSpalonePaliwoNa1Km = iloscKilometrow == 0 ? 0 : spalonePaliwo / iloscKilometrow;

				table.Cell().Element(CellStyle).AlignRight().Text(srednieSpalonePaliwoNa1Km.ToString("0.00"));

				double iloscMinutSpoznienia = 0;
				var iloscSpoznien = 0;

				foreach (var przejazd in kierowca.Przejazdy)
				{
					if (przejazd.RealizacjePrzejazdu is not null && przejazd.RealizacjePrzejazdu.Count > 0)
					{
						iloscSpoznien++;
						iloscMinutSpoznienia += przejazd.RealizacjePrzejazdu.Sum(x => (x.FaktycznaGodzina - x.PlanKursu.PlanowaGodzina).TotalMinutes);
					}
				}

				table.Cell().Element(CellStyle).AlignRight().Text(iloscSpoznien);

				var srednieSpoznienie = iloscSpoznien == 0 ? 0 : iloscMinutSpoznienia / iloscSpoznien;

				table.Cell().Element(CellStyle).AlignRight().Text(srednieSpoznienie);
			}
		});
	}

	
	private static void CreateLinieInfo(IContainer container)
	{
		List<Linia> linieFromDb;

		using (var repo = new DatabaseRepository<Linia>(new AutobusyContext()))
		{
			linieFromDb = repo.List(x => x.Kursy);
		}

		using (var repo = new DatabaseRepository<Kurs>(new AutobusyContext()))
		{
			foreach (var linia in linieFromDb)
			{
				var kursyLinii = repo.List(x => x.LiniaId == linia.Id, y => y.Linia, z => z.Przejazdy);;
				
				container.Column(column =>
				{
					// Informacje o linii
					column.Item().Text(text =>
					{
						text.Line($"Id linii: {linia.Id}");
						text.Line($"Numer linii: {linia.Numer}");
						text.Line($"Typ linii: {linia.Typ.ToString()}");
						text.Line($"Długość linii: {linia.DlugoscWKm:0.00}");
					});
					
					// Tabela z kursami
					column.Item().Table(table =>{
						table.ColumnsDefinition(columns =>
						{
							columns.ConstantColumn(25); // Id kursu w ramach linii
							columns.RelativeColumn(2); // Dzień tygodnia
							columns.RelativeColumn(1); // Godzina
							columns.RelativeColumn(1); // Ilość przejazdów w ramach kursu
							columns.RelativeColumn(1); // Ilość sprzedanych biletów
							columns.RelativeColumn(1); // Ilość spalonego paliwa
						});

						table.Header(header =>
						{
							header.Cell().Element(CellStyle).Text("Id");
							header.Cell().Element(CellStyle).Text("Dzień tygodnia");
							header.Cell().Element(CellStyle).AlignRight().Text("Godzina");
							header.Cell().Element(CellStyle).AlignRight().Text("Ilość przejazdów");
							header.Cell().Element(CellStyle).AlignRight().Text("Ilość sprzedanych biletów");
							header.Cell().Element(CellStyle).AlignRight().Text("Ilość spalonego paliwa");
						});
						
						foreach (var kurs in kursyLinii)
						{
							table.Cell().Element(CellStyle).Text(kurs.Id);
							table.Cell().Element(CellStyle).Text(kurs.DzienTygodnia.ToString());
							table.Cell().Element(CellStyle).AlignRight().Text(kurs.GodzinaRozpoczecia.ToString("hh:mm"));
							table.Cell().Element(CellStyle).AlignRight().Text(kurs.Przejazdy.Count);

							if (kurs.Przejazdy.Count == 0)
							{
								table.Cell().Element(CellStyle).AlignRight().Text("-");
								table.Cell().Element(CellStyle).AlignRight().Text("-");
							}
							else
							{
								table.Cell().Element(CellStyle).AlignRight().Text(kurs.Przejazdy.Sum(x => x.IloscSkasowanychBiletow));
								table.Cell().Element(CellStyle).AlignRight().Text(kurs.Przejazdy.Sum(x => x.IloscSpalonegoPaliwa));
							}
						}
					});
				});
			}
		}
	}

	private static void CreateEntityInfo(IContainer container)
	{
		container.Table(table =>
		{
			table.ColumnsDefinition(columns =>
			{
				columns.ConstantColumn(25); // Lp
				columns.RelativeColumn(2); // Typ obiektu
				columns.RelativeColumn(); // Ilość obiektów w bazie danych
				columns.RelativeColumn(2); // Puste miejsce
			});

			table.Header(header =>
			{
				header.Cell().Element(CellStyle).Text("#");
				header.Cell().Element(CellStyle).Text("Typ obiektu");
				header.Cell().Element(CellStyle).AlignRight().Text("Ilość obiektów");
				header.Cell().Element(CellStyle).AlignRight().Text("");
			});

			using (var statsRepo = new StatisticsRepository(new AutobusyContext()))
			{
				var i = 1;

				foreach ((var header, var count) in statsRepo.GetDatabaseObjectsStatistics())
				{
					table.Cell().Element(CellStyle).Text($"{i++}");
					table.Cell().Element(CellStyle).Text(header);
					table.Cell().Element(CellStyle).AlignRight().Text(count);
					table.Cell().Element(CellStyle).AlignRight().Text("");
				}
			}
		});
	}

	private static IContainer CellStyle(IContainer container)
	{
		return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
	}
}