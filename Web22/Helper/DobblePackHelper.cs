﻿using System;
using System.Collections.Generic;
using System.Linq;
using Web22.Models;

namespace Web22.Helper
{
    public class DobblePackHelper
    {
        public static List<DobbleCard> GetDobblePack()
        {
            return new List<DobbleCard>()
            {
                new DobbleCard() {Counter = 8, Pictures = new int[] { 1,11,22,24,33,41,42,56} },
                new DobbleCard() {Counter = 8, Pictures = new int[] { 12,16,18,25,30,35,40,41} },
                new DobbleCard() {Counter = 8, Pictures = new int[] { 4,10,16,21,23,26,42,48} },
                new DobbleCard() {Counter = 8, Pictures = new int[] { 13,15,23,32,41,51,53,54} },
                new DobbleCard() {Counter = 8, Pictures = new int[] { 1,16,27,37,44,47,49,51} },
                new DobbleCard() {Counter = 8, Pictures = new int[] { 18,20,21,24,29,34,36,51} },
                new DobbleCard() {Counter = 8, Pictures = new int[] { 3,23,25,27,29,31,55,56} },
                new DobbleCard() {Counter = 8, Pictures = new int[] { 9,18,27,28,42,43,54,57} },
                new DobbleCard() {Counter = 8, Pictures = new int[] { 3,9,11,15,16,19,36,50} },
                new DobbleCard() {Counter = 8, Pictures = new int[] { 2,7,9,10,29,37,41,45} },
                new DobbleCard() {Counter = 8, Pictures = new int[] { 3,7,22,28,39,40,48,51} },
                new DobbleCard() {Counter = 8, Pictures = new int[] { 7,13,18,19,26,38,47,56} },
                new DobbleCard() {Counter = 8, Pictures = new int[] { 15,22,26,29,30,43,44,52} },
                new DobbleCard() {Counter = 8, Pictures = new int[] { 10,12,19,20,22,27,46,53} },
                new DobbleCard() {Counter = 8, Pictures = new int[] { 9,14,24,26,31,40,49,53} },
                new DobbleCard() {Counter = 8, Pictures = new int[] { 3,5,30,34,42,45,47,53} },
                new DobbleCard() {Counter = 8, Pictures = new int[] { 7,8,11,20,23,30,49,57} },
                new DobbleCard() {Counter = 8, Pictures = new int[] { 17,19,34,41,43,48,49,55} },
                new DobbleCard() {Counter = 8, Pictures = new int[] { 12,26,33,45,50,51,55,57} },
                new DobbleCard() {Counter = 8, Pictures = new int[] { 2,6,13,16,22,31,34,57} },
                new DobbleCard() {Counter = 8, Pictures = new int[] { 2,4,11,18,39,44,53,55} },
                new DobbleCard() {Counter = 8, Pictures = new int[] { 2,24,27,30,32,38,48,50} },
                new DobbleCard() {Counter = 8, Pictures = new int[] { 13,29,35,39,42,46,49,50} },
                new DobbleCard() {Counter = 8, Pictures = new int[] { 1,9,12,23,34,38,39,52} },
                new DobbleCard() {Counter = 8, Pictures = new int[] { 3,14,21,38,41,44,46,57} },
                new DobbleCard() {Counter = 8, Pictures = new int[] { 5,7,16,24,46,52,54,55} },
                new DobbleCard() {Counter = 8, Pictures = new int[] { 2,8,14,19,25,42,51,52} },
                new DobbleCard() {Counter = 8, Pictures = new int[] { 5,14,17,18,22,23,37,50} },
                new DobbleCard() {Counter = 8, Pictures = new int[] { 5,6,8,26,27,36,39,41} },
                new DobbleCard() {Counter = 8, Pictures = new int[] { 8,16,17,28,29,33,38,53} },
                new DobbleCard() {Counter = 8, Pictures = new int[] { 4,6,9,17,30,46,51,56} },
                new DobbleCard() {Counter = 8, Pictures = new int[] { 3,6,10,18,32,33,49,52} },
                new DobbleCard() {Counter = 8, Pictures = new int[] { 4,7,14,15,27,33,34,35} },
                new DobbleCard() {Counter = 8, Pictures = new int[] { 1,4,5,19,29,32,40,57} },
                new DobbleCard() {Counter = 8, Pictures = new int[] { 8,9,21,22,32,35,47,55} },
                new DobbleCard() {Counter = 8, Pictures = new int[] { 1,2,3,17,20,26,35,54} },
                new DobbleCard() {Counter = 8, Pictures = new int[] { 1,6,7,21,25,43,50,53} },
                new DobbleCard() {Counter = 8, Pictures = new int[] { 19,21,30,31,33,37,39,54} },
                new DobbleCard() {Counter = 8, Pictures = new int[] { 4,22,25,36,38,45,49,54} },
                new DobbleCard() {Counter = 8, Pictures = new int[] { 11,25,26,28,32,34,37,46} },
                new DobbleCard() {Counter = 8, Pictures = new int[] { 35,36,37,48,52,53,56,57} },
                new DobbleCard() {Counter = 8, Pictures = new int[] { 1,10,13,14,28,30,36,55} },
                new DobbleCard() {Counter = 8, Pictures = new int[] { 2,5,12,15,21,28,49,56} },
                new DobbleCard() {Counter = 8, Pictures = new int[] { 8,10,34,40,44,50,54,56} },
                new DobbleCard() {Counter = 8, Pictures = new int[] { 10,15,17,24,25,39,47,57} },
                new DobbleCard() {Counter = 8, Pictures = new int[] { 6,15,20,37,38,40,42,55} },
                new DobbleCard() {Counter = 8, Pictures = new int[] { 2,23,33,36,40,43,46,47} },
                new DobbleCard() {Counter = 8, Pictures = new int[] { 4,20,28,31,41,47,50,52} },
                new DobbleCard() {Counter = 8, Pictures = new int[] { 14,16,20,32,39,43,45,56} },
                new DobbleCard() {Counter = 8, Pictures = new int[] { 5,10,11,31,35,38,43,51} },
                new DobbleCard() {Counter = 8, Pictures = new int[] { 1,8,15,18,31,45,46,48} },
                new DobbleCard() {Counter = 8, Pictures = new int[] { 11,13,17,21,27,40,45,52} },
                new DobbleCard() {Counter = 8, Pictures = new int[] { 5,9,13,20,25,33,44,48} },
                new DobbleCard() {Counter = 8, Pictures = new int[] { 6,11,12,14,29,47,48,54} },
                new DobbleCard() {Counter = 8, Pictures = new int[] { 3,4,8,12,13,24,37,43} },
                new DobbleCard() {Counter = 8, Pictures = new int[] { 6,19,23,24,28,35,44,45} },
                new DobbleCard() {Counter = 8, Pictures = new int[] { 7,12,17,31,32,36,42,44} }            };
        }

        public static List<DobbleCard> ShuffleDobblePack()
        {
            var pack = GetDobblePack();
            pack.ForEach(delegate (DobbleCard card)
            {
                card.Pictures = ShuffleList(card.Pictures.ToList()).ToArray();
            });
            return ShuffleList(pack);
        }

        public static List<T> ShuffleList<T>(List<T> sourceList)
        {
            var random = new Random();
            var targetList = new List<T>();
            var numberOfElements = sourceList.Count;
            for (int i = 0; i < numberOfElements; i++)
            {
                var index = random.Next(numberOfElements - i);
                var element = sourceList.Skip(index).First();
                sourceList.RemoveAt(index);
                targetList.Add(element);
            }
            return targetList;
        }
    }
}
