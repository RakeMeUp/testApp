using Backend.Models;

namespace Backend.Services.Interfaces
{
    public interface IAGIService
    {
        Task<AGIResponseDTO> Grade(string prompt);
    }
}
