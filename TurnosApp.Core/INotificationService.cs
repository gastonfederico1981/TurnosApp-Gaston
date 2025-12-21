namespace TurnosApp.Core.Services
{
    public interface INotificationService
    {
        Task EnviarMensaje(string telefono, string mensaje);
    }

    public class WhatsAppMockService : INotificationService
    {
        public async Task EnviarMensaje(string telefono, string mensaje)
        {
            // Aquí iría la llamada a la API de WhatsApp
            await Task.Delay(100); 
            Console.WriteLine($"[WHATSAPP SENT TO {telefono}]: {mensaje}");
        }
    }
}