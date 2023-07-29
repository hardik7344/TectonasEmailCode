using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OwnYITCommon;
using TecMailAPI.BusinessLogic;
using TecMailAPI.Models;

namespace TecMailAPI.Controller
{
    [Route("TecMail/[controller]/[action]")]
    [ApiController]
    public class TecMailSendController : ControllerBase
    {
        TecMailCommon objCom = new TecMailCommon();
        HandleMail objMail = new HandleMail();
        DataTableConversion dtconversion = new DataTableConversion();
        // POST: TecMail/TecMailSend
        [HttpPost]
        public string Post([FromBody] DataJson objDataJson)
        {
            string strReturn = "";
            //string JsonData = JsonConvert.SerializeObject(objDataJson.data_json); 
            //string InputData = objCom.Base64Decode(objDataJson.data_json);
            string InputData = dtconversion.Base64Decode(objDataJson.data_json);
            DataTableConversion objJC = new DataTableConversion();            
            Dictionary<string, string> objProperty = objJC.getJSONPropertiesFromString("[" + InputData + "]");
            strReturn = objMail.SendMailMessage(objProperty["from"], objProperty["to"], objProperty["cc"], objProperty["bcc"], objProperty["Message"], objProperty["subject"], objProperty["MailBodyType"]);
            return strReturn;
        }
    }
}
