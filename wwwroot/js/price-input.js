$(document).ready(function () {
  $('.price-input').on('input', function (event) {
    const string = $(this).val().replace(/\D/g, "");

    const value = parseInt(string, 10) || 0;

    $(this).val(value.toLocaleString("en-US"));
  });
})