namespace ASPNETAPITheStep.Models
{
    public class AddBranchRequest
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string BranchManager { get; set; }

    }

    public class BankBranchResponse
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string BranchManager { get; set; }
        public List<Employee> Employees { get; set; } = new List<Employee>();

    }

    public class EditBranchRequest
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string BranchManager { get; set; }

    }



}
