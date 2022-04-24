using Microsoft.AspNetCore.Components;

namespace Common.Helpers.RazorComponent.CommonControls
{
    public partial class CarouselCard : PageComponentBase
    {
        [Parameter]
        public IList<CarouselCardItem> CarouselItem { get; set; } = new List<CarouselCardItem>();

        [Parameter]
        public int Interval { get; set; } = 5000;

        [Parameter]
        public EventCallback<CarouselCardItem> OnDeleteCard { get; set; }

        [Parameter]
        public bool ShowDeleteButton { get; set; } = false;

        protected override void OnInitialized()
        {
            JSRuntime.InvokeAsync<string>("SetFirstActiveCarouselItem", null);
        }


        private async Task DeleteCmd(CarouselCardItem item)
        {
            _ = await JSRuntime.InvokeAsync<string>("SetFirstActiveCarouselItem", null);

            await OnDeleteCard.InvokeAsync(item);
        }

    }

    public class CarouselCardItem
    {
        public int? Id { get; set; } = null;
        public string ImageUrl { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public object ObjectMapped { get; set; }

        public CarouselCardItem(int? id, string imageUrl, string comment, object objectMapped)
        {
            Id = id;
            ImageUrl = imageUrl;
            Comment = comment;
            ObjectMapped = objectMapped;
        }
    }
}