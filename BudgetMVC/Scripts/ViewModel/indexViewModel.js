function Revenue(Description, CreationDate, Value) {
    this.Description = Description;
    this.CreationDate = CreationDate;
    this.Value = Value;
}

function GetData(month) {
    var result;
    $.getJSON(CONTEXT + "/Home/InitialData", function (data) {
        result = data;
    });
    return result;
}

function IndexViewModel() {
    var self = this;
    self.currentMonth = ko.observable(getCurrentMonth());
    self.revenues = ko.observableArray();
    self.expenses = ko.observableArray();

    self.currentMonthName = ko.computed(function () {
        return getMonthName(self.currentMonth());
    });

    self.populate = function () {
        $.getJSON(CONTEXT + "/Home/InitialData", function (data) {
            self.revenues(data.revenues);
            self.expenses(data.expenses);
        });
    }
};

ko.applyBindings(new IndexViewModel());