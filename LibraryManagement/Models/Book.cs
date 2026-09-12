namespace LibraryManagement.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public string ISBN { get; set; }

        public string PublicationYear { get; set; }

        public  string Availability { get; set; } = "Yes";
        public Book(string title, string author, string availability)
        {
            
            this.Author = author;
            this.Title = title;
            this.Availability = availability;
        }

        public bool getBookAvailability()
        {
            return this.Availability=="Yes"?true:false;
        }

        public void updateBookAvailability()
        {

            if (this.Availability == "Yes")
                this.Availability = "No";
            else
                this.Availability = "Yes";
        }


    }
}
