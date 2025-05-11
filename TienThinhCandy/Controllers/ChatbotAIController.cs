using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TienThinhCandy.Models;
using OpenAI.Chat;
using System.Threading.Tasks;
using TienThinhCandy.Models.DB;
using System.Configuration;
using TienThinhCandy.Models.Service;

namespace TienThinhCandy.Controllers
{
    public class ChatbotAIController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        //private ChatClient openAiClient = new ChatClient(model: "gpt-3.5-turbo", apiKey: ConfigurationManager.AppSettings["api_key"]);
        
        // GET: ChatbotAI
        public ActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> Ask(string question)
        {
            // Tìm bản ghi theo từ khóa có trong câu hỏi (ví dụ: dùng Contains)
            var entry = db.ChatbotDatas
                .FirstOrDefault(c => !string.IsNullOrEmpty(c.Keyword) && question.Contains(c.Keyword));

            string answer;
            if (entry != null)
            {
                // Nếu tìm thấy bản ghi mẫu hoặc đã lưu
                if (entry.IsAIGenerated)
                    answer = entry.GPTAnswer;   // sử dụng câu trả lời đã lưu từ AI
                else
                    answer = entry.SampleAnswer; // sử dụng câu trả lời mẫu
            }
            else
            {
                var gpt = new GPTService();
                answer = await gpt.GetAnswerFromGPT(question);

                db.ChatbotDatas.Add(new ChatbotData
                {
                    UserQuestion = question,
                    GPTAnswer = answer
                });
                db.SaveChanges();
                //// Nếu không tìm thấy, gọi OpenAI GPT để lấy câu trả lời
                //ChatCompletion completion = await openAiClient.CompleteChatAsync(question);
                //answer = completion.Content[0].Text.Trim();

                //// Lưu câu hỏi và câu trả lời mới vào bảng với IsAIGenerated = true
                //var newEntry = new ChatbotData
                //{
                //    Keyword = question,  // hoặc tách/chọn từ khóa phù hợp
                //    IsAIGenerated = true,
                //    UserQuestion = question,
                //    GPTAnswer = answer
                //
            }

            // Trả về câu trả lời dưới dạng JSON (đơn giản)
            return Json(answer, JsonRequestBehavior.AllowGet);
        }
    }
}
