$(function () {
    var fromDate = $("#fromDatepicker").datepicker({
        dateFormat: "dd/mm/yy"
    });

    fromDate.datepicker("setDate", new Date());

    var toDate = $("#toDatepicker").datepicker({
        dateFormat: "dd/mm/yy"
    });

    toDate.datepicker("setDate", new Date());

    $("#filteredAmount").text("0.00");

    $(function () {
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
});