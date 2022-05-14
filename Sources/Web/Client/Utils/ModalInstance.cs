using Microsoft.AspNetCore.Components;

namespace Client.Utils
{
    public class ModalInstance
    {
        public string Id { get; set; }
        internal RenderFragment ModalFragment { get; set; }
        public Action<bool> ModalClosed { get; set; }


        public ModalInstance()
        {

        }

        public ModalInstance(string id, RenderFragment modale)
        {
            Id = id;
            ModalFragment = modale;
        }



    }
}
