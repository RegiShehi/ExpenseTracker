$(function () {
    $("#fromDatepicker").datepicker({
        dateFormat: "dd/mm/yy"
    });

    $("#toDatepicker").datepicker({
        dateFormat: "dd/mm/yy"
    });

    $(function () {
        $('#submit').on('click', function (e) {
            e.preventDefault();

            var form = $("#filterForm");
            var isValid = form.valid();

            if (isValid) {
                $.post('', $("#filterForm").serialize(), function (data) {
                    if (data.ok)
                        alert('success');
                    else
                        alert('problem');
                });
            }
        });
    });
});