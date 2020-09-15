using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace CheckOutProcess.Core
{
    public static class ApplyPromotions
    {
        public static int ApplyPromotion(Cart cart, IEnumerable<Promotions> promotions)
        {
            int finalPrice = 0;
            var items = cart.GetAllItems();
            List<char> itemList = new List<char>();
            List<char> promoApplied = new List<char>();
            
            foreach(var item in items)
            {
                itemList.Add(item.SKU);
            }

            //Apply Single Promotions
            int priceAfterSinglePromotion = 0;
            foreach (var item in items)
            {
                var itemPromotion = promotions.FirstOrDefault(p => p.PrimarySKUId == item.SKU && p.PromotionType=="Single");
                if (itemPromotion != null && item.Quantity>=itemPromotion.PromoQty)
                {
                    int promotionQty = itemPromotion.PromoQty;
                    int promotionPrice = itemPromotion.PromoPrice;
                    int itemInPromotion = item.Quantity/promotionQty;
                    int itemsRemaing = item.Quantity % promotionQty;
                    priceAfterSinglePromotion += (itemInPromotion * promotionPrice + itemsRemaing * item.Price);
                    promoApplied.Add(item.SKU);
                }
            }

            //Apply paired promotions
            int priceAfterPairedPromotion = 0;
            foreach(var item in items)
            {
                var itemPromotion = promotions.FirstOrDefault(p => p.PrimarySKUId == item.SKU && p.PromoType == "Pair" && p.SecondarySKUId != '\0');
                if (itemPromotion != null)
                {
                    var secondarySKUID = promotions.FirstOrDefault(p => p.PrimarySKUId == item.SKU && p.Promoype == "Pair").SecondarySKUId;
                    int primaryQty = item.Quantity;
                    int primaryPrice = item.Price;
                    int secondaryQty = items.FirstOrDefault(i => i.SKU == secondarySKUID).Quantity;
                    int secondaryPrice = items.FirstOrDefault(i => i.SKU == secondarySKUID).Price;
                    if (secondaryQty != 0)
                    {
                        if (primaryQty == secondaryQty)
                        {
                            int totalQty = (primaryQty + secondaryQty) / 2;
                            priceAfterPairedPromotion += totalQty * itemPromotion.PromoPrice;
                        }
                        else if (primaryQty > secondaryQty)
                        {
                            int normalQty = primaryQty - secondaryQty;
                            int promoQty = (primaryQty + secondaryQty + -normalQty) / 2;
                            priceAfterPairedPromotion += (promoQty * itemPromotion.PromoPrice) + (normalQty * primaryPrice);
                        }
                        else if (primaryQty < secondaryQty)
                        {
                            int normalQty = secondaryQty - primaryQty;
                            int promoQty = (primaryQty + secondaryQty + -normalQty) / 2;
                            priceAfterPairedPromotion += (promoQty * itemPromotion.PromoPrice) + (normalQty * secondaryPrice);
                        }
                        promoApplied.Add(item.SKU);
                        promoApplied.Add(secondarySKUID);
                    }
                }
            }

            //Price Without Promotion
            int priceWithoutPromotion = 0;
            
            var remainingItems= (from item in itemList
                                 where !promoApplied.Contains(item)
                                 select item).ToList();
            foreach (var remain in remainingItems)
            {
                var item = items.FirstOrDefault(i => i.SKU == remain);
                priceWithoutPromotion += item.Quantity * item.Price;
            }

            finalPrice = priceAfterSinglePromotion + priceAfterPairedPromotion + priceWithoutPromotion;
            cart.IsPromotionApplied = true;
            return finalPrice;
        }

        public static bool CheckPromotionStatus(Cart cart)
        {
            return cart.IsPromotionApplied;
        }
    }
}