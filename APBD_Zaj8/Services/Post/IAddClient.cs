namespace APBD_Zaj8.Services.Post;
using APBD_Zaj8.Models;
public interface IAddClient
{

    Task<int> AddClient(string firstName, string lastName, string email, string telephone, string pesel);

    public Task<int> AddClient(ClientDTO client)
    {
        return AddClient(client.FirstName, client.LastName, client.Email, client.Phone, client.Pesel);
    }

}