$(function() {
    $("[name='chkProduct']").change(function () {
        $.ajax({
            url: $(this).data('url'),
            type: 'post',
            success: function (totalPrice) {
                $("#totalPrice").text(totalPrice);
            }
        });
    });

    $('#btnBuy').click(function () {
        $.ajax({
            url: $(this).data('url'),
            type: 'post',
            success: function (response) {
                if (!response.isCartFull) {
                    event.preventDefault();
                    return alert('Please select one or more items');
                }
            }
        });
    });
});