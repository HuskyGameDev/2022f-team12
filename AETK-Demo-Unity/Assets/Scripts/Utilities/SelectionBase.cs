/*
 * This entire class file is a development utility in order to mark
 * invidual objects of a larger branching object (child/parent) as the
 * selection base object.
 * 
 * That is, if this script is assigned to a gameobject that is the child of another,
 * whenever a developer clicks on the parent in the scene view, the object with this
 * script attached will be selected.
 */
#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class SelectionBase : MonoBehaviour
{ }
#endif