using UnityEngine;

public class GunAnimationReload : MonoBehaviour
{
    public StarterAssets.StarterAssetsInputs playerInputs;

    public void FinishReload()          // called by Animation Event
    {
        playerInputs.FinishReload();
    }
}
