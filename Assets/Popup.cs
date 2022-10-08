using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Popup : MonoBehaviour
{
    public GameObject popUpPrefab;
    public Vector3 offset;
    //public Vector3 scale;
    public int fontSize = 200;
    public float animSpeed = 1;

    public bool overrideColor = false;
    public Color color;
    public Color healingColor;

    public void SpawnPopUp(float damage)
    {
        PopUpText(damage.ToString(), Color.white);
    }
    public void PopUpText(string value, Color col)
    {
        if (popUpPrefab != null)
        {
            
            GameObject go = Instantiate(popUpPrefab, offset + transform.position + new Vector3(Random.Range(-0.25f, 0.25f), Random.Range(0.4f, 0.6f), 0), Quaternion.Euler(0, 0, Random.Range(-7.5f, 7.5f)));
            TextMesh textObject = go.GetComponentInChildren<TextMesh>();
            textObject.text = value;
            textObject.color = col;
            textObject.fontSize = fontSize;
            if (overrideColor)
            {
                float damage = float.Parse(value);
                if (damage >= 0)
                {
                    textObject.color = color;
                }
                else
                {
                    textObject.text = Mathf.Abs(damage).ToString();
                    textObject.color = healingColor;
                }
                
            }


            Animator anim = go.GetComponentInChildren<Animator>();
            anim.speed = animSpeed;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position + offset, 1);
    }
}
