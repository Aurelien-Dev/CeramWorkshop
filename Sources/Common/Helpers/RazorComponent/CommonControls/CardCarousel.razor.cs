﻿using Microsoft.AspNetCore.Components;

namespace Common.Helpers.RazorComponent.CommonControls
{
    public partial class CardCarousel : ComponentBase
    {
        [Parameter]
        public IList<CardCarouselItem> CarouselItem { get; set; } = new List<CardCarouselItem>();

        [Parameter]
        public int Interval { get; set; } = 5000;

        public CardCarousel()
        {
            //CarouselItem.Add(new CardCarouselItem("assets/IMG_1892.jpg", "plein de de  blab lade  blab lade  blab lade  blab lade  blab lade  blab lade  blab lade  blab lade  blab lade  blab lade  blab lade  blab lade  blab lade  blab lade  blab lablabla;"));
            //CarouselItem.Add(new CardCarouselItem("assets/IMG_1680.jpg", "plein de blde  blab laabla;"));
            //CarouselItem.Add(new CardCarouselItem("assets/IMG_1892.jpg", "plein de blade  blab lab  lablablapl ein de blablaplein de blablaplein de blablaplein de blablaplein de blablaplein de blablaplein de blablaplein de blablaplein de blablaplein de blablaplein de blablaplein de blablaplein de blablaplein de blablaplein de blablaplein de blablaplein de blablaplein de blablaplein de blablaplein de blablaplein de blablaplein de blablaplein de blablaplein de blablaplein de blablaplein de blablaplein de blablaplein de blablaplein de blablaplein de blablaplein de blablaplein de blablaplein de blablaplein de blablaplein de blablaplein de blablaplein de blablaplein de blablaplein de blablaplein de blablaplein de blablaplein de blablaplein de blablaplein de blablaplein de blabla;"));
            //CarouselItem.Add(new CardCarouselItem("assets/IMG_1892.jpg", "plein de  blade  blab lab lade  blab lade  blab lade  blab lade  blab lade  blab lade  blab lade  blab lade  blab la;"));
            //CarouselItem.Add(new CardCarouselItem("assets/IMG_1680.jpg", "plein de blabla;"));
            //CarouselItem.Add(new CardCarouselItem("assets/IMG_1892.jpg", "plein de blablde  blab lade  blab lade  blab lade  blab lade  blab laa;"));
            //CarouselItem.Add(new CardCarouselItem("assets/IMG_1892.jpg", "plde  blab lade  blab lde  blab laade  blab laeinde  blab la de blabla;"));
        }

        public void AddItem(CardCarouselItem item)
        {
            CarouselItem.Add(item);
        }

    }

    public class CardCarouselItem
    {
        public string ImageUrl { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;

        public CardCarouselItem(string imageUrl, string comment)
        {
            ImageUrl = imageUrl;
            Comment = comment;
        }
    }
}