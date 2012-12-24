function Monetary(json) {
    this.Description = json.Description;
    this.CreationDate = new Date(json.CreationDate).toLocaleDateString();
    this.Value = json.Value;
}

function GetData(month) {
    var result;
    $.getJSON("Home/InitialData", function (data) {
        result = data;
    });
    return result;
}

function IndexViewModel() {
    var self = this;
    self.currentMonth = ko.observable(getCurrentMonth());
    self.currentYear = ko.observable(getCurrentYear());
    self.revenues = ko.observableArray();
    self.expenses = ko.observableArray();

    self.goPreviousMonth = function goPreviousMonth() {
        var previousMonth = self.currentMonth() - 1;
        if (previousMonth >= 0) {
            self.currentMonth(previousMonth);
        }
        else {
            self.currentMonth(11);
            self.currentYear(self.currentYear() - 1);
        }
        self.populate(self.currentMonth, self.currentYear);
    }

    self.goNextMonth = function goNextMonth() {
        var nextMonth = self.currentMonth() + 1;
        if (nextMonth >= 0 && nextMonth <= 11) {
            self.currentMonth(nextMonth);
        }
        else {
            self.currentMonth(0);
            self.currentYear(self.currentYear() + 1);
        }
        self.populate(self.currentMonth, self.currentYear);
    }

    self.populate = function populate(month, year) {
        self.revenues([]);
        self.expenses([]);
        month = parseInt(month()) + 1;
        $.getJSON("Home/InitialData", { month: month, year: year },
            function (data) {
                data = JSON.parse(data);
                for (var index in data.revenues) {
                    self.revenues.push(new Monetary(data.revenues[index]));
                }
                for (var index in data.expenses) {
                    self.expenses.push(new Monetary(data.expenses[index]));
                }
            });
    }

    self.currentMonthName = ko.computed(function computeMonthName() {
        return getMonthName(self.currentMonth());
    });
};
var viewModel = new IndexViewModel();
ko.applyBindings(viewModel);
viewModel.populate(viewModel.currentMonth, viewModel.currentYear);

key('left', viewModel.goPreviousMonth);
key('right', viewModel.goNextMonth);