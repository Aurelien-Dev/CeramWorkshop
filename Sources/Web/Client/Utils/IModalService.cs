using Microsoft.AspNetCore.Components;

namespace Client.Utils
{
    public interface IModalService
    {
        ModalInstance OpenModal<TModal>(string id, Dictionary<string, object> parameters) where TModal : ComponentBase;
    }
}