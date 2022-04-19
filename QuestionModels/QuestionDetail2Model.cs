using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuestionModels
{
    public class QuestionDetail2Model
    {
        public Guid quesID { get; set; }
        public Guid quesDetailID { get; set; }
        public Guid quesDetail2ID { get; set; }
        public string answer { get; set; }

    }
}