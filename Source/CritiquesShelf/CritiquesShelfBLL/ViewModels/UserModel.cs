using System;
using System.Collections.Generic;
namespace CritiquesShelfBLL.ViewModels
{
    public class UserModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public byte[] Photo { get; set; }
        public List<ReviewModel> Reviews { get; set; }
        public ReadingStatModel ReadingStat { get; set; }
    }
}
