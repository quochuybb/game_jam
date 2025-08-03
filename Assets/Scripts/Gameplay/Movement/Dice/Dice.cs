using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour {

    [SerializeField] private Sprite[] diceSides;

    private SpriteRenderer rend;

    private void Start () {

        rend = GetComponent<SpriteRenderer>();
    }

    public IEnumerator RollTheDice(System.Action<int> onRollFinished)
    {
        int randomDiceSide = 0;
        int finalSide = 0;
        for (int i = 0; i <= 15; i++)
        {
            randomDiceSide = Random.Range(0, 6);

            rend.sprite = diceSides[randomDiceSide];

            yield return new WaitForSeconds(0.1f);
        }

        finalSide = randomDiceSide + 1;
        onRollFinished?.Invoke(finalSide); 
    }
}
