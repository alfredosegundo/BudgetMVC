CONTEXT = 'http://localhost/BudgetMVC/';
$('body').ajaxSend(function (e, jqxhr, settings) {
    settings.url = CONTEXT + settings.url;
});