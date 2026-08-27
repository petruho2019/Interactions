namespace Not.Contracts.Hubs;



public interface IBaseHub
{
    Task SendNotification(string message, string recieverUsername);
}