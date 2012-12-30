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
        $.getJSON("Home/InitialData", { month: self.currentMonth() + 1, year: self.currentYear() },
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
        $("#newExpenses").reveal();
        $("#newExpenses form input:first").focus();
    }

    self.saveExpense = function saveExpense() {
        $.post('Expenses/Create', $("#newExpenses form").serialize(), function () { self.populate(); });
        $("#newExpenses").trigger('reveal:close');
    }

    self.newRenevue = function newRenevue() {
        $("#newRevenues").reveal();
        $("#newRevenues form input:first").focus();
    }

    self.saveRevenue = function saveRevenue() {
        $.post('Revenues/Create', $("#newRevenues form").serialize(), function () { self.populate(); });
        $("#newRevenues").trigger('reveal:close');
    }

    self.currentMonthName = ko.computed(function computeMonthName() {
        return getMonthName(self.currentMonth());
    });

    ko.computed(function () {
        if (self.currentMonthBalance() >= 0.0) {
            $('div.balance span').addClass('positive');
            $('div.balance span').removeClass('negative');
        }
        else {
            $('div.balance span').addClass('negative');
            $('div.balance span').removeClass('positive');            
        }
    });
};
var viewModel = new IndexViewModel();
ko.applyBindings(viewModel);
viewModel.populate();

key('left', viewModel.goPreviousMonth);
key('right', viewModel.goNextMonth);