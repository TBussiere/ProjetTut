using UnityEngine;
using System.Collections;

public class TurnManager : MonoBehaviour {


    public playerControler[] players;
    public static bool Passer;
    // Use this for initialization
    void Start () {
        
    }
    
    void Update()
    {
		
        if (pop_player.playerCount>0)
        {
            players = FindObjectsOfType<playerControler>();
        }
        if (Passer == true)
        {
            validerTour();
            Passer = false;
        }
    }
    public void validerTour()
    {
        foreach (playerControler player in players)
        {
            player.curPM = player.maxPM;
        }
    }



	public static void otherValiderTour(){
		var others=FindObjectsOfType<OtherPlayerControler>();
		if (others.Length >= 2) {
			OtherPlayerControler other1 = others [0];
			OtherPlayerControler other2 = others [1];
			if (other1.validerTour && other2.validerTour) {
				float diffPV1 = other1.vie;
				float diffPV2 = other2.vie;
				other1.frapper ();
				diffPV2 -= other2.vie;
				other2.frapper ();
				diffPV1 -= other1.vie;
				other1.setReward ((diffPV2-diffPV1)/10f);
				other1.setReward ((diffPV1-diffPV2)/10f);
				if (other1.vie <= 0) {
					other1.setReward (-1);
					other2.setReward (1);
					other1.AgentReset ();
					other2.AgentReset ();
					return;
				}
				if (other2.vie <= 0) {
					other2.setReward (-1);
					other1.setReward (1);
					other1.AgentReset ();
					other2.AgentReset ();
					return;
				}
				if (other1.compteurTour >= 20) {
					other2.setReward (-1);
					other1.setReward (-1);
					other1.AgentReset ();
					other2.AgentReset ();
				}
				other1.deplacer ();
				other2.deplacer ();
				other1.finTour ();
				other2.finTour ();
			}
		}
	}
}
