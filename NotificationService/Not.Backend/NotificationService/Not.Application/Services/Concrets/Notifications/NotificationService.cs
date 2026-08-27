using Microsoft.AspNetCore.SignalR;
using Not.Application.Hubs;
using Not.Application.Services.Abstracts.Notifications;
using Not.Contracts.DTOs.Requests;
using Not.Contracts.Mappers;
using Not.RabbitMq.Helpers.Abstracts;

namespace Not.Application.Services.Concrets.Notifications;


public class NotificationService(IRabbitHelper rabbitHelper, NotificationRequestMapper mapper, IHubContext<NotificationHub> hub) : INotificationService
{
    public async Task SendRabbitNotificationAsync(SendNotificationRequest request)
    {
        var rabbitMessage = mapper.SendNotificationRequestToNotificationEventModel(request, DateTime.UtcNow);

        await rabbitHelper.PublishMessage(rabbitMessage);
    }

    public async Task SendSignalRNotificationAsync(SendNotificationRequest request)
    {
        // var signalrMessage = mapper.SendNotificationRequestToNotificationMessageModel(request, DateTime.UtcNow);

        // await hub.Clients.All.SendAsync("ReceiveNotification", signalrMessage);
    }
}