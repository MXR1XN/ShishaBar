namespace Assets.Scripts
{
	internal interface IInteractible
	{
        bool IsUIInitiator { get; set; }
        void UpdateUI();
		void ShowUI();
	}
}
