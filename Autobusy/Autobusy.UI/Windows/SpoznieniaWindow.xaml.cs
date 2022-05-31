using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using Autobusy.Logic.Contexts;
using Autobusy.Logic.Models;
using Autobusy.Logic.Repositories;

namespace Autobusy.UI.Windows;

public partial class SpoznieniaWindow : Window
{
	private readonly Przejazd _przejazd;

	private readonly List<RealizacjaPrzejazdu> _realizacjePrzejazdu;

	public SpoznieniaWindow(Przejazd przejazd)
	{
		InitializeComponent();

		using (var repo = new DatabaseRepository<Przejazd>(new AutobusyContext()))
		{
			_przejazd = repo.GetById(przejazd.Id, x => x.RealizacjePrzejazdu, y=>y.Kurs, z=>z.Kierowca, a=>a.Kurs.Linia, b=>b.Kurs.PlanyKursu);
		}

		using (var repo = new DatabaseRepository<PrzystanekWLinii>(new AutobusyContext()))
		{
			foreach (var planKursu in _przejazd.Kurs.PlanyKursu)
			{
				planKursu.PrzystanekWLinii = repo.GetById(planKursu.PrzystanekWLiniiId, x => x.Przystanek);
			}
		}

		PrzejazdInfoBlock.Text = $"Data: {_przejazd.Data}, linia: {_przejazd.Kurs.Linia.Numer}";

		_realizacjePrzejazdu = new List<RealizacjaPrzejazdu>();

		foreach (var planKursu in _przejazd.Kurs.PlanyKursu)
		{
			var realizacjaPrzejazdu = new RealizacjaPrzejazdu
			{
				FaktycznaGodzina = planKursu.PlanowaGodzina,
				PlanKursu = planKursu,
				Przejazd = _przejazd
			};

			_realizacjePrzejazdu.Add(realizacjaPrzejazdu);

			planKursu.RealizacjePrzejazdu = _realizacjePrzejazdu;
		}

		_przejazd.RealizacjePrzejazdu = _realizacjePrzejazdu;

		this.DataContext = _realizacjePrzejazdu;
	}

	private void SpoznieniaWindow_OnClosing(object sender, CancelEventArgs e)
	{
		using (var repo = new DatabaseRepository<RealizacjaPrzejazdu>(new AutobusyContext()))
		{
			foreach (var realizacjaPrzejazdu in _realizacjePrzejazdu)
			{
				repo.ExecuteSqlQuery($"INSERT INTO dbo.RealizacjePrzejazdu (FaktycznaGodzina, PlanKursuId, PrzejazdId) VALUES ('{realizacjaPrzejazdu.FaktycznaGodzina:s}', {realizacjaPrzejazdu.PlanKursu.Id}, {realizacjaPrzejazdu.Przejazd.Id})");
			}
		}
	}
}