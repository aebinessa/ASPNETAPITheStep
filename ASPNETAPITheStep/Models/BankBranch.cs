namespace ASPNETAPITheStep.Models
{
    public class BankBranch
    {
        public int Id { get; set; }
        public string Name {  get; set; }
        public string Location { get; set; }
        public string BranchManager { get; set; }
        public int EmployeeCount { get; set; }
        public List<Employee> Employees { get; set; }=new List<Employee>();



    }
}
