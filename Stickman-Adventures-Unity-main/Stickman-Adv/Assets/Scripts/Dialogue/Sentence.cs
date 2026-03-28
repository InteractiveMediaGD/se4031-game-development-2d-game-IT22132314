using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sentence
{
    [TextArea(1,2)]
    public string SpeakerName;
    [TextArea(1,5)]
    public string phrase;

}
