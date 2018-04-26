using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BlackBee.Toolkit.Rest.EasyData;
using BlackBee.Toolkit.Rest.EasyData.Classes;
using BS.Vms.JShoping.ImExPrice.DAL.Classes;

namespace BS.Vms.JShoping.ImExPrice.DAL
{
    public class ImExPriceDataContext : DataContext
    {
        private readonly string _pathGetProducts;
        private readonly string _pathGetExtraFieldsValues;
        private readonly string _pathGetOrderClients;
        

        public ImExPriceDataContext(string nameConnnect, string baseAddress, Encoding encoding) : base(nameConnnect,
            baseAddress, encoding)
        {
            _pathGetProducts = "/rest/products";
            _pathGetExtraFieldsValues = "/rest/extra_fields_values";
            _pathGetOrderClients = "/rest/order_clients";
        }

        public async Task<List<Product>> API_GET_Products()
        {
            var resl = await Proxy.GetAsync<ResponceServer<Product>>(BaseAddress + _pathGetProducts);
            return resl.DataList;
        }

        public async Task<List<ExtraFieldsValues>> API_GET_ExtraFieldsValues()
        {
            var resl = await Proxy.GetAsync<ResponceServer<ExtraFieldsValues>>(BaseAddress + _pathGetExtraFieldsValues);
            return resl.DataList;
        }

        public async Task<List<Client>> API_GET_Orders_Client()
        {
            var resl = await Proxy.GetAsync<ResponceServer<Client>>(BaseAddress + _pathGetOrderClients);
            return resl.DataList;
            
        }
    }
}