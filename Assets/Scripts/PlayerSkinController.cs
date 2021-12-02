using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkinController : MonoBehaviour
{
    public List<AnimatorOverrideController> animatiorOverrideControllers = new List<AnimatorOverrideController>();

    public void ChangeHeroSkin(int heroNum = 1)
    {
        GetComponent<Animator>().runtimeAnimatorController = animatiorOverrideControllers[heroNum - 1] as RuntimeAnimatorController;
    }
}
