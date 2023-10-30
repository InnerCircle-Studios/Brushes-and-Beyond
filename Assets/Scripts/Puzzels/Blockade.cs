using UnityEngine;

public class Blockade : MonoBehaviour
{
    private bool _hasEnoughPaints = false;


    public void onMaxPaintsCollected()
    {
        _hasEnoughPaints = true;
    }
    public void onPaintsUsed()
    {
        _hasEnoughPaints = false;
    }

    public void Update(){
        if (_hasEnoughPaints){
            Destroy(gameObject);
        }
    }



}