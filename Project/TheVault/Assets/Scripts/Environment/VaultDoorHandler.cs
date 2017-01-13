using UnityEngine;

public class VaultDoorHandler : BaseMonoBehaviour
{
    private Animator vaultDoorAnim;

    private void OnEnable()
    {
        EventManager.StartListening("OpenVault", OpenVaultDoor); //Trigger from LM
        EventManager.StartListening("EndLevel", CloseVaultDoor);
        EventManager.StartListening("ResetLevel", ResetVaultDoor);
    }

    private void OnDisable()
    {
        EventManager.StopListening("OpenVault", OpenVaultDoor);
        EventManager.StopListening("EndLevel", CloseVaultDoor);
        EventManager.StopListening("ResetLevel", ResetVaultDoor);
    }

    private void Start()
    {
        Initialise();
    }

    private void Initialise()
    {
        vaultDoorAnim = this.GetComponent<Animator>();
    }

    private void OpenVaultDoor()
    {
        ExtensionMethods.TriggerAnimation(vaultDoorAnim, "Open");
    }

    private void CloseVaultDoor()
    {
        ExtensionMethods.TriggerAnimation(vaultDoorAnim, "Close");
    }

    private void ResetVaultDoor()
    {
        ExtensionMethods.ResetTriggerAnimations(vaultDoorAnim);
    }
}
