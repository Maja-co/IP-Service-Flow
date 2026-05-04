using Data_Access_Layer.Repositories;

namespace Business_Logic_Layer
{
    // Staged
    public class MaskineManager
    {
        private readonly MaskineRepository _maskineRepo;
        private readonly KundeRepository _kundeRepo;

        public MaskineManager(MaskineRepository maskineRepo, KundeRepository kundeRepo)
        {
            _maskineRepo = maskineRepo;
            _kundeRepo = kundeRepo;
        }
    }
}
