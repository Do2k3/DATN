using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Configuration;

namespace TienThinhCandy.Models.Service
{
    
    public class GPTService
    {
        private readonly string _apiKey = ConfigurationManager.AppSettings["api_key"];
        

        public async Task<string> GetAnswerFromGPT(string question)
        {
             
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", _apiKey);

                var requestBody = new
                {
                    model = "gpt-3.5-turbo",
                    messages = new[] { new { role = "user", content = question } }
                };

                var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);
                var json = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return $"Lỗi gọi GPT: {response.StatusCode} - {json}";
                }

                dynamic result = JsonConvert.DeserializeObject(json);

                if (result?.choices == null || result.choices.Count == 0)
                    return "Không có phản hồi từ GPT.";

                string answer = result.choices[0].message.content.ToString();
                return answer.Trim();

            }
        }
    }

}
