# Heater
Quick runtime managers for Unity 3rd party Plugins.

### Admob

```c#
m_admobManager = new AdmobManager();
m_admobManager.RequestInterstitial( ADMOB_ID );
m_admobManager.RequestBanner( ADMOB_ID, AdSize.Banner, AdPosition.Top );
```

[Blog: Admob Tutorial (iOS + Android)](http://blog.ctrlxctrlv.net/en/unity-admob/)

### Facebook

```c#
m_fbManager = new FBManager();
m_fbManager.LoginCompleteEvent += OnFBLoginComplete;
m_fbManager.ErrorEvent += OnFBError;
m_fbManager.Login();
m_fbManager.Snapshot( texture, title, fileName );
```

[Blog: Facebook SDK Tutorial](http://blog.ctrlxctrlv.net/unity-facebook-sdk/)

### Soomla

You can create a class to define all your goods.

```c#
public class StoreDefine
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
}
```

Using Example:

```c#
// StoreAssets
var assets = new StoreAssets( StoreDefine.VERSION );
assets.AddGood( StoreDefine.REMOVE_AD );
SoomlaStore.Initialize( assets );

// StoreManager
m_storeManager = new StoreManager();
m_storeManager.InitializedEvent += OnStoreInitialized;
m_storeManager.PurchasedEvent += OnStorePurchased;

m_storeManager.BuyItem( StoreDefine.UNLOCK_ALL_ID );
m_storeManager.GetBalanceItem( StoreDefine.UNLOCK_ALL_ID );
```
