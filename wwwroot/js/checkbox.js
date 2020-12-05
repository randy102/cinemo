$(document).ready(function() {
    $("form").submit(function(){
        var formats = [];
        $.each($("input[type=checkbox]:checked"), function(){            
            formats.push($(this).val());
            formats.join(",");
        });
        $("#formats").val(formats);
    });

    $('form').on('click', '.required_group', function() {
      $('input.required_group').prop('required', $('input.required_group:checked').length === 0);
    });
});