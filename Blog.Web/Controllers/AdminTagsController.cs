using Blog.Web.Data;
using Blog.Web.Models.Domain;
using Blog.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    public class AdminTagsController : Controller
    {
        private readonly BlogDbContext _context;

        public AdminTagsController(BlogDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public IActionResult Add(AddTagRequest addTagRequest)
        {
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName,
            };
            _context.Tags.Add(tag);
            _context.SaveChanges();

            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]
        public IActionResult List()
        {
            var tags = _context.Tags.ToList();

            return View(tags);
        }

        [HttpGet]
        [ActionName("Edit")]
        public IActionResult Edit(Guid id)
        {
            var tag = _context.Tags.FirstOrDefault(t => t.Id == id);

            if (tag != null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };

                return View(editTagRequest);
            }

            return View(null);
        }

        [HttpPost]
        [ActionName("Edit")]
        public IActionResult Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };

            var exisistingTag = _context.Tags.Find(tag.Id);

            if (exisistingTag != null)
            {
                exisistingTag.Name = tag.Name;
                exisistingTag.DisplayName = tag.DisplayName;

                _context.SaveChanges();

                // Show success notification
                // return RedirectToAction("Edit", new { id = editTagRequest.Id});

                return RedirectToAction("List");
            }
            // Show error notification
            // return RedirectToAction("Edit", new { id = editTagRequest.Id});

            return RedirectToAction("Add");
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult Delete(EditTagRequest editTagRequest)
        {
            var tag = _context.Tags.Find(editTagRequest.Id);

            if (tag != null)
            {
                _context.Tags.Remove(tag);
                _context.SaveChanges();

                // Show a success notification

                return RedirectToAction("List");
            }
            // Show an error notification

            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }
    }
}
