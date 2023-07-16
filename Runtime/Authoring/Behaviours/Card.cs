using System.Threading.Tasks;
using AlephVault.Unity.UIGames.Authoring.ScriptableObjects;
using AlephVault.Unity.Support.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace AlephVault.Unity.UIGames
{
    namespace Authoring
    {
        namespace Behaviours
        {
            /// <summary>
            ///   Each card is a UI image but linked to a repository. There
            ///   are twp types of card layouts: Static layouts, where the
            ///   layouts are determined by the rules, and dynamic layouts,
            ///   where the layouts are totally determined by the user, or
            ///   to some extent. In the first layout, the cards are already
            ///   pre-allocated. In the second layout, cards can be added
            ///   dynamically.
            /// </summary>
            [RequireComponent(typeof(Image))]
            public class Card : MonoBehaviour
            {
                // The underlying image.
                private Image image;

                /// <summary>
                ///   The deck this card belongs to. Either as a prefab or
                ///   inserted in a scene, this value must be set a priori.
                /// </summary>
                [SerializeField]
                private Deck deck;

                /// <summary>
                ///   See <see cref="deck" />.
                /// </summary>
                public Deck Deck => deck;

                /// <summary>
                ///   The background. It must be a non-negative index in the
                ///   deck.
                /// </summary>
                [SerializeField]
                private int background;

                /// <summary>
                ///   See <see cref="background" />.
                /// </summary>
                public int Background
                {
                    get => background;
                    set
                    {
                        int old = background;
                        background = Values.Clamp(0, value, Deck.Backgrounds.Count - 1);
                        if (old != background && !FacingUp)
                        {
                            image.sprite = deck.Backgrounds.Count > 0 ? deck.Backgrounds[background] : null;
                        }
                    }
                }

                /// <summary>
                ///   The face. It must be a non-negative index in the deck.
                /// </summary>
                [SerializeField]
                private int face;

                /// <summary>
                ///   See <see cref="face" />.
                /// </summary>
                public int Face
                {
                    get => face;
                    set
                    {
                        int old = face;
                        face = Values.Clamp(0, value, Deck.Cards.Count - 1);
                        if (old != face && FacingUp)
                        {
                            image.sprite = deck.Cards.Count > 0 ? deck.Cards[face] : null;
                        }
                    }
                }

                /// <summary>
                ///   The flip status (<c>true</c> means the card looks up).
                /// </summary>
                public bool FacingUp { get; private set; }

                /// <summary>
                ///   This is the animation time of a semi-flip.
                /// </summary>
                public float SemiFlipTime = 0.25f;

                // This is the current flip action. Tracked for when a new
                // action is triggered.
                private int currentFlipAction = 0;

                private void Awake()
                {
                    image = GetComponent<Image>();
                    image.sprite = deck.Backgrounds.Count > 0 ? deck.Backgrounds[0] : null;
                }

                /// <summary>
                ///   Flips the card up, if it was flipped down.
                /// </summary>
                /// <param name="animated">Whether to make it instant or use an animation</param>
                public async void FaceUp(bool animated = false)
                {
                    if (FacingUp) return;
                    FacingUp = true;
                    int action = currentFlipAction++;
                    if (currentFlipAction == 65536) currentFlipAction = 0;
                    float semiFlipTime = SemiFlipTime;
                    if (!animated || semiFlipTime <= 0)
                    {
                        image.sprite = deck.Cards.Count > 0 ? deck.Cards[face] : null;
                    }
                    else
                    {
                        await FlipStart(action, semiFlipTime);
                        image.sprite = deck.Cards.Count > 0 ? deck.Cards[face] : null;
                        await FlipEnd(action, semiFlipTime);
                    }

                }

                /// <summary>
                ///   Flips the card down, if it was flipped up.
                /// </summary>
                /// <param name="animated">Whether to make it instant or use an animation</param>
                public async void FaceDown(bool animated = false)
                {
                    if (!FacingUp) return;
                    FacingUp = false;
                    int action = currentFlipAction++;
                    if (currentFlipAction == 65536) currentFlipAction = 0;
                    float semiFlipTime = SemiFlipTime;
                    if (!animated || semiFlipTime <= 0)
                    {
                        image.sprite = deck.Backgrounds.Count > 0 ? deck.Backgrounds[background] : null;
                    }
                    else
                    {
                        await FlipStart(action, semiFlipTime);
                        image.sprite = deck.Backgrounds.Count > 0 ? deck.Backgrounds[background] : null;
                        await FlipEnd(action, semiFlipTime);
                    }
                }

                private async Task FlipStart(int index, float semiFlipTime)
                {
                    float time = 0;
                    Debug.Log($"Index is: {index}, and next is: {(currentFlipAction + 65535) % 65536}");
                    while (time < semiFlipTime && index == (currentFlipAction + 65535) % 65536)
                    {
                        Debug.Log("Step Start");
                        time = Values.Min(semiFlipTime, time + Time.deltaTime);
                        transform.localScale = new Vector3(
                            1 - time / semiFlipTime, 1, 1
                        );
                        await Task.Yield();
                    }
                    transform.localScale = new Vector3(0, 1, 1);
                }

                private async Task FlipEnd(int index, float semiFlipTime)
                {
                    float time = 0;
                    Debug.Log($"Index is: {index}, and next is: {(currentFlipAction + 65535) % 65536}");
                    while (time < semiFlipTime && index == (currentFlipAction + 65535) % 65536)
                    {
                        Debug.Log("Step End");
                        time = Values.Min(semiFlipTime, time + Time.deltaTime);
                        transform.localScale = new Vector3(
                            time / semiFlipTime, 1, 1
                        );
                        await Task.Yield();
                    }
                    transform.localScale = new Vector3(1, 1, 1);
                    if (currentFlipAction == 65536) currentFlipAction = 0;
                }
            }
        }
    }
}
