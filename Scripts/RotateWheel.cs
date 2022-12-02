using UnityEngine ;
using EasyUI.PickerWheelUI ;
using UnityEngine.UI ;

public class RotateWheel : MonoBehaviour {
   [SerializeField] private Button uiSpinButton ;
    //[SerializeField] private Text uiSpinButtonText ;
    public static bool adWatched;
   [SerializeField] private PickerWheel pickerWheel ;


   private void Start () {
      uiSpinButton.onClick.AddListener (() => {

         uiSpinButton.interactable = false ;
         //uiSpinButtonText.text = "Spinning" ;

         pickerWheel.OnSpinEnd (wheelPiece => {
            
            addSpecialItems(wheelPiece.Amount, wheelPiece.Label);
            uiSpinButton.interactable = true ;
            //uiSpinButtonText.text = "Spin" ;
         }) ;
         if (GoogleAdsScript._instance.rewardedAd.IsLoaded())
         {
              GoogleAdsScript._instance.rewardedAd.Show();
              adWatched = true;
         }
         GoogleAdsScript._instance.RequestRewarded();
         if(adWatched)
         {
              pickerWheel.Spin();
              adWatched = false;
          }
         else
         {
              uiSpinButton.interactable = true;
         }
         

      }) ;

   }
    public void addSpecialItems(int amount, string label)
    {
        if (PlayerPrefs.HasKey(label))
        {
            PlayerPrefs.SetInt(label, PlayerPrefs.GetInt(label) + amount);
        }
        else
        {
            PlayerPrefs.SetInt(label, amount);
        }
        UIManager._instance.SetLimitedValues();
    }

}
