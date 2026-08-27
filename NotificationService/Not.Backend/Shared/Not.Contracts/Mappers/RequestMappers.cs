using Not.Contracts.DTOs.Requests;
using Not.RabbitMq.Models;
using Riok.Mapperly.Abstractions;

namespace Not.Contracts.Mappers;


[Mapper]
public partial class NotificationRequestMapper
{
    [MapProperty(nameof(SendNotificationRequest.Message), nameof(NotificationEventModel.Message))]
    [MapProperty(nameof(SendNotificationRequest.SenderUsername), nameof(NotificationEventModel.Sender))]
    [MapProperty(nameof(SendNotificationRequest.ReceiverUsername), nameof(NotificationEventModel.Recipient))]
    public partial NotificationEventModel SendNotificationRequestToNotificationEventModel(SendNotificationRequest req, DateTime timestamp);


    [MapProperty(nameof(SendNotificationRequest.Message), nameof(NotificationMessageModel.Message))]
    [MapProperty(nameof(SendNotificationRequest.SenderUsername), nameof(NotificationMessageModel.Sender))]
    [MapProperty(nameof(SendNotificationRequest.ReceiverUsername), nameof(NotificationMessageModel.Recipient))]
    public partial NotificationMessageModel SendNotificationRequestToNotificationMessageModel(SendNotificationRequest req, DateTime timestamp);

}