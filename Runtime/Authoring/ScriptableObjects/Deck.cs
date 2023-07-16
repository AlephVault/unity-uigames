using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlephVault.Unity.UIGames
{
    namespace Authoring
    {
        namespace ScriptableObjects
        {
            /// <summary>
            ///   <para>
            ///     A deck is a repository of images for cards: their faces
            ///     and their backgrounds. It does NOT represent an actual
            ///     game deck (which is just a server-side or otherwise well
            ///     protected array of indices/values).
            ///   </para>
            ///   <para>
            ///     It is generally meant as a singleton, either statically
            ///     or dynamically created.
            ///   </para>
            /// </summary>
            [CreateAssetMenu(fileName = "NewDeck", menuName = "UI Games/Cards/New Deck", order = 110)]
            public class Deck : ScriptableObject
            {
                private void OnEnable()
                {
                    Cards ??= new List<Sprite>();
                    Backgrounds ??= new List<Sprite>();
                }

                /// <summary>
                ///   <para>
                ///     The cards are images of the same size and pivot.
                ///     These images are sprites whose textures were
                ///     imported as 2D/UI. The number of cards to import
                ///     here is arbitrary, but typically the setup is to
                ///     have 53 cards here (for a french deck) and 49
                ///     cards here (for a spanish deck). Other decks may
                ///     have other cards, and cards are meant as unique,
                ///     since this is not a logical deck but a repository
                ///     of the distinct images. 
                ///   </para>
                ///   <para>
                ///     For most card types and games, they don't need
                ///     any modification at all, but there might be games
                ///     where the deck must be constructed dynamically.
                ///   </para>
                /// </summary>
                public List<Sprite> Cards;

                /// <summary>
                ///   <para>
                ///     These are the card backgrounds. Typically, only
                ///     one is needed, but the user is allowed to change
                ///     it real-time for each card (or visually for the
                ///     whole cards in the table). This is just aesthetics
                ///     and should seldom to never contribute to the game's
                ///     overall logic.
                ///   </para>
                /// </summary>
                public List<Sprite> Backgrounds;
            }
        }
    }
}