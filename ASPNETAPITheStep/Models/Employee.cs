namespace ASPNETAPITheStep.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CivilId { get; set; }
        public string Position { get; set; }
        public BankBranch BankBranch { get; set; }

    }
    public class AddEmployeeRequest
    {
        public string Name { get; set; }
        public int CivilId { get; set; }
        public string Position { get; set; }




    }
    public class AddEmployeeResponse
    {
        public string Name { get; set; }
        public int CivilId { get; set; }
        public string Position { get; set; }
        public int BankBranch { get; set; }

    }

}
