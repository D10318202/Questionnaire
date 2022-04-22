using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Questionnaire.ShareControls
{
    public partial class ucPager : System.Web.UI.UserControl
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public int TotalRows { get; set; } = 0;
        public string _url = null;

        public string Url
        {
            get
            {
                if (this._url == null)
                    return Request.Url.LocalPath;
                else
                    return this._url;
            }
            set
            {
                this._url = value;
            }
        }
        public void Bind()
        {
            NameValueCollection collection = new NameValueCollection();
            this.Bind(collection);
        }

        public void Bind(string paramkey, string paramValue)
        {
            NameValueCollection collection = new NameValueCollection();
            collection.Add(paramkey, paramValue);
            this.Bind(collection);
        }

        public void Bind(NameValueCollection collection)
        {
            int pageCount = (TotalRows / PageSize);
            if ((TotalRows % PageSize) > 0)
                pageCount += 1;

            // LocalPath :   MapList.aspx
            string url = Request.Url.LocalPath;
            string qsText = this.BuildQueryString(collection);

            this.aLinkFirst.HRef = url + "?Page=1" + qsText;

            this.aLinkPrev.HRef = url + "?Page=" + (PageIndex - 1) + qsText;

            this.aLinkNext.HRef = url + "?Page=" + (PageIndex + 1) + qsText;

         
            this.aLinkPage1.HRef = url + "?Page=" + (PageIndex - 2) + qsText;
            if (PageIndex <= 2)
                this.aLinkPage1.Visible = false;

            this.aLinkPage2.HRef = url + "?Page=" + (PageIndex - 1) + qsText;
            this.aLinkPage2.InnerText = (PageIndex - 1).ToString();
            if (PageIndex <= 1)
                this.aLinkPage2.Visible = false;

            this.aLinkPage3.HRef = "";
            this.aLinkPage3.InnerText = PageIndex.ToString();

            this.aLinkPage4.HRef = url + "?Page=" + (PageIndex + 1) + qsText;
            this.aLinkPage4.InnerText = (PageIndex + 1).ToString();
            if ((PageIndex + 1) > pageCount)
                this.aLinkPage4.Visible = false;

            this.aLinkPage5.HRef = url + "?Page=" + (PageIndex + 2) + qsText; 
            this.aLinkPage5.InnerText = (PageIndex + 2).ToString();
            if ((PageIndex + 2) > pageCount)
                this.aLinkPage5.Visible = false;
            
            this.aLinkLast.HRef = url + "?Page=" + pageCount;
        }

        /// <summary> 組合 QueryString </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        private string BuildQueryString(NameValueCollection collection)
        {
            List<string> paramList = new List<string>();
            //&key=Value
            foreach (string key in collection.AllKeys)
            {
                if (collection.GetValues(key) == null)
                    continue;

                foreach (string val in collection.GetValues(key))
                {
                    paramList.Add($"&{key}={val}");
                }
            }
            string result = string.Join("", paramList);
            return result;
        }
    }
}