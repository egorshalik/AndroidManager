using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ManagerAndroid.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManagerAndroid.Controllers
{
    [Authorize]
    public class JobController : Controller
    {
        private ManagerContext db;
        public JobController(ManagerContext options)
        {
            db = options;
        }

        public async Task<IActionResult> Index()
        {
            return View(await db.Jobs.ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Job job)
        {
            if (ModelState.IsValid)
            {
                db.Jobs.Add(job);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(job);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Job job = await db.Jobs.FirstOrDefaultAsync(p => p.Id == id);
                if (job != null)
                    return View(job);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Job job)
        {
            if (ModelState.IsValid)
            {
                Job oldJob = await db.Jobs.FirstOrDefaultAsync(p => p.Id == job.Id);
                if (job == null)
                    return NotFound();
                if (job.Name != null)
                    oldJob.Name = job.Name;
                if (job.Description != null)
                    oldJob.Description = job.Description;
                if (job.Complexity != 0)
                    oldJob.Complexity = job.Complexity;
                db.Jobs.Update(oldJob);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(job);
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                IQueryable<Assignent> assignents = db.Assignents.Include(p => p.Job).Include(p => p.Android);
                if (id != 0)
                {
                    assignents = assignents.Where(p => p.JobId == id);
                }
                List<Android> androids = db.Androids.ToList();
                if (assignents != null)
                {
                    for (int i = androids.Count - 1; i >= 0; i--)
                    {
                        int sh = 0;
                        foreach (Assignent a in assignents)
                        {

                            if (a.AndroidId == androids[i].Id)
                            {
                                sh = 1;
                            }
                        }
                        if (sh == 0)
                        {
                            androids.RemoveAt(i);
                        }
                    }
                }
                else
                    androids.Clear();
                Job job = await db.Jobs.FirstOrDefaultAsync(p => p.Id == id);
                if (job != null)
                    return View(new JobViewModel(job, androids));
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Job job = await db.Jobs.FirstOrDefaultAsync(p => p.Id == id);
                if (job != null)
                {
                    db.Jobs.Remove(job);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }

        public async Task<IActionResult> Job(int? id)
        {
            if (id != null)
            {
                IQueryable<Assignent> assignents = db.Assignents.Include(p => p.Job).Include(p => p.Android);
                if (id != 0)
                {
                    assignents = assignents.Where(p => p.JobId == id);
                }
                List<Android> androids = db.Androids.ToList();
                List<Assignent> assignentsList = assignents.ToList();
                if (assignentsList.Count != 0)
                {
                    for (int i = androids.Count - 1; i >= 0; i--)
                    {
                        int sh = 0;
                        foreach (Assignent a in assignentsList)
                        {
                            if (a.AndroidId == androids[i].Id)
                            {
                                sh = 1;
                            }
                        }
                        if (sh == 0)
                        {
                            androids.RemoveAt(i);
                        }
                    }
                }
                else
                    androids.Clear();
                Job job = await db.Jobs.FirstOrDefaultAsync(p => p.Id == id);
                if (job != null)
                    return View(new JobViewModel(job, androids));
            }
            return NotFound();
        }

        public async Task<IActionResult> Assign(int? id)
        {            
            if (id != null)
            {
                IQueryable<Assignent> assignents = db.Assignents.Include(p => p.Job).Include(p => p.Android);
                if (id != 0)
                {
                    assignents = assignents.Where(p => p.JobId == id);
                }
                List<Android> androids = db.Androids.ToList();
                if (assignents != null)
                {
                    for (int i = androids.Count - 1; i >= 0; i--)
                    {
                        int sh = 0;
                        foreach (Assignent a in assignents)
                        {
                            if (a.AndroidId == androids[i].Id)
                            {
                                sh = 1;
                            }
                        }
                        if(androids[i].Status == 0)
                        {
                            androids.RemoveAt(i);
                            break;
                        }
                        if (sh == 1)
                        {
                            androids.RemoveAt(i);
                        }
                    }
                }
                Job job = await db.Jobs.FirstOrDefaultAsync(p => p.Id == id);
                return View(new JobViewModel(job, androids));
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Assign(int? jobId, int? androidId)
        {
            if (jobId != null && androidId != null)
            {
                Assignent assignent = new Assignent { JobId = (int)jobId, AndroidId = (int)androidId };
                Android android = await db.Androids.FirstOrDefaultAsync(p => p.Id == androidId);
                android.Change();
                db.Androids.Update(android);
                db.Assignents.Add(assignent);
                await db.SaveChangesAsync();
                return RedirectToAction("Job", new { id = jobId });
            }
            return NotFound();
        }
                
        [HttpPost]
        public async Task<IActionResult> Deactivate(int? jobId, int? androidId)
        {
            if (jobId != null && androidId != null)
            {
                Assignent assignent = await db.Assignents.FirstOrDefaultAsync(p => p.JobId == jobId & p.AndroidId == (int)androidId);
                db.Assignents.Remove(assignent);
                await db.SaveChangesAsync();
                return RedirectToAction("Job", new { id = jobId });
            }
            return NotFound();
        }
    }
}