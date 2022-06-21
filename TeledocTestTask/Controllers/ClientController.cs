using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TeledocTestTask.Models;
using TeledocTestTask.ViewModels;

namespace TeledocTestTask.Controllers
{
    [Route("client")]
    public class ClientController : Controller
    {
        DataBaseContext _context;

        public ClientController(DataBaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("view/{id}")]
        public IActionResult View(int id)
        {
            Client client = _context.clients.Where(i => i.Id == id).FirstOrDefault();
            if (client == null) return Redirect("~/home/index");
            client.ClientFounders = _context.clientFounders.Where(i => i.ClientId == client.Id).ToList();
            ViewBag.allfounders = _context.founders;
            ViewBag.clientData = client;
            return View();
        }

        [HttpGet]
        [Route("add")]
        public IActionResult Add()
        {
            List<ChekerViewModel> chekers = new List<ChekerViewModel>();
            foreach (var i in _context.founders)
            {
                chekers.Add(new ChekerViewModel
                {
                    Id = i.Id,
                    Name = i.Name + " " + i.MiddleName + " " + i.LastName,
                    IsCheked = false
                });
            }
            ViewBag.clientData = new Client();
            return View(chekers);
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Add(string name, string middleName, string lastName, string type, string inn, List<ChekerViewModel> chekers)
        {
            ClientType clientType;
            if (type == "IP")
                clientType = ClientType.IndividualEntrepreneur;
            else
            {
                clientType = ClientType.LegalEntity;
            }
            Client client = new Client(inn, name, middleName, lastName, clientType, DateTime.Now, DateTime.Now);

            //ViewBag.clientData = new Client();
            if (name == null || middleName == null || lastName == null || inn == null)
            {
                ViewBag.errMsg = "Не все поля заполненны";
                ViewBag.clientData = client;
                return View(chekers);
            }
            if (type == "IP")
            {
                foreach (var i in chekers)
                {
                    if (i.IsCheked)
                    {
                        ViewBag.errMsg = "Только ЮЛ могут иметь учередителей";
                        ViewBag.clientData = client;
                        return View(chekers);
                    }
                }
            }
            if (inn.Length != 10)
            {
                ViewBag.errMsg = "ИНН должен содержать 10 цифр";
                ViewBag.clientData = client;
                return View(chekers);
            }

            foreach (var i in chekers)
            {
                if (i.IsCheked == true)
                {
                    ClientFounder clientFounder = new ClientFounder();
                    clientFounder.FounderId = i.Id;
                    client.ClientFounders.Add(clientFounder);
                }
            }
            _context.Add(client);
            _context.SaveChanges();
            return Redirect("~/home/index");
        }

        [HttpGet]
        [Route("edit/{id}")]
        public IActionResult Edit(int id)
        {
            Client client = _context.clients.Where(i => i.Id == id).FirstOrDefault();
            client.ClientFounders = _context.clientFounders.Where(i => i.ClientId == client.Id).ToList();
            ViewBag.allfounders = _context.founders;
            ViewBag.clientData = client;

            List<ChekerViewModel> chekers = new List<ChekerViewModel>();
            foreach (var i in _context.founders)
            {
                bool isCheked = false;
                if (_context.clientFounders.Where(j => j.ClientId == client.Id && j.FounderId == i.Id).FirstOrDefault() != null)
                {
                    isCheked = true;
                }
                chekers.Add(new ChekerViewModel
                {
                    Id = i.Id,
                    Name = i.Name + " " + i.MiddleName + " " + i.LastName,
                    IsCheked = isCheked
                });
            }
            return View(chekers);
        }

        [HttpPost]
        [Route("edit/{id}")]
        public IActionResult Edit(int id, string name, string middleName, string lastName, string type, string inn, List<ChekerViewModel> chekers)
        {
            Client client = _context.clients.Where(i => i.Id == id).FirstOrDefault();
            client.Name = name;
            client.MiddleName = middleName;
            client.LastName = lastName;
            client.INN = inn;
            client.DateUpdate = System.DateTime.Now;
            if (type == "IP")
            {
                client.clientType = ClientType.IndividualEntrepreneur;
            }
            else
            {
                client.clientType = ClientType.LegalEntity;
            }
            ViewBag.clientData = new Client();
            if (name == null || middleName == null || lastName == null || inn == null)
            {
                ViewBag.errMsg = "Не все поля заполненны";
                ViewBag.clientData = client;
                return View(chekers);
            }
            if (type == "IP")
            {
                foreach (var i in chekers)
                {
                    if (i.IsCheked)
                    {
                        ViewBag.errMsg = "Только ЮЛ могут иметь учередителей";
                        ViewBag.clientData = client;
                        return View(chekers);
                    }
                }
            }
            if (inn.Length != 10)
            {
                ViewBag.errMsg = "ИНН должен содержать 10 цифр";
                ViewBag.clientData = client;
                return View(chekers);
            }

            ClientFounder[] clientFoundersToRemove = _context.clientFounders.Where(i => i.ClientId == client.Id).ToArray();
            _context.RemoveRange(clientFoundersToRemove);
            foreach (var i in chekers)
            {
                if (i.IsCheked == true)
                {
                    ClientFounder clientFounder = new ClientFounder();
                    clientFounder.FounderId = i.Id;
                    clientFounder.Client = client;
                    client.ClientFounders.Add(clientFounder);
                }
            }
            _context.Update(client);
            _context.SaveChanges();
            return Redirect("~/home/index");
        }

        [HttpPost]
        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            ClientFounder[] clientFounders = _context.clientFounders.Where(i => i.ClientId == id).ToArray();
            Client client = _context.clients.Where(i => i.Id == id).FirstOrDefault();
            _context.RemoveRange(clientFounders);
            _context.Remove(client);
            _context.SaveChanges();
            return Redirect("~/home/index");
        }
    }
}
