using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public string cellColorName;
    private Animator cellAnimator;
    public GameObject spawnerGameObject;
    public int cellIndex;
    [Header("Cell Texture")]
    public Texture textureBlue, textureGreen, textureRed, textureYellow, textureGrey;
    // Start is called before the first frame update
    void Start()
    {
        cellAnimator = GetComponent<Animator>();
        Renderer renderer = GetComponent<Renderer>();
        Material[] materials = renderer.materials;
        materials[1].color = Color.white;
        switch (cellColorName)
        {
            case "Blue":
                materials[0].SetTexture("_MainTex", textureBlue);
                break;
            case "Green":
                materials[0].SetTexture("_MainTex", textureGreen);
                break;
            case "Red":
                materials[0].SetTexture("_MainTex", textureRed);
                break;
            case "Yellow":
                materials[0].SetTexture("_MainTex", textureYellow);
                break;
            default:
                materials[0].SetTexture("_MainTex", textureGrey);
                materials[1].SetTexture("_MainTex", textureGrey);
                break;
        }
        renderer.materials = materials;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DestroyCell(string objname = "")
    {
        StartCoroutine(DestroyAnimIE(objname));
    }
    IEnumerator DestroyAnimIE(string objname = "")
    {
        if (objname == "Frog")
            spawnerGameObject.GetComponent<Spawner>().SubtractFrog();
        if (cellIndex == 1)
        {
            Renderer renderer = GetComponent<Renderer>();
            Material[] materials = renderer.materials;
            materials[1].color = Color.white;
            materials[0].SetTexture("_MainTex", textureGrey);
            materials[1].SetTexture("_MainTex", textureGrey);
            renderer.materials = materials;
            
        }
        else
        {
            cellAnimator.SetBool("destroyBool", true);
            yield return new WaitForSeconds(0.5f);
            spawnerGameObject.GetComponent<Spawner>().SpawnIndex--;
            spawnerGameObject.GetComponent<Spawner>().SpawnStarter();
            Destroy(this.gameObject);
        }

    }

}
