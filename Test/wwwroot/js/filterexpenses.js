$(function () {
    var fromDate = $("#fromDatepicker").datepicker({
        dateFormat: "dd/mm/yy"
    });

    fromDate.datepicker("setDate", new Date());

    var toDate = $("#toDatepicker").datepicker({
        dateFormat: "dd/mm/yy"
    });

    toDate.datepicker("setDate", new Date());

    $(function () {
        $('#submit').on('click', function (e) {
            e.preventDefault();

            var form = $("#filterForm");
            var isValid = form.valid();

            if (isValid) {
                $.post('', form.serialize(), function (data) {
                    if (data.ok)
                        alert('success');
                    else
                        alert('problem');
                });
            }
        });
    });
});