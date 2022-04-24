using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuestionModels
{
    public class QuestionDetailModel
    {
        public Guid quesID {get; set;}
        public Guid quesDetailID {get; set;}
        public string quesDetailTitle {get; set;}
        public string quesDetailBody {get; set;}
        public QuestionType quesDetailType {get; set;}
        public bool quesDetailMustKeyIn { get; set; }
        public int quesNumber {get; set;}
        //public QuestionMustFill quesDetailMustKeyIn { get; set; }
    }
    public enum QuestionType
    {
        單選方塊 = 0,
        複選方塊 = 1,
        文字方塊 = 2
    } 
    //public enum QuestionMustFill
    //{
    //    是必填 = 0,
    //    不是必填 = 1
    //}
}