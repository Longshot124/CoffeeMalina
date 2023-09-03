using Malina.BLL.Extensions;
using Malina.BLL.VIewModels.Slider;
using Malina.Core.Entities;
using Malina.Data.Data;
using Malina.Data.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Malina.Areas.Admin.Controllers
{
    public class SliderController : BaseController
    {
        private readonly MalinaDbContext _malinaDbContext;

        public SliderController(MalinaDbContext malinaDbContext)
        {
            _malinaDbContext = malinaDbContext;
        }

        public async Task<IActionResult> Index()
        {
            var sliders = await _malinaDbContext
                .Sliders
                .Where(s => !s.IsDeleted)
                .OrderByDescending(e => e.Id)
                .ToListAsync();

            return View(sliders);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (!model.Image.IsImage())
            {
                ModelState.AddModelError("Image", "An image must be selected..!");
                return View(model);
            }

            if (!model.Image.IsAllowedSize(20))
            {
                ModelState.AddModelError("Image", "Image size can be maximum 20mb..!");
                return View(model);
            }

            var unicalFileName = await model.Image.GenerateFile(Constants.SliderPath);

            var slider = new Slider
            {
                ImageUrl = unicalFileName,
                SliderName = model.SliderName,
                Description = model.Description
                
            };
            await _malinaDbContext.Sliders.AddAsync(slider);
            await _malinaDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id is null) return NotFound();
            var slider = await _malinaDbContext.Sliders.FindAsync(id);
            if (slider is null) return BadRequest();

            var sliderUpdateViewModel = new SliderUpdateViewModel
            {
                ImageUrl = slider.ImageUrl,
                SliderName = slider.SliderName,
                Description = slider.Description,
                
            };
            return View(sliderUpdateViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(SliderUpdateViewModel model, int? id)
        {
            if (id is null) return NotFound();
            var slider = await _malinaDbContext.Sliders.FindAsync(id);
            if (slider is null) return BadRequest();
            if (slider.Id != id) return BadRequest();
            if (model.Image != null)
            {

                if (!ModelState.IsValid)
                {
                    return View(new SliderUpdateViewModel
                    {

                        ImageUrl = slider.ImageUrl,

                    });
                }

                if (!model.Image.IsImage())
                {
                    ModelState.AddModelError("Image", "An image must be selected..!");
                    return View(new SliderUpdateViewModel
                    {

                        ImageUrl = slider.ImageUrl,

                    });
                }

                if (!model.Image.IsAllowedSize(50))
                {
                    ModelState.AddModelError("Image", "Image size can be maximum 20mb..!");
                    return View(new SliderUpdateViewModel
                    {

                        ImageUrl = slider.ImageUrl,

                    });
                }


                var path = Path.Combine(Constants.RootPath, "img", "slider", slider.ImageUrl);

                if (System.IO.File.Exists(path))
                    System.IO.File.Delete(path);

                var unicalFileName = await model.Image.GenerateFile(Constants.SliderPath);
                slider.ImageUrl = unicalFileName;
            }

            slider.SliderName = model.SliderName;
            slider.Description = model.Description;
           
            await _malinaDbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return NotFound();
            var slider = await _malinaDbContext.Sliders.Where(s => s.Id == id).FirstOrDefaultAsync();
            if (slider is null) return BadRequest();
            if (slider.Id != id) return BadRequest();
            var path = Path.Combine(Constants.RootPath, "img", "slider", slider.ImageUrl);

            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);

            _malinaDbContext.Sliders.Remove(slider);
            await _malinaDbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }




    }
}
