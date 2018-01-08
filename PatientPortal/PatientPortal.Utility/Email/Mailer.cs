﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Security.Authentication;
using PatientPortal.Domain.LogManager;
using PatientPortal.Domain.Models.CORE;

namespace PatientPortal.Utility.Email
{
    public class Mailer
    {
        static bool OurCertificateValidation(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
            //var actualCertificate = X509Certificate.CreateFromCertFile("certificate.cert");

            //return certificate.Equals(actualCertificate);
        }

        public static bool Send(MailSetting config, Mail obj, List<string> cc = null)
        {
            // Register our own certificate validation
            ServicePointManager.ServerCertificateValidationCallback = OurCertificateValidation;

            // Message
            var from = new MailAddress(obj.Email, obj.Name);
            //var to = new MailAddress(obj.Email, obj.Name);
            var to = new MailAddress(config.Email, config.Name);

            var message = new MailMessage(from, to)
            {
                Subject = (obj.Subject.Length < 1 ? "No Subject" : obj.Subject),
                Body = obj.Body,
                IsBodyHtml = true
            };

            if(cc != null && cc.Count > 0)
            {
               // MailAddressCollection collection = new MailAddressCollection();
                foreach(var receiver in cc)
                {
                   // collection.Add(receiver);
                    message.CC.Add(receiver);
                }
                
            }

            // Create client
            var client = new SmtpClient(config.Host, config.Port)
            {
                EnableSsl = config.IsSSL,
                Credentials = new NetworkCredential
                {
                    UserName = config.Email,
                    Password = config.Pwd,
                }
            };
            // Try to send
            using (client)
            {
                try
                {
                    client.Send(message);
                    return true;
                }
                catch (AuthenticationException ex)
                {
                    Logger.LogError(ex);
                    return false;
                }
                catch (SmtpException ex)
                {
                    Logger.LogError(ex);
                    return false;
                }
            }
        }

        public static bool ServerSendMail(MailSetting config, Mail obj, List<string> cc = null)
        {
            // Register our own certificate validation
            ServicePointManager.ServerCertificateValidationCallback = OurCertificateValidation;

            // Message
            var from = new MailAddress(config.Email, config.Name);
            var to = new MailAddress(obj.Email, obj.Name);

            var message = new MailMessage(from, to)
            {
                Subject = (obj.Subject.Length < 1 ? "No Subject" : obj.Subject),
                Body = obj.Body,
                IsBodyHtml = true
            };

            //Send mail cc to many
            if (cc != null && cc.Count > 0)
            {
                // MailAddressCollection collection = new MailAddressCollection();
                foreach (var receiver in cc)
                {
                    // collection.Add(receiver);
                    message.CC.Add(receiver);
                }
            }

            // Create client
            var client = new SmtpClient(config.Host, config.Port)
            {
                EnableSsl = config.IsSSL,
                Credentials = new NetworkCredential
                {
                    UserName = config.Email,
                    Password = config.Pwd,
                }
            };

            // Try to send
            using (client)
            {
                try
                {
                    client.Send(message);
                    return true;
                }
                catch (AuthenticationException ex)
                {
                    Logger.LogError(ex);
                    return false;
                }
                catch (SmtpException ex)
                {
                    Logger.LogError(ex);
                    return false;
                }
            }
        }
    }
}
