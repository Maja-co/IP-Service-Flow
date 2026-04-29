using Data_Access_Layer.Models;
using Data_Access_Layer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Logic_Layer.Services
{
    public class PåmindelsesService
    {
        private readonly MaskinContext _context;
        public PåmindelsesService(MaskinContext context)
        {
            _context = context;
        }
        public List<Påmindelse> HentAktivePåmindelser()
        {
            return _context.Påmindelser
                           .Where(p => p.ServiceOpgave.Maskine.Kunde.ErAktiv == true)
                           .ToList();
        }
    }
}
