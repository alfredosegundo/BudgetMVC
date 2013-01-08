var opts = {
    lines: 13, // The number of lines to draw
    length: 7, // The length of each line
    width: 4, // The line thickness
    radius: 10, // The radius of the inner circle
    corners: 1, // Corner roundness (0..1)
    rotate: 0, // The rotation offset
    color: 'White', // #rgb or #rrggbb
    speed: 1, // Rounds per second
    trail: 60, // Afterglow percentage
    shadow: true, // Whether to render a shadow
    hwaccel: false, // Whether to use hardware acceleration
    className: 'spinner', // The CSS class to assign to the spinner
    zIndex: 2e9 // The z-index (defaults to 2000000000)
};
spinner = new Spinner(opts);

$('body').ajaxSend(function (e, jqxhr, settings) {
    settings.url = CONTEXT + settings.url;
}).ajaxStart(function () {
    $('.content').append($('.loading'));
    $('.loading').css({ 'width': $('.content').width(), 'height': $('.content').height() * 2 });
    $('.loading').show();
    spinner.spin(document.getElementById('loading'));
}).ajaxStop(function () {
    spinner.stop();
    $('.loading').hide();
});


$(function () {
    $("form").each(function (index) {
        var validator = $(this).data('validator');
        validator.settings.errorClass = "error",
        validator.settings.errorElement = "small"
    });
});