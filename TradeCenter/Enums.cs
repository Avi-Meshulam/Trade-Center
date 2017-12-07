using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TradeCenter
{
    public enum ProductState
    {
        Avaialble,
        InCart,
        OnHold,
        Sold
    }

    public enum ProductsSortOrder
    {
        Title,
        PublishDate
    }

    public enum AppMessages
    {
        None,
        ItemAdded,
        ItemDeleted,
        ItemSaved,
        ItemAddedToCart,
        ItemRemovedFromCart,
        PictureDeleted,
        TransactionCompleted
    }
}