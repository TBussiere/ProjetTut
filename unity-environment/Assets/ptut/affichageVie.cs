using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class affichageVie : MonoBehaviour {

    public playerControler player;
    public GameObject zoneCoeur;
    public Text NbPA;
    public Text[] Cost;

    public Image coeurVide;
    public Image coeurPlein;

    public List<Image> coeursPleins;
    public Image[] coeursPleinsTAB;
    public int placeCoeur=1;
    private int v;
    public bool noPlayer = true;

    // Use this for initialization
    void Start () {
        coeurPlein.rectTransform.localScale = new Vector3(0.25f, 0.25f, 1);
        coeurVide.rectTransform.localScale = new Vector3(0.25f, 0.25f, 1);
    }
	
	// Update is called once per frame
	void Update () {
        if (noPlayer)
        {
            player = FindObjectOfType<playerControler>();
        }
        if (player != null && noPlayer)
        {
            noPlayer = false;
            initVie();
        }
        if (!noPlayer)
        {
            if (player.vie == 0)
            {
                noPlayer = true;
            }
            if (v != player.vie)
            {
                damageAffichageUpdate(v - player.vie);
            }

            NbPA.text = "" + player.curPM;

            Cost[0].text = "" + 1;
			if (player.CurWeapon != null) {
				Cost [1].text = "" + player.CurWeapon.cost;
				Cost [2].text = "" + player.CurSpell.cost;
			}
		}   
	}

    void initVie()
    {
        if (coeursPleins.Count == 0)
        {

            v = player.vie;
            for (int i = 0; i < v; i++)
            {
                var temp = Instantiate(coeurVide);
                temp.transform.SetParent(zoneCoeur.transform);
                //RectTransform tempRect = temp.GetComponent<RectTransform>();
                //temp.rectTransform.offsetMax = Vector2.zero;
                //tempRect.offsetMax = new Vector2((70 + placeCoeur * 25) , 45);
                temp.rectTransform.anchoredPosition = new Vector2((placeCoeur * 25), 0);

                var temp2 = Instantiate(coeurPlein);
                temp2.transform.SetParent(zoneCoeur.transform);
                //RectTransform tempRect2 = temp.GetComponent<RectTransform>();
                //temp2.rectTransform.offsetMax = Vector2.zero;
                //tempRect.offsetMax = new Vector2((70 + placeCoeur * 25), 45);
                temp2.rectTransform.anchoredPosition = new Vector2((placeCoeur * 25), 0);
                temp2.name = "" + i;
                coeursPleins.Add(temp2);

                placeCoeur++;
            }
            coeursPleinsTAB = coeursPleins.ToArray();
        }
        else
        {
            damageAffichageReset();
        }
    }

    public void damageAffichageUpdate(int dmg)
    {
        for (int i = v; i > player.vie; i--)
        {
            coeursPleinsTAB[i-1].enabled = false;
        }
    }

    public void damageAffichageReset()
    {
        for (int i = 0; i < player.vie; i++)
        {
            coeursPleinsTAB[i].enabled = true;
        }
    }
}
