using System.ComponentModel;

namespace Not.RabbitMq.Models;


[Description("Model for notification messages sent through SignalR.")]
public record NotificationMessageModel
{
    public string Message { get; set; }
    public string Recipient { get; set; }
    public string Sender { get; set; }
    public DateTime Timestamp { get; set; }
}