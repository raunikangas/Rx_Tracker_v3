using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rx_Tracker_v3;

namespace Rx_Tracker_UI.Controllers
{
    [Authorize]
    public class PrescriptionsController : Controller
    {
        private readonly RxContext _context;

        public PrescriptionsController(RxContext context)
        {
            _context = context;
        }

        // GET: Prescriptions
        public async Task<IActionResult> Index(int? id)
        {
            if(id == null)
            {
                id = Processing.ReturnPatientIdFromEmail(HttpContext.User.Identity.Name);
            }
            var rxList = Processing.ListPrescriptionsByPatientID(id.Value);
            return View(rxList);

            //var rxContext = _context.Prescriptions.Include(p => p.Patient);
            //return View(await rxContext.ToListAsync());
        }

        // GET: Prescriptions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prescription = await _context.Prescriptions
                .Include(p => p.Patient)
                .FirstOrDefaultAsync(m => m.PrescriptionID == id);
            if (prescription == null)
            {
                return NotFound();
            }

            return View(prescription);
        }

        // GET: Prescriptions/Create
        public IActionResult Create()
        {
            ViewData["PrescriptionPatientID"] = new SelectList(_context.Patients, "PatientID", "PatientEmail");
            return View();
        }

        // POST: Prescriptions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PrescriptionID,PrescriptionPatientID,PrescriptionName,PrescriptionPatientTrackingNumber,PrescriptionRefillQuantity,PrescriptionRefillRemaining,PrescriptionPillQuantity,PrescriptionPillQuantityRemaining,PrescriptionPillDose,PrescriptionExpireDate,PrescriptionEntryDate,PrescriptionNextRefillEnableDate,PrescriptionActive")] Prescription prescription)
        {
            
            if (ModelState.IsValid)
            {
                //_context.Add(prescription);
                Processing.AddPrescription(Processing.ReturnPatientIdFromEmail(User.Identity.Name), prescription.PrescriptionName, prescription.PrescriptionRefillQuantity, prescription.PrescriptionPillQuantity, prescription.PrescriptionPillDose, prescription.PrescriptionExpireDate);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PrescriptionPatientID"] = new SelectList(_context.Patients, "PatientID", "PatientEmail", prescription.PrescriptionPatientID);
            return View(prescription);
        }

        // GET: Prescriptions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prescription = await _context.Prescriptions.FindAsync(id);
            if (prescription == null)
            {
                return NotFound();
            }
            ViewData["PrescriptionPatientID"] = new SelectList(_context.Patients, "PatientID", "PatientEmail", prescription.PrescriptionPatientID);
            return View(prescription);
        }

        // POST: Prescriptions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PrescriptionID,PrescriptionPatientID,PrescriptionName,PrescriptionPatientTrackingNumber,PrescriptionRefillQuantity,PrescriptionRefillRemaining,PrescriptionPillQuantity,PrescriptionPillQuantityRemaining,PrescriptionPillDose,PrescriptionExpireDate,PrescriptionEntryDate,PrescriptionNextRefillEnableDate,PrescriptionActive")] Prescription prescription)
        {
            if (id != prescription.PrescriptionID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prescription);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrescriptionExists(prescription.PrescriptionID))
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
            ViewData["PrescriptionPatientID"] = new SelectList(_context.Patients, "PatientID", "PatientEmail", prescription.PrescriptionPatientID);
            return View(prescription);
        }

        // GET: Prescriptions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prescription = await _context.Prescriptions
                .Include(p => p.Patient)
                .FirstOrDefaultAsync(m => m.PrescriptionID == id);
            if (prescription == null)
            {
                return NotFound();
            }

            return View(prescription);
        }

        // POST: Prescriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prescription = await _context.Prescriptions.FindAsync(id);
            _context.Prescriptions.Remove(prescription);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrescriptionExists(int id)
        {
            return _context.Prescriptions.Any(e => e.PrescriptionID == id);
        }


        //Refill GET
        [HttpGet]
        public async Task<IActionResult> Refill(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            IEnumerable<Prescription> rxObject = Processing.db.Prescriptions.Where(a => a.PrescriptionID == id.Value);

            return View(rxObject);
        }

        //Refill POST
        //[HttpPost]
        //public IActionResult Refill(IFormCollection data)
        //{
        //    Processing.AddRefill(data["PrescriptionID"]);

        //    var rxObject = Processing.db.Prescriptions.First(a => a.PrescriptionID == rxID);

        //    Processing.AddRefill(rxID.Value, rxObject.PrescriptionPatientID);

        //    return View()
        //}

        [HttpGet]
        public async Task<IActionResult> Dose(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var prescription = await _context.Prescriptions
                .Include(p => p.Patient)
                .FirstOrDefaultAsync(m => m.PrescriptionID == id);
            if (prescription == null)
            {
                return NotFound();
            }

            return View(prescription);
        }

        [HttpPost, ActionName ("Dose")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DoseConfirmed ()
        {


            //return View();
            return RedirectToAction(nameof(Index));
        }

    }
}
