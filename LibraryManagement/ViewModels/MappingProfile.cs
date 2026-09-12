using AutoMapper;
using LibraryManagement.Models;

namespace LibraryManagement.ViewModels
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
        CreateMap<Member, ProfileViewModel>();
        CreateMap<Book, IssueBookViewModel>();
        CreateMap<Book, BookViewModel>();
            
        }
    }
}
