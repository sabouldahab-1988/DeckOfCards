using DeckOfCards.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DeckOfCards.Services
{
    //Base Card game class is avalaible for any card game that uses the same techniques
    public abstract class BaseCardGame
    {
        #region Properties 
        protected Dictionary<int, Card> defaultCards { get; set; } // All cards in order which we will use as a reference to build shuffled play cards
        protected Dictionary<int, Card> playCards { get; set; } //All play cards with key which would allow us to shuffle the cards later on
        protected List<Card> cards { get; set; } //All play cards without any keys
        protected List<int> drawnCardKeys { get; set; } //Stores the drawn card keys in order to remove it from the play cards later on when the user draw a card 
        #endregion

        #region Constructor
        public BaseCardGame()
        {
            inializeCards();
        }
        #endregion

        #region Card Game Methods shuffleCards, Deal One Card, Remove Drawn Cards From Play Cards, Print All Cards, get Card String
        /// <summary>
        /// Shuffle the Cards
        /// </summary>
        /// <returns></returns>
        public string shuffleCards()
        {
            try
            {
                Random rand = new Random();
                Dictionary<int, Card> shuffleCardList = new Dictionary<int, Card>();

                for (int i = 1; i <= 52; i++)
                {
                    int randomNumber = rand.Next(1, 52);
                    while (shuffleCardList.ContainsKey(randomNumber))
                    {
                        randomNumber = rand.Next(1, 53);
                    }

                    if (!shuffleCardList.ContainsKey(randomNumber))
                    {
                        shuffleCardList.Add(randomNumber, defaultCards[randomNumber]);
                    }
                }
                playCards = shuffleCardList;
                removeDrawnCardsFromPlayCardsList();

                return "Cards are now Shuffled";
            }
            catch(Exception ex)
            {
                //TODO: Implement Log4Net Here to log application errors
                return "Error occured while shuffling cards";
            }
        }

        /// <summary>
        /// Deal one care from the play cards list
        /// </summary>
        /// <returns></returns>
        public string dealOneCard()
        {
            try
            {
                if (playCards.Count() > 0)
                {
                    Card c = playCards.FirstOrDefault().Value;
                    int key = playCards.FirstOrDefault().Key;
                    playCards.Remove(key);
                    drawnCardKeys.Add(key);
                    return getCardString(c);
                }
                return "No card is dealt";
            }catch(Exception ex)
            {
                //TODO: Implement Log4Net Here to log application errors
                return "Error occured while attempting to deal a card";
            }
        }
       

        /// <summary>
        /// Remove drawn cards from the play cards list
        /// </summary>
        protected void removeDrawnCardsFromPlayCardsList()
        {
            try
            {
                foreach (int key in drawnCardKeys)
                {
                    if (playCards.ContainsKey(key))
                    {
                        playCards.Remove(key);
                    }
                }
            }
            catch(Exception ex)
            {
                //TODO: Implement Log4Net Here to log application errors
            }
        }

        /// <summary>
        /// Print all play cards
        /// </summary>
        /// <returns></returns>
        public string printAllPlayCards()
        {
            try
            {
                string allCards = "";
                foreach (KeyValuePair<int, Card> playCard in playCards)
                {
                    allCards += getCardString(playCard.Value) + "\n";
                }

                return allCards;
            }
            catch
            {
                //TODO: Implement Log4Net Here to log application errors
                return "Error occured while printing play cards";
            }
        }

        /// <summary>
        /// Get card string for an example: Ace Clubs
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public string getCardString(Card card)
        {
            try
            {
                switch (card.faceType)
                {
                    case FaceType.Ace:
                        return card.faceType + " " + card.suitsType;
                    case FaceType.Number:
                        return card.number.ToString() + " " + card.suitsType;
                    default:
                        return card.faceType + " " + card.suitsType;
                }
            }
            catch(Exception ex)
            {
                //TODO: Implement Log4Net Here to log application errors
                return "error occured while getting card name";
            }
        }

        /// <summary>
        /// Rest All cards
        /// </summary>
        public void restGame()
        {
            inializeCards();
        }
        #endregion

        #region Card Creation Methods

        protected void inializeCards()
        {
            cards = new List<Card>();
            defaultCards = new Dictionary<int, Card>();
            drawnCardKeys = new List<int>();
            createStandardCards();
            putCardsInPlayList();
        }
        /// <summary>
        /// Put cards in play card list
        /// </summary>
        protected void putCardsInPlayList()
        {
            try
            {
                int id = 1;
                for (int i = 0; i <= 51; i++)
                {
                    defaultCards.Add(id, cards[i]);
                    id++;
                }
                playCards = new Dictionary<int, Card>(defaultCards);
            }catch(Exception ex)
            {
                //TODO: Implement Log4Net Here to log application errors
            }
        }


        /// <summary>
        /// Create standard 52 Cards
        /// </summary>
        protected void createStandardCards()
        {
            try
            {
                for (int i = 1; i <= 4; i++)
                {

                    for (int j = 1; j <= 10; j++)
                    {
                        switch (i)
                        {
                            case 1:
                                addNewCardToList(j, FaceType.Number, SuitType.Clubs);
                                break;
                            case 2:
                                addNewCardToList(j, FaceType.Number, SuitType.Spades);
                                break;
                            case 3:
                                addNewCardToList(j, FaceType.Number, SuitType.Hearts);
                                break;
                            default:
                                addNewCardToList(j, FaceType.Number, SuitType.Diamonds);
                                break;
                        }
                    }
                }
            }catch(Exception ex)
            {
                //TODO: Implement Log4Net Here to log application errors
            }
        }

        /// <summary>
        /// Add new standard card to cards list
        /// </summary>
        /// <param name="number">The card number</param>
        /// <param name="faceType">Face Type (Ace, Number, Jack, Queen, King)</param>
        /// <param name="suitType">Suite Type(Hearts, Spades, Clubs,Diamonds)</param>
        protected void addNewCardToList(int number, FaceType faceType, SuitType suitType)
        {
            try
            {
                Card card = new Card { number = number, faceType = faceType, suitsType = suitType };
                if (number == 1)
                    card.faceType = FaceType.Ace;
                cards.Add(card);
                if (number == 10)
                    addJackQueenAndKing(suitType);
            }catch(Exception ex)
            {
                //TODO: Implement Log4Net Here to log application errors
            }

        }

        /// <summary>
        /// Add Jack Queen and King Card Type
        /// </summary>
        /// <param name="suitType">Suite Type(Hearts, Spades, Clubs,Diamonds)</param>
        protected void addJackQueenAndKing(SuitType suitType)
        {
            try
            {
                for (int i = 11; i <= 13; i++)
                {
                    FaceType faceType;
                    switch (i)
                    {
                        case 11:
                            faceType = FaceType.Jack;
                            break;
                        case 12:
                            faceType = FaceType.King;
                            break;
                        default:
                            faceType = FaceType.Queen;
                            break;
                    }

                    addNewCardToList(i, faceType, suitType);
                }
            }catch(Exception ex)
            {
                //TODO: Implement Log4Net Here to log application errors
            }
        }

        #endregion
    }
}
