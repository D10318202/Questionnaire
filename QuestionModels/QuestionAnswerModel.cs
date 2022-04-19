using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuestionModels
{
    public class QuestionAnswerModel
    {
        public Guid AnswerID { get; set; }
        public Guid quesID { get; set; }
        public Guid AccountID { get; set; }
        public int quesNumber { get; set; }
        public string Answer { get; set; }

    }
}