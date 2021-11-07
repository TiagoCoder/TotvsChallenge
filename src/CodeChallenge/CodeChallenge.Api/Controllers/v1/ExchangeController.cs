using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallenge.Api.Controllers.v1
{
    public class ExchangeController : Controller
    {
        // GET: ExchangeController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ExchangeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ExchangeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ExchangeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ExchangeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ExchangeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ExchangeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ExchangeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
