using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Questionnaire.ShareControls
{
    public partial class ucPage : System.Web.UI.UserControl
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public int TotalRows { get; set; } = 0;
        private string _url = null;
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
            Bind(collection);
        }
        public void Bind(string[] paramKey, string[] paramValues)
        {
            NameValueCollection collection = new NameValueCollection();
            for (int i = 0; i < paramKey.Length; i++)
            {
                collection.Add(paramKey[i], paramValues[i]);
            }
            Bind(collection);
        }

        public void Bind(NameValueCollection collection)
        {
            int pageCount = (TotalRows / PageSize);
            if (pageCount == 0 ||(TotalRows % PageSize) > 0)
                pageCount += 1;

            // LocalPath :   MapList.aspx
            string url = Request.Url.LocalPath;
            string paramKeyword = this.BuildQueryString(collection);

            this.aLinkFirst.HRef = url + "?Index=1" + paramKeyword;

            this.aLinkPrev.HRef = url + "?Index=" + (PageIndex - 1) + paramKeyword;
            if (PageIndex <= 1)
                this.aLinkPrev.Visible = false;

            this.aLinkNext.HRef = url + "?Index=" + (PageIndex + 1) + paramKeyword;
            if (PageIndex + 1 > pageCount)
                this.aLinkNext.Visible = false;

            this.aLinkPage1.HRef = url + "?Index=" + (PageIndex - 2) + paramKeyword;
            this.aLinkPage1.InnerText = (PageIndex - 2).ToString();
            if (PageIndex <= 2)
                this.aLinkPage1.Visible = false;

            this.aLinkPage2.HRef = url + "?Index=" + (PageIndex - 1) + paramKeyword;
            this.aLinkPage2.InnerText = (PageIndex - 1).ToString();
            if (PageIndex <= 1)
                this.aLinkPage2.Visible = false;

            this.aLinkPage3.HRef = "";
            this.aLinkPage3.InnerText = PageIndex.ToString();

            this.aLinkPage4.HRef = url + "?Index=" + (PageIndex + 1) + paramKeyword;
            this.aLinkPage4.InnerText = (PageIndex + 1).ToString();
            if (PageIndex + 1 > pageCount)
                this.aLinkPage4.Visible = false;

            this.aLinkPage5.HRef = url + "?Index=" + (PageIndex + 2) + paramKeyword;
            this.aLinkPage5.InnerText = (PageIndex + 2).ToString();
            if (PageIndex + 2 > pageCount)
                this.aLinkPage5.Visible = false;

            this.aLinkLast.HRef = url + "?Page=" + pageCount + paramKeyword;

        }
        private string BuildQueryString(NameValueCollection collection)
        {
            List<string> paramList = new List<string>();
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