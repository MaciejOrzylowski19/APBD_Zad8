namespace APBD_Zaj8.Services;
using Microsoft.Data.SqlClient;

public class TripsDBConnection
{
    
    public readonly static string ConnectionString = "Data Source=db-mssql;Initial Catalog=2019SBD;Integrated Security=True;Trust Server Certificate=True";
    
    public static SqlConnection GetConnection()
    {
        return new SqlConnection(ConnectionString);
    }
    
}