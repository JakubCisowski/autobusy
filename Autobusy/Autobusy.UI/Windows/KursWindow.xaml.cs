using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Autobusy.Logic.Models;
using Autobusy.Logic.Operations;

namespace Autobusy.UI.Windows;

public partial class KursWindow : Window
{
	private Kurs _kurs;

	private List<PlanKursu> _planyKursu;
	
	public KursWindow(Kurs kurs)
	{
		InitializeComponent();
		
		_kurs = kurs;
		
		if (_kurs.PlanyKursu is null)
		{
			_kurs.PlanyKursu = new List<PlanKursu>();
		}
		
		_planyKursu = _kurs.PlanyKursu;
		
		var przystanki = DatabaseOperations.GetPrzystanki();

		foreach (var planKursu in _planyKursu)
		{
			var przystanek = przystanki.FirstOrDefault(x => x.Przystanki.Any(y=>y.PrzystanekWLiniiId == planKursu.PrzystanekWLinii.PrzystanekWLiniiId));

			if (przystanek != null)
			{
				planKursu.PrzystanekWLinii.Przystanek = przystanek;
			}
		}

		this.DataContext = _planyKursu;

		KursInfoBlock.Text = $"{_kurs.KursId} - {_kurs.DzienTygodnia.ToString()} - {_kurs.GodzinaRozpoczecia.ToString("HH:mm:ss")}";
	}

	private void KursWindow_OnClosing(object? sender, CancelEventArgs e)
	{
		//DatabaseOperations.UpdateCollection(new List<Kurs> { _kurs });
	}

	private void DodajPlanKursuButton_OnClick(object sender, RoutedEventArgs e)
	{
		var nowyPlanKursu = new PlanKursu()
		{
			Kurs = _kurs
		};
		
		_kurs.PlanyKursu.Add(nowyPlanKursu);
		
		PlanyKursowGrid.Items.Refresh();
	}

	private void UsuwanieKursuButton_OnClick(object sender, RoutedEventArgs e)
	{
		if ((sender as Button)?.CommandParameter is not PlanKursu planKursu)
		{
			return;
		}

		_planyKursu.Remove(planKursu);
		DatabaseOperations.Delete(planKursu);
		
		PlanyKursowGrid.Items.Refresh();
	}
}