// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$('input[type=radio][name=selector1]').on('change', function () {
    switch ($(this).val()) {
        case 'a-option1':
            $('#val1').removeAttr("hidden");
            $('#a-option2').removeAttr('checked');
            $('#val2').attr("hidden", true);
            break;
        case 'a-option2':
            $('#val2').removeAttr("hidden");
            $('#a-option1').removeAttr('checked');
            $('#val1').attr("hidden", true);
            break;
    }
});

$("input[type='radio']").click(function () {
    var previousValue = $(this).attr('previousValue');
    var name = $(this).attr('name');

    if (previousValue == 'checked') {
        $(this).removeAttr('checked');
        $(this).attr('previousValue', false);
        if ($(this).val() == 'a-option1')
            $('#val1').attr("hidden", true);
        if ($(this).val() == 'a-option2')
            $('#val2').attr("hidden", true);
    }
    else {
        $("input[name=" + name + "]:radio").attr('previousValue', false);
        $(this).attr('previousValue', 'checked');
    }
});