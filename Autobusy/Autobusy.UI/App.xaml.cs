using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Autobusy.Logic.Contexts;

namespace Autobusy
{
	public partial class App : Application
	{
		private void App_OnStartup(object sender, StartupEventArgs e)
		{
			using (var db = new AutobusyContext())
			{
				if (!db.Database.CanConnect())
				{
					db.Database.EnsureCreated();
				}
			}
		}
	}
}