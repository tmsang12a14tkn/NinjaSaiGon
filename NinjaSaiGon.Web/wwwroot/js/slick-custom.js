

(function ($) {
    // USE STRICT
    "use strict";

        /*==================================================================
        [ Slick2 ]*/
        $('.wrap-slick2').each(function(){
            $(this).find('.slick2').slick({
              slidesToShow: 1,
              slidesToScroll: 1,
              infinite: false,
              autoplay: false,
              autoplaySpeed: 6000,
              arrows: true,
              appendArrows: $(this).find('.wrap-arrow-slick2'),
              prevArrow:'<button class="arrow-slick2 prev-slick2"><i class="fa fa-angle-left" aria-hidden="true"></i></button>',
              nextArrow:'<button class="arrow-slick2 next-slick2"><i class="fa fa-angle-right" aria-hidden="true"></i></button>',  
          });
        });


        /*==================================================================
        [ Slick3 ]*/
        $('.wrap-slick3').each(function(){
            var wrapSlick3 = $(this);
            var slick3 = $(this).find('.slick3');

            var delay = 100;
            var itemSlick3 = $(slick3).find('.item-slick3');
            var layerSlick3 = $(slick3).find('.layer-slick3');
            var actionSlick3 = [];
            

            $(slick3).on('init', function(){
                var layerCurrentItem = $(itemSlick3[0]).find('.layer-slick3');

                for(var i=0; i<actionSlick3.length; i++) {
                    clearTimeout(actionSlick3[i]);
                }

                $(layerSlick3).each(function(){
                    $(this).removeClass($(this).data('appear') + ' visible-true');
                });

                for(var i=0; i<layerCurrentItem.length; i++) {

                    if($(layerCurrentItem[i]).data('delay') != null) {
                      delay = $(layerCurrentItem[i]).data('delay');
                    }

                    actionSlick3[i] = setTimeout(function(index) {
                        $(layerCurrentItem[index]).addClass($(layerCurrentItem[index]).data('appear') + ' visible-true');
                    },delay,i); 
                }        
            });


            var showDot = false;
            if($(wrapSlick3).find('.wrap-slick3-dots').length > 0) {
                showDot = true;
            }

            var showArrow = false;
            if($(wrapSlick3).find('.wrap-slick3-arrows').length > 0) {
                showArrow = true;
            }

            $(wrapSlick3).find('.slick3').slick({
                pauseOnFocus: false,
                pauseOnHover: false,
                slidesToShow: 1,
                slidesToScroll: 1,
                fade: false,
                infinite: false,
                autoplay: true,
                autoplaySpeed: 6000,
                arrows: showArrow,
                appendArrows: $(wrapSlick3).find('.wrap-slick3-arrows'),
                prevArrow: $(wrapSlick3).find('.prev-slick3'),
                nextArrow: $(wrapSlick3).find('.next-slick3'),
                dots: showDot,
                appendDots: $(wrapSlick3).find('.wrap-slick3-dots'),
                dotsClass:'slick3-dots',
                customPaging: function(slick, index) {
                    return '<div></div>';
                },
            });

            $(slick3).on('afterChange', function(event, slick, currentSlide){ 

                var layerCurrentItem = $(itemSlick3[currentSlide]).find('.layer-slick3');

                for(var i=0; i<actionSlick3.length; i++) {
                    clearTimeout(actionSlick3[i]);
                }

                $(layerSlick3).each(function(){
                    $(this).removeClass($(this).data('appear') + ' visible-true');
                });

                for(var i=0; i<layerCurrentItem.length; i++) {

                    if($(layerCurrentItem[i]).data('delay') != null) {
                      delay = $(layerCurrentItem[i]).data('delay');
                    }

                    actionSlick3[i] = setTimeout(function(index) {
                        $(layerCurrentItem[index]).addClass($(layerCurrentItem[index]).data('appear') + ' visible-true');
                    },delay,i); 
                }
                         
            });

        });


        /*==================================================================
        [ Slick4 ]*/
        $('.wrap-slick4').each(function(){
            var wrapSlick4 = $(this);
            var slick4 = $(this).find('.slick4');
    

            var showDot = false;
            if($(wrapSlick4).find('.wrap-dot-slick4').length > 0) {
                showDot = true;
            }

            var showArrow = false;
            if($(wrapSlick4).find('.wrap-arrow-slick4').length > 0) {
                showArrow = true;
            }

            $(wrapSlick4).find('.slick4').slick({
                pauseOnFocus: false,
                pauseOnHover: false,
                slidesToShow: 3,
                slidesToScroll: 3,
                fade: false,
                infinite: false,
                autoplay: false,
                autoplaySpeed: 6000,
                arrows: showArrow,
                appendArrows: $(wrapSlick4).find('.wrap-arrow-slick4'),
                prevArrow: $(wrapSlick4).find('.prev-slick4'),
                nextArrow: $(wrapSlick4).find('.next-slick4'),
                dots: showDot,
                appendDots: $(wrapSlick4).find('.wrap-dot-slick4'),
                dotsClass:'dots-slick4',
                customPaging: function(slick, index) {
                    return '<div></div>';
                },
                responsive: [
                    {
                      breakpoint: 1900,
                      settings: {
                        slidesToShow: 3,
                        slidesToScroll: 3
                      }
                    },
                    {
                      breakpoint: 1400,
                      settings: {
                        slidesToShow: 2,
                        slidesToScroll: 2
                      }
                    },
                    {
                      breakpoint: 991,
                      settings: {
                        slidesToShow: 3,
                        slidesToScroll: 3
                      }
                    },
                    {
                      breakpoint: 767,
                      settings: {
                        slidesToShow: 2,
                        slidesToScroll: 2
                      }
                    },
                    {
                      breakpoint: 575,
                      settings: {
                        slidesToShow: 1,
                        slidesToScroll: 1
                      }
                    }
                ]

            });

        });
        

        /*==================================================================
        [ Slick5 ]*/
        $('.wrap-slick5').each(function(){
            var wrapSlick = $(this);
            var slick = $(this).find('.slick5');
    

            var showDot = false;
            if($(wrapSlick).find('.wrap-dot-slick5').length > 0) {
                showDot = true;
            }

            var showArrow = false;
            if($(wrapSlick).find('.wrap-arrow-slick5').length > 0) {
                showArrow = true;
            }

            $(wrapSlick).find('.slick5').slick({
                pauseOnFocus: false,
                pauseOnHover: false,
                slidesToShow: 6,
                slidesToScroll: 6,
                fade: false,
                infinite: false,
                autoplay: false,
                autoplaySpeed: 6000,
                arrows: showArrow,
                appendArrows: $(wrapSlick).find('.wrap-arrow-slick5'),
                prevArrow: $(wrapSlick).find('.prev-slick5'),
                nextArrow: $(wrapSlick).find('.next-slick5'),
                dots: showDot,
                appendDots: $(wrapSlick).find('.wrap-dot-slick5'),
                dotsClass:'slick5-dots',
                customPaging: function(slick, index) {
                    return '<div></div>';
                },
                responsive: [
                    {
                      breakpoint: 1900,
                      settings: {
                        slidesToShow: 6,
                        slidesToScroll: 6
                      }
                    },
                    {
                      breakpoint: 1680,
                      settings: {
                        slidesToShow: 5,
                        slidesToScroll: 5
                      }
                    },
                    {
                      breakpoint: 1420,
                      settings: {
                        slidesToShow: 4,
                        slidesToScroll: 4
                      }
                    },
                    {
                      breakpoint: 991,
                      settings: {
                        slidesToShow: 3,
                        slidesToScroll: 3
                      }
                    },
                    {
                      breakpoint: 767,
                      settings: {
                        slidesToShow: 2,
                        slidesToScroll: 2
                      }
                    },
                    {
                      breakpoint: 575,
                      settings: {
                        slidesToShow: 1,
                        slidesToScroll: 1
                      }
                    }
                ]

            });

        });


        /*==================================================================
        [ Slick6 ]*/
        $('.wrap-slick6').each(function(){
            var wrapSlick = $(this);
            var slick = $(this).find('.slick6');
    

            var showDot = false;
            if($(wrapSlick).find('.wrap-dot-slick6').length > 0) {
                showDot = true;
            }

            var showArrow = false;
            if($(wrapSlick).find('.wrap-arrow-slick6').length > 0) {
                showArrow = true;
            }

            $(wrapSlick).find('.slick6').slick({
                pauseOnFocus: false,
                pauseOnHover: false,
                slidesToShow: 3,
                slidesToScroll: 3,
                fade: false,
                infinite: false,
                autoplay: false,
                autoplaySpeed: 6000,
                arrows: showArrow,
                appendArrows: $(wrapSlick).find('.wrap-arrow-slick6'),
                prevArrow: $(wrapSlick).find('.prev-slick6'),
                nextArrow: $(wrapSlick).find('.next-slick6'),
                dots: showDot,
                appendDots: $(wrapSlick).find('.wrap-dot-slick6'),
                dotsClass:'slick6-dots',
                customPaging: function(slick, index) {
                    return '<div></div>';
                },
                responsive: [
                    {
                      breakpoint: 1199,
                      settings: {
                        slidesToShow: 2,
                        slidesToScroll: 2
                      }
                    },
                    {
                      breakpoint: 991,
                      settings: {
                        slidesToShow: 2,
                        slidesToScroll: 2
                      }
                    },
                    {
                      breakpoint: 767,
                      settings: {
                        slidesToShow: 3,
                        slidesToScroll: 3
                      }
                    },
                    {
                      breakpoint: 575,
                      settings: {
                        slidesToShow: 2,
                        slidesToScroll: 2
                      }
                    },
                    {
                      breakpoint: 480,
                      settings: {
                        slidesToShow: 1,
                        slidesToScroll: 1
                      }
                    }
                ]

            });

        });

        /*==================================================================
        [ Slick7 ]*/
        $('.wrap-slick7').each(function(){
            var wrapSlick = $(this);
            var slick = $(this).find('.slick7');
    

            var showDot = false;
            if($(wrapSlick).find('.wrap-dot-slick7').length > 0) {
                showDot = true;
            }

            var showArrow = false;
            if($(wrapSlick).find('.wrap-arrow-slick7').length > 0) {
                showArrow = true;
            }

            $(wrapSlick).find('.slick7').slick({
                pauseOnFocus: false,
                pauseOnHover: false,
                slidesToShow: 2,
                slidesToScroll: 2,
                fade: false,
                infinite: false,
                autoplay: false,
                autoplaySpeed: 6000,
                arrows: showArrow,
                appendArrows: $(wrapSlick).find('.wrap-arrow-slick7'),
                prevArrow: $(wrapSlick).find('.prev-slick7'),
                nextArrow: $(wrapSlick).find('.next-slick7'),
                dots: showDot,
                appendDots: $(wrapSlick).find('.wrap-dot-slick7'),
                dotsClass:'slick7-dots',
                customPaging: function(slick, index) {
                    return '<div></div>';
                },
                responsive: [
                    {
                      breakpoint: 1199,
                      settings: {
                        slidesToShow: 2,
                        slidesToScroll: 2
                      }
                    },
                    {
                      breakpoint: 991,
                      settings: {
                        slidesToShow: 1,
                        slidesToScroll: 1
                      }
                    },
                    {
                      breakpoint: 767,
                      settings: {
                        slidesToShow: 1,
                        slidesToScroll: 1
                      }
                    },
                    {
                      breakpoint: 575,
                      settings: {
                        slidesToShow: 1,
                        slidesToScroll: 1
                      }
                    }
                ]

            });

        });

        
        /*==================================================================
        [ Slick8 ]*/
        $('.wrap-slick8').each(function(){
            var wrapSlick = $(this);
            var slick = $(this).find('.slick8');
    

            var showDot = false;
            if($(wrapSlick).find('.wrap-dot-slick8').length > 0) {
                showDot = true;
            }

            var showArrow = false;
            if($(wrapSlick).find('.wrap-arrow-slick8').length > 0) {
                showArrow = true;
            }

            $(wrapSlick).find('.slick8').slick({
                pauseOnFocus: false,
                pauseOnHover: false,
                slidesToShow: 3,
                slidesToScroll: 3,
                fade: false,
                infinite: false,
                autoplay: false,
                autoplaySpeed: 6000,
                arrows: showArrow,
                appendArrows: $(wrapSlick).find('.wrap-arrow-slick8'),
                prevArrow: $(wrapSlick).find('.prev-slick8'),
                nextArrow: $(wrapSlick).find('.next-slick8'),
                dots: showDot,
                appendDots: $(wrapSlick).find('.wrap-dot-slick8'),
                dotsClass:'slick8-dots',
                customPaging: function(slick, index) {
                    return '<div></div>';
                },
                responsive: [
                    {
                      breakpoint: 1199,
                      settings: {
                        slidesToShow: 2,
                        slidesToScroll: 2
                      }
                    },
                    {
                      breakpoint: 991,
                      settings: {
                        slidesToShow: 2,
                        slidesToScroll: 2
                      }
                    },
                    {
                      breakpoint: 767,
                      settings: {
                        slidesToShow: 1,
                        slidesToScroll: 1
                      }
                    },
                    {
                      breakpoint: 575,
                      settings: {
                        slidesToShow: 1,
                        slidesToScroll: 1
                      }
                    }
                ]

            });

        });


        /*==================================================================
        [ Slick9 ]*/
        $('.wrap-slick9').each(function(){
            $(this).find('.slick9').slick({
              slidesToShow: 4,
              slidesToScroll: 4,
              infinite: false,
              autoplay: false,
              autoplaySpeed: 6000,
              arrows: true,
              appendArrows: $(this).find('.wrap-arrow-slick9'),
              prevArrow:'<button class="arrow-slick9 prev-slick9"><i class="fa fa-angle-left" aria-hidden="true"></i></button>',
              nextArrow:'<button class="arrow-slick9 next-slick9"><i class="fa fa-angle-right" aria-hidden="true"></i></button>',  
              responsive: [
                    {
                      breakpoint: 1199,
                      settings: {
                        slidesToShow: 4,
                        slidesToScroll: 4
                      }
                    },
                    {
                      breakpoint: 991,
                      settings: {
                        slidesToShow: 3,
                        slidesToScroll: 3
                      }
                    },
                    {
                      breakpoint: 767,
                      settings: {
                        slidesToShow: 2,
                        slidesToScroll: 2
                      }
                    },
                    {
                      breakpoint: 575,
                      settings: {
                        slidesToShow: 1,
                        slidesToScroll: 1
                      }
                    }
                ]
          });
        });


})(jQuery);