﻿using System.IO;
using TableauAPI.RESTHelpers;

namespace TableauAPI.RESTRequests
{
    /// <summary>
    /// Download a Tableau View and associated artifacts such as Preview Images.
    /// </summary>
    public class DownloadView : TableauServerSignedInRequestBase
    {

        private readonly TableauServerUrls _onlineUrls;

        /// <summary>
        /// Create a View request for the Tableau REST API
        /// </summary>
        /// <param name="onlineUrls">Tableau Server Information</param>
        /// <param name="logInInfo">Tableau Sign In Information</param>
        public DownloadView(TableauServerUrls onlineUrls, TableauServerSignIn logInInfo) : base(logInInfo)
        {
            _onlineUrls = onlineUrls;
        }

        /// <summary>
        /// Return a Preview Image for a View
        /// </summary>
        /// <param name="workbookId"></param>
        /// <param name="viewId"></param>
        /// <returns></returns>
        public byte[] GetPreviewImage(string workbookId, string viewId)
        {
            var url = _onlineUrls.Url_ViewThumbnail(workbookId, viewId, OnlineSession);
            var webRequest = CreateLoggedInWebRequest(url);
            webRequest.Method = "GET";
            var response = GetWebResponseLogErrors(webRequest, "get view thumbnail");
            byte[] thumbnail;
            using (var stream = response.GetResponseStream())
            {

                using (var ms = new MemoryStream())
                {
                    stream.CopyTo(ms);
                    thumbnail = ms.ToArray();
                }
            }
            return thumbnail;
        }

        /// <summary>
        /// Return an Image for a View
        /// </summary>
        /// <param name="workbookId"></param>
        /// <param name="viewId"></param>
        /// <returns></returns>
        public byte[] GetImage(string workbookId, string viewId)
        {
            var url = _onlineUrls.Url_ViewImage(workbookId, viewId, OnlineSession);
            var webRequest = CreateLoggedInWebRequest(url);
            webRequest.Method = "GET";
            var response = GetWebResponseLogErrors(webRequest, "get view image");
            byte[] thumbnail;
            using (var stream = response.GetResponseStream())
            {

                using (var ms = new MemoryStream())
                {
                    stream.CopyTo(ms);
                    thumbnail = ms.ToArray();
                }
            }
            return thumbnail;
        }

        /// <summary>
        /// Return an Image for a View with filters.
        /// </summary>
        /// <param name="workbookId"></param>
        /// <param name="viewId"></param>
        /// <param name="param1"></param>
        /// <param name="param2"></param>
        /// <param name="val1"></param>
        /// <param name="val2"></param>
        /// <returns></returns>
        public byte[] GetImageWithFilters(string workbookId, string viewId, string param1, string param2, string val1, string val2, string ticket)
        {
            var url = "https://tableau.onware.com/trusted/" + ticket + "/t/AssessmentTool/views/Responses/Snapshot.png?CompanyId=fd59cde3-90f8-463a-8c82-c5c1f030e9ea&ModuleId=c9cb1e8e-ab27-4957-83a9-27f3bbdcdbf5";
            var webRequest = CreateLoggedInWebRequest(url);
            webRequest.Method = "GET";
            var response = GetWebResponseLogErrors(webRequest, "get view image");
            byte[] thumbnail;
            using (var stream = response.GetResponseStream())
            {

                using (var ms = new MemoryStream())
                {
                    stream.CopyTo(ms);
                    thumbnail = ms.ToArray();
                }
            }
            return thumbnail;
        }


        /// <summary>
        /// Return data for a View (CSV format)
        /// </summary>
        /// <param name="viewId"></param>
        /// <returns></returns>
        public string GetData(string viewId)
        {
            var url = _onlineUrls.Url_ViewData(viewId, OnlineSession);
            var webRequest = CreateLoggedInWebRequest(url);
            webRequest.Method = "GET";
            var response = GetWebResponseLogErrors(webRequest, "get view data");
            byte[] data;
            using (var stream = response.GetResponseStream())
            {
                using (var ms = new MemoryStream())
                {
                    stream.CopyTo(ms);
                    data = ms.ToArray();
                }
            }
            return System.Text.Encoding.UTF8.GetString(data);
        }
    }
}
