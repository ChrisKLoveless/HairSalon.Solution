using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
  public class AppointmentsController : Controller
  {
    private readonly HairSalonContext _db;

    public AppointmentsController(HairSalonContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Appointment> model = _db.Appointments.Include(appointment => appointment.Client).ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      ViewBag.ClientId = new SelectList(_db.Clients, "ClientId", "Name");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Appointment appointment)
    {
      if (appointment.ClientId == 0)
      {
        return RedirectToAction("Create");
      }
      _db.Appointments.Add(appointment);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Appointment thisAppointment = _db.Appointments.FirstOrDefault(appointment => appointment.AppointmentId == id);
      return View(thisAppointment);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Appointment thisAppointment = _db.Appointments.FirstOrDefault(appointment => appointment.AppointmentId == id);
      _db.Appointments.Remove(thisAppointment);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Appointment thisAppointment = _db.Appointments.Include(appointment => appointment.Client).FirstOrDefault(appointment => appointment.AppointmentId == id);
      return View(thisAppointment);
    }

    public ActionResult Edit(int id)
    {
      Appointment thisAppointment = _db.Appointments.FirstOrDefault(resraurant => resraurant.AppointmentId == id);
      ViewBag.ClientId = new SelectList(_db.Clients, "ClientId", "Name");
      return View(thisAppointment);
    }

    [HttpPost]
    public ActionResult Edit(Appointment appointment)
    {
      _db.Appointments.Update(appointment);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}