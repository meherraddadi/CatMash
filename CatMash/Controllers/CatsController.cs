using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CatMash.Data;
using CatMash.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;


namespace CatMash.Controllers
{
    public class CatsController : Controller
    {
        private readonly CatMashContext _context;
        private IHostingEnvironment _env;

        public CatsController(CatMashContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }
        
        // GET: Cats
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cat.ToListAsync());
        }

        // GET: Cats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cat = await _context.Cat
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cat == null)
            {
                return NotFound();
            }

            return View(cat);
        }

        // GET: Cats/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Cat_Name,Cat_picture")] Cat cat,IFormFile Cat_picture)
        {
            
            using (var stream = new MemoryStream())
            {
                await Cat_picture.CopyToAsync(stream);
                cat.Cat_picture = stream.ToArray();
            }

            //picture path
            long totalbytes = Cat_picture.Length;
            string fileName = Cat_picture.FileName;
            var webRoot = _env.WebRootPath;
            cat.Picture_Path = fileName;
            byte[] buffer = new byte[16 * 1024];
            using (FileStream output = System.IO.File.Create(webRoot + "\\images\\" + fileName))
            {
                using (Stream input= Cat_picture.OpenReadStream())
                {
                    int readbytes;
                    while( (readbytes = input.Read(buffer,0,buffer.Length)) > 0)
                    {
                        await output.WriteAsync(buffer, 0, readbytes);
                        totalbytes +=readbytes;
                        
                    }
                }
            }

                if (ModelState.IsValid)
                {
                    _context.Add(cat);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            return View(cat);
        }

        // GET: Cats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cat = await _context.Cat.FindAsync(id);
            if (cat == null)
            {
                return NotFound();
            }
            return View(cat);
        }

        // POST: Cats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Cat_Name,Cat_picture")] Cat cat)
        {
            if (id != cat.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CatExists(cat.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cat);
        }

        // GET: Cats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cat = await _context.Cat
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cat == null)
            {
                return NotFound();
            }

            return View(cat);
        }

        // POST: Cats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cat = await _context.Cat.FindAsync(id);
            _context.Cat.Remove(cat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CatExists(int id)
        {
            return _context.Cat.Any(e => e.Id == id);
        }
        // Get Rating
        public async Task<IActionResult> Rating (int? id)
        {
            if (id == null)
            {
                var result = await _context.Cat.OrderBy(b => b.Id).Take(2).ToListAsync();
                return View(result);
            }
            else
            {
                var result = await _context.Cat.OrderBy(b => b.Id).ToListAsync();
                if (result.Last().Id == id)
                    return RedirectToAction("Rating_Result"); 
                else
                    return View(await _context.Cat.Where(b => b.Id > id).OrderBy(b => b.Id).Take(2).ToListAsync());
            }
        }

        //Post Rating
        [HttpPost]
        public async Task<IActionResult> Rating()
        {
            var cat = await _context.Cat.FindAsync(int.Parse(Request.Form["Id"].ToString()));
            cat.Rate++;
            _context.Update(cat);
            await _context.SaveChangesAsync();
            return RedirectToAction("Rating", new { id = int.Parse(Request.Form["LastId"].ToString()) } );
        }

        //List Rating result
        public async Task<IActionResult> Rating_Result()
        {
            var rating_results = await _context.Cat.FromSqlRaw("select c.[Id],c.[Cat_Name],c.[Date_Cre],c.[Cat_picture],c.[Picture_Path],c.[Rate]," +
                " ROUND(SUM(CAST(c.Rate AS FLOAT)) / (select SUM(CAST(NULLIF(rate, 0) AS FLOAT)) from Cat) * 100,2) as Score from Cat c  " +
                "GROUP BY c.[Id],c.[Cat_Name],c.[Date_Cre],c.[Cat_picture],c.[Picture_Path],c.[Rate] order by c.rate desc ")
            .Select(p => new Cat()
            {
                Cat_Name = p.Cat_Name,
                Score = p.Score,
                Picture_Path= p.Picture_Path
            })

                .ToListAsync();

            return View(rating_results);
        }
    }
}
