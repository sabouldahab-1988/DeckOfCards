using System;
using System.Collections.Generic;
using System.Text;

namespace DeckOfCards.Models
{
    #region enumeration FaceType,SuitType
    public enum FaceType
    {
        Ace,
        Number,
        Jack,
        Queen,
        King
    }

    public enum SuitType
    {
        Hearts,
        Spades,
        Clubs,
        Diamonds
    }
    #endregion

    #region Card Class and Properties
    public class Card
    {
        public int number { get; set; }
        public FaceType faceType { get; set; }
        public SuitType suitsType { get; set; }
    }
    #endregion
}
