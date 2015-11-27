using System;
using System.Collections.Generic;
using Soomla.Store;
using UnityEngine;

namespace Heater.Soomla
{
    public class StoreManager
    {
        public static readonly int VERSION = 1;

        public static readonly string REMOVE_AD = "REMOVE_AD";

        public static readonly LifetimeVG REMOVE_AD_GOOD = new LifetimeVG
        (
            "Remove AD",
            "Remove AD",
            REMOVE_AD,
            new PurchaseWithMarket( REMOVE_AD, 0.99 )
        );

        private static readonly bool IS_DEBUG = false;

        public StoreManager()
        {
            StoreEvents.OnSoomlaStoreInitialized += OnStoreInitialized;
            StoreEvents.OnItemPurchaseStarted += OnItemPurchaseStarted;
            StoreEvents.OnItemPurchased += OnItemPurchased;
            StoreEvents.OnMarketPurchaseStarted += OnMarketPurchaseStarted;
            StoreEvents.OnMarketPurchase += OnMarketPurchase;
            StoreEvents.OnUnexpectedStoreError += OnUnexpectedStoreError;

			var assets = new StoreAssets( VERSION );
            assets.AddGood( REMOVE_AD );
            SoomlaStore.Initialize( assets );
        }

        public void BuyItem( string id )
        {
            var item = StoreInfo.GetItemByItemId( id );
            StoreInventory.BuyItem( item.ItemId );
        }

        public int GetItemBalance( string id )
        {
            var balance = StoreInventory.GetItemBalance( id );

            DebugLog( "GetItemBalance: " + balance );

            return balance;
        }

        #region StoreEvents

        private void OnStoreInitialized()
        {
            DebugLog( "OnStoreInitialized" );
        }

        private void OnItemPurchaseStarted( PurchasableVirtualItem item )
        {
            DebugLog( "OnItemPurchaseStarted: " + item.ItemId );
        }

        private void OnItemPurchased( PurchasableVirtualItem item, string payload )
        {
            DebugLog( "OnItemPurchaseStarted: " + item.ItemId );
            DebugLog( "Payload: " + payload );
        }

        private void OnMarketPurchaseStarted( PurchasableVirtualItem item )
        {
            DebugLog( "OnMarketPurchaseStarted: " + item.ItemId );
        }

        private void OnMarketPurchase( PurchasableVirtualItem item, string payload, Dictionary<string, string> extra )
        {
            DebugLog( "OnMarketPurchase: " + item.ItemId );
            DebugLog( "Payload: " + payload );
        }

        private void OnUnexpectedStoreError( int error )
        {
            DebugLog( "OnUnexpectedStoreError: " + error );
        }

        #endregion

        private void DebugLog( string log )
        {
            if( IS_DEBUG )
            {
                Debug.Log( "[StoreManager]: " + log );
            }
        }
    }
}
