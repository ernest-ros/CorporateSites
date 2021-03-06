﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Mvc;

using Codebase.Website.Models;
using System.Web.Helpers;

namespace Codebase.Website.Controllers
{
    public class account_setupController : Controller
    {
        //
        // GET: /account_setup/

        public ActionResult index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult index(AccountSetupViewModel accountSetupViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string path = Server.MapPath("App_Data") + "\\" + "client_inquiry.txt";

                    if (System.IO.File.Exists(path))
                    {                        
                        string body = System.IO.File.ReadAllText(path);
                        string subject = System.Configuration.ConfigurationManager.AppSettings.Get("InquiryEMailSubject");
                        string from = System.Configuration.ConfigurationManager.AppSettings.Get("InquiryEMailFrom");
                        string cc = System.Configuration.ConfigurationManager.AppSettings.Get("InquiryEMailCc");

                        body = body.Replace("[*NAME*]", accountSetupViewModel.Name);
                        body = body.Replace("[*BUSINESSNAME*]", accountSetupViewModel.BusinessName);
                        body = body.Replace("[*PHONENUMBER*]", accountSetupViewModel.PhoneNumber);
                        body = body.Replace("[*EMAILADDRESS*]", accountSetupViewModel.EmailAddress);

                        WebMail.SmtpServer = System.Configuration.ConfigurationManager.AppSettings.Get("SmtpServer"); 
                        WebMail.Send(accountSetupViewModel.EmailAddress, subject, body, from, cc, null, true);     
                    }             
                }
                catch (Exception ex)
                {
                    string test = "";

                }

                accountSetupViewModel = new AccountSetupViewModel();
                accountSetupViewModel.Message = "Thank You!";
            }
            else
            {
                accountSetupViewModel.Message = "Please correct the following:";
            }



            return View(accountSetupViewModel);
        }

    }
}
