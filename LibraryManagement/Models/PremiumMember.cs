namespace LibraryManagement.Models
{
    public class PremiumMember:Member
    {
        public PremiumMember(string name, string password, string email) : base(name, password, email) { }
        public override int borrowLimit()
        {
            return 7;

        }

        public override int FineValue()
        {
            return 10;

        }

        public override int maxBorrowDuration()
        {
            return 20;
        }
    }
}
