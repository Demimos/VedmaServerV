using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vedma0.Data;
using Vedma0.Models.Helper;
using Vedma0.Models.Properties;

namespace Vedma0.Controllers
{
    [AccessRule(AccessLevel.Developer)]
    public class BasePropertiesController : VedmaController
    {

        public BasePropertiesController(ApplicationDbContext context):base(context)
        {
        }

        // GET: BaseProperties
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BaseProperties.Include(b => b.Game).Include(b => b.Preset);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BaseProperties/Details/5
        //public async Task<IActionResult> Details(long? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var baseProperty = await _context.BaseProperties
        //        .Include(b => b.Game)
        //        .Include(b => b.Preset)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (baseProperty == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(baseProperty);
        //}

        // GET: BaseProperties/Create
        public IActionResult Create( long? Id, string type)
        {
            if (Id==null || type==null || ! Enum.TryParse(type, out PropertyType ptype) || !PresetExists((long)Id))
            {
                return BadRequest();
            }
            ViewBag.PresetId = Id;
            switch (ptype)
            {
                case PropertyType.Text: return View("CreateText");
                case PropertyType.Number: return View("CreateNumber");
                case PropertyType.TextArray: return View("CreateTextArray");
                case PropertyType.Identity: throw new NotImplementedException();
                default: throw new MissingMethodException($"Необработанное значение Enum PropertyType: {ptype.ToString()}");
            }
          
        }

        // POST: BaseProperties/CreateText
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateText( [Bind("Name,Description,PresetId,Visible,DefaultValue")] BaseTextProperty baseProperty)
        {
            return await ManageCreation(baseProperty);

        }
        // POST: BaseProperties/CreateNumber
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateNumber([Bind("Name,Description,PresetId,Visible,DefaultValue")] BaseNumericProperty baseProperty)
        {
            return await ManageCreation(baseProperty);
        }
        // POST: BaseProperties/CreateArray
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateArray([Bind("Name,Description,PresetId,Visible,_DefaultValues")] BaseTextArrayProperty baseProperty)
        {
            return await ManageCreation(baseProperty);
        }
        // POST: BaseProperties/CreateIdentity
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateIdentity([Bind("Name,Description,PresetId,Visible,DefaultValue")] BaseTextProperty baseProperty)
        {
            return await new Task<IActionResult>(new Func<IActionResult>(() => throw new NotImplementedException()));
            //return await ManageCreation(baseProperty);
        }

        private async Task<IActionResult> ManageCreation(BaseProperty baseProperty)
        {
            if (ModelState.IsValid && PresetExists((long)baseProperty.PresetId))
            {
                baseProperty.GameId = (Guid)GameId();
                _context.Add(baseProperty);
                await _context.SaveChangesAsync();
                baseProperty.SortValue = baseProperty.Id;
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Presets",new { id = baseProperty.PresetId});
            }
            return View(baseProperty);
        }

        // GET: BaseProperties/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baseProperty = await _context.BaseProperties.FirstOrDefaultAsync(bp=>bp.Id==id && bp.GameId==GameId());
            if (baseProperty == null)
            {
                return NotFound();
            }
            var @switch = new Dictionary<Type, Func<IActionResult>> {
                { typeof(BaseTextProperty), () =>  this.View("EditText", baseProperty) },
                { typeof(BaseNumericProperty), () =>  this.View("EditNumber", baseProperty) },
                { typeof(BaseTextArrayProperty), () => this.View("EditTextArray", baseProperty) }
            };
            var ptype = baseProperty.GetType();
            if (@switch.ContainsKey(ptype))
                return @switch[ptype]();
            throw new IndexOutOfRangeException($"неизвестный тип {ptype.ToString()}");
        }

        // POST: BaseProperties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditText(long id, [Bind("Id,Name,GameId,Description,PresetId,SortValue,Visible,DefaultValue")] BaseTextProperty baseProperty)
        {
            return await ManageEditing(id, baseProperty);
        }
        // POST: BaseProperties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditNumber(long id, [Bind("Id,Name,GameId,Description,PresetId,SortValue,Visible,DefaultValue")] BaseNumericProperty baseProperty)
        {
            return await ManageEditing(id, baseProperty);
        }
        // POST: BaseProperties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTextArray(long id, [Bind("Id,Name,GameId,Description,PresetId,SortValue,Visible,DefaultValues")] BaseTextArrayProperty baseProperty)
        {
            return await ManageEditing(id, baseProperty);
        }
        // POST: BaseProperties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditIdentity(long id, [Bind("Id,Name,GameId,Description,PresetId,SortValue,Visible")] BaseTextProperty baseProperty)
        {
            return await new Task<IActionResult>(new Func<IActionResult> ( () => throw new NotImplementedException() ));
          //  return await ManageEditing(id, baseProperty);
        }

        private async Task<IActionResult> ManageEditing(long id, BaseProperty baseProperty)
        {
            if (id != baseProperty.Id || baseProperty.GameId!=(Guid)GameId() || baseProperty.PresetId==null ||!PresetExists((long)baseProperty.PresetId))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(baseProperty);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BasePropertyExists(baseProperty.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Presets", new { id = baseProperty.PresetId });
            }
            return View(baseProperty);
        }

        // GET: BaseProperties/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baseProperty = await _context.BaseProperties.AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (baseProperty == null || baseProperty.PresetId==null || !PresetExists( (long)baseProperty.PresetId))
            {
                return NotFound();
            }

            return View(baseProperty);
        }

        // POST: BaseProperties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var baseProperty = await _context.BaseProperties.FindAsync(id);
            _context.BaseProperties.Remove(baseProperty);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BasePropertyExists(long id)
        {
            return _context.BaseProperties.Any(e => e.Id == id);
        }
        private bool PresetExists(long id)
        {
            return _context.Presets.Any(e => e.Id == id && e.GameId==GameId());
        }
    }
}
