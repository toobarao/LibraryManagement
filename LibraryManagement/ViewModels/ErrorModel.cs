using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.ViewModels
{
    public class ErrorModel
    {
        public string ErrorMsg { get; set; }
        public int statusCode { get; set; }

        public string link { get; set; }
    public void MakeErrorModel(Exception exception)
    {
            switch (exception)
            {
                case KeyNotFoundException:
                    this.statusCode = 404;
                    this.ErrorMsg = "The requested resource was not found.";
                    break;

                case ArgumentException:
                    statusCode = 400;
                    ErrorMsg = "Invalid data was provided.";
                    break;

                case UnauthorizedAccessException:
                    statusCode = 403;
                    ErrorMsg = "You do not have permission to perform this operation.";
                    break;

                case DbUpdateConcurrencyException:
                    statusCode = 409;
                    ErrorMsg = "The data was modified by another operation.";
                    break;

                case DbUpdateException:
                    statusCode = 500;
                    ErrorMsg = "A database error occurred.";
                    break;

                default:
                    statusCode = 500;
                    ErrorMsg = "An unexpected error occurred.";
                    break;
            }
        }
 }
 } 
