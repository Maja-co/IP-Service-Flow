using Data_Access_Layer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Access_Layer.Services
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
