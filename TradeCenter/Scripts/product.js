$(function () {
    $('.delete-picture').click(function () {
        var result = confirm("Are you sure you want to delete this picture?");
        if (result == false)
            return;

        var pictureId = $(this).data('picture-id');
        $.ajax({
            url: $(this).data('url'),
            type: 'post',
            success: function () {
                $("#picture" + pictureId)[0].src = "/Content/Images/missingImage.png";
                // Remove delete picture link/icon
                $("#divPicture" + pictureId).find("#bin").remove();
            }
        });
    });
});