using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SimplePieGraph : MonoBehaviour
{
    public Image image_circel;
    public void set_valu(float valu , float max_valu) { image_circel.fillAmount = valu / max_valu; }
}
