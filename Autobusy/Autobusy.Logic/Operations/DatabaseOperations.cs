using Autobusy.Logic.Contexts;
using Autobusy.Logic.Models;
using Microsoft.EntityFrameworkCore;

namespace Autobusy.Logic.Operations;

public static class DatabaseOperations
{
	public static TEntity GetById<TEntity>(int id) where TEntity:class
	{
		using (var db = new AutobusyContext())
		{
			return db.Set<TEntity>().Find(id);
		}
	}
	
	public static List<TEntity> GetCollection<TEntity>() where TEntity:class
	{
		using (var db = new AutobusyContext())
		{
			return db.Set<TEntity>().ToList();
		}
	}

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
	
	public static void Add<TEntity>(TEntity entity) where TEntity:class
	{
		using (var db = new AutobusyContext())
		{
			db.Set<TEntity>().Add(entity);
			db.SaveChanges();
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

				db.Kursy.Add(kurs);
			}

			db.SaveChanges();
		}
	}

	public static void AddSerwis(Serwis serwis)
	{
		using (var db = new AutobusyContext())
		{
			db.Serwisy.Add(serwis);
			
			db.SaveChanges();
		}
	}

	public static void UpdateKursy(List<Kurs> kursy)
	{
		using (var db = new AutobusyContext())
		{
			foreach (var kurs in kursy)
			{
				var liniaFromDb = db.Linie.Find(kurs.Linia.LiniaId);
				kurs.Linia = liniaFromDb;
				
				var kursFromDb = db.Kursy.Find(kurs.KursId);

				if (kursFromDb is null)
				{
					db.Kursy.Add(kurs);
				}
				else
				{
					db.Entry(kursFromDb).CurrentValues.SetValues(kurs);
					db.Entry(kursFromDb).State = EntityState.Modified;
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