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
    public class PatientsController : Controller
    {
        private readonly RxContext _context;

        public PatientsController(RxContext context)
        {
            _context = context;
        }

        // GET: Patients
        public IActionResult Index()
        {
            return View(Processing.ListIndividualPatient(HttpContext.User.Identity.Name));
            //return View(await _context.Patients.ToListAsync());
        }

        // GET: Patients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients
                .FirstOrDefaultAsync(m => m.PatientID == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // GET: Patients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PatientFirstName,PatientLastName,PatientBirthDate")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                Processing.AddPatient(patient.PatientFirstName, patient.PatientLastName, HttpContext.User.Identity.Name, patient.PatientBirthDate);
                patient.PatientEmail = HttpContext.User.Identity.Name;
                //await _context.SaveChangesAsync();
                //await Processing.db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }

        // GET: Patients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var patientObject = Processing.db.Patients.First(a => a.PatientEmail == User.Identity.Name);
            if (id == null)
            {
                id = patientObject.PatientID;
                //return NotFound();
            }

            var patient = Processing.ReturnIndividualPatient(patientObject.PatientID);
            //var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PatientID,PatientFirstName,PatientLastName,PatientBirthDate,PatientEmail")] Patient patient)
        {
            if (id != patient.PatientID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    patient.PatientFullName = $"{patient.PatientLastName}, {patient.PatientFirstName}";
                    _context.Update(patient);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientExists(patient.PatientID))
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
            return View(patient);
        }

        // GET: Patients/Disable/5
        //public async Task<IActionResult> Disable(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var patient = await _context.Patients
        //        .FirstOrDefaultAsync(m => m.PatientID == id);
        //    if (patient == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(patient);
        //}

        //// POST: Patients/Disable/5
        //[HttpPost, ActionName("Disable")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DisableConfirmed(int id)
        //{
        //    //var patient = await _context.Patients.FindAsync(id);
        //    //_context.Patients.Remove(patient);
        //    var patient = Processing.ReturnIndividualPatient(id);
        //    patient.PatientActive = false;
        //    Processing.db.SaveChanges();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool PatientExists(int id)
        {
            return Processing.db.Patients.Any(e => e.PatientID == id);
        }


        #region Controller Methods
        [HttpGet]
        public IActionResult PatientRX(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //var account = Bank.GetAccountByAccountNumber(id.Value);
            var rxList = Processing.ListPrescriptionsByPatientID(id.Value);
            return View(rxList);

        }

        //[HttpPost]
        //public IActionResult PatientRx(IFormCollection data)
        //{
        //    var accountNumber = Convert.ToInt32(data["AccountNumber"]);
        //    var amount = Convert.ToDecimal(data["Amount"]);

        //    Bank.Withdraw(accountNumber, amount);
        //    return RedirectToAction(nameof(Index));
        //}

        #endregion
    }
}
