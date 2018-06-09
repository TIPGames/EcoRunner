using UnityEngine;
using UnityEngine.UI;

namespace com.tip.games.ecorunner.ui
{
	public class UiCommonDialogBox : MonoBehaviour 
	{
		public delegate void DialogCancelledDelegate();
		public delegate void DialogAcceptedDelegate();

		[SerializeField]
		private Button _closeButton;
		[SerializeField]
		private Button _okButton;

		public virtual void Show(DialogCancelledDelegate onCancelled, 
			DialogAcceptedDelegate onAccepted)
		{
			if(_closeButton != null)
			{
				_closeButton.onClick.RemoveAllListeners();
				_closeButton.onClick.AddListener( 
					() => 
					{
						if(onCancelled != null)
							onCancelled();
						Close();
					} );
			}
			if(_okButton != null)
			{
				_okButton.onClick.RemoveAllListeners();
				_okButton.onClick.AddListener( 
					() => 
					{
						if(onAccepted != null)
							onAccepted();
						Close();
					} );
			}
			gameObject.SetActive(true);
		}

		protected virtual void Close()
		{
			gameObject.SetActive(false);
		}
	}
}