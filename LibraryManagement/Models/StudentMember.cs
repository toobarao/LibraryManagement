namespace LibraryManagement.Models
{
    public class StudentMember : Member
    {
        public StudentMember(string name, string password, string email) : base(name,password,email) { }
        public override int borrowLimit()
        {
            return 3;
            
        }

        public override int FineValue()
        {
            return 20;
            
        }

        public override int maxBorrowDuration()
        {
            return 15;
        }
    }
}
