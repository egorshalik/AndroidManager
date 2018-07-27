using System.Threading.Tasks;
using ManagerAndroid.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManagerAndroid.Controllers
{
    [Authorize]
    public class AndroidController : Controller
    {
        private ManagerContext db;
        public AndroidController(ManagerContext options)
        {
            db = options;
        }

        public async Task<IActionResult> Index()
        {
            return View(await db.Androids.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AndroidViewModel androidView)
        {
            if (ModelState.IsValid)
            {
                Android android = (Android)androidView;
                android.Reability = 10;
                android.Status = 1;
                db.Androids.Add(android);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(androidView);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Android android = await db.Androids.FirstOrDefaultAsync(p => p.Id == id);
                if (android != null)
                    return View((AndroidViewModel)android);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AndroidViewModel androidView)
        {
            if (ModelState.IsValid)
            {
                Android oldAndroid = await db.Androids.FirstOrDefaultAsync(p => p.Id == androidView.Id);
                Android android = (Android)androidView;
                if (android.Name != null)
                    oldAndroid.Name = android.Name;
                if (android.Skills != null)
                    oldAndroid.Skills = android.Skills;
                if (android.Avatar != null)
                    oldAndroid.Avatar = android.Avatar;
                db.Androids.Update(oldAndroid);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(androidView);
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                Android android = await db.Androids.FirstOrDefaultAsync(p => p.Id == id);
                if (android != null)
                    return View(android);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Android android = await db.Androids.FirstOrDefaultAsync(p => p.Id == id);
                if (android != null)
                {
                    db.Androids.Remove(android);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }

        public async Task<IActionResult> Android(int? id)
        {
            if (id != null)
            {
                Android android = await db.Androids.FirstOrDefaultAsync(p => p.Id == id);
                if (android != null)
                    return View(android);
            }
            return NotFound();
        }
    }
}