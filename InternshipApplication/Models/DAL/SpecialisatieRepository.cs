﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using InternshipApplication.Models.Domain;

namespace InternshipApplication.Models.DAL
{
    public class SpecialisatieRepository : ISpecialisatieRepository
    {
        private InternshipContext context;
        private DbSet<Specialisatie> specialisaties;

        public SpecialisatieRepository(InternshipContext context)
        {
            this.context = context;
            this.specialisaties = context.Specialisaties;
        }


        public IQueryable<Specialisatie> FindAllSpecialisaties()
        {
            return specialisaties.OrderBy(s=>s.Title);
        }

        public Specialisatie FindSpecialisatie(int id)
        {
            return specialisaties.Find(id);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}