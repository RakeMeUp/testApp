using Microsoft.AspNetCore.Components;
using TestApp.Client.Services.Interfaces;
namespace TestApp.Client.Services
{
    public class QRService(NavigationManager navigationManager) : IQRService
    {
        public string GetQRCodeURI(long testId)
        {
            string uri =navigationManager.ToAbsoluteUri($"/test/{testId}").ToString();
            return $"https://api.qrserver.com/v1/create-qr-code/?data={uri}&size=100x100";
        }
    }
}
