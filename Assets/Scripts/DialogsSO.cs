using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Dialogs")]
public class DialogsSO : ScriptableObject
{
    [TextArea]
    public List<string> PositiveDialogs;

    [TextArea]
    public List<string> NegativeDialogs;
}
