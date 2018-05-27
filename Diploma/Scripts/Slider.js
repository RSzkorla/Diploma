$(document).ready(function () {

    $('.slide').hide().eq(0).show();

    var pause = 5000;
    var motion = 1500;
    var slide = $('.slide');
    var count = slide.length;
    var i = 0;

    setTimeout(transition, pause);

    function transition() {
        slide.eq(i).animate({ opacity: 'toggle', top: '40px' }, motion);

        if (++i >= count) {
            i = 0;
        }

        slide.eq(i).animate({ opacity: 'toggle', top: '40px' }, motion);

        setTimeout(transition, pause);
    }
});