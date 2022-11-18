using UnityEngine ;
using EasyUI.PickerWheelUI ;
using UnityEngine.UI ;

public class RotateWheel : MonoBehaviour {
   [SerializeField] private Button uiSpinButton ;
   //[SerializeField] private Text uiSpinButtonText ;

   [SerializeField] private PickerWheel pickerWheel ;


   private void Start () {
      uiSpinButton.onClick.AddListener (() => {

         uiSpinButton.interactable = false ;
         //uiSpinButtonText.text = "Spinning" ;

         pickerWheel.OnSpinEnd (wheelPiece => {
            Debug.Log (
               @" <b>Index:</b> " + wheelPiece.Index + "           <b>Label:</b> " + wheelPiece.Label
               + "\n <b>Amount:</b> " + wheelPiece.Amount + "      <b>Chance:</b> " + wheelPiece.Chance + "%"
            ) ;
             addSpecialItems(wheelPiece.Amount, wheelPiece.Label);
            uiSpinButton.interactable = true ;
            //uiSpinButtonText.text = "Spin" ;
         }) ;

         pickerWheel.Spin () ;

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
