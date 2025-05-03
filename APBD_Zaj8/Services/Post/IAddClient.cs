namespace APBD_Zaj8.Services.Post;

public interface IAddClient
{

    Task<int> AddClient(string firstName, string lastName, string email, string telephone, string pesel);

}