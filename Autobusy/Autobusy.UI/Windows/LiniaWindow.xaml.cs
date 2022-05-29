using System.ComponentModel;
using System.Windows;
using Autobusy.Logic.Contexts;
using Autobusy.Logic.Models;
using Autobusy.Logic.Repositories;

namespace Autobusy.UI.Windows;

public partial class LiniaWindow : Window
{
	private readonly Linia _linia;

	public LiniaWindow(Linia linia)
	{
		InitializeComponent();

		_linia = linia;

		DataContext = _linia;
	}

	private void LiniaWindow_OnClosing(object sender, CancelEventArgs e)
	{
		using (var repo = new DatabaseRepository<Linia>(new AutobusyContext()))
		{
			repo.Update(_linia);
		}
	}
}