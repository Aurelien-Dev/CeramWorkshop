using Microsoft.AspNetCore.Components;

namespace Client.Utils
{
    public class ModalService : IModalService
    {
        internal event Func<ModalInstance, Task> ModalAdded;

        public Func<string, Task> ShowModal;
        public Func<string, Task> CloseModal;

        public ModalInstance OpenModal<TModal>(string id, Dictionary<string, object> parameters) where TModal : ComponentBase
        {
            ModalInstance instance = new() { Id = id };

            RenderFragment modale = __builder =>
            {
                __builder.OpenComponent(1, typeof(TModal));
                __builder.AddAttribute(2, "IdModal", id);
                __builder.AddAttribute(3, "Instance", instance);
                __builder.AddMultipleAttributes(4, parameters);
                __builder.CloseComponent();
            };

            instance.ModalFragment = modale;
            ModalAdded?.Invoke(instance);

            return instance;
        }
    }
}
