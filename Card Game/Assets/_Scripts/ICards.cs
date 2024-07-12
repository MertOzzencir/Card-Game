
using Unity.VisualScripting;
using UnityEngine;

public interface ICards 
{
    public void Perform();
    public int ManaCost();
    public string CardName();
    public Transform TransformInformations();
   
}
