using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class STMTest : MonoBehaviour
{
    public SuperTextMesh stm;

    public Vector3 TextScale = Vector3.one;

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
                    stm.text = "<drawAnim=AssText><j>ASSMAN, ASSMAN!!!<d=4>\nPLEASE COME QUICK!";
                    break;
                case 1:
                    stm.text = "<drawAnim=AssText>Ok, so:";
                    break;
                case 2:
                    stm.text = "<drawAnim=AssText><w=loop>There was this crazy guy chasing me down the street!";
                    break;
                case 3:
                    stm.text = "<drawAnim=AssText>He was <sub>lanky</sub>,<d=8> <w=seasick>strange</w>,<d=8> <sup>oblong,</sup><d=8> <w>obtuse...</w>";
                    break;
                case 4:
                    stm.text = "<drawAnim=AssText><w=loop>Roundtuse</w>,<d=6> quirky,<d=6> goofy...<d=15>\nand his face looked like this --><q=pog>";
                    break;
                case 5:
                    stm.text = "<drawAnim=mystical><w=mystical>One of the many things that I've thought about in my time include the right of passage.";
                    break;
                case 6:
                    stm.text = "<drawAnim=mystical><w=mystical>Ways in which a man may eventually fulfill his destiny.";
                    break;
                case 7:
                    stm.text = "<drawAnim=mystical><w=mystical>I've looked long and hard for a man like that. A man who has no fear, who takes fate into his own hands.";
                    break;
                case 8:
                    stm.text = "<drawAnim=mystical><w=mystical>It's only today where I realize that the man that I've been looking for, is<d=10>.<d=10>.<d=10>.";
                    break;
                case 9:
                    stm.text = "<drawAnim=AssText>My Dad.";
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

    private static void ScaleV3(ref Vector3 vert, Vector3 middle, Vector3 scale)
    {
        var oDist = vert - middle;

        vert = middle + Vector3.Scale(oDist, scale);
    }

    public void ChangeVertices(Vector3[] verts, Vector3[] middles, Vector3[] positions)
    {

        for (int i = 0; i < verts.Length / 4; i++)

        {

            var tl = verts[4 * i + 0];
            var tr = verts[4 * i + 1];
            var br = verts[4 * i + 2];
            var bl = verts[4 * i + 3];

            var realMiddle = Vector3.Lerp(tl, br, 0.5f);

            //change a letter's verts:

            ScaleV3(ref verts[4 * i + 0], realMiddle, TextScale);
            ScaleV3(ref verts[4 * i + 1], realMiddle, TextScale);
            ScaleV3(ref verts[4 * i + 2], realMiddle, TextScale);
            ScaleV3(ref verts[4 * i + 3], realMiddle, TextScale);

            /*verts[4 * i + 0] += Vector3.right;

            verts[4 * i + 1] += Vector3.right;

            verts[4 * i + 2] += Vector3.right;

            verts[4 * i + 3] += Vector3.right;*/

            //move all to the right
        }

    }
}
