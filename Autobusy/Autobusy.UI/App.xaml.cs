using System.Windows;
using Autobusy.Logic.Contexts;

namespace Autobusy;

public partial class App : Application
{
	private void App_OnStartup(object sender, StartupEventArgs e)
	{
		using (var db = new AutobusyContext())
		{
			if (!db.Database.CanConnect()) db.Database.EnsureCreated();
		}
	}
}