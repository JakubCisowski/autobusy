using Autobusy.Logic.Contexts;
using Autobusy.Logic.Models;
using Microsoft.EntityFrameworkCore;

namespace Autobusy.Logic.Operations;

public static class DatabaseOperations
{
	public static List<Przystanek> GetPrzystanki()
	{
		using (var db = new AutobusyContext())
		{
			return db.Przystanki.Include(x=>x.Przystanki).ToList();
		}
	}

	public static List<PrzystanekWLinii> GetPrzystankiWLinii()
	{
		using (var db = new AutobusyContext())
		{
			return db.PrzystankiWLinii.Include(x => x.Linia).Include(y => y.Przystanek).ToList();
		}
	}
	
	public static List<Linia> GetLinie()
	{
		using (var db = new AutobusyContext())
		{
			return db.Linie.Include(x=>x.Przystanki).Include(y=>y.Kursy)
				.ToList();
		}
	}

	public static List<Kierowca> GetKierowcy()
	{
		using (var db = new AutobusyContext())
		{
			return db.Kierowcy.Include(x => x.Przejazdy).ToList();
		}
	}
	
	public static List<Autobus> GetAutobusy()
	{
		using (var db = new AutobusyContext())
		{
			return db.Autobusy.Include(x => x.Przejazdy).Include(y=>y.Serwisy).ToList();
		}
	}

	public static List<Kurs> GetKursy()
	{
		using (var db = new AutobusyContext())
		{
			return db.Kursy.Include(x => x.Linia).Include(y=>y.Przejazdy).ToList();
		}
	}

	public static List<Przejazd> GetPrzejazdy()
	{
		using (var db = new AutobusyContext())
		{
			return db.Przejazdy.Include(x => x.Autobus).Include(y => y.Kierowca).ToList();
		}
	}

	public static void AddPrzystanekWLinii(PrzystanekWLinii przystanekWLinii)
	{
		using (var db = new AutobusyContext())
		{
			var liniaZDb = db.Linie.Find(przystanekWLinii.Linia.LiniaId);
			
			var przystanekZDb = db.Przystanki.Find(przystanekWLinii.Przystanek.PrzystanekId);

			if (liniaZDb != null)
			{
				przystanekWLinii.Linia = liniaZDb;
				przystanekWLinii.Przystanek = przystanekZDb;

				db.PrzystankiWLinii.Add(przystanekWLinii);
			}

			db.SaveChanges();
		}
	}

	public static void AddKurs(Kurs kurs)
	{
		using (var db = new AutobusyContext())
		{
			var liniaZDb = db.Linie.Find(kurs.Linia.LiniaId);
			
			if (liniaZDb != null)
			{
				kurs.Linia = liniaZDb;
			}
			else
			{
				db.Attach(kurs.Linia);
			}

			foreach (var planKursu in kurs.PlanyKursu)
			{
				db.PlanyKursu.Add(planKursu);
			}
			
			// TODO: możliwe że trzeba wejść głębiej i pobrać z bazy danych np. przystanki
			
			db.Kursy.Add(kurs);

			db.SaveChanges();
		}
	}

	public static void DeleteKurs(Kurs kurs)
	{
		using (var db = new AutobusyContext())
		{
			var kursZDb = db.Kursy.Find(kurs.KursId);

			if (kursZDb != null)
			{
				db.Kursy.Remove(kursZDb);
			}

			db.SaveChanges();
		}
	}

	public static void UpdateKursy(IEnumerable<Kurs> kursy)
	{
		using (var db = new AutobusyContext())
		{
			foreach (var kurs in kursy)
			{
				var kursZDb = db.Kursy.Find(kurs.KursId);

				if (kursZDb != null)
				{
					kursZDb.DzienTygodnia = kurs.DzienTygodnia;
					kursZDb.GodzinaRozpoczecia = kurs.GodzinaRozpoczecia;
				}

				var liniaZDb = db.Linie.Find(kurs.Linia.LiniaId);
				
				if (liniaZDb != null)
				{
					kursZDb.Linia = liniaZDb;
				}
				else
				{
					db.Attach(kurs.Linia);
					kursZDb.Linia = kurs.Linia;
				}
			}

			db.SaveChanges();
		}
	}

	public static void AddSerwis(Serwis serwis)
	{
		using (var db = new AutobusyContext())
		{
			var autobusZDb = db.Autobusy.Find(serwis.NaprawianyAutobus.AutobusId);

			if (autobusZDb != null)
			{
				serwis.NaprawianyAutobus = autobusZDb;
			}
			else
			{
				db.Attach(serwis.NaprawianyAutobus);
			}
			
			db.Serwisy.Add(serwis);
			
			db.SaveChanges();
		}
	}

	public static void UpdateSerwisy(List<Serwis> serwisy)
	{
		using (var db = new AutobusyContext())
		{
			foreach (var serwis in serwisy)
			{
				var serwisZDb = db.Serwisy.Find(serwis.SerwisId);

				if (serwisZDb != null)
				{
					serwisZDb.Typ = serwis.Typ;
					serwisZDb.Cena = serwis.Cena;
					serwisZDb.Opis = serwis.Opis;
				}
				else
				{
					var autobusFromDb = db.Autobusy.Find(serwis.NaprawianyAutobus.AutobusId);
					serwis.NaprawianyAutobus = autobusFromDb;
					
					db.Serwisy.Add(serwis);
				}
			}

			db.SaveChanges();
		}
	}

	public static void UpdateCollection<TEntity>(IEnumerable<TEntity> entities) where TEntity:class
	{
		using (var db = new AutobusyContext())
		{
			foreach (var entity in entities)
			{
				var entityFromDb = db.Set<TEntity>().FirstOrDefault(x=>entity.Equals(x));

				if (entityFromDb is null)
				{
					db.Set<TEntity>().Add(entity);
				}
				else
				{
					db.Entry(entityFromDb).CurrentValues.SetValues(entity);
					db.Entry(entityFromDb).State = EntityState.Modified;
				}
			}
			
			db.SaveChanges();
		}
	}
	
	
	public static void Delete<TEntity>(TEntity entity) where TEntity:class
	{
		using (var db = new AutobusyContext())
		{
			db.Set<TEntity>().Remove(entity);
			db.SaveChanges();
		}
	}
}