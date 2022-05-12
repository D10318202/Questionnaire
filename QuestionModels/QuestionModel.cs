using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuestionModels
{
    public class QuestionModel
    {
        public Guid quesID { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime quesstart { get; set; }
        public DateTime quesend { get; set; }
        public string quesTitle { get; set; }
        public string quesBody { get; set; }
        public StateType stateType { get; set; }
        public AnswerType answerType { get; set; }
    }

    public enum StateType
    {
        關閉 = 0,
        已啟用 = 1
    }

    public enum AnswerType
    {
        尚未投票 = 0,
        投票中 = 1,
        已結束 = 2
    }
}