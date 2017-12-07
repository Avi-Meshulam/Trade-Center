using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TradeCenter
{
    public static class Messages
    {
        private static string _itemSuccess = "Item has been {0} successfully";
        private static string _pictureSuccess = "Picture has been {0} successfully";
        private static string _transactionSuccess = "Transaction has been {0} successfully";

        public static string Message { get; private set; }

        public static void Set(AppMessages message)
        {
            switch (message)
            {
                case AppMessages.None:
                    Message = string.Empty;
                    break;
                case AppMessages.ItemAdded:
                    Message = string.Format(_itemSuccess, "added");
                    break;
                case AppMessages.ItemDeleted:
                    Message = string.Format(_itemSuccess, "deleted");
                    break;
                case AppMessages.ItemSaved:
                    Message = string.Format(_itemSuccess, "saved");
                    break;
                case AppMessages.ItemAddedToCart:
                    Message = string.Format(_itemSuccess, "added to cart");
                    break;
                case AppMessages.ItemRemovedFromCart:
                    Message = string.Format(_itemSuccess, "removed from cart");
                    break;
                case AppMessages.PictureDeleted:
                    Message = string.Format(_pictureSuccess, "deleted");
                    break;
                case AppMessages.TransactionCompleted:
                    Message = string.Format(_transactionSuccess, "completed");
                    break;
                default:
                    Message = string.Empty;
                    break;
            }
        }
    }
}