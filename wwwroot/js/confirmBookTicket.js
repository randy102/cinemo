function confirmBookTicket({ form, title, message }) {
    bootbox.confirm({
        title: title,
        centerVertical: true,
        message: message,
        buttons: {
            confirm: {
                label: 'Yes'
            },
            cancel: {
                label: 'No',
            },
        },
        callback: function (result) {
            result && form.submit();
        },
    });
}
