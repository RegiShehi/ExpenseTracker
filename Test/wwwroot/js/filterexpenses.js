﻿var dateFormat = "dd/mm/yy";

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

    $('form[id="filterForm"]').validate({
        rules: {
            from: 'required',
            to: 'required'
        },
        messages: {
            from: 'This field is required',
            to: 'This field is required'
        },
        submitHandler: function (form) {
            $(form).ajaxSubmit({
                success: function (data) {
                    var value = data.toLocaleString(undefined, {
                        minimumFractionDigits: 2,
                        maximumFractionDigits: 2
                    });

                    $("#filteredAmount").text(value);
                }
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