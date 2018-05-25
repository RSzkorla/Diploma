$(document).ready(function () {
    // hide all quotes except the first
    $('.slide').hide().eq(0).show();

    var pause = 3000;
    var motion = 500;

    var slide = $('.slide');
    var count = slide.length;
    var i = 0;

    setTimeout(transition, pause);

    function transition() {
        slide.eq(i).animate({ opacity: 'toggle', top: '40px' }, motion);

        if (++i >= count) {
            i = 0;
        }

        slide.eq(i).animate({ opacity: 'toggle', top: '20px' }, motion);

        setTimeout(transition, pause);
    }
});