using System.Collections.Generic;
using System.Linq;
using Soomla.Store;

namespace Heater.Soomla
{
    public class StoreAssets : IStoreAssets
    {
        public int Version
        {
            get;
            private set;
        }

        public IList<VirtualCurrency> Currencies
        {
            get;
            private set;
        }

        public IList<VirtualGood> Goods
        {
            get;
            private set;
        }

        public IList<VirtualCurrencyPack> CurrencyPacks
        {
            get;
            private set;
        }

        public IList<VirtualCategory> Categories
        {
            get;
            private set;
        }

        public StoreAssets( int version )
        {
            Version = version;

            Currencies = new List<VirtualCurrency>();
            Goods = new List<VirtualGood>();
            CurrencyPacks = new List<VirtualCurrencyPack>();
            Categories = new List<VirtualCategory>();
        }

        #region IStoreAssets

        int IStoreAssets.GetVersion()
        {
            return Version;
        }

        VirtualCurrency[] IStoreAssets.GetCurrencies()
        {
            return Currencies.ToArray();
        }

        VirtualGood[] IStoreAssets.GetGoods()
        {
            return Goods.ToArray();
        }

        VirtualCurrencyPack[] IStoreAssets.GetCurrencyPacks()
        {
            return CurrencyPacks.ToArray();
        }

        VirtualCategory[] IStoreAssets.GetCategories()
        {
            return Categories.ToArray();
        }

        #endregion

        public void AddCurrency( VirtualCurrency currency )
        {
            Currencies.Add( currency );
        }

        public void AddGood( VirtualGood good )
        {
            Goods.Add( good );
        }

        public void AddCurrencyPack( VirtualCurrencyPack currencyPack )
        {
            CurrencyPacks.Add( currencyPack );
        }

        public void AddCategory( VirtualCategory category )
        {
            Categories.Add( category );
        }
    }
}
