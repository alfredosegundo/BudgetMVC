namespace BudgetMVC.Model.DTO
{
    public struct InitialData
    {
        public object[] expenses;
        public object[] revenues;
        public double monthBalance;

        public InitialData(object[] expenses, object[] revenues, double monthBalance)
        {
            this.expenses = expenses;
            this.revenues = revenues;
            this.monthBalance = monthBalance;
        }
    }
}
