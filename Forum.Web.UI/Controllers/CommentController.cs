using Microsoft.AspNetCore.Mvc;
namespace Forum.Web.UI.Controllers
{
    
    public class CommentController : Controller
    {
        // GET: Comment
        public ActionResult Index(int topicId)
        {
            // Retrieve and display comments by topicId
            return View();
        }

        // GET: Comment/Add
        public ActionResult Add(int topicId)
        {
            ViewBag.TopicId = topicId;
            return View();
        }

        // POST: Comment/Add
        [HttpPost]
        public ActionResult Add(int topicId, FormCollection collection)
        {
            try
            {
                // Add comment adding logic here
                return RedirectToAction("Index", new { topicId = topicId });
            }
            catch
            {
                ViewBag.TopicId = topicId;
                return View();
            }
        }

        // GET: Comment/Edit/5
        public ActionResult Edit(int id)
        {
            // Retrieve the comment by id to edit
            return View();
        }

        // POST: Comment/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // Update comment logic here
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Comment/Delete/5
        public ActionResult Delete(int id)
        {
            // Retrieve the comment by id to delete
            return View();
        }

        // POST: Comment/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // Delete comment logic here
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }

}
