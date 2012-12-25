CONTEXT = 'http://turing/BudgetMVC/';
$('body').ajaxSend(function (e, jqxhr, settings) {
    settings.url = CONTEXT + settings.url;
}).ajaxStart(function () {
    console.debug('ajax start!');
}).ajaxStop(function () {
    console.debug('ajax stop!');
});