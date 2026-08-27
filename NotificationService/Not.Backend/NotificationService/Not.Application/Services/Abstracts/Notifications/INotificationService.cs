using Not.Contracts.DTOs.Requests;
using Not.RabbitMq.Helpers.Abstracts;

namespace Not.Application.Services.Abstracts.Notifications;


public interface INotificationService
{
    Task SendRabbitNotificationAsync(SendNotificationRequest request);
    Task SendSignalRNotificationAsync(SendNotificationRequest request);
}