using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyYahoo.Models;
using X.PagedList;

namespace MyYahoo.Controllers
{
    public class HomeController : Controller
    {

        private DataContext _context;

        public HomeController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index(int? page)
        {
            List<Question> questions = _context.questions.AsNoTracking().OrderByDescending(e => e.Id).ToList();
            ViewBag.count = questions.Count;
            int pageNumber = page ?? 1;

            var pagedQuestions = questions.ToPagedList(pageNumber, 3);
            return View("Home", pagedQuestions);
        }

        [HttpGet]
        [Route("perguntas/{id}/detalhes")]
        public IActionResult Details(int id, int? page)
        {
            int pageNumber = page ?? 1;
            Question question = _context.questions
                .Include(e => e.Answers)
                .Where(e => e.Id == id)
                .FirstOrDefault();

            return View("Details", question);
        }

        [HttpGet]
        [Route("sobre")]
        public IActionResult About() => View();

        // INSERT =========================

        [HttpGet]
        [Route("perguntas/criar")]
        public IActionResult Create() => View();

        [HttpPost]
        [Route("perguntas/criar")]
        public IActionResult Create([FromForm]Question question)
        {
            if(!ModelState.IsValid) return View("Create", question);
            
            _context.questions.Add(question);
            _context.SaveChanges();
            return RedirectToAction("Details", new { id = question.Id });
        }

        // ANSWER =========================

        [HttpGet]
        [Route("resposta/{id}/responder")]
        public IActionResult Answer(int id)
        {
            Question question = _context.questions.Find(id);
            ViewData["question"] = question;
            return View();
        }

        [HttpPost]
        [Route("resposta/{id}/responder")]
        public IActionResult Answer([FromForm]Answer answer, int id)
        {
            if(!ModelState.IsValid) return View(answer);

            _context.answers.Add(answer);
            _context.SaveChanges();


            return RedirectToAction("Details", "Home", new{ id = id }, "respostas");
        }

        //  =========================
    }
}