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


    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < numberOfCards; i++)
        {
            Vector3 position = generateCardPosition(i, numberOfCards); ;
            GameObject card = Instantiate(cardPrefab, transform.position + position, Quaternion.identity);
            cards.Add(card);
            card.GetComponent<CardScript>().index = i;
        }
    }
    private Vector3 generateCardPosition(int i, int numberOfCards)
    {
        return new Vector3((i - (numberOfCards - 1) / 2f) * offset, 0, i + 1);
    }
    private void adjustCardPositions()
    {

        Debug.Log("cards.Count => " + cards.Count);
        for (int i = 0; i < cards.Count; i++)
        {
            Vector3 position = generateCardPosition(i, cards.Count);
            MoveObject card = cards[i].GetComponent<MoveObject>();
            StartCoroutine(card.MoveToDesiredPostion(transform.position + position));
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
                adjustCardPositions();
            }
        }

        if (!(cardRemovedFromHand == default(GameObject)))
        {
            if (cardRemovedFromHand.GetComponent<CardScript>().transform.position == cardRemovedFromHand.GetComponent<MoveObject>().originalPosition)
            {
                cards.Insert(cardRemovedFromHand.GetComponent<CardScript>().index, cardRemovedFromHand);
                cardRemovedFromHand = default(GameObject);
                adjustCardPositions();
            }
        }






    }
}
