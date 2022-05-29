using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Autobusy.Logic.Contexts;
using Autobusy.Logic.Models;
using Autobusy.Logic.Repositories;

namespace Autobusy.UI.Windows;

public partial class KursWindow : Window
{
	private readonly Kurs _kurs;

	private readonly List<PlanKursu> _planyKursu;

	public KursWindow(Kurs kurs)
	{
		InitializeComponent();

		using (var repo = new DatabaseRepository<Kurs>(new AutobusyContext()))
		{
			var kursFromDb = repo.GetById(kurs.Id, x => x.PlanyKursu);

			_kurs = kursFromDb;
		}

		using (var repo = new DatabaseRepository<PrzystanekWLinii>(new AutobusyContext()))
		{
			foreach (var planKursu in _kurs.PlanyKursu)
			{
				planKursu.PrzystanekWLinii = repo.GetById(planKursu.PrzystanekWLiniiId, x => x.Przystanek);
			}
		}

		_planyKursu = _kurs.PlanyKursu;

		this.DataContext = _planyKursu;

		KursInfoBlock.Text = $"{_kurs.Id} - {_kurs.DzienTygodnia.ToString()} - {_kurs.GodzinaRozpoczecia.ToString("HH:mm:ss")}";
	}

	private void KursWindow_OnClosing(object sender, CancelEventArgs e)
	{
		using (var repo = new DatabaseRepository<Kurs>(new AutobusyContext()))
		{
			var kursFromDb = repo.GetById(_kurs.Id, x=>x.PlanyKursu);
			
			kursFromDb.PlanyKursu = _planyKursu;
			
			repo.SaveChanges();
		}
	}

	private void DodajPlanKursuButton_OnClick(object sender, RoutedEventArgs e)
	{
		var nowyPlanKursu = new PlanKursu
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

		using (var repo = new DatabaseRepository<PlanKursu>(new AutobusyContext()))
		{
			repo.Delete(planKursu);
		}

		PlanyKursowGrid.Items.Refresh();
	}
}