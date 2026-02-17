using GovUKFrontend.Components.Test.Models;
using Microsoft.AspNetCore.Mvc;

namespace GovUKFrontend.Components.Test.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult TextInput()
        {
            var model = new InputFormModel();
            ViewBag.ModelState = ModelState;
            return View(model);
        }

        [HttpPost]
        public IActionResult TextInput(InputFormModel model)
        {
            ViewBag.ModelState = ModelState;

            if (ModelState.IsValid)
            {
                return RedirectToAction("TextInput");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Textarea()
        {
            var model = new TextareaModel();
            ViewBag.ModelState = ModelState;
            return View(model);
        }

        [HttpPost]
        public IActionResult Textarea(TextareaModel model)
        {
            ViewBag.ModelState = ModelState;

            if (ModelState.IsValid)
            {
                return RedirectToAction("Textarea");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Button()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Accordion()
        {
            return View();
        }

        [HttpGet]
        public IActionResult BackLink()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Checkboxes()
        {
            var model = new CheckboxesModel();
            ViewBag.ModelState = ModelState;
            return View(model);
        }

        [HttpPost]
        public IActionResult Checkboxes(CheckboxesModel model)
        {
            if (!model.Checkbox1 && !model.Checkbox2 && !model.Checkbox3)
            {
                ModelState.AddModelError("CheckBoxesTest", "You must check at least one of the choices below.");
            }

            if (ModelState.IsValid)
            {
                return RedirectToAction("Checkboxes");
            }

            ViewBag.ModelState = ModelState;
            return View(model);
        }

        [HttpGet]
        public IActionResult ErrorSummary()
        {
            var model = new ErrorSummaryModel();
            ViewBag.ModelState = ModelState;
            return View(model);
        }

        [HttpPost]
        public IActionResult ErrorSummary(ErrorSummaryModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("ErrorSummary");
            }

            ViewBag.ModelState = ModelState;
            return View(model);
        }

        [HttpGet]
        public IActionResult Fieldset()
        {
            var model = new ErrorSummaryModel();
            ViewBag.ModelState = ModelState;
            return View(model);
        }

        [HttpPost]
        public IActionResult Fieldset(ErrorSummaryModel model)
        {
            ViewBag.ModelState = ModelState;
            return View(model);
        }

        [HttpGet]
        public IActionResult Table()
        {
            return View();
        }


    }
}
