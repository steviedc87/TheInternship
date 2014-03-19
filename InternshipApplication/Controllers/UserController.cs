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

       
        //public UserController(){}

        public UserController(IBedrijfRepository bedrijfR, IStudentRepository studentR , 
            IStagebegeleiderRepository stagebegeleiderR,IUserRepository usersRepository,ISpecialisatieRepository specialisatie)
        {
            this.bedrijfRepository = bedrijfR;
            this.stagebegeleiderRepository = stagebegeleiderR;
            this.studentRepository = studentR;
            this.userRepository = usersRepository;
            this.specialisatieRepository = specialisatie;
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
            return View(bedrijf.Opdrachten);
        }

        public ActionResult UserToolbar(int id)
        {
            Bedrijf bedrijf = bedrijfRepository.FindById(id);
            return PartialView(bedrijf);
        }
   
        }

    }

