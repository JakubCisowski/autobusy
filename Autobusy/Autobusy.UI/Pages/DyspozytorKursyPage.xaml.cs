using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Autobusy.Logic.Models;

namespace Autobusy.UI.Pages;

public partial class DyspozytorKursyPage : Page
{
	private List<Kurs> _kursy;
	
	public DyspozytorKursyPage()
	{
		InitializeComponent();

		_kursy = new List<Kurs>()
		{
			new Kurs()
			{
				KursId = 1,
				DzienTygodnia = DzienTygodnia.Poniedzialek
			}
		};

		this.DataContext = _kursy;
	}

	private void BackButton_OnClick(object sender, RoutedEventArgs e)
	{
		var window = Application.Current.MainWindow as MainWindow;

		window.MainFrame.Navigate(new DyspozytorMenuPage());
	}
}