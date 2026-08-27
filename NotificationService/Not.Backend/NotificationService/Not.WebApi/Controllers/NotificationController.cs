using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Not.Application.Services.Abstracts.Notifications;
using Not.Contracts.DTOs.Requests;

namespace Not.WebApi.Controllers;



[ApiController]
[Route("api/v1/{controller}")]
[Authorize]
public class NotificationController(INotificationService notifService) : ControllerBase
{

    [HttpPost("send")]
    public async Task<IActionResult> SendNotification([FromBody] SendNotificationRequest request)
    {
        await notifService.SendSignalRNotificationAsync(request);

        return Ok(new { Message = "Notification sent successfully." });
    }

}