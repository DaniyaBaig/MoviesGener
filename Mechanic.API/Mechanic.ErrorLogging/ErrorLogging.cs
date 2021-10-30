using System;
using Microsoft.AspNetCore.Http;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.IO;
using Mechanic.ErrorLogging.Enums;
using Mechanic.ErrorLogging.Models;
using Microsoft.AspNetCore.Http.Extensions;
using System.Linq;
using Mechanic.Common;

namespace Mechanic.ErrorLogging
{
    public class ErrorLogging
    {

        public static ConnectionString Setting { get; set; }
        static ErrorLogging()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "logSettings.json"));
            var root = builder.Build();
            Setting = new ConnectionString();
            root.GetSection("ConnectionStrings").Bind(Setting);
        }
        public static void AddErrorByException(Exception exception, HttpContext httpContext, OriginType origin = OriginType.DippedFruit)
        {
            string ipAddress = httpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault() == null ? httpContext.Connection.RemoteIpAddress.ToString() : httpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (httpContext == null)
            {
                // AddErrorByExceptionWithoutCurrentHttpContext(exception);
                return;
            }
            ErrorLog objErrorLog = new ErrorLog();
            string strAdditionalInfo;
            if (httpContext.Request.Headers["Referer"].ToString() == null == false)
                strAdditionalInfo = "URL" + Environment.NewLine + "====" + Environment.NewLine + httpContext.Request.GetEncodedUrl().ToString() + Environment.NewLine + Environment.NewLine + "URL Referrer" + Environment.NewLine + "===========" + Environment.NewLine + httpContext.Request.Headers["Referer"].ToString();
            else if (httpContext.Request != null)
                strAdditionalInfo = "URL" + Environment.NewLine + "====" + Environment.NewLine + httpContext.Request.GetEncodedUrl().ToString() + Environment.NewLine + Environment.NewLine + "URL Referrer" + Environment.NewLine + "===========" + Environment.NewLine + string.Empty;
            else
                strAdditionalInfo = string.Empty;
            strAdditionalInfo = strAdditionalInfo + Environment.NewLine + Environment.NewLine + "Cookies" + Environment.NewLine + "======";
            if (httpContext.Request.Cookies == null == false && httpContext.Request.Cookies.Keys == null == false)
            {
                var cookie = "";
                foreach (var strKey in httpContext.Request.Cookies.Keys)
                {
                    httpContext.Request.Cookies.TryGetValue(strKey, out cookie);
                    strAdditionalInfo = strAdditionalInfo + Environment.NewLine + strKey + " = " + cookie;
                    cookie = "";
                }
            }
            strAdditionalInfo = strAdditionalInfo + Environment.NewLine;
            if (exception.InnerException == null)
            {
                {
                    var withBlock = objErrorLog;
                    withBlock.ParentErrorLogId = Int32.MinValue;
                    withBlock.Date = System.DateTime.Now;
                    withBlock.ErrorMessage = exception.ToString();
                    withBlock.StackTrace = exception.StackTrace;
                    withBlock.Source = exception.Source;
                    withBlock.MethodName = exception.TargetSite.Name;
                    if (httpContext.Request.Headers["User-Agent"].ToString() == null == true)
                        withBlock.Browser = string.Empty;
                    else
                        withBlock.Browser = httpContext.Request.Headers["User-Agent"].ToString();// 'todo
                    withBlock.IP = ipAddress;
                    try
                    {
                        withBlock.Server = System.Environment.MachineName;
                    }
                    catch (Exception)
                    {
                        withBlock.Server = string.Empty;
                    }

                    strAdditionalInfo = strAdditionalInfo + "==== ServicePointManager : " + System.Net.ServicePointManager.SecurityProtocol.ToString() + " ====";

                    withBlock.AdditionalInfo = strAdditionalInfo;
                    withBlock.Origin = origin;
                }
                Add(objErrorLog);
            }
            else
            {
                {
                    var withBlock = objErrorLog;
                    withBlock.ParentErrorLogId = Int32.MinValue;
                    withBlock.Date = System.DateTime.Now;
                    withBlock.ErrorMessage = exception.InnerException.ToString();

                    if (exception.InnerException.StackTrace == null == false)
                        withBlock.StackTrace = exception.InnerException.StackTrace;
                    else if (exception.StackTrace == null == false)
                        withBlock.StackTrace = "== exception.StackTrace == " + exception.StackTrace;

                    if (exception.InnerException.Source == null == false)
                        withBlock.Source = exception.InnerException.Source;
                    else if (exception.Source == null == false)
                        withBlock.Source = "== exception.Source == " + exception.Source;

                    if (exception.InnerException.TargetSite == null == false && exception.InnerException.TargetSite.Name == null == false)
                        withBlock.MethodName = exception.InnerException.TargetSite.Name;
                    else if (exception.TargetSite == null == false && exception.TargetSite.Name == null == false)
                        withBlock.MethodName = "== exception.TargetSite.Name == " + exception.TargetSite.Name;

                    withBlock.Browser = httpContext.Request.Headers["User-Agent"].ToString();
                    withBlock.IP = ipAddress;
                    try
                    {
                        withBlock.Server = System.Environment.MachineName;
                    }
                    catch (Exception)
                    {
                        withBlock.Server = string.Empty;
                    }

                    strAdditionalInfo = strAdditionalInfo + "==== ServicePointManager : " + System.Net.ServicePointManager.SecurityProtocol.ToString() + " ====";
                    withBlock.AdditionalInfo = strAdditionalInfo;
                    withBlock.Origin = origin;
                }
                Add(objErrorLog);
            }
        }


        public static void Add(ErrorLog objErrorLog)
        {

            SqlConnection con = new SqlConnection(Setting.ErrorLog.ConnectionString);

            try
            {
                SqlCommand cmd = new SqlCommand("ErrorLog_Add", con);
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("@Id", objErrorLog.ID).Direction = ParameterDirection.Output;
                cmd.Parameters.AddWithValue("@ParentErrorLogID", objErrorLog.ParentErrorLogId);
                cmd.Parameters.AddWithValue("@Date", objErrorLog.Date);
                cmd.Parameters.AddWithValue("@ErrorMessage", objErrorLog.ErrorMessage);
                cmd.Parameters.AddWithValue("@StackTrace", objErrorLog.StackTrace);
                cmd.Parameters.AddWithValue("@Source", objErrorLog.Source);
                cmd.Parameters.AddWithValue("@MethodName", objErrorLog.MethodName);
                cmd.Parameters.AddWithValue("@Browser", objErrorLog.Browser);
                cmd.Parameters.AddWithValue("@IP", objErrorLog.IP);
                try
                {
                    objErrorLog.Server = System.Environment.MachineName;
                }
                catch (Exception)
                {
                    objErrorLog.Server = string.Empty;
                }

                cmd.Parameters.AddWithValue("@Server", objErrorLog.Server);
                cmd.Parameters.AddWithValue("@AdditionalInfo", objErrorLog.AdditionalInfo);
                cmd.Parameters.AddWithValue("@origin", objErrorLog.Origin);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                    throw ex;
                }
            }
        }


        public static void AddErrorByExceptionWithExtraInfo(Exception ex, string strExtraInfo, string ipAddress, HttpContext httpContext, OriginType origin = OriginType.DippedFruit)
        {
            if (httpContext == null)
            {
                //AddErrorByExceptionWithoutCurrentHttpContext(ex);
                return;
            }
            ErrorLog objErrorLog = new ErrorLog();
            string strAdditionalInfo;


            if (httpContext.Request.Headers["Referer"].ToString() == null == false)
                strAdditionalInfo = "URL" + Environment.NewLine + "====" + Environment.NewLine + httpContext.Request.GetEncodedUrl().ToString() + Environment.NewLine + Environment.NewLine + "URL Referrer" + Environment.NewLine + "===========" + Environment.NewLine + httpContext.Request.Headers["Referer"].ToString();
            else
                strAdditionalInfo = "URL" + Environment.NewLine + "====" + Environment.NewLine + httpContext.Request.GetEncodedUrl().ToString() + Environment.NewLine + Environment.NewLine + "URL Referrer" + Environment.NewLine + "===========" + Environment.NewLine + string.Empty;

            strAdditionalInfo = strExtraInfo + " " + strAdditionalInfo;

            strAdditionalInfo = strAdditionalInfo + Environment.NewLine + Environment.NewLine + "Session Variables" + Environment.NewLine + "==============";
            if (httpContext.Session == null == false && httpContext.Session.Keys == null == false)
            {
                foreach (var strKey in httpContext.Session.Keys)
                    strAdditionalInfo = strAdditionalInfo + Environment.NewLine + strKey + " = " + httpContext.Session.GetString(strKey).ToString();
            }

            strAdditionalInfo = strAdditionalInfo + Environment.NewLine + Environment.NewLine + "Cookies" + Environment.NewLine + "======";
            if (httpContext.Request.Cookies == null == false && httpContext.Request.Cookies.Keys == null == false)
            {
                var cookie = "";
                foreach (var strKey in httpContext.Request.Cookies.Keys)
                {
                    httpContext.Request.Cookies.TryGetValue(strKey, out cookie);
                    strAdditionalInfo = strAdditionalInfo + Environment.NewLine + strKey + " = " + cookie;
                    cookie = "";
                }

            }

            strAdditionalInfo = strAdditionalInfo + Environment.NewLine; //+ Utility.Generic.GetUserXFFInformationForErrorLog();

            if (ex.InnerException == null)
            {
                {
                    var withBlock = objErrorLog;
                    // .ID As Integer = NullValues.NullInteger
                    withBlock.ParentErrorLogId = Int32.MinValue;
                    withBlock.Date = System.DateTime.Now;
                    withBlock.ErrorMessage = ex.ToString();
                    withBlock.StackTrace = ex.StackTrace;
                    withBlock.Source = ex.Source;
                    withBlock.MethodName = ex.TargetSite.Name;
                    withBlock.Browser = httpContext.Request.Headers["User-Agent"].ToString(); // 'todo
                    withBlock.IP = ipAddress;
                    try
                    {
                        withBlock.Server = System.Environment.MachineName;
                    }
                    catch (Exception)
                    {
                        withBlock.Server = string.Empty;
                    }
                    strAdditionalInfo = strAdditionalInfo + "==== ServicePointManager : " + System.Net.ServicePointManager.SecurityProtocol.ToString() + " ====";
                    withBlock.AdditionalInfo = strAdditionalInfo;
                    withBlock.Origin = origin;
                }
                Add(objErrorLog);
            }
            else
            {
                {
                    var withBlock = objErrorLog;
                    // .ID As Integer = NullValues.NullInteger
                    withBlock.ParentErrorLogId = Int32.MinValue;
                    withBlock.Date = System.DateTime.Now;
                    withBlock.ErrorMessage = ex.InnerException.ToString();

                    if (ex.InnerException.StackTrace == null == false)
                        withBlock.StackTrace = ex.InnerException.StackTrace;
                    else if (ex.StackTrace == null == false)
                        withBlock.StackTrace = "== exception.StackTrace == " + ex.StackTrace;

                    if (ex.InnerException.Source == null == false)
                        withBlock.Source = ex.InnerException.Source;
                    else if (ex.Source == null == false)
                        withBlock.Source = "== exception.Source == " + ex.Source;

                    if (ex.InnerException.TargetSite == null == false && ex.InnerException.TargetSite.Name == null == false)
                        withBlock.MethodName = ex.InnerException.TargetSite.Name;
                    else if (ex.TargetSite == null == false && ex.TargetSite.Name == null == false)
                        withBlock.MethodName = "== exception.TargetSite.Name == " + ex.TargetSite.Name;


                    withBlock.Browser = httpContext.Request.Headers["User-Agent"].ToString();
                    withBlock.IP = ipAddress;
                    try
                    {
                        withBlock.Server = System.Environment.MachineName;
                    }
                    catch (Exception)
                    {
                        withBlock.Server = string.Empty;
                    }
                    strAdditionalInfo = strAdditionalInfo + "==== ServicePointManager : " + System.Net.ServicePointManager.SecurityProtocol.ToString() + " ====";
                    withBlock.AdditionalInfo = strAdditionalInfo;
                    withBlock.Origin = origin;
                }
                Add(objErrorLog);
            }
        }
    }
}
