﻿using System;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text;
namespace MobileRecruiter
{
	public class AgentCalls
	{
		private string agentDataUrl = "http://134.213.136.240:1081/api/agents";
		public AgentCalls ()
		{
		}

		private SignUpPage callerPage { get; set;}
		public void AddAgent (string userName, SignUpPage signUpPage)
		{
			callerPage = signUpPage;
			UriBuilder signUpURI = new UriBuilder (agentDataUrl);
			signUpURI.Query = "&id=" + WebUtility.HtmlEncode (userName) + WebUtility.HtmlEncode("&DeviceType=Android&DeviceOS=Android 4.4&DeviceMake=HTC&FirstName=Hal 3&LastName=Bent&Phone=11111111&AgencyName=Agent C Name&AdditionalInfo=No Info");
			HttpWebRequest signUpRequest = (HttpWebRequest)WebRequest.Create (signUpURI.Uri);

			using (HttpWebResponse response = signUpRequest.GetResponse () as HttpWebResponse) 
			{
				if (response.StatusCode != HttpStatusCode.OK)
					Console.WriteLine (response.StatusCode);
				else 
				{
					this.callerPage.UpdateUI (response.StatusCode.ToString());
				}
			}
			//			
		}

		public string GetAgent(String agent)
		{
			string agentData = null;

			UriBuilder getAgentURI = new UriBuilder (agentDataUrl+"/"+agent);
			HttpWebRequest getAgentRequest = (HttpWebRequest)WebRequest.Create (getAgentURI.Uri);
			getAgentRequest.Method = WebRequestMethods.Http.Get;
			getAgentRequest.Accept = "application/json";
			using (HttpWebResponse response =  getAgentRequest.GetResponse () as HttpWebResponse)
				if (response.StatusCode != HttpStatusCode.OK) 
				{
					Console.WriteLine ("Error Fetching Agent Details");
					agentData = "Error";

				}
				else
				{
					using (StreamReader reader = new StreamReader (response.GetResponseStream()))
					{
						var content = reader.ReadToEnd();
						if (string.IsNullOrWhiteSpace (content)) {
							Console.WriteLine ("No Response Data");
							agentData = "Empty";

						} else
							agentData = content;

					}
				}
			return agentData;
		}

		public string GetAgents()
		{

			string agentData = null;

			UriBuilder getAgentsURI = new UriBuilder (agentDataUrl);
			HttpWebRequest getAgentsRequest = (HttpWebRequest)WebRequest.Create (getAgentsURI.Uri);
			getAgentsRequest.Method = WebRequestMethods.Http.Get;
			getAgentsRequest.Accept = "application/json";
			using (HttpWebResponse response =  getAgentsRequest.GetResponse () as HttpWebResponse)
				if (response.StatusCode != HttpStatusCode.OK) 
				{
					Console.WriteLine ("Error Fetching Agent Details");
					agentData = "Error";
				}
				else
				{
					using (StreamReader reader = new StreamReader (response.GetResponseStream()))
					{
						var content = reader.ReadToEnd();
						if (string.IsNullOrWhiteSpace (content)) {
							Console.WriteLine ("No Response Data");
							agentData = "Empty";

						} else
							agentData = content;
					}
				}
			return agentData;
		}


		public void UpdateAgent (string userName, SignUpPage signUpPage)
		{
			callerPage = signUpPage;
			UriBuilder signUpURI = new UriBuilder (agentDataUrl);
			signUpURI.Query = "&id=" + WebUtility.HtmlEncode (userName) + WebUtility.HtmlEncode("&DeviceType=Android&DeviceOS=Android 4.4&DeviceMake=HTC&FirstName=Hal 3&LastName=Bent&Phone=11111111&AgencyName=Agent C Name&AdditionalInfo=No Info");
			HttpWebRequest agentUpdateRequest = (HttpWebRequest)WebRequest.Create (signUpURI.Uri);
			agentUpdateRequest.Method = WebRequestMethods.Http.Put;
			agentUpdateRequest.Accept = "application/json";

			using (HttpWebResponse response = agentUpdateRequest.GetResponse () as HttpWebResponse) 
			{
				if (response.StatusCode != HttpStatusCode.OK)
					Console.WriteLine (response.StatusCode);
				else 
				{
					this.callerPage.UpdateUI (response.StatusCode.ToString());
				}
			}
			//			
		}

	}
}

