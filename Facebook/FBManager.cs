using System;
using System.Collections.Generic;
using Facebook.MiniJSON;
using Facebook.Unity;
using UnityEngine;

namespace Heater.Facebook
{
    /// <summary>
    /// m_fbManager = new FBManager();
    /// m_fbManager.LoginCompleteEvent += OnFBLoginComplete;
    /// m_fbManager.ErrorEvent += OnFBError;
    /// 
    /// m_fbManager.Login();
    /// m_fbManager.Snapshot( texture, title, fileName );
    /// </summary>
    public class FBManager
    {
        public event Action<FBUser> LoginCompleteEvent = delegate
        {
        };
        public event Action<string> ErrorEvent = delegate
        {
        };
        
        public FBUser Me
        {
            get;
            private set;
        }

        private enum FBState
        {
            None,
            Init,
            ReadPermission,
            Me,
        }

        private static readonly bool IS_DEBUG = false;

		private readonly IList<string> READ_PERMISSIONS = new List<string>() { "public_profile", "user_friends" };
		private readonly IList<string> PUBLISH_PERMISSIONS = new List<string>() { "publish_actions" };

        private FBState m_state = FBState.None;

        public void Login()
        {
            if ( !FB.IsInitialized )
            {
                Init();
            }
            else
            {
                OnInit();
            }
        }

        private void Init()
        {
            m_state = FBState.Init;

            Me = new FBUser();

            FB.Init( OnInit, OnHideUnity );
        }

        private void OnInit()
        {
            m_state = FBState.ReadPermission;

            DebugLog( "OnInit: " + FB.IsLoggedIn );

			if ( FB.IsLoggedIn )
			{
				Me.AccessToken = AccessToken.CurrentAccessToken.TokenString;
				FB.API( "/me", HttpMethod.GET, HandleResult );
				m_state = FBState.Me;
			}
			else
			{
            	FB.LogInWithReadPermissions( READ_PERMISSIONS, HandleResult );
			}
        }

        private void OnHideUnity( bool isGameShown )
        {
            DebugLog( "OnHideUnity" );
        }

        private void HandleResult( IResult result )
        {
            if ( result == null )
            {
                return;
            }

            if ( !string.IsNullOrEmpty( result.Error ) )
            {
                DebugLog( "FBManager Error: " + result.Error );

                ErrorEvent( result.Error );
            }
            else if ( result.Cancelled )
            {
                DebugLog( "FBManager Cancelled" );

                ErrorEvent( string.Empty );
            }
            else if ( !string.IsNullOrEmpty( result.RawResult ) )
            {
                DebugLog( "FBManager Success: " + result.RawResult );

                OnSuccessResult( result.RawResult );
            }
            else
            {
                DebugLog( "FBManager Empty Result" );

                ErrorEvent( string.Empty );
            }
        }

        private void OnSuccessResult( string result )
        {
            var dict = Json.Deserialize( result ) as Dictionary<string, object>;

            switch ( m_state )
            {
            case FBState.ReadPermission:
                Me.AccessToken = dict[ "access_token" ] as string;

                FB.API( "/me", HttpMethod.GET, HandleResult );
                m_state = FBState.Me;
                break;

            case FBState.Me:
                Me.Id = dict[ "id" ] as string;
                Me.Name = dict[ "name" ] as string;

                LoginCompleteEvent( Me );
                break;
            }
        }

        public void Snapshot( Texture2D texture, string message, string pictureName )
        {
			byte[] screenshot = texture.EncodeToPNG();
            var wwwForm = new WWWForm();
            wwwForm.AddBinaryData( "image", screenshot, pictureName );
			wwwForm.AddField( "message", message );
            FB.API( "me/photos", HttpMethod.POST, OnSnapshot, wwwForm );
        }

        private void OnSnapshot( IGraphResult result )
        {
            DebugLog( "OnSnapshot: " + result );
        }

        private void DebugLog( string log )
        {
            if ( IS_DEBUG )
            {
                Debug.Log( "[FBManager]: " + log );
            }
        }
    }
}
