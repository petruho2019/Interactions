namespace Not.Contracts.DTOs.Requests;


public record SendNotificationRequest
{
    public string SenderUsername { get; set; }
    public string ReceiverUsername { get; set; }
    public string Message { get; set; }
}