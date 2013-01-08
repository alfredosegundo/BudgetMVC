var Monetary = function (json) {
    this.Description = json.Description;
    this.CreationDate = new Date(json.CreationDate).toLocaleDateString();
    this.Value = json.Value;
}

var IndexViewModel = function () {
    var self = this;
    self.currentMonth = ko.observable(getCurrentMonth());
    self.currentYear = ko.observable(getCurrentYear());
    self.currentMonthBalance = ko.observable();
    self.revenues = ko.observableArray();
    self.expenses = ko.observableArray();

    self.goPreviousMonth = function goPreviousMonth() {
        var previousMonth = self.currentMonth() - 1;
        var previousYear = self.currentYear() - 1;
        if (previousMonth >= 0) {
            self.currentMonth(previousMonth);
        }
        else {
            self.currentMonth(11);
            self.currentYear(previousYear);
        }
        self.populate();
    }

    self.goNextMonth = function goNextMonth() {
        var nextMonth = self.currentMonth() + 1;
        var nextYear = self.currentYear() + 1
        if (nextMonth >= 0 && nextMonth <= 11) {
            self.currentMonth(nextMonth);
        }
        else {
            self.currentMonth(0);
            self.currentYear(nextYear);
        }
        self.populate();
    }

    self.populate = function populate() {
        self.revenues([]);
        self.expenses([]);
        self.currentMonthBalance(0.0);
        $.getJSON('Home/InitialData', { month: self.currentMonth() + 1, year: self.currentYear() },
            function (data) {
                data = JSON.parse(data);
                for (var revenuesIndex in data.revenues) {
                    self.revenues.push(new Monetary(data.revenues[revenuesIndex]));
                }
                for (var expensesIndex in data.expenses) {
                    self.expenses.push(new Monetary(data.expenses[expensesIndex]));
                }
                self.currentMonthBalance(data.monthBalance);
            });
    };

    self.newExpense = function newExpense() {
        showForm('.newExpenses');
    }

    self.cancelExpense = function cancelExpense() {
        hideForm('.newExpenses');
    }

    self.saveExpense = function saveExpense() {
        var form = $('.newExpenses form');
        if (form.valid()) {
            $.post('Expenses/Create', form.serialize(), function () { self.populate(); });
            hideForm('.newExpenses');
        }
    }

    self.newRevenue = function newRevenue() {
        showForm('.newRevenues');
    }

    self.cancelRevenue = function cancelRevenue() {
        hideForm('.newRevenues');
    }

    self.saveRevenue = function saveRevenue() {
        var form = $('.newRevenues form');
        if (form.valid()) {
            $.post('Revenues/Create', form.serialize(), function () { self.populate(); });
            hideForm('.newRevenues');
        }
    }

    self.currentMonthName = ko.computed(function computeMonthName() {
        return getMonthName(self.currentMonth());
    });

    ko.computed(function () {
        var target = $('div.balance h3');
        if (self.currentMonthBalance() >= 0.0) {
            target.addClass('positive');
            target.removeClass('negative');
        }
        else {
            target.addClass('negative');
            target.removeClass('positive');
        }
    });
};
var viewModel = new IndexViewModel();
ko.applyBindings(viewModel);
viewModel.populate();

function showForm(formSelector) {
    $('nav.top-bar').removeAttr('style').removeClass('expanded');
    $('.appTitle').fadeOut('fast', function () {
        $(formSelector).fadeIn();
    });
    $(formSelector + ' form input:first').focus();
}

function hideForm(formSelector) {
    $(formSelector).slideUp('fast', function () {
        if ($('.header > div').filter(":visible").length == 0) {
            $('.appTitle').fadeIn('fast');
        }
    });
    $(formSelector + ' form').find('input').val('');
}

key('left', viewModel.goPreviousMonth);
key('right', viewModel.goNextMonth);

$('form input[type="datetime"]').pickadate({ disablePicker: Modernizr.touch });