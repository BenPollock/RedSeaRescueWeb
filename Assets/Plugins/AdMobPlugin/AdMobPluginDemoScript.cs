using System.Collections;
using UnityEngine;

// Example script showing how you can easily call into the AdMobPlugin.
public class AdMobPluginDemoScript : MonoBehaviour {

    void Start()
	{
        // Pass in any extras you have as JSON.
        string extras = "{\"color_bg\":\"AAAAFF\", \"color_bg_top\":\"FFFFFF\"}";
		AdMobPlugin.CreateBannerView("ca-app-pub-6094620705506822/5066994799", AdMobPlugin.AdSize.Banner, false);
        print("Created Banner View");
        AdMobPlugin.RequestBannerAd(true, extras);
        print("Requested Banner Ad");
		//Hide until further notice
		AdMobPlugin.HideBannerView ();
    }

    void OnEnable()
	{
        print("Registering for AdMob Events");
        AdMobPlugin.ReceivedAd += HandleReceivedAd;
        AdMobPlugin.FailedToReceiveAd += HandleFailedToReceiveAd;
        AdMobPlugin.ShowingOverlay += HandleShowingOverlay;
        AdMobPlugin.DismissingOverlay += HandleDismissingOverlay;
        AdMobPlugin.DismissedOverlay += HandleDismissedOverlay;
        AdMobPlugin.LeavingApplication += HandleLeavingApplication;
    }

    void OnDisable()
	{
        print("Unregistering for AdMob Events");
        AdMobPlugin.ReceivedAd -= HandleReceivedAd;
        AdMobPlugin.FailedToReceiveAd -= HandleFailedToReceiveAd;
        AdMobPlugin.ShowingOverlay -= HandleShowingOverlay;
        AdMobPlugin.DismissingOverlay -= HandleDismissingOverlay;
        AdMobPlugin.DismissedOverlay -= HandleDismissedOverlay;
        AdMobPlugin.LeavingApplication -= HandleLeavingApplication;
    }

    public void HandleReceivedAd()
	{
        print("HandleReceivedAd event received");
    }

    public void HandleFailedToReceiveAd(string message)
	{
        print("HandleFailedToReceiveAd event received with message:");
        print(message);
    }

    public void HandleShowingOverlay()
	{
        print("HandleShowingOverlay event received");
    }

    public void HandleDismissingOverlay()
	{
        print("HandleDismissingOverlay event received");
    }

    public void HandleDismissedOverlay()
	{
        print("HandleDismissedOverlay event received");
    }

    public void HandleLeavingApplication()
	{
        print("HandleLeavingApplication event received");
    }
}
