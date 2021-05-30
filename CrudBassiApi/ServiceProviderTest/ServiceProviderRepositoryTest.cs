using Bassi.ADO.Utilities;
using Bassi.API.Configuration;
using Bassi.ServiceProvider.Api.Controllers;
using Bassi.ServiceProvider.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ServiceProviderTest
{
    [TestClass]
    public class ServiceProviderRepositoryTest
    {

        private readonly NewCaseServiceProviderController  _newCaseServiceProviderController;
        public ServiceProviderRepositoryTest(INewCaseRepository newCaseRepository)
        {
           // _newCaseServiceProviderController = new NewCaseServiceProviderController();
        }

        [TestMethod]
        public void NewCaseRepositoryGetAll()
        {
         //   _newCaseServiceProviderController.GetServicesProviderByDistanceGeoByTriage();

  
        }

        //declare @lt float = -37.8150101
        //declare @lon float = 144.954102
        //declare @distancia float = 25
        //declare @specialization_minima varchar(1024) = '111,'
        //declare @specialization_maxima  varchar(1024) = null
        //declare @ServiceProviderTypeID int = 2
        //declare @CountryId int = 2
        //declare @CountryIsoCode varchar(100) = 'AU'
    }
}
