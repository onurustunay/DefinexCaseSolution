using Microsoft.VisualStudio.TestTools.UnitTesting;
using DefinexCase.Business.Services.Services.CartServices;

namespace DefinexCase.Test
{
    [TestClass]
    public class UnitTest1
    {
        public ICartServices _cartServices;

        [TestInitialize]
        public void Init(ICartServices cartServices)
        {
            _cartServices = cartServices;
        }

        [TestMethod]
        public void TestMethod1()
        {
            
            bool response=_cartServices.CalculateCart();
            
        }
    }
}
