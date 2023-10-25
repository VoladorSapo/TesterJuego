using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NarrativeParts
{
    public int PlatformNarrative;
    public int CarNarrative;
    public int ZeldaNarrative;

    public NarrativeParts(){
        PlatformNarrative=0; CarNarrative=0; ZeldaNarrative=0;
    }

    public NarrativeParts(int p, int c, int z){
        PlatformNarrative=p; CarNarrative=c; ZeldaNarrative=z;
    }

    public void CopyNarrative(out int p, out int c, out int z){
        p=PlatformNarrative;
        c=CarNarrative;
        z=ZeldaNarrative;
    }

    public NarrativeParts Clone(){
        return new NarrativeParts(this.PlatformNarrative, this.CarNarrative, this.ZeldaNarrative);
    }
}
