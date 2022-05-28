using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Autobusy.Logic.Models;
using Autobusy.Logic.Operations;

namespace Autobusy.UI.Windows;

public partial class SpoznieniaWindow : Window
{
	private Przejazd _przejazd;

	private List<RealizacjaPrzejazdu> _realizacjePrzejazdu;

	public SpoznieniaWindow(Przejazd przejazd)
	{
		InitializeComponent();

		_przejazd = DatabaseOperations.GetPrzejazd(przejazd.PrzejazdId);

		PrzejazdInfoBlock.Text = $"Data: {_przejazd.Data}, linia: {_przejazd.Kurs.Linia.Numer}";

		_realizacjePrzejazdu = new List<RealizacjaPrzejazdu>();
		
		foreach (var planKursu in _przejazd.Kurs.PlanyKursu)
		{
			var realizacjaPrzejazdu = new RealizacjaPrzejazdu()
			{
				FaktycznaGodzina = DateTime.Now
			};
			
			_realizacjePrzejazdu.Add(realizacjaPrzejazdu);

			planKursu.RealizacjePrzejazdu = _realizacjePrzejazdu;
		}

		_przejazd.RealizacjePrzejazdu = _realizacjePrzejazdu;

		this.DataContext = _realizacjePrzejazdu;
	}

	private void SpoznieniaWindow_OnClosing(object sender, CancelEventArgs e)
	{
		// todo: zapisz spoznienia
	}
}