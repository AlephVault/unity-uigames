using System;
using System.Collections;
using System.Collections.Generic;
using AlephVault.Unity.UIGames.Authoring.Behaviours;
using UnityEngine;

namespace AlephVault.Unity.UIGames
{
    namespace Samples
    {
        [RequireComponent(typeof(Card))]
        public class SampleBigCardSwapper : MonoBehaviour
        {
            private Card card;

            private void Awake()
            {
                card = GetComponent<Card>();
            }
            
            void Update()
            {
                int bgs = card.Deck.Backgrounds.Count;
                int cards = card.Deck.Cards.Count;

                if (Input.GetKeyDown(KeyCode.Q))
                {
                    card.Face = (card.Face - 1 + cards) % cards;
                }
                else if (Input.GetKeyDown(KeyCode.W))
                {
                    card.Face = (card.Face + 1) % cards;
                }
                else if (Input.GetKeyDown(KeyCode.A))
                {
                    card.Background = (card.Background - 1 + cards) % bgs;
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    card.Background = (card.Background + 1) % bgs;
                }
                else if (Input.GetKeyDown(KeyCode.Z))
                {
                    card.FaceUp(true);
                }
                else if (Input.GetKeyDown(KeyCode.X))
                {
                    card.FaceDown(true);
                }
            }
        }
    }
}
