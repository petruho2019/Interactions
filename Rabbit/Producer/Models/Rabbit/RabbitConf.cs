namespace RabbitMQInteraction.Models.Rabbit;

public record RabbitConf
{
    public string Host { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Port { get; set; }
}