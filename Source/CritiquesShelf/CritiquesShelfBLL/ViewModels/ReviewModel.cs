using System;
namespace CritiquesShelfBLL.ViewModels
{
    public class ReviewModel
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public double Score { get; set; }
        public long BookId { get; set; }
        public string BookTitle { get; set; }
    }
}