using Microsoft.AspNetCore.Mvc;
using asp_net_mvc_lab1.Models;
using System.Reflection;

namespace asp_net_mvc_lab1.Controllers
{
    public class BlogController : Controller
    {
        private readonly ILogger<BlogController> _logger;

        private static readonly List<BlogArticleViewModel> _articles = new()
            {
                new BlogArticleViewModel {Id = "1", Title="Welcome to My Blog", Description="Intro post about the blog.", Content = "Hello and welcome to my blog! Here, I'll be sharing my journey as I learn and work with ASP.NET MVC and related technologies."},
                new BlogArticleViewModel {Id = "2", Title="Learning ASP.NET MVC", Description="Basics of ASP.NET MVC.", Content = "ASP.NET MVC is a powerful framework for building web applications. It follows the Model-View-Controller pattern, which helps separate concerns and makes your code more maintainable." },
                new BlogArticleViewModel {Id = "3", Title = "Understanding Routing", Description="How routing works.", Content= "Routing in ASP.NET MVC determines how URLs map to controller actions. The routing system uses route templates to match incoming requests and direct them to the appropriate controller and action method." },
            };
        public BlogController(ILogger<BlogController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View(_articles);
        }
        public IActionResult Article(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return NotFound();
            }
            var post = _articles.FirstOrDefault(p => p.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        public IActionResult Details(string id)
        {
            var article = _articles.FirstOrDefault(a => a.Id == id);
            if (article == null)
                return NotFound();
            return View(article);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateBlogArticleModel());
        }

        [HttpPost]
        public IActionResult Create(CreateBlogArticleModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Id))
                return Content("Provide an Id.");

            var stored = new BlogArticleViewModel
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                Content = model.Content
            };
            _articles.Add(stored);
            return RedirectToAction(nameof(Details), new { id = stored.Id });
        }
    }
}
