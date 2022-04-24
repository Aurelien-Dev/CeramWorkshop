function SetFirstActiveCarouselItem() {
    $(".carousel-item").each(function () {
        $(this).removeClass("active");
    });

    $(".carousel-item:first").addClass("active");

    bootstrap.Carousel.getOrCreateInstance("#carouselExampleDark");
    //var myCarousel = document.querySelector('#carouselExampleDark')
    //var carousel = new bootstrap.Carousel(myCarousel)

}