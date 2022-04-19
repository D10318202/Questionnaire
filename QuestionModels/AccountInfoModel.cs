using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuestionModels
{
    public class AccountInfoModel
    {
        public Guid quesID { get; set; }
        public Guid AccountID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Age { get; set; }
        public DateTime CreateTime { get; set; }
    }
}