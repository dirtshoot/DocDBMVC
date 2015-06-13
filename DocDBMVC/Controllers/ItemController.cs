using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Threading.Tasks;
using DocDBMVC.Models;

namespace DocDBMVC.Models
{
    //[Authorize]
    public class ItemController : Controller
    {
        // GET: Item
        public ActionResult Index()
        {
            //var items = DocumentDBRepository<Item>.GetItems(d => !d.Completed);
            var items = DocumentDBRepository<Item>.GetAllItems();
            return View(items);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Description,Completed")] Item item)
        {
            if (ModelState.IsValid)
            {
                await DocumentDBRepository<Item>.CreateItemAsync(item);
                return RedirectToAction("Index");
            }
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Description,Completed")] Item item)
        {
            if (ModelState.IsValid)
            {
                await DocumentDBRepository<Item>.UpdateItemAsync(item.Id, item);
                return RedirectToAction("Index");
            }

            return View(item);
        }

        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Item item = (Item)DocumentDBRepository<Item>.GetItem(d => d.Id == id);

            if (item == null)
            {
                return HttpNotFound();
            }

            return View(item);
        }
        public ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Item item = (Item)DocumentDBRepository<Item>.GetItem(d => d.Id == id);

            if (item == null)
            {
                return HttpNotFound();
            }

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete([Bind(Include = "Id")] Item item)
        {
            await DocumentDBRepository<Item>.DeleteItemAsync(item.Id);
            return RedirectToAction("Index");
        }



        public ActionResult Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Item item = (Item)DocumentDBRepository<Item>.GetItem(d => d.Id == id);

            if (item == null)
            {
                return HttpNotFound();
            }

            return View(item);
        }



    }
}