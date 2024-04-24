using Microsoft.AspNetCore.Mvc;

namespace Forum.Web.UI.Controllers
{
    public class TopicController : Controller
    {
        // GET: Topic
        public ActionResult Index()
        {
            // Retrieve and display all topics
            return View();
        }

        // GET: Topic/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Topic/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // Add topic creation logic here
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Topic/Edit/5
        public ActionResult Edit(int id)
        {
            // Retrieve the topic by id to edit
            return View();
        }

        // POST: Topic/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // Update topic logic here
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Topic/Delete/5
        public ActionResult Delete(int id)
        {
            // Retrieve the topic by id to delete
            return View();
        }

        // POST: Topic/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // Delete topic logic here
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
