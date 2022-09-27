using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class STMTest : MonoBehaviour
{
    public SuperTextMesh stm;

    int textNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        stm.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            switch (textNum)
            {
                case 0:
                    stm.text = "<f=8-bit><drawAnim=Stamp><j>ASSMAN, ASSMAN!!!<d=4>\nPLEASE COME QUICK!";
                    break;
                case 1:
                    stm.text = "<f=8-bit><drawAnim=Stamp>Ok, so:";
                    break;
                case 2:
                    stm.text = "<f=8-bit><drawAnim=Stamp><w=loop>There was this crazy guy chasing me down the street!";
                    break;
                case 3:
                    stm.text = "<f=8-bit><drawAnim=Stamp>He was <sub>lanky</sub>,<d=8> <w=seasick>strange</w>,<d=8> <sup>oblong,</sup><d=8> <w>obtuse...</w>";
                    break;
                case 4:
                    stm.text = "<f=8-bit><drawAnim=Stamp><w=loop>Roundtuse</w>,<d=6> quirky,<d=6> goofy...<d=15>\nand his face looked like this --><q=pog>";
                    break;
                /*case 0:
                    stm.text = "<f=8-bit><drawAnim=Fill><c=green>I'm feeling a <s=0.5>little</s>...\n<drawAnim=Stamp><j=dayquil>sleepy</j>";
                    break;
                case 1:
                    stm.text = "<f=8-bit><drawAnim=Stamp><c=green>Once upon a time,\nIn the great great greens of Herocity";
                    break;
                case 2:
                    stm.text = "<f=8-bit><drawAnim=Stamp><c=green>I'm feeling <s=0.5>(a little)</s> <j=dayquil>sleepy</j> look --> <q=clap>";
                    break;*/
                default:
                    stm.text = "from da top";
                    textNum = -1;
                    break;
            }
            stm.Read();
            textNum++;
        }
    }
}
