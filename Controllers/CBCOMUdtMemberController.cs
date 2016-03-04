﻿/**
* @file CBCOMUdtMemberController.cs
* @brief Common API for a member update on members table. \n
* Set parameter null or remove json property for no change on column data.
* @author Dae Woo Kim
* @param member object
* @return string "1" - affected rows
* @see uspComUdtMember SP, BehaviorID : B07, B08, B09, B10, B16, B17, B18, B52
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Config;

using System.Threading.Tasks;
using System.Diagnostics;
using Logger.Logging;
using CloudBread.globals;
using CloudBreadLib.BAL.Crypto;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using Newtonsoft.Json;
using CloudBreadAuth;
using System.Security.Claims;
using Microsoft.Practices.TransientFaultHandling;
using Microsoft.Practices.EnterpriseLibrary.WindowsAzure.TransientFaultHandling.SqlAzure;

namespace CloudBread.Controllers
{
    [MobileAppController]
    public class CBCOMUdtMemberController : ApiController
    {
                
        public class InputParams
        {
            public string MemberID { get; set; }
            public string MemberPWD { get; set; }   // CloudBread 2.0.0-beta deplicated. Use 3rd party authentication by default.
            public string EmailAddress { get; set; }
            public string EmailConfirmedYN { get; set; }
            public string PhoneNumber1 { get; set; }
            public string PhoneNumber2 { get; set; }
            public string PINumber { get; set; }
            public string Name1 { get; set; }
            public string Name2 { get; set; }
            public string Name3 { get; set; }
            public string DOB { get; set; }
            public string RecommenderID { get; set; }
            public string MemberGroup { get; set; }
            public string LastDeviceID { get; set; }
            public string LastIPaddress { get; set; }
            public string LastLoginDT { get; set; }
            public string LastLogoutDT { get; set; }
            public string LastMACAddress { get; set; }
            public string AccountBlockYN { get; set; }
            public string AccountBlockEndDT { get; set; }
            public string AnonymousYN { get; set; }
            public string _3rdAuthProvider { get; set; }
            public string _3rdAuthID { get; set; }
            public string _3rdAuthParam { get; set; }
            public string PushNotificationID { get; set; }
            public string PushNotificationProvider { get; set; }
            public string PushNotificationGroup { get; set; }
            public string sCol1 { get; set; }
            public string sCol2 { get; set; }
            public string sCol3 { get; set; }
            public string sCol4 { get; set; }
            public string sCol5 { get; set; }
            public string sCol6 { get; set; }
            public string sCol7 { get; set; }
            public string sCol8 { get; set; }
            public string sCol9 { get; set; }
            public string sCol10 { get; set; }
            public string TimeZoneID { get; set; }
        }

        public string Post(InputParams p)
        {
            string result = "";

            // Get the sid or memberID of the current user.
            var claimsPrincipal = this.User as ClaimsPrincipal;
            string sid = CBAuth.getMemberID(p.MemberID, claimsPrincipal);
            p.MemberID = sid;

            Logging.CBLoggers logMessage = new Logging.CBLoggers();
            string jsonParam = JsonConvert.SerializeObject(p);

            try
            {
                // start task log
                //logMessage.memberID = p.MemberID;
                //logMessage.Level = "INFO";
                //logMessage.Logger = "CBCOMUdtMemberController";
                //logMessage.Message = jsonParam;
                //Logging.RunLog(logMessage);

                /// Database connection retry policy
                RetryPolicy retryPolicy = new RetryPolicy<SqlAzureTransientErrorDetectionStrategy>(globalVal.conRetryCount, TimeSpan.FromSeconds(globalVal.conRetryFromSeconds));
                using (SqlConnection connection = new SqlConnection(globalVal.DBConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("uspComUdtMember", connection))
                    {
                        
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@MemberID", SqlDbType.NVarChar, -1).Value = p.MemberID;
                        command.Parameters.Add("@MemberPWD", SqlDbType.NVarChar, -1).Value = p.MemberPWD;
                        command.Parameters.Add("@EmailAddress", SqlDbType.NVarChar, -1).Value = p.EmailAddress;
                        command.Parameters.Add("@EmailConfirmedYN", SqlDbType.NVarChar, -1).Value = p.EmailConfirmedYN;
                        command.Parameters.Add("@PhoneNumber1", SqlDbType.NVarChar, -1).Value = p.PhoneNumber1;
                        command.Parameters.Add("@PhoneNumber2", SqlDbType.NVarChar, -1).Value = p.PhoneNumber2;
                        command.Parameters.Add("@PINumber", SqlDbType.NVarChar, -1).Value = p.PINumber;
                        command.Parameters.Add("@Name1", SqlDbType.NVarChar, -1).Value = p.Name1;
                        command.Parameters.Add("@Name2", SqlDbType.NVarChar, -1).Value = p.Name2;
                        command.Parameters.Add("@Name3", SqlDbType.NVarChar, -1).Value = p.Name3;
                        command.Parameters.Add("@DOB", SqlDbType.NVarChar, -1).Value = p.DOB;
                        command.Parameters.Add("@RecommenderID", SqlDbType.NVarChar, -1).Value = p.RecommenderID;
                        command.Parameters.Add("@MemberGroup", SqlDbType.NVarChar, -1).Value = p.MemberGroup;
                        command.Parameters.Add("@LastDeviceID", SqlDbType.NVarChar, -1).Value = p.LastDeviceID;
                        command.Parameters.Add("@LastIPaddress", SqlDbType.NVarChar, -1).Value = p.LastIPaddress;
                        command.Parameters.Add("@LastLoginDT", SqlDbType.NVarChar, -1).Value = p.LastLoginDT;
                        command.Parameters.Add("@LastLogoutDT", SqlDbType.NVarChar, -1).Value = p.LastLogoutDT;
                        command.Parameters.Add("@LastMACAddress", SqlDbType.NVarChar, -1).Value = p.LastMACAddress;

                        command.Parameters.Add("@AccountBlockYN", SqlDbType.NVarChar, -1).Value = p.AccountBlockYN;
                        command.Parameters.Add("@AccountBlockEndDT", SqlDbType.NVarChar, -1).Value = p.AccountBlockEndDT;
                        command.Parameters.Add("@AnonymousYN", SqlDbType.NVarChar, -1).Value = p.AnonymousYN;

                        command.Parameters.Add("@3rdAuthProvider", SqlDbType.NVarChar, -1).Value = p._3rdAuthProvider;
                        command.Parameters.Add("@3rdAuthID", SqlDbType.NVarChar, -1).Value = p._3rdAuthID;
                        command.Parameters.Add("@3rdAuthParam", SqlDbType.NVarChar, -1).Value = p._3rdAuthParam;
                        command.Parameters.Add("@PushNotificationID", SqlDbType.NVarChar, -1).Value = p.PushNotificationID;
                        command.Parameters.Add("@PushNotificationProvider", SqlDbType.NVarChar, -1).Value = p.PushNotificationProvider;
                        command.Parameters.Add("@PushNotificationGroup", SqlDbType.NVarChar, -1).Value = p.PushNotificationGroup;

                        command.Parameters.Add("@sCol1", SqlDbType.NVarChar, -1).Value = p.sCol1;
                        command.Parameters.Add("@sCol2", SqlDbType.NVarChar, -1).Value = p.sCol2;
                        command.Parameters.Add("@sCol3", SqlDbType.NVarChar, -1).Value = p.sCol3;
                        command.Parameters.Add("@sCol4", SqlDbType.NVarChar, -1).Value = p.sCol4;
                        command.Parameters.Add("@sCol5", SqlDbType.NVarChar, -1).Value = p.sCol5;
                        command.Parameters.Add("@sCol6", SqlDbType.NVarChar, -1).Value = p.sCol6;
                        command.Parameters.Add("@sCol7", SqlDbType.NVarChar, -1).Value = p.sCol7;
                        command.Parameters.Add("@sCol8", SqlDbType.NVarChar, -1).Value = p.sCol8;
                        command.Parameters.Add("@sCol9", SqlDbType.NVarChar, -1).Value = p.sCol9;
                        command.Parameters.Add("@sCol10 ", SqlDbType.NVarChar, -1).Value = p.sCol10;
                        command.Parameters.Add("@TimeZoneID ", SqlDbType.NVarChar, -1).Value = p.TimeZoneID;

                        connection.OpenWithRetry(retryPolicy);
                        using (SqlDataReader dreader = command.ExecuteReaderWithRetry(retryPolicy))
                        {
                            while (dreader.Read())
                            {
                                result = dreader[0].ToString();
                            }
                            dreader.Close();
                        }
                        connection.Close();

                        // end task log
                        logMessage.memberID = p.MemberID;
                        logMessage.Level = "INFO";
                        logMessage.Logger = "CBCOMUdtMemberController";
                        logMessage.Message = jsonParam;
                        Logging.RunLog(logMessage);

                        return result;
                    }

                }
            }

            catch (Exception ex)
            {
                // error log
                logMessage.memberID = p.MemberID;
                logMessage.Level = "ERROR";
                logMessage.Logger = "CBCOMUdtMemberController";
                logMessage.Message = jsonParam;
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

    }
}
