$(function () {
    $('#themeSelector').on('change', function () {
        var theme = $(this).val();
        $('#themeLink').attr('href', 'https://cdnjs.cloudflare.com/ajax/libs/bootswatch/4.1.1/' + theme + '/bootstrap.min.css');
        localStorage.setItem('theme', theme);
    });
    if (localStorage.getItem('theme')) {
        $('#themeSelector').val(localStorage.getItem('theme'));
        $('#themeSelector').trigger('change');
    }
});