using UnityEngine;

public class VaultDoorHandler : BaseMonoBehaviour
{
    private Animator vaultDoorAnim;

    private void OnEnable()
    {
        EventManager.StartListening("OpenVault", OpenVaultDoor); //Trigger from LM
    }

    private void OnDisable()
    {
        EventManager.StopListening("OpenVault", OpenVaultDoor);
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
}
