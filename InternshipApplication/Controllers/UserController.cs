﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InternshipApplication.Models;
using InternshipApplication.Models.Domain;
using InternshipApplication.ViewModels;
using WebMatrix.WebData;
using LoginModel = InternshipApplication.ViewModels.LoginModel;

namespace InternshipApplication.Controllers
{
    public class UserController : Controller
    {
        private IBedrijfRepository bedrijfRepository;
        private IStudentRepository studentRepository;
        private IStagebegeleiderRepository stagebegeleiderRepository;
        private IUserRepository userRepository;
        private ISpecialisatieRepository specialisatieRepository;
        private IOpdrachtRepository opdrachtRepository;

        //public UserController(){}

        public UserController(IBedrijfRepository bedrijfR, IStudentRepository studentR,
            IStagebegeleiderRepository stagebegeleiderR, IUserRepository usersRepository,
            ISpecialisatieRepository specialisatie, IOpdrachtRepository opdracht)
        {
            this.bedrijfRepository = bedrijfR;
            this.stagebegeleiderRepository = stagebegeleiderR;
            this.studentRepository = studentR;
            this.userRepository = usersRepository;
            this.specialisatieRepository = specialisatie;
            this.opdrachtRepository = opdracht;
        }

        public ActionResult UserIndex()
        {
            Bedrijf b = bedrijfRepository.FindById(1);
            return View(b);
        }

        public ActionResult ContactPersonen(int id)
        {
            Bedrijf bedrijf = bedrijfRepository.FindById(id);
            if (bedrijf.ContactPersonen == null)
            {
                //voorlopig
                return RedirectToAction("UserIndex");
            }
            return View(bedrijf.ContactPersonen);
        }

        public ActionResult OpdrachtenView(int id)
        {
            Bedrijf bedrijf = bedrijfRepository.FindById(id);
            if (bedrijf.Opdrachten == null)
            {
                return RedirectToAction("UserIndex");
            }
            return View(bedrijf);
        }

        public ActionResult UserToolbar(int id)
        {
            Bedrijf bedrijf = bedrijfRepository.FindById(id);
            return PartialView(bedrijf);
        }

        public ActionResult OpdrachtenPartial(int bedrijfId, int opdrachtId)
        {
            Opdracht o = opdrachtRepository.FindOpdracht(opdrachtId);
            return PartialView(o);
        }


        public ActionResult AddContact()
        {
            return View();
        }
        // Contact wordt toegevoegd
        [HttpPost]
        public ActionResult AddContact(ContactModel contact, int id)
        {
            if (ModelState.IsValid)
            {
                bedrijfRepository.FindById(id).AddContactPersoon(new ContactPersoon(contact.Naam, contact.Voornaam, contact.Functie, contact.ContactEmail, contact.ContactTelNr, contact.GsmNummer));
                bedrijfRepository.SaveChanges();
                return View("ContactPersonen");

            }
            return View(contact);
        }
        public ActionResult AddOpdracht()
        {
            IEnumerable<Specialisatie> specialisaties;
            specialisaties = specialisatieRepository.FindAllSpecialisaties();
            return View(new CreateOpdrachtViewModel(specialisaties, new OpdrachtViewModel()));
        }

        [HttpPost]
        public ActionResult AddOpdracht([Bind(Prefix = "OpdrachtViewModel")]OpdrachtViewModel model, int id)
        {
            if (ModelState.IsValid)
            {
                Opdracht opdracht = new Opdracht();
                opdracht.Title = model.Title;
                opdracht.Omschrijving = model.Omschrijving;
                if (model.Semesters.Equals("Semester 1"))
                {
                    opdracht.IsSemester1 = true;
                    opdracht.IsSemester2 = false;
                }
                else if (model.Semesters.Equals("Semester 2"))
                {
                    opdracht.IsSemester2 = true;
                    opdracht.IsSemester1 = false;
                }
                else
                {
                    opdracht.IsSemester1 = true;
                    opdracht.IsSemester2 = true;
                }
                opdracht.Specialisatie = specialisatieRepository.FindSpecialisatieNaam(model.Specialisatie);
                opdracht.AdminComment = model.AdminComment;
                bedrijfRepository.FindById(id).AddOpdracht(opdracht);

                bedrijfRepository.SaveChanges();
                RedirectToAction("UserIndex");
            }
            IEnumerable<Specialisatie> specialisaties;
            specialisaties = specialisatieRepository.FindAllSpecialisaties();
            return View(new CreateOpdrachtViewModel(specialisaties, model));

        }



    }

}

