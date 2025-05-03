namespace APBD_Zaj8.Services;
using Microsoft.Data.SqlClient;

public class TripsDBConnection
{
    
    public readonly static string ConnectionString = "Server=db-mssql;Database=2019SBD;User Id=s31569;Password=r!6P5jG*3;TrustServerCertificate=True;";
    
    public static SqlConnection GetConnection()
    {
        return new SqlConnection(ConnectionString);
    }
    
}