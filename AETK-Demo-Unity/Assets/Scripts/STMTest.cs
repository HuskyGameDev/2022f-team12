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
                    stm.text = "<drawAnim=Fill><c=green>I'm feeling a <s=0.5>little</s>...\n<drawAnim=Stamp><j=dayquil>sleepy</j>";
                    break;
                case 1:
                    stm.text = "<readDelay=0.05><drawAnim=Stamp><c=green>Once upon a time,\nIn the great great greens of <u>Herocity</u>";
                    break;
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
