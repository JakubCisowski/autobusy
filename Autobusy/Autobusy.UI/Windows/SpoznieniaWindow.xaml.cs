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
			_przejazd = repo.GetById(przejazd.Id);
		}

		PrzejazdInfoBlock.Text = $"Data: {_przejazd.Data}, linia: {_przejazd.Kurs.Linia.Numer}";

		_realizacjePrzejazdu = new List<RealizacjaPrzejazdu>();

		foreach (PlanKursu planKursu in _przejazd.Kurs.PlanyKursu)
		{
			var realizacjaPrzejazdu = new RealizacjaPrzejazdu
			{
				FaktycznaGodzina = DateTime.Now
			};

			_realizacjePrzejazdu.Add(realizacjaPrzejazdu);

			planKursu.RealizacjePrzejazdu = _realizacjePrzejazdu;
		}

		_przejazd.RealizacjePrzejazdu = _realizacjePrzejazdu;

		DataContext = _realizacjePrzejazdu;
	}

	private void SpoznieniaWindow_OnClosing(object sender, CancelEventArgs e)
	{
		using (var repo = new DatabaseRepository<Przejazd>(new AutobusyContext()))
		{
			repo.Update(_przejazd);
		}
	}
}