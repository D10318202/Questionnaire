using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuestionModels
{
    public class QuestionModel
    {
       public DateTime quesstart { get; set;}
       public DateTime quesend { get; set;}
       public Guid quesID { get; set;}
       public string quesTitle { get; set;}
       public string quesBody{ get; set; }
       public StateType stateType { get; set; }
    }

    public enum StateType
    {
        關閉 = 0,
        已啟用 = 1
    }
}