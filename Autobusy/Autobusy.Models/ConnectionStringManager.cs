namespace Autobusy.Models;

public static class ConnectionStringManager
{
	public static string ConnectionString => "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AutobusyDB.MDF;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

}