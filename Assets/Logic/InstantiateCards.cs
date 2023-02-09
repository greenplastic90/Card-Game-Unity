using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateCards : MonoBehaviour
{
    public GameObject cardPrefab;
    public int numberOfCards;
    private float offset = 1f;
    private List<GameObject> cards = new List<GameObject>();

    private GameObject cardRemovedFromHand;
    private bool adjustCardPositions = false;

    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < numberOfCards; i++)
        {
            Vector3 position = new Vector3((i - (numberOfCards - 1) / 2f) * offset, 0, i + 1);
            GameObject card = Instantiate(cardPrefab, transform.position + position, Quaternion.identity);
            cards.Add(card);
            card.GetComponent<CardScript>().index = i;
        }
    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < cards.Count; i++)
        {

            if (cards[i].GetComponent<MoveObject>().overCollider)
            {

                cardRemovedFromHand = cards[i];
                cards.Remove(cards[i]);
                adjustCardPositions = true;
            }
        }

        if (!(cardRemovedFromHand == default(GameObject)))
        {
            if (cardRemovedFromHand.GetComponent<CardScript>().transform.position == cardRemovedFromHand.GetComponent<MoveObject>().originalPosition)
            {
                cards.Insert(cardRemovedFromHand.GetComponent<CardScript>().index, cardRemovedFromHand);
                cardRemovedFromHand = default(GameObject);
                adjustCardPositions = true;
            }
        }



        if (adjustCardPositions)
        {
            Debug.Log("cards.Count => " + cards.Count);
            for (int i = 0; i < cards.Count; i++)
            {
                Vector3 position = new Vector3((i - (cards.Count - 1) / 2f) * offset, 0, i + 1);
                MoveObject card = cards[i].GetComponent<MoveObject>();
                StartCoroutine(card.MoveToDesiredPostion(transform.position + position));
            }
            adjustCardPositions = false;
        }


    }
}
