jQuery(document).ready(function() {
    "use strict";

    // Show/Hide second level in navigation
    $(".level1>li").hover(
            function() {
                // Styles to show the box
                if ($(this).find('ul.level2').length) {
                    $(this).find('ul.level2').show();
                    $(this).find('div.wraplevel2').append('<div class="active">&nbsp;</div>');
                }
            },
            function() {
                // Styles to hide the box
                if ($(this).find('ul.level2').length) {
                    $(this).find('ul.level2').hide();
                    $(this).find('div.active').remove();
                }
            }
    );


    //Show/Hide rating on item rollover
    $(".item").hover(
            function() {
                // Styles to show the box
                $(this).find('.mo').show();
                $(this).find('.rating').hide();

            },
            function() {
                // Styles to hide the box
                $(this).find('.mo').hide();
                $(this).find('.rating').show();
            }
    );


    //Switch tabs on Product info page
    $('#myTab a').click(function(e) {
        e.preventDefault();
        $(this).tab('show');
    });
    

    

});