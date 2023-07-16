using System.Collections;
using System.Collections.Generic;
using AlephVault.Unity.UIGames.Authoring.Behaviours;
using UnityEngine;

namespace AlephVault.Unity.UIGames
{
    namespace Samples
    {
        [RequireComponent(typeof(Card))]
        public class SampleMiniCardSwapper : MonoBehaviour
        {
            private Card card;

            private void Awake()
            {
                card = GetComponent<Card>();
            }
            
            // Update is called once per frame
            void Update()
            {
                int bgs = card.Deck.Backgrounds.Count;
                int cards = card.Deck.Cards.Count;

                if (Input.GetKeyDown(KeyCode.T))
                {
                    card.Face = (card.Face - 1 + cards) % cards;
                }
                else if (Input.GetKeyDown(KeyCode.Y))
                {
                    card.Face = (card.Face + 1) % cards;
                }
                else if (Input.GetKeyDown(KeyCode.G))
                {
                    card.Background = (card.Background - 1 + cards) % bgs;
                }
                else if (Input.GetKeyDown(KeyCode.H))
                {
                    card.Background = (card.Background + 1) % bgs;
                }
                else if (Input.GetKeyDown(KeyCode.B))
                {
                    card.FaceUp(true);
                }
                else if (Input.GetKeyDown(KeyCode.N))
                {
                    card.FaceDown(true);
                }
            }
        }
    }
}
