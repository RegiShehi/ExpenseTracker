var dateFormat = "dd/mm/yy";

$(function () {
    $("#filteredAmount").text("0.00");

    var fromDate = $("#fromDatepicker").datepicker({
        defaultDate: "+1w",
        changeMonth: true,
        numberOfMonths: 1,
        dateFormat: dateFormat
    }).on("change", function () {
        toDate.datepicker("option", "minDate", getDate(this));
    });

    var toDate = $("#toDatepicker").datepicker({
        defaultDate: "+1w",
        changeMonth: true,
        numberOfMonths: 1,
        dateFormat: dateFormat
    }).on("change", function () {
        fromDate.datepicker("option", "maxDate", getDate(this));
    });

    fromDate.datepicker("setDate", new Date());
    toDate.datepicker("setDate", new Date());

    var form = $("#filterForm");
    form.validate();

    $('#submit').on('click', function (e) {
        e.preventDefault();

        var form = $("#filterForm");
        var isValid = form.valid();

        if (isValid) {
            $.post('', form.serialize(), function (data) {
                var value = data.toLocaleString(undefined, {
                    minimumFractionDigits: 2,
                    maximumFractionDigits: 2
                });

                $("#filteredAmount").text(value);
            });
        }
    });
});

function getDate(element) {
    var date;
    try {
        date = $.datepicker.parseDate(dateFormat, element.value);
    } catch (error) {
        date = null;
    }

    return date;
}