using Backend.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Backend.Controllers
{
    public class ScheduleController : Controller
    {
        [HttpPost]

        public IActionResult Register(ScheduleModel schedule)
        {
            // if (HttpContext.Session.GetInt32("id") == null)
            // {
            //     return RedirectToAction("Login", "User");
            // }
            int i = 1;
            i += 1;
            ScheduleRepository scheduleEntity = new ScheduleRepository();
            schedule.id = i;
            scheduleEntity.insertNewSchedule(schedule);
            return Json(new { mensagem = "Cadastro realizado com sucesso" });
        }


        public IActionResult Register()
        {
            // if (HttpContext.Session.GetInt32("id") == null)
            // {
            //     return RedirectToAction("Login", "User");
            // }

            return View();
        }


        public IActionResult Listing()
        {
            // if (HttpContext.Session.GetInt32("id") == null)
            // {
            //     return RedirectToAction("Login");
            // }

            ScheduleRepository scheduleEntity = new ScheduleRepository();
            List<ScheduleModel> scheduleList = scheduleEntity.listAllSchedules();

            return Json(scheduleList);
        }


        public IActionResult Remove(int id)
        {
            // if (HttpContext.Session.GetInt32("id") == null)
            // {
            //     return RedirectToAction("Login");
            // }

            ScheduleRepository scheduleEntity = new ScheduleRepository();
            ScheduleModel schedule = scheduleEntity.searchScheduleFromId(id);
            scheduleEntity.deleteSchedule(schedule);
            return RedirectToAction("Listing");
        }


        public IActionResult Edit(int id)
        {
            // if (HttpContext.Session.GetInt32("id") == null)
            // {
            //     return RedirectToAction("Login");
            // }

            ScheduleRepository scheduleEntity = new ScheduleRepository();
            ScheduleModel schedule = scheduleEntity.searchScheduleFromId(id);
            return Json(schedule);
        }

        [HttpPost]

        public IActionResult Edit(ScheduleModel schedule)
        {
            // if (HttpContext.Session.GetInt32("id") == null)
            // {
            //     return RedirectToAction("Login");
            // }

            ScheduleRepository scheduleEntity = new ScheduleRepository();
            scheduleEntity.updateDataSchedule(schedule);
            return RedirectToAction("Listing");
        }
    }
}
