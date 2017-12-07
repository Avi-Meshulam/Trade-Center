function custom_alert(output_msg, title_msg) {
    if (!title_msg)
        title_msg = 'Trade Center';

    if (!output_msg)
        output_msg = 'No Message to Display.';

    $("<div></div>").html(output_msg).dialog({
        title: title_msg,
        resizable: false,
        modal: true,
        buttons: {
            "Ok": function () {
                $(this).dialog("close");
            }
        }
    });
}

$(function () {
    // Browser suuports HTML5 date input? Use native : Use jQuery UI
    if (!Modernizr.inputtypes.date) {
        $(function () {
            var $dates = $("input[type='date']")
                .datepicker({
                    changeMonth: true,
                    changeYear: true,
                    yearRange: "-120:+0",
                    dateFormat: "dd/mm/yy"
                });

            if ($dates.length) {
                for (var index = 0; index < $dates.length; index++) {
                    $dates.get(index).setAttribute("type", "text");

                    if ($dates.get(index).value) {
                        $dates.datepicker("setDate", Date.parse($dates.get(0).value).toString("dd/MM/yyyy"));
                    }
                }
            }
        });
    }
});
