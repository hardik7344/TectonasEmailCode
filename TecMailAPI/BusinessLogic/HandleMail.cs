using OwnYITCommon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace TecMailAPI.BusinessLogic
{
    public class HandleMail
    {
        TecMailCommon objCom = new TecMailCommon();
        DataTableConversion dtconversion = new DataTableConversion();
        public string SendMailMessage(string from, string to, string cc, string bcc, string Message, string subject, string MailBodyType) // MailBodyType -- Text/HTML
        {
            string strReturn = "";
            try
            {
                MailMessage mMailMessage = new MailMessage();
                mMailMessage.From = new MailAddress(from);
                mMailMessage.IsBodyHtml = false;
                if (MailBodyType.ToUpper() == "HTML")
                    mMailMessage.IsBodyHtml = true;
                mMailMessage.Body = Message;
                mMailMessage.Subject = subject;
                mMailMessage.Priority = MailPriority.Normal;
                AddTo(ref mMailMessage, to);
                AddCC(ref mMailMessage, cc);
                AddBCC(ref mMailMessage, bcc);
                SmtpClient smtpClient = new SmtpClient("smtpout.secureserver.net", 25);
                smtpClient.UseDefaultCredentials = false;
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new System.Net.NetworkCredential(TecMailCommon.MailUserName, TecMailCommon.MailUserPassword);
                smtpClient.Send(mMailMessage);
           
                strReturn = "Mail send successfully";
            }
            catch (Exception ex)
            {
                objCom.WriteLog("SendMailMessage", "log", "TecMailAPI", "Exception while send mail : " + ex.Message, true);
                strReturn = "Mail send fail : " + ex.Message;
            }
            return strReturn;
        }
        private void AddTo(ref MailMessage mMailMessage, string To)
        {
            try
            {
                if ((To != null) && (To != string.Empty))
                {
                    string[] strTo = To.Split(';');
                    for (int i = 0; i < strTo.Length; i++)
                    {
                        if (strTo[i].ToString().Trim().Length > 1)
                            mMailMessage.To.Add(new MailAddress(strTo[i].ToString().Trim()));
                    }
                }
            }
            catch (Exception ex)
            {
                objCom.WriteLog("SendMailMessage", "log", "TecMailAPI", "Exception while Add TO : " + ex.Message, true);
            }
        }
        private void AddCC(ref MailMessage mMailMessage, string CC)
        {
            try
            {
                if ((CC != null) && (CC != string.Empty))
                {
                    string[] strCC = CC.Split(';');
                    for (int i = 0; i < strCC.Length; i++)
                    {
                        if (strCC[i].ToString().Trim().Length > 1)
                            mMailMessage.CC.Add(new MailAddress(strCC[i].ToString().Trim()));
                    }
                }
            }
            catch (Exception ex)
            {
                objCom.WriteLog("SendMailMessage", "log", "TecMailAPI", "Exception while Add CC : " + ex.Message, true);
            }
        }
        private void AddBCC(ref MailMessage mMailMessage, string BCC)
        {
            try
            {
                if ((BCC != null) && (BCC != string.Empty))
                {
                    string[] strBCC = BCC.Split(';');
                    for (int i = 0; i < strBCC.Length; i++)
                    {
                        if (strBCC[i].ToString().Trim().Length > 1)
                            mMailMessage.Bcc.Add(new MailAddress(strBCC[i].ToString().Trim()));
                    }
                }
            }
            catch (Exception ex)
            {
                objCom.WriteLog("SendMailMessage", "log", "TecMailAPI", "Exception while Add BCC : " + ex.Message, true);
            }
        }
    }
}