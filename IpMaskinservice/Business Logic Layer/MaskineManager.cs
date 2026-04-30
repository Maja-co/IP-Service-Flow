using Data_Access_Layer.Models;
using Data_Access_Layer.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Text;


namespace Business_Logic_Layer
{
    public class MaskineManager
    {
        // Repositories injiceres gennem constructor
        private readonly MaskineRepository _maskineRepo;
        private readonly KundeRepository _kundeRepo;

        public MaskineManager(MaskineRepository maskineRepo, KundeRepository kundeRepo)
        {
            _maskineRepo = maskineRepo;
            _kundeRepo = kundeRepo;
        }
    }
}
