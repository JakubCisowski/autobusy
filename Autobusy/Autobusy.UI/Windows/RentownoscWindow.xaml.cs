using System.Globalization;
using System.Linq;
using System.Windows;
using Autobusy.Logic.Contexts;
using Autobusy.Logic.Models;
using Autobusy.Logic.Repositories;

namespace Autobusy.UI.Windows;

public partial class RentownoscWindow : Window
{
	private readonly Linia _linia;

	public RentownoscWindow(Linia linia)
	{
		InitializeComponent();

		using (var repo = new DatabaseRepository<Linia>(new AutobusyContext()))
		{
			_linia = repo.GetById(linia.Id, x => x.Kursy);
		}

		using (var repo = new DatabaseRepository<Przejazd>(new AutobusyContext()))
		{
			foreach (var kurs in _linia.Kursy)
			{
				kurs.Przejazdy = repo.List(x => x.KursId == kurs.Id, y=>y.Kurs);
			}
		}
		
		ObliczRentownosc();
	}

	private void ObliczRentownosc()
	{
		var iloscKursow = _linia.Kursy?.Count;

		var iloscSpalonegoPaliwa = _linia.Kursy?.Sum(x => x.Przejazdy?.Sum(y => y.IloscSpalonegoPaliwa));

		var iloscSkasowanychBiletow = _linia.Kursy?.Sum(x => x.Przejazdy?.Sum(y => y.IloscSkasowanychBiletow));

		var kursNajwiekszyRuch = _linia.Kursy?.SelectMany(x => x.Przejazdy).OrderByDescending(x => x?.IloscSkasowanychBiletow).FirstOrDefault()?.Kurs;

		var najwiekszyRuchDzienTygodnia = kursNajwiekszyRuch?.DzienTygodnia.ToString();
		var najwiekszyRuchGodzina = kursNajwiekszyRuch?.GodzinaRozpoczecia.ToString("HH:mm:ss");

		var kursNajmniejszyRuch = _linia.Kursy?.SelectMany(x => x.Przejazdy).OrderBy(x => x.IloscSkasowanychBiletow).FirstOrDefault()?.Kurs;

		var najmniejszyRuchDzienTygodnia = kursNajmniejszyRuch?.DzienTygodnia.ToString();
		var najmniejszyRuchGodzina = kursNajmniejszyRuch?.GodzinaRozpoczecia.ToString("HH:mm:ss");

		IloscKursowBlock.Text = iloscKursow.HasValue ? iloscKursow.Value.ToString() : "0";
		IloscPaliwaBlock.Text = iloscSpalonegoPaliwa.HasValue ? iloscSpalonegoPaliwa.Value.ToString(CultureInfo.CurrentCulture) : "Brak danych";
		IloscBiletowBlock.Text = iloscSkasowanychBiletow.HasValue ? iloscSkasowanychBiletow.Value.ToString() : "Brak danych";
		NajwiekszyRuchBlock.Text = kursNajwiekszyRuch is null ? "Brak danych" : najwiekszyRuchDzienTygodnia + " " + najwiekszyRuchGodzina;

		if (kursNajmniejszyRuch?.Id != kursNajwiekszyRuch?.Id)
		{
			NajmniejszyRuchBlock.Text = kursNajmniejszyRuch is null ? "Brak danych" : najmniejszyRuchDzienTygodnia + " " + najmniejszyRuchGodzina;
		}
	}
}