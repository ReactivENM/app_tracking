using System.Threading.Tasks;
using Models.PackageModel;

namespace Controllers.PackageController
{
    public class PackageController
    {
        private PackageModel model;

        public PackageController()
        {
            PackageModel packageModel = new PackageModel();
            model = packageModel;
        }

        public async Task<string> GetStatus(string id)
        {
            return await model.GetStatus(id);
        }
    }
}
